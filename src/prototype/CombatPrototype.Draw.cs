using Godot;
using System;

namespace RequiemEcosDoSilencio.Prototype;

public partial class CombatPrototype
{
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
        DrawSessionOverlay();
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
        DrawCircle(new Vector2(_arena.Position.X + 78f, _arena.Position.Y + 72f), 28f, Gold.Darkened(0.60f));
        DrawArc(new Vector2(_arena.Position.X + 78f, _arena.Position.Y + 72f), 34f, 0f, Mathf.Tau * 0.84f, 28, Gold.Darkened(0.28f), 2f);

        Vector2 rightBell = new(_arena.End.X - 82f, _arena.End.Y - 70f);
        DrawCircle(rightBell, 22f, Gold.Darkened(0.68f));
        DrawLine(rightBell + new Vector2(-24f, 20f), rightBell + new Vector2(24f, -18f), Night, 5f);

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
        DrawLine(_playerPosition + new Vector2(-6f, -8f), _playerPosition - _playerFacing * 28f + new Vector2(-4f, 4f), Crimson, 4f);

        float pulse = 3.6f + (MathF.Sin(_elapsed * 6f) + 1f) * 0.8f;
        DrawCircle(_playerPosition + new Vector2(4f, -4f), pulse, requiem ? Ivory : Spectral);

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
        if (_enemy.Health <= 0f)
            return;

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

        DrawString(_font, new Vector2(28f, 30f), "RÉQUIEM // NAVE SILENCIOSA — PROTÓTIPO V2", HorizontalAlignment.Left, -1f, 18, Ivory.Darkened(0.12f));
        DrawString(_font, new Vector2(28f, 54f), "WASD mover  ·  ESPAÇO esquiva  ·  1–4 cartas  ·  clique nas cartas  ·  R reiniciar", HorizontalAlignment.Left, -1f, 14, Ivory.Darkened(0.42f));

        Rect2 healthBack = new(28f, 76f, 210f, 12f);
        DrawRect(healthBack, Night.Lightened(0.11f));
        DrawRect(new Rect2(healthBack.Position, new Vector2(healthBack.Size.X * (_playerHealth / 100f), healthBack.Size.Y)), Crimson);
        DrawString(_font, new Vector2(28f, 110f), $"VIDA {MathF.Round(_playerHealth)}", HorizontalAlignment.Left, 120f, 14, Ivory.Darkened(0.12f));

        Rect2 cadenceBack = new(265f, 76f, 300f, 12f);
        DrawRect(cadenceBack, Night.Lightened(0.11f));
        Color cadenceColor = _cadence >= 90f ? Ivory : Spectral;
        DrawRect(new Rect2(cadenceBack.Position, new Vector2(cadenceBack.Size.X * (_cadence / 100f), cadenceBack.Size.Y)), cadenceColor);
        DrawString(_font, new Vector2(265f, 110f), $"CADÊNCIA  {GetCadenceRank()}  {MathF.Round(_cadence)}", HorizontalAlignment.Left, 300f, 14, cadenceColor);

        string accuracy = _actions == 0 ? "—" : $"{MathF.Round((_goodActions + _perfectActions) * 100f / _actions)}%";
        DrawString(_font, new Vector2(size.X - 325f, 32f), $"PROVA {_kills}/{TargetKills}   //   NO PULSO {accuracy}", HorizontalAlignment.Left, 300f, 14, Ivory.Darkened(0.28f));

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

        for (int i = 0; i < 4; i++)
            DrawCardSlot(i);
    }

    private void DrawSessionOverlay()
    {
        if (IsSessionRunning)
            return;

        Vector2 size = GetViewportRect().Size;
        DrawRect(new Rect2(Vector2.Zero, size), new Color(0.01f, 0.015f, 0.025f, 0.76f));

        Rect2 panel = new(size * 0.5f - new Vector2(275f, 118f), new Vector2(550f, 236f));
        Color accent = _sessionState == SessionState.Victory ? Spectral : Crimson;
        DrawRect(panel, new Color(0.025f, 0.03f, 0.05f, 0.98f));
        DrawRect(panel, accent.Darkened(0.15f), false, 2f);
        DrawRect(new Rect2(panel.Position, new Vector2(6f, panel.Size.Y)), accent);

        string title = _sessionState == SessionState.Victory
            ? "O PRIMEIRO ECO RESPONDEU"
            : "A NAVE VOLTOU AO SILÊNCIO";
        string subtitle = _sessionState == SessionState.Victory
            ? "Três Peregrinos cederam. A porta da Catedral reconhece o fragmento."
            : "O fragmento ainda pulsa. Recomece e leia os sinais do Peregrino.";

        float pulseAccuracy = _actions == 0 ? 0f : (_goodActions + _perfectActions) * 100f / _actions;
        DrawString(_font, panel.Position + new Vector2(30f, 48f), title, HorizontalAlignment.Left, panel.Size.X - 60f, 24, Ivory);
        DrawString(_font, panel.Position + new Vector2(30f, 82f), subtitle, HorizontalAlignment.Left, panel.Size.X - 60f, 14, Ivory.Darkened(0.32f));
        DrawString(_font, panel.Position + new Vector2(30f, 124f), $"TEMPO {FormatSessionTime()}   ·   PULSO {pulseAccuracy:0}%   ·   PERFEITOS {_perfectActions}", HorizontalAlignment.Left, panel.Size.X - 60f, 15, accent);
        DrawString(_font, panel.Position + new Vector2(30f, 164f), $"CADÊNCIA FINAL {GetCadenceRank()}   ·   AÇÕES {_actions}", HorizontalAlignment.Left, panel.Size.X - 60f, 14, Ivory.Darkened(0.18f));
        DrawString(_font, panel.Position + new Vector2(30f, 207f), "R  REINICIAR A PROVA", HorizontalAlignment.Left, panel.Size.X - 60f, 16, Gold);
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
            const float maxDelay = 0.38f;
            float progress = 1f - Mathf.Clamp(_drawTimers[index] / maxDelay, 0f, 1f);
            DrawString(_font, rect.Position + new Vector2(46f, 50f), "COMPRANDO...", HorizontalAlignment.Left, 130f, 14, Ivory.Darkened(0.55f));
            DrawRect(new Rect2(rect.Position + new Vector2(14f, 82f), new Vector2((rect.Size.X - 28f) * progress, 3f)), baseColor.Darkened(0.12f));
            return;
        }

        DrawString(_font, rect.Position + new Vector2(40f, 28f), card.ShortName, HorizontalAlignment.Left, 150f, 18, Ivory);
        DrawString(_font, rect.Position + new Vector2(14f, 57f), card.Name, HorizontalAlignment.Left, rect.Size.X - 28f, 14, Ivory.Darkened(0.14f));
        DrawString(_font, rect.Position + new Vector2(14f, 83f), card.Description, HorizontalAlignment.Left, rect.Size.X - 28f, 12, Ivory.Darkened(0.42f));
    }
}
