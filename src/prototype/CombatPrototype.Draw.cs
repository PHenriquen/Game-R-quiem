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
        Color doorAccent = _bellResponseTriggered ? Gold : Ivory.Darkened(0.58f);
        DrawRect(door, doorAccent.Darkened(0.28f), false, _bellResponseTriggered ? 2.4f : 1.4f);
        DrawArc(new Vector2(door.GetCenter().X, door.Position.Y + 26f), 20f, Mathf.Pi, Mathf.Tau, 20, doorAccent, 2f);

        if (_bellResponseTriggered)
        {
            Vector2 memoryMark = new(door.GetCenter().X, door.End.Y - 10f);
            DrawCircle(memoryMark, 3.5f, Gold);
            DrawArc(memoryMark, 8f, 0f, Mathf.Tau, 20, Gold.Darkened(0.24f), 1.2f);
        }

        if (_bellResponseRemaining > 0f)
            DrawBellMicroEcho();
    }

    private void DrawBellMicroEcho()
    {
        float progress = 1f - _bellResponseRemaining / BellEchoDuration;
        Vector2 point = GetBellResponsePoint();

        if (progress < 0.34f)
        {
            float phase = progress / 0.34f;
            float radius = 18f + phase * 42f;
            float alpha = 0.86f * (1f - phase);
            Color response = new(Gold.R, Gold.G, Gold.B, alpha);
            DrawArc(point, radius, -Mathf.Pi * 0.86f, -Mathf.Pi * 0.14f, 32, response, 2.4f);
            DrawArc(point, radius + 11f, -Mathf.Pi * 0.78f, -Mathf.Pi * 0.22f, 28, response.Darkened(0.16f), 1.4f);
            DrawString(_font, point + new Vector2(-74f, 34f), "O SINO RESPONDE", HorizontalAlignment.Center, 148f, 12, Gold);
        }

        if (progress >= 0.20f && progress < 0.78f)
        {
            float phase = (progress - 0.20f) / 0.58f;
            float alpha = MathF.Sin(phase * MathF.PI) * 0.64f;
            Color memory = new(Spectral.R, Spectral.G, Spectral.B, alpha);
            Vector2 waterLine = point + new Vector2(0f, 24f);

            DrawLine(waterLine + new Vector2(-19f, 0f), waterLine + new Vector2(-15f, 34f), memory, 5f);
            DrawCircle(waterLine + new Vector2(-19f, -7f), 6f, memory);
            DrawLine(waterLine + new Vector2(19f, 4f), waterLine + new Vector2(22f, 28f), memory.Darkened(0.26f), 4f);
            DrawArc(waterLine + new Vector2(19f, -3f), 5f, 0.2f, Mathf.Pi * 1.7f, 14, memory.Darkened(0.26f), 2f);
            DrawLine(waterLine + new Vector2(-36f, 42f), waterLine + new Vector2(39f, 42f), memory.Darkened(0.38f), 1.2f);
        }

        if (progress >= 0.62f)
        {
            float phase = (progress - 0.62f) / 0.38f;
            float alpha = MathF.Sin(Mathf.Clamp(phase, 0f, 1f) * MathF.PI) * 0.86f;
            Color memoryGold = new(Gold.R, Gold.G, Gold.B, alpha);
            Vector2 sequence = point + new Vector2(-30f, 58f);
            DrawCircle(sequence, 3f, memoryGold);
            DrawCircle(sequence + new Vector2(20f, 0f), 3f, memoryGold);
            DrawCircle(sequence + new Vector2(40f, 0f), 3f, memoryGold);
            DrawArc(sequence + new Vector2(60f, 0f), 4f, 0f, Mathf.Tau, 16, memoryGold.Darkened(0.62f), 1.2f);
            DrawString(_font, point + new Vector2(-68f, 84f), "UM PULSO FALTA", HorizontalAlignment.Center, 136f, 11, memoryGold);
        }
    }

    private void DrawPlayer()
    {
        bool requiem = _cadence >= 90f;
        Vector2 forward = _playerFacing.LengthSquared() > 0.01f ? _playerFacing.Normalized() : Vector2.Right;
        Vector2 side = new(-forward.Y, forward.X);
        Color mantle = requiem ? Ivory : Ivory.Darkened(0.08f);
        Color underlayer = new(0.025f, 0.035f, 0.055f, 1f);
        Color face = new(0.018f, 0.024f, 0.04f, 1f);
        Color eyes = requiem ? Spectral.Lightened(0.35f) : Ivory;

        DrawCircle(_playerPosition + new Vector2(2f, 10f), 20f, new Color(0f, 0f, 0f, 0.28f));

        DrawLine(_playerPosition - forward * 2f + side * 7f, _playerPosition - forward * 15f + side * 8f, underlayer, 7f);
        DrawLine(_playerPosition - forward * 2f - side * 7f, _playerPosition - forward * 15f - side * 8f, underlayer, 7f);
        DrawCircle(_playerPosition - forward * 3f, 13f, underlayer);

        DrawCircle(_playerPosition + forward * 2f, 16f, mantle);
        DrawLine(_playerPosition - side * 14f, _playerPosition + side * 14f, mantle.Darkened(0.22f), 2f);

        DrawNoahHair(forward, side);
        DrawCircle(_playerPosition + forward * 7f, 10.5f, face);
        DrawNoahEyes(forward, side, eyes, requiem);

        Vector2 scarfStart = _playerPosition - forward * 3f + side * 10f;
        Vector2 scarfTurn = _playerPosition - forward * 18f + side * 13f;
        Vector2 scarfEnd = _playerPosition - forward * 31f + side * 7f;
        DrawLine(scarfStart, scarfTurn, Crimson, 5f);
        DrawLine(scarfTurn, scarfEnd, Crimson.Darkened(0.08f), 4f);

        Vector2 leftHand = _playerPosition + forward * 3f + side * 14f;
        Vector2 rightHand = _playerPosition + forward * 3f - side * 14f;
        DrawCircle(leftHand, 3.5f, Ivory.Darkened(0.18f));
        DrawCircle(rightHand, 3.5f, Ivory.Darkened(0.18f));

        Vector2 bell = _playerPosition - forward * 5f;
        DrawLine(bell - forward * 4f, bell, Gold.Darkened(0.20f), 1.5f);
        DrawCircle(bell, 2.8f, Gold);
        DrawLine(bell - side * 2f, bell + side * 2f, Gold.Darkened(0.42f), 1f);

        float pulse = 2.8f + (MathF.Sin(_elapsed * 6f) + 1f) * 0.55f;
        DrawCircle(_playerPosition + forward * 1f, pulse, requiem ? Ivory : Spectral.Darkened(0.08f));

        Vector2 bladeStart = rightHand + forward * 2f;
        Vector2 bladeEnd = rightHand + forward * 38f;
        DrawLine(bladeStart, bladeEnd, requiem ? Ivory : Spectral, 3f);

        if (requiem)
        {
            DrawArc(_playerPosition, 29f, 0f, Mathf.Tau, 40, Spectral, 2f);
            DrawArc(_playerPosition + forward * 7f, 13f, -0.45f, Mathf.Pi + 0.45f, 20, Ivory.Darkened(0.18f), 1.4f);
        }

        DrawPulseIndicator();
    }

    private void DrawNoahHair(Vector2 forward, Vector2 side)
    {
        Vector2 root = _playerPosition + forward * 7f;
        Color hair = Ivory.Lightened(0.05f);
        float flare = _dashRemaining > 0f ? 4f : 0f;

        DrawLine(root - forward + side * 7f, root - forward * 7f + side * (18f + flare), hair, 4.2f);
        DrawLine(root - forward * 3f + side * 4f, root - forward * 15f + side * (13f + flare), hair.Darkened(0.05f), 4.6f);
        DrawLine(root - forward * 4f, root - forward * (18f + flare), hair, 4.8f);
        DrawLine(root - forward * 3f - side * 4f, root - forward * 14f - side * (14f + flare), hair.Darkened(0.08f), 4.4f);
        DrawLine(root - forward + side * 6f, root + forward * 6f + side * 3f, hair, 3.4f);
        DrawLine(root - forward - side * 4f, root + forward * 6f - side * 2f, hair.Darkened(0.04f), 3.2f);
    }

    private void DrawNoahEyes(Vector2 forward, Vector2 side, Color color, bool requiem)
    {
        bool recoiling = _playerReaction > 0f;
        bool perfect = _gradeDisplay > 0f && _lastGrade == TimingGrade.Perfect;
        bool dashing = _dashRemaining > 0f;
        float focus = Mathf.Clamp(_cadence / 100f, 0f, 1f);
        float spread = requiem ? 3.7f : 4.7f - focus * 0.7f;
        float tilt = requiem ? 2.2f : 1.7f + focus * 1.3f;
        float leftInner = 1.2f;
        float rightInner = 1.2f;

        if (recoiling)
        {
            spread += 0.9f;
            tilt = 0.8f;
            leftInner = 0.2f;
            rightInner = 1.8f;
        }
        else if (perfect)
        {
            spread += 0.7f;
            tilt = 1.4f;
        }
        else if (dashing)
        {
            spread -= 0.4f;
            tilt += 0.8f;
        }
        Vector2 center = _playerPosition + forward * 10f;
        Vector2 eyeTilt = forward * tilt;
        DrawLine(center + side * spread - eyeTilt, center + side * leftInner + eyeTilt * 0.35f, color, 2.6f);
        DrawLine(center - side * spread - eyeTilt, center - side * rightInner + eyeTilt * 0.35f, color, recoiling ? 2.1f : 2.6f);
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

        if (_bellResponseTriggered)
        {
            DrawString(_font, new Vector2(size.X - 356f, 116f), "ECO DO SINO  //  ENCONTRADO", HorizontalAlignment.Right, 328f, 13, Gold);
        }
        else if (_kills > 0)
        {
            DrawString(_font, new Vector2(size.X - 356f, 116f), "PISTA  //  O SINO VIBRA PERTO DA PORTA", HorizontalAlignment.Right, 328f, 13, Gold.Darkened(0.12f));
        }

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

        Rect2 panel = new(size * 0.5f - new Vector2(275f, 132f), new Vector2(550f, 264f));
        Color accent = _sessionState == SessionState.Victory ? Spectral : Crimson;
        DrawRect(panel, new Color(0.025f, 0.03f, 0.05f, 0.98f));
        DrawRect(panel, accent.Darkened(0.15f), false, 2f);
        DrawRect(new Rect2(panel.Position, new Vector2(6f, panel.Size.Y)), accent);

        string title = _sessionState == SessionState.Victory
            ? "O PRIMEIRO ECO RESPONDEU"
            : "A NAVE VOLTOU AO SILÊNCIO";
        string subtitle = _sessionState == SessionState.Victory
            ? (_bellResponseTriggered
                ? "Três Peregrinos cederam. A porta da Catedral reconhece o fragmento."
                : "Três Peregrinos cederam, mas um eco continua oculto junto à porta.")
            : "O fragmento ainda pulsa. Recomece e leia os sinais do Peregrino.";

        float pulseAccuracy = _actions == 0 ? 0f : (_goodActions + _perfectActions) * 100f / _actions;
        DrawString(_font, panel.Position + new Vector2(30f, 48f), title, HorizontalAlignment.Left, panel.Size.X - 60f, 24, Ivory);
        DrawString(_font, panel.Position + new Vector2(30f, 82f), subtitle, HorizontalAlignment.Left, panel.Size.X - 60f, 14, Ivory.Darkened(0.32f));
        DrawString(_font, panel.Position + new Vector2(30f, 124f), $"TEMPO {FormatSessionTime()}   ·   PULSO {pulseAccuracy:0}%   ·   PERFEITOS {_perfectActions}", HorizontalAlignment.Left, panel.Size.X - 60f, 15, accent);
        DrawString(_font, panel.Position + new Vector2(30f, 164f), $"CADÊNCIA FINAL {GetCadenceRank()}   ·   AÇÕES {_actions}", HorizontalAlignment.Left, panel.Size.X - 60f, 14, Ivory.Darkened(0.18f));
        string discovery = _bellResponseTriggered ? "ECO DO SINO  ENCONTRADO" : "ECO DO SINO  OCULTO";
        DrawString(_font, panel.Position + new Vector2(30f, 198f), discovery, HorizontalAlignment.Left, panel.Size.X - 60f, 14, _bellResponseTriggered ? Gold : Ivory.Darkened(0.48f));
        DrawString(_font, panel.Position + new Vector2(30f, 238f), "R  REINICIAR A PROVA", HorizontalAlignment.Left, panel.Size.X - 60f, 16, Gold);
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
