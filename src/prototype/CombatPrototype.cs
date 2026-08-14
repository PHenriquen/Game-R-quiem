using Godot;
using System;
using System.Collections.Generic;

namespace RequiemEcosDoSilencio.Prototype;

public partial class CombatPrototype : Node2D
{
    private enum TimingGrade
    {
        Free,
        Good,
        Perfect
    }

    private enum CardKind
    {
        ShortCut,
        Needle,
        PhantomStep,
        BrokenBell
    }

    private sealed class CardDefinition
    {
        public CardKind Kind { get; init; }
        public string Name { get; init; } = string.Empty;
        public string ShortName { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public Color Accent { get; init; }
        public float DrawDelay { get; init; }
    }

    private sealed class EnemyState
    {
        public Vector2 Position;
        public float Health = 70f;
        public const float MaxHealth = 70f;
        public float AttackCooldown = 0.9f;
        public float TelegraphRemaining;
        public float HitFlash;
        public bool Telegraphing;
    }

    private sealed class EffectState
    {
        public Vector2 Start;
        public Vector2 End;
        public float Radius;
        public float Lifetime;
        public float MaxLifetime;
        public Color Color;
        public bool Ring;
        public bool Line;
    }

    private static readonly Color Night = new("090B12");
    private static readonly Color Ivory = new("E9E2D0");
    private static readonly Color Spectral = new("54C7CE");
    private static readonly Color Gold = new("C4A35A");
    private static readonly Color Crimson = new("9E1738");
    private static readonly Color Violet = new("6651A6");
    private static readonly Color Water = new(0.09f, 0.15f, 0.20f, 1f);
    private static readonly Color Stone = new(0.075f, 0.09f, 0.13f, 1f);

    private readonly Font _font = ThemeDB.FallbackFont;
    private readonly Random _random = new();
    private readonly List<CardDefinition> _discard = new();
    private readonly Queue<CardDefinition> _deck = new();
    private readonly CardDefinition?[] _hand = new CardDefinition?[4];
    private readonly float[] _drawTimers = new float[4];
    private readonly List<EffectState> _effects = new();

    private readonly CardDefinition _shortCut = new()
    {
        Kind = CardKind.ShortCut,
        Name = "Corte Breve",
        ShortName = "CORTE",
        Description = "rápido · ligação",
        Accent = Spectral,
        DrawDelay = 0.20f
    };

    private readonly CardDefinition _needle = new()
    {
        Kind = CardKind.Needle,
        Name = "Agulha",
        ShortName = "AGULHA",
        Description = "linha · pressão",
        Accent = Spectral.Lightened(0.18f),
        DrawDelay = 0.25f
    };

    private readonly CardDefinition _phantomStep = new()
    {
        Kind = CardKind.PhantomStep,
        Name = "Passo Fantasma",
        ShortName = "VÉU",
        Description = "avanço · corte",
        Accent = Violet,
        DrawDelay = 0.28f
    };

    private readonly CardDefinition _brokenBell = new()
    {
        Kind = CardKind.BrokenBell,
        Name = "Sino Partido",
        ShortName = "SINO",
        Description = "peso · impacto",
        Accent = Gold,
        DrawDelay = 0.38f
    };

    private Vector2 _playerPosition;
    private Vector2 _playerFacing = Vector2.Right;
    private float _playerHealth = 100f;
    private float _dashCooldown;
    private float _dashRemaining;
    private Vector2 _dashDirection;
    private float _actionLock;
    private float _hitStop;
    private float _elapsed;
    private float _cadence;
    private float _cadenceIdle;
    private float _requiemLock;
    private TimingGrade _lastGrade = TimingGrade.Free;
    private float _gradeDisplay;
    private string _lastAction = "";
    private EnemyState _enemy = new();
    private int _kills;
    private int _actions;
    private int _goodActions;
    private int _perfectActions;
    private Rect2 _arena;

