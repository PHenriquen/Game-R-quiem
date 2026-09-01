using Godot;
using System;

namespace RequiemEcosDoSilencio.Prototype;

public partial class CombatPrototype
{
    private enum SessionState
    {
        Briefing,
        Playing,
        Victory,
        Defeat
    }

    private const int TargetKills = 3;
    private const float BellEchoDuration = 5.2f;

    private SessionState _sessionState = SessionState.Playing;
    private float _sessionElapsed;
    private bool _bellResponseTriggered;
    private float _bellResponseRemaining;

    private bool IsSessionRunning => _sessionState == SessionState.Playing;
    private bool IsSessionBriefing => _sessionState == SessionState.Briefing;
    private bool IsSessionResolved => _sessionState is SessionState.Victory or SessionState.Defeat;
    private bool IsBellMicroEchoActive => _bellResponseRemaining > 0f;

    private void StartSession(bool showBriefing)
    {
        _sessionState = showBriefing ? SessionState.Briefing : SessionState.Playing;
        _sessionElapsed = 0f;
        _bellResponseTriggered = false;
        _bellResponseRemaining = 0f;
    }

    private void BeginBriefedSession()
    {
        if (!IsSessionBriefing)
            return;

        _sessionState = SessionState.Playing;
        _combatRhythmBridge?.SetPrototypePaused(false);
        QueueRedraw();
    }

    private void UpdateSession(float delta)
    {
        if (!IsSessionRunning)
            return;

        if (!IsBellMicroEchoActive)
            _sessionElapsed += delta;

        bool echoWasActive = IsBellMicroEchoActive;
        _bellResponseRemaining = MathF.Max(0f, _bellResponseRemaining - delta);
        if (echoWasActive && !IsBellMicroEchoActive)
            _combatRhythmBridge?.SetPrototypePaused(_prototypePaused);

        Vector2 responsePoint = GetBellResponsePoint();
        if (_kills > 0 && !_bellResponseTriggered && _playerPosition.DistanceTo(responsePoint) <= 74f)
        {
            _bellResponseTriggered = true;
            _bellResponseRemaining = BellEchoDuration;
            _combatRhythmBridge?.SetPrototypePaused(true);
            _enemy.Telegraphing = false;
            _enemy.TelegraphRemaining = 0f;
            _enemy.AttackCooldown = MathF.Max(_enemy.AttackCooldown, 0.82f);
        }
    }

    private Vector2 GetBellResponsePoint() => new(_arena.GetCenter().X, _arena.Position.Y + 76f);

    private void CompleteSession(SessionState outcome)
    {
        if (!IsSessionRunning)
            return;

        _sessionState = outcome;
        _actionLock = 0f;
        _dashRemaining = 0f;
        _enemy.Telegraphing = false;
        _combatRhythmBridge?.SetPrototypePaused(true);
    }

    private string FormatSessionTime()
    {
        int totalSeconds = Math.Max(0, (int)MathF.Floor(_sessionElapsed));
        return $"{totalSeconds / 60:00}:{totalSeconds % 60:00}";
    }
}
