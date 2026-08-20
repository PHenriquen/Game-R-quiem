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
    private string _lastAction = string.Empty;
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

        if (@event is not InputEventMouseButton mouse || !mouse.Pressed || mouse.ButtonIndex != MouseButton.Left)
            return;

        Vector2 position = GetLocalMousePosition();
        for (int i = 0; i < _hand.Length; i++)
        {
            if (!GetCardRect(i).HasPoint(position))
                continue;

            TryPlayCard(i);
            break;
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
        _dashRemaining = 0f;
        _actionLock = 0f;
        _hitStop = 0f;
        _kills = 0;
        _actions = 0;
        _goodActions = 0;
        _perfectActions = 0;
        _effects.Clear();

        SpawnEnemy();
        RebuildDeck();

        for (int i = 0; i < _hand.Length; i++)
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
            AttackCooldown = 0.75f
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

        Shuffle(cards);
        _deck.Clear();

        foreach (CardDefinition card in cards)
            _deck.Enqueue(card);
    }

    private CardDefinition DrawCard()
    {
        if (_deck.Count == 0)
        {
            if (_discard.Count == 0)
            {
                RebuildDeck();
            }
            else
            {
                var refill = new List<CardDefinition>(_discard);
                _discard.Clear();
                Shuffle(refill);
                foreach (CardDefinition card in refill)
                    _deck.Enqueue(card);
            }
        }

        return _deck.Dequeue();
    }

    private void Shuffle(List<CardDefinition> cards)
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    private void UpdateCards(float dt)
    {
        for (int i = 0; i < _hand.Length; i++)
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

        Vector2 direction = _playerFacing.LengthSquared() < 0.01f ? Vector2.Right : _playerFacing;
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

        AddLineEffect(_playerPosition + _playerFacing * 18f, _playerPosition + _playerFacing * range, grade == TimingGrade.Perfect ? Ivory : Spectral, 0.13f);
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

        AddLineEffect(_playerPosition + _playerFacing * 14f, end, grade == TimingGrade.Perfect ? Ivory : Spectral, 0.16f);
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
        AddLineEffect(start, end, Violet, 0.20f);

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

        AddRingEffect(_playerPosition, 105f, grade == TimingGrade.Perfect ? Ivory : Gold, 0.28f);
    }

    private void AddLineEffect(Vector2 start, Vector2 end, Color color, float lifetime)
    {
        _effects.Add(new EffectState
        {
            Start = start,
            End = end,
            Lifetime = lifetime,
            MaxLifetime = lifetime,
            Color = color,
            Line = true
        });
    }

    private void AddRingEffect(Vector2 position, float radius, Color color, float lifetime)
    {
        _effects.Add(new EffectState
        {
            Start = position,
            Radius = radius,
            Lifetime = lifetime,
            MaxLifetime = lifetime,
            Color = color,
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

        if (_enemy.Health > 0f)
            return;

        _kills++;
        AddCadence(5f);
        SpawnEnemyAtRandomEdge();
    }

    private void SpawnEnemyAtRandomEdge()
    {
        float x = _random.Next(0, 2) == 0 ? _arena.Position.X + 110f : _arena.End.X - 110f;
        float y = (float)(_arena.Position.Y + 90f + _random.NextDouble() * Math.Max(40f, _arena.Size.Y - 180f));

        _enemy = new EnemyState
        {
            Position = new Vector2(x, y),
            Health = EnemyState.MaxHealth,
            AttackCooldown = 0.9f
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
                FinishEnemyAttack();
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

    private void FinishEnemyAttack()
    {
        _enemy.Telegraphing = false;
        _enemy.AttackCooldown = 0.82f;

        if (_enemy.Position.DistanceTo(_playerPosition) > 125f || _dashRemaining > 0f)
            return;

        _playerHealth = MathF.Max(0f, _playerHealth - 18f);
        _cadence = MathF.Max(0f, _cadence - 20f);
        _hitStop = MathF.Max(_hitStop, 0.045f);
        AddRingEffect(_enemy.Position, 92f, Crimson, 0.18f);
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

        if (before >= 90f || _cadence < 90f)
            return;

        _requiemLock = 6f;
        AddRingEffect(_playerPosition, 145f, Ivory, 0.65f);
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
}