    private const float PlayerSpeed = 245f;
    private const float BeatPeriod = 0.60f; // 100 BPM
    private const float PerfectWindow = 0.065f;
    private const float GoodWindow = 0.140f;

    public override void _Ready()
    {
        ResetArena();
        GD.Print("Réquiem combat prototype ready: WASD, Espaço, 1-4, R.");
    }

    public override void _Process(double delta)
    {
        float dt = (float)delta;
        _elapsed += dt;

        UpdateTransient(dt);
        UpdateCards(dt);

        if (_hitStop > 0f)
        {
            _hitStop = MathF.Max(0f, _hitStop - dt);
            QueueRedraw();
            return;
        }

        UpdatePlayer(dt);
        UpdateEnemy(dt);
        UpdateCadence(dt);
        QueueRedraw();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey key && key.Pressed && !key.Echo)
        {
            Key physical = key.PhysicalKeycode;

            if (physical == (Key)49) TryPlayCard(0); // 1
            if (physical == (Key)50) TryPlayCard(1); // 2
            if (physical == (Key)51) TryPlayCard(2); // 3
            if (physical == (Key)52) TryPlayCard(3); // 4
            if (physical == (Key)32) TryDash();      // Space
            if (physical == (Key)82) ResetArena();   // R
        }

        if (@event is InputEventMouseButton mouse && mouse.Pressed && mouse.ButtonIndex == MouseButton.Left)
        {
            Vector2 position = GetLocalMousePosition();
            for (int i = 0; i < 4; i++)
            {
                if (GetCardRect(i).HasPoint(position))
                {
                    TryPlayCard(i);
                    break;
                }
            }
        }
    }

    private void ResetArena()
    {
        Vector2 size = GetViewportRect().Size;
        _arena = new Rect2(72f, 70f, MathF.Max(900f, size.X - 144f), MathF.Max(430f, size.Y - 255f));
        _playerPosition = _arena.GetCenter() + new Vector2(-210f, 20f);
        _playerFacing = Vector2.Right;
        _playerHealth = 100f;
        _cadence = 0f;
        _cadenceIdle = 0f;
        _requiemLock = 0f;
        _dashCooldown = 0f;
        _actionLock = 0f;
        _hitStop = 0f;
        _kills = 0;
        _actions = 0;
        _goodActions = 0;
        _perfectActions = 0;
        _effects.Clear();
        SpawnEnemy();
        RebuildDeck();
        for (int i = 0; i < 4; i++)
        {
            _hand[i] = DrawCard();
            _drawTimers[i] = 0f;
        }
        QueueRedraw();
    }

    private void SpawnEnemy()
    {
        _enemy = new EnemyState
        {
            Position = _arena.GetCenter() + new Vector2(230f, -10f),
            Health = EnemyState.MaxHealth,
            AttackCooldown = 0.75f,
            TelegraphRemaining = 0f,
            HitFlash = 0f,
            Telegraphing = false
        };
    }

    private void RebuildDeck()
    {
        var cards = new List<CardDefinition>
        {
            _shortCut, _shortCut,
            _needle, _needle,
            _phantomStep, _phantomStep,
            _brokenBell, _brokenBell
        };

        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }

        _deck.Clear();
        foreach (CardDefinition card in cards)
            _deck.Enqueue(card);
    }

    private CardDefinition DrawCard()
    {
        if (_deck.Count == 0)
        {
            if (_discard.Count > 0)
            {
                var refill = new List<CardDefinition>(_discard);
                _discard.Clear();
                for (int i = refill.Count - 1; i > 0; i--)
                {
                    int j = _random.Next(i + 1);
                    (refill[i], refill[j]) = (refill[j], refill[i]);
                }
                foreach (CardDefinition card in refill)
                    _deck.Enqueue(card);
            }
            else
            {
                RebuildDeck();
            }
        }

        return _deck.Dequeue();
    }

    private void UpdateCards(float dt)
    {
        for (int i = 0; i < 4; i++)
        {
            if (_hand[i] != null || _drawTimers[i] <= 0f)
                continue;

            _drawTimers[i] -= dt;
            if (_drawTimers[i] <= 0f)
                _hand[i] = DrawCard();
        }
    }

    private void UpdateTransient(float dt)
    {
        _dashCooldown = MathF.Max(0f, _dashCooldown - dt);
        _actionLock = MathF.Max(0f, _actionLock - dt);
        _gradeDisplay = MathF.Max(0f, _gradeDisplay - dt);
        _enemy.HitFlash = MathF.Max(0f, _enemy.HitFlash - dt);
        _requiemLock = MathF.Max(0f, _requiemLock - dt);

        for (int i = _effects.Count - 1; i >= 0; i--)
        {
            _effects[i].Lifetime -= dt;
            if (_effects[i].Lifetime <= 0f)
                _effects.RemoveAt(i);
        }
    }

    private void UpdatePlayer(float dt)
    {
        Vector2 input = Vector2.Zero;
        if (Input.IsPhysicalKeyPressed((Key)87)) input.Y -= 1f; // W
        if (Input.IsPhysicalKeyPressed((Key)83)) input.Y += 1f; // S
        if (Input.IsPhysicalKeyPressed((Key)65)) input.X -= 1f; // A
        if (Input.IsPhysicalKeyPressed((Key)68)) input.X += 1f; // D

        if (input.LengthSquared() > 0.01f)
        {
            input = input.Normalized();
            _playerFacing = input;
        }

        if (_dashRemaining > 0f)
        {
            _dashRemaining -= dt;
            _playerPosition += _dashDirection * 700f * dt;
        }
        else
        {
            _playerPosition += input * PlayerSpeed * dt;
        }

        _playerPosition.X = Mathf.Clamp(_playerPosition.X, _arena.Position.X + 26f, _arena.End.X - 26f);
        _playerPosition.Y = Mathf.Clamp(_playerPosition.Y, _arena.Position.Y + 26f, _arena.End.Y - 26f);
    }

    private void TryDash()
    {
        if (_dashCooldown > 0f)
            return;

        Vector2 direction = _playerFacing;
        if (direction.LengthSquared() < 0.01f)
            direction = Vector2.Right;

        _dashDirection = direction.Normalized();
        _dashRemaining = 0.15f;
        _dashCooldown = 0.65f;
        TimingGrade grade = EvaluateTiming();
        if (grade == TimingGrade.Good) AddCadence(2f);
        if (grade == TimingGrade.Perfect) AddCadence(4f);

        _effects.Add(new EffectState
        {
            Start = _playerPosition,
            End = _playerPosition - _dashDirection * 55f,
            Lifetime = 0.16f,
            MaxLifetime = 0.16f,
            Color = Spectral.Darkened(0.25f),
            Line = true
        });
    }

    private void TryPlayCard(int slot)
    {
        if (slot < 0 || slot >= _hand.Length || _hand[slot] == null || _actionLock > 0f || _playerHealth <= 0f)
            return;

        CardDefinition card = _hand[slot]!;
        TimingGrade grade = EvaluateTiming();
        _lastGrade = grade;
        _gradeDisplay = 0.50f;
        _lastAction = card.Name;
        _actions++;

        if (grade == TimingGrade.Good)
        {
            _goodActions++;
            AddCadence(4f);
        }
        else if (grade == TimingGrade.Perfect)
        {
            _perfectActions++;
            AddCadence(8f);
        }

        _cadenceIdle = 0f;

        switch (card.Kind)
        {
            case CardKind.ShortCut:
                ExecuteShortCut(grade);
                break;
            case CardKind.Needle:
                ExecuteNeedle(grade);
                break;
            case CardKind.PhantomStep:
                ExecutePhantomStep(grade);
                break;
            case CardKind.BrokenBell:
                ExecuteBrokenBell(grade);
                break;
        }

        _discard.Add(card);
        _hand[slot] = null;
        float drawMultiplier = GetCadenceRank() == "RÉQUIEM" ? 0.80f : 1f;
        _drawTimers[slot] = card.DrawDelay * drawMultiplier;
    }

    private void ExecuteShortCut(TimingGrade grade)
    {
        _actionLock = 0.22f;
        float damage = grade == TimingGrade.Perfect ? 16.8f : 14f;
        float range = grade == TimingGrade.Good ? 90f : 82f;

        Vector2 toEnemy = _enemy.Position - _playerPosition;
        if (toEnemy.Length() <= range && toEnemy.LengthSquared() > 0.01f && _playerFacing.Dot(toEnemy.Normalized()) > 0.05f)
            DamageEnemy(damage, grade == TimingGrade.Perfect ? 0.045f : 0.035f);

        _effects.Add(new EffectState
        {
            Start = _playerPosition + _playerFacing * 18f,
            End = _playerPosition + _playerFacing * range,
            Lifetime = 0.13f,
            MaxLifetime = 0.13f,
            Color = grade == TimingGrade.Perfect ? Ivory : Spectral,
            Line = true
        });
    }

    private void ExecuteNeedle(TimingGrade grade)
    {
        _actionLock = 0.28f;
        Vector2 end = _playerPosition + _playerFacing * 230f;
        float distance = DistancePointToSegment(_enemy.Position, _playerPosition, end);
        if (distance <= 22f && (_enemy.Position - _playerPosition).Dot(_playerFacing) > 0f)
        {
            DamageEnemy(11f, 0.025f);
            if (grade == TimingGrade.Perfect && _enemy.Health > 0f)
                DamageEnemy(4.95f, 0.018f);
        }

        _effects.Add(new EffectState
        {
            Start = _playerPosition + _playerFacing * 14f,
            End = end,
            Lifetime = 0.16f,
            MaxLifetime = 0.16f,
            Color = grade == TimingGrade.Perfect ? Ivory : Spectral,
            Line = true
        });
    }

    private void ExecutePhantomStep(TimingGrade grade)
    {
        _actionLock = 0.32f;
        Vector2 start = _playerPosition;
        float distance = grade == TimingGrade.Good ? 160f : 145f;
        Vector2 end = start + _playerFacing * distance;
        end.X = Mathf.Clamp(end.X, _arena.Position.X + 26f, _arena.End.X - 26f);
        end.Y = Mathf.Clamp(end.Y, _arena.Position.Y + 26f, _arena.End.Y - 26f);

        if (DistancePointToSegment(_enemy.Position, start, end) <= 30f)
            DamageEnemy(9f, 0.025f);

        _playerPosition = end;

        _effects.Add(new EffectState
        {
            Start = start,
            End = end,
            Lifetime = 0.20f,
            MaxLifetime = 0.20f,
            Color = Violet,
            Line = true
        });

        if (grade == TimingGrade.Perfect && _enemy.Health > 0f && _enemy.Position.DistanceTo(start) <= 78f)
            DamageEnemy(5f, 0.02f);
    }

    private void ExecuteBrokenBell(TimingGrade grade)
    {
        _actionLock = 0.48f;
        if (_enemy.Position.DistanceTo(_playerPosition) <= 105f)
        {
            DamageEnemy(24f, grade == TimingGrade.Perfect ? 0.075f : 0.060f);
            if (grade == TimingGrade.Perfect && _enemy.Health > 0f)
                DamageEnemy(9.6f, 0.025f);
        }

        _effects.Add(new EffectState
        {
            Start = _playerPosition,
            Radius = 105f,
            Lifetime = 0.28f,
            MaxLifetime = 0.28f,
            Color = grade == TimingGrade.Perfect ? Ivory : Gold,
            Ring = true
        });
    }

    private void DamageEnemy(float amount, float hitStop)
    {
        if (_enemy.Health <= 0f)
            return;

        _enemy.Health = MathF.Max(0f, _enemy.Health - amount);
        _enemy.HitFlash = 0.08f;
        _hitStop = MathF.Max(_hitStop, hitStop);

        if (_enemy.Health <= 0f)
        {
            _kills++;
            AddCadence(5f);
            SpawnEnemyAtRandomEdge();
        }
    }

    private void SpawnEnemyAtRandomEdge()
    {
        float x = _random.Next(0, 2) == 0 ? _arena.Position.X + 110f : _arena.End.X - 110f;
        float y = (float)(_arena.Position.Y + 90f + _random.NextDouble() * Math.Max(40f, _arena.Size.Y - 180f));
        _enemy = new EnemyState
        {
            Position = new Vector2(x, y),
            Health = EnemyState.MaxHealth,
            AttackCooldown = 0.9f,
            TelegraphRemaining = 0f,
            HitFlash = 0f,
            Telegraphing = false
        };
    }

    private void UpdateEnemy(float dt)
    {
        if (_enemy.Health <= 0f)
            return;

        Vector2 toPlayer = _playerPosition - _enemy.Position;
        float distance = toPlayer.Length();
        Vector2 direction = distance > 0.01f ? toPlayer / distance : Vector2.Zero;

        if (_enemy.Telegraphing)
        {
            _enemy.TelegraphRemaining -= dt;
            if (_enemy.TelegraphRemaining <= 0f)
            {
                _enemy.Telegraphing = false;
                _enemy.AttackCooldown = 0.82f;

                if (_enemy.Position.DistanceTo(_playerPosition) <= 125f && _dashRemaining <= 0f)
                {
                    _playerHealth = MathF.Max(0f, _playerHealth - 18f);
                    _cadence = MathF.Max(0f, _cadence - 20f);
                    _hitStop = MathF.Max(_hitStop, 0.045f);
                    _effects.Add(new EffectState
                    {
                        Start = _enemy.Position,
                        Radius = 92f,
                        Lifetime = 0.18f,
                        MaxLifetime = 0.18f,
                        Color = Crimson,
                        Ring = true
                    });
                }
            }
            return;
        }

        _enemy.AttackCooldown -= dt;

        if (distance > 108f)
            _enemy.Position += direction * 105f * dt;

        if (distance <= 122f && _enemy.AttackCooldown <= 0f)
        {
            _enemy.Telegraphing = true;
            _enemy.TelegraphRemaining = 0.48f;
        }

        if (_playerHealth <= 0f)
        {
            _playerHealth = 100f;
            _cadence = 0f;
            _playerPosition = _arena.GetCenter() + new Vector2(-210f, 20f);
        }
    }

    private void UpdateCadence(float dt)
    {
        _cadenceIdle += dt;
        if (_requiemLock > 0f)
            return;

        if (_cadenceIdle > 2.5f)
            _cadence = MathF.Max(0f, _cadence - 5f * dt);
    }

    private void AddCadence(float amount)
    {
        float before = _cadence;
        _cadence = Mathf.Clamp(_cadence + amount, 0f, 100f);
        if (before < 90f && _cadence >= 90f)
        {
            _requiemLock = 6f;
            _effects.Add(new EffectState
            {
                Start = _playerPosition,
                Radius = 145f,
                Lifetime = 0.65f,
                MaxLifetime = 0.65f,
                Color = Ivory,
                Ring = true
            });
        }
    }

    private TimingGrade EvaluateTiming()
    {
        float phase = _elapsed % BeatPeriod;
        float distance = MathF.Min(phase, BeatPeriod - phase);
        if (distance <= PerfectWindow)
            return TimingGrade.Perfect;
        if (distance <= GoodWindow)
            return TimingGrade.Good;
        return TimingGrade.Free;
    }

    private string GetCadenceRank()
    {
        if (_cadence >= 90f) return "RÉQUIEM";
        if (_cadence >= 70f) return "S";
        if (_cadence >= 50f) return "A";
        if (_cadence >= 30f) return "B";
        if (_cadence >= 15f) return "C";
        return "D";
    }

    private static float DistancePointToSegment(Vector2 point, Vector2 start, Vector2 end)
    {
        Vector2 segment = end - start;
        float lengthSquared = segment.LengthSquared();
        if (lengthSquared <= 0.0001f)
            return point.DistanceTo(start);

        float t = Mathf.Clamp((point - start).Dot(segment) / lengthSquared, 0f, 1f);
        Vector2 projection = start + segment * t;
        return point.DistanceTo(projection);
    }

    private Rect2 GetCardRect(int index)
    {
        Vector2 size = GetViewportRect().Size;
        const float width = 205f;
        const float height = 108f;
        const float gap = 18f;
        float total = width * 4f + gap * 3f;
        float startX = (size.X - total) * 0.5f;
        return new Rect2(startX + index * (width + gap), size.Y - height - 24f, width, height);
    }

    public override void _Draw()
    {
        DrawBackground();
        DrawArenaDecoration();
        DrawEnemy();
        DrawPlayer();
        DrawEffects();
        DrawHud();
    }

    private void DrawBackground()
    {
        Vector2 size = GetViewportRect().Size;
        DrawRect(new Rect2(Vector2.Zero, size), Night);
        DrawRect(_arena, Stone);
        DrawRect(_arena.Grow(-10f), Water.Darkened(0.28f), false, 2f);

        for (float y = _arena.Position.Y + 34f; y < _arena.End.Y; y += 46f)
            DrawLine(new Vector2(_arena.Position.X + 18f, y), new Vector2(_arena.End.X - 18f, y), Water.Lightened(0.06f), 1f);
    }

    private void DrawArenaDecoration()
    {
        // Broken bells / cathedral markers. Prototype shapes only.
        DrawCircle(new Vector2(_arena.Position.X + 78f, _arena.Position.Y + 72f), 28f, Gold.Darkened(0.60f));
        DrawArc(new Vector2(_arena.Position.X + 78f, _arena.Position.Y + 72f), 34f, 0f, Mathf.Tau * 0.84f, 28, Gold.Darkened(0.28f), 2f);

        Vector2 rightBell = new(_arena.End.X - 82f, _arena.End.Y - 70f);
        DrawCircle(rightBell, 22f, Gold.Darkened(0.68f));
        DrawLine(rightBell + new Vector2(-24f, 20f), rightBell + new Vector2(24f, -18f), Night, 5f);

        // A distant sealed door as a narrative focal point.
        Rect2 door = new(_arena.GetCenter().X - 44f, _arena.Position.Y + 8f, 88f, 58f);
        DrawRect(door, Night.Lightened(0.02f));
        DrawArc(new Vector2(door.GetCenter().X, door.Position.Y + 26f), 20f, Mathf.Pi, Mathf.Tau, 20, Ivory.Darkened(0.58f), 2f);
    }

    private void DrawPlayer()
    {
        bool requiem = _cadence >= 90f;
        Color body = requiem ? Ivory.Darkened(0.70f) : new Color(0.07f, 0.12f, 0.18f, 1f);

        DrawCircle(_playerPosition + new Vector2(2f, 9f), 19f, new Color(0f, 0f, 0f, 0.28f));
        DrawRect(new Rect2(_playerPosition + new Vector2(-12f, -13f), new Vector2(24f, 33f)), body);
        DrawCircle(_playerPosition + new Vector2(0f, -19f), 11f, new Color(0.035f, 0.035f, 0.05f, 1f));

        // Crimson signature cloth, kept small on purpose.
        DrawLine(_playerPosition + new Vector2(-6f, -8f), _playerPosition - _playerFacing * 28f + new Vector2(-4f, 4f), Crimson, 4f);

        // Heart fragment.
        float pulse = 3.6f + (MathF.Sin(_elapsed * 6f) + 1f) * 0.8f;
        DrawCircle(_playerPosition + new Vector2(4f, -4f), pulse, requiem ? Ivory : Spectral);

        // Vesper Needle.
        Vector2 bladeStart = _playerPosition + _playerFacing * 13f;
        Vector2 bladeEnd = _playerPosition + _playerFacing * 42f;
        DrawLine(bladeStart, bladeEnd, requiem ? Ivory : Spectral, 3f);

        if (requiem)
        {
            DrawArc(_playerPosition, 28f, 0f, Mathf.Tau, 40, Spectral, 2f);
            DrawLine(_playerPosition + new Vector2(-8f, -4f), _playerPosition + new Vector2(-20f, 12f), Gold, 1.5f);
        }

        DrawPulseIndicator();
    }

    private void DrawPulseIndicator()
    {
        float phase = _elapsed % BeatPeriod;
        float normalized = phase / BeatPeriod;
        float radius = 31f + normalized * 16f;
        float alpha = 0.48f * (1f - normalized);
        Color color = new(Spectral.R, Spectral.G, Spectral.B, alpha);
        DrawArc(_playerPosition, radius, 0f, Mathf.Tau, 40, color, 1.6f);
    }

    private void DrawEnemy()
    {
        Color mask = _enemy.HitFlash > 0f ? Ivory : new Color(0.58f, 0.60f, 0.59f, 1f);
        Color cloth = new Color(0.16f, 0.17f, 0.19f, 1f);

        DrawCircle(_enemy.Position + new Vector2(0f, 8f), 20f, new Color(0f, 0f, 0f, 0.25f));
        DrawRect(new Rect2(_enemy.Position + new Vector2(-14f, -10f), new Vector2(28f, 35f)), cloth);
        DrawCircle(_enemy.Position + new Vector2(0f, -18f), 12f, mask);
        DrawLine(_enemy.Position + new Vector2(-6f, -20f), _enemy.Position + new Vector2(7f, -17f), Night, 2f);

        if (_enemy.Telegraphing)
        {
            float t = 1f - Mathf.Clamp(_enemy.TelegraphRemaining / 0.48f, 0f, 1f);
            DrawArc(_enemy.Position, 42f + t * 40f, -0.25f, Mathf.Pi + 0.25f, 30, Crimson, 3f);
        }

        float hpRatio = _enemy.Health / EnemyState.MaxHealth;
        Rect2 bar = new(_enemy.Position + new Vector2(-30f, -43f), new Vector2(60f, 5f));
        DrawRect(bar, Night.Lightened(0.10f));
        DrawRect(new Rect2(bar.Position, new Vector2(bar.Size.X * hpRatio, bar.Size.Y)), Ivory.Darkened(0.25f));
    }

    private void DrawEffects()
    {
        foreach (EffectState effect in _effects)
        {
            float alpha = Mathf.Clamp(effect.Lifetime / effect.MaxLifetime, 0f, 1f);
            Color color = new(effect.Color.R, effect.Color.G, effect.Color.B, alpha * 0.85f);

            if (effect.Line)
                DrawLine(effect.Start, effect.End, color, 3f + alpha * 3f);

            if (effect.Ring)
            {
                float radius = effect.Radius * (1f + (1f - alpha) * 0.15f);
                DrawArc(effect.Start, radius, 0f, Mathf.Tau, 48, color, 2f + alpha * 2f);
            }
        }
    }

    private void DrawHud()
    {
        Vector2 size = GetViewportRect().Size;

        // Header.
        DrawString(_font, new Vector2(28f, 30f), "RÉQUIEM // NAVE SILENCIOSA — PROTÓTIPO V2", HorizontalAlignment.Left, -1f, 18, Ivory.Darkened(0.12f));
        DrawString(_font, new Vector2(28f, 54f), "WASD mover  ·  ESPAÇO esquiva  ·  1–4 cartas  ·  clique nas cartas  ·  R reiniciar", HorizontalAlignment.Left, -1f, 14, Ivory.Darkened(0.42f));

        // Health.
        Rect2 healthBack = new(28f, 76f, 210f, 12f);
        DrawRect(healthBack, Night.Lightened(0.11f));
        DrawRect(new Rect2(healthBack.Position, new Vector2(healthBack.Size.X * (_playerHealth / 100f), healthBack.Size.Y)), Crimson);
        DrawString(_font, new Vector2(28f, 110f), $"VIDA {MathF.Round(_playerHealth)}", HorizontalAlignment.Left, 120f, 14, Ivory.Darkened(0.12f));

        // Cadence.
        Rect2 cadenceBack = new(265f, 76f, 300f, 12f);
        DrawRect(cadenceBack, Night.Lightened(0.11f));
        Color cadenceColor = _cadence >= 90f ? Ivory : Spectral;
        DrawRect(new Rect2(cadenceBack.Position, new Vector2(cadenceBack.Size.X * (_cadence / 100f), cadenceBack.Size.Y)), cadenceColor);
        DrawString(_font, new Vector2(265f, 110f), $"CADÊNCIA  {GetCadenceRank()}  {MathF.Round(_cadence)}", HorizontalAlignment.Left, 300f, 14, cadenceColor);

        string accuracy = _actions == 0 ? "—" : $"{MathF.Round((_goodActions + _perfectActions) * 100f / _actions)}%";
        DrawString(_font, new Vector2(size.X - 265f, 32f), $"ABATES {_kills}   //   NO PULSO {accuracy}", HorizontalAlignment.Left, 240f, 14, Ivory.Darkened(0.28f));

        if (_gradeDisplay > 0f)
        {
            string grade = _lastGrade switch
            {
                TimingGrade.Perfect => "PERFEITO",
                TimingGrade.Good => "BOM",
                _ => "LIVRE"
            };
            Color gradeColor = _lastGrade == TimingGrade.Perfect ? Ivory : (_lastGrade == TimingGrade.Good ? Spectral : Ivory.Darkened(0.55f));
            DrawString(_font, _playerPosition + new Vector2(-70f, -58f), $"{grade} · {_lastAction}", HorizontalAlignment.Center, 140f, 13, gradeColor);
        }

        // Card hand.
        for (int i = 0; i < 4; i++)
            DrawCardSlot(i);
    }

    private void DrawCardSlot(int index)
    {
        Rect2 rect = GetCardRect(index);
        CardDefinition? card = _hand[index];

        Color baseColor = card?.Accent ?? Ivory.Darkened(0.72f);
        DrawRect(rect, new Color(0.025f, 0.03f, 0.05f, 0.96f));
        DrawRect(rect, baseColor.Darkened(0.15f), false, 2f);
        DrawRect(new Rect2(rect.Position, new Vector2(5f, rect.Size.Y)), baseColor);

        DrawString(_font, rect.Position + new Vector2(14f, 24f), (index + 1).ToString(), HorizontalAlignment.Left, 24f, 14, baseColor);

        if (card == null)
        {
            float maxDelay = 0.38f;
            float p = 1f - Mathf.Clamp(_drawTimers[index] / maxDelay, 0f, 1f);
            DrawString(_font, rect.Position + new Vector2(46f, 50f), "COMPRANDO...", HorizontalAlignment.Left, 130f, 14, Ivory.Darkened(0.55f));
            DrawRect(new Rect2(rect.Position + new Vector2(14f, 82f), new Vector2((rect.Size.X - 28f) * p, 3f)), baseColor.Darkened(0.12f));
            return;
        }

        DrawString(_font, rect.Position + new Vector2(40f, 28f), card.ShortName, HorizontalAlignment.Left, 150f, 18, Ivory);
        DrawString(_font, rect.Position + new Vector2(14f, 57f), card.Name, HorizontalAlignment.Left, rect.Size.X - 28f, 14, Ivory.Darkened(0.14f));
        DrawString(_font, rect.Position + new Vector2(14f, 83f), card.Description, HorizontalAlignment.Left, rect.Size.X - 28f, 12, Ivory.Darkened(0.42f));
    }
}
