using System;

namespace RequiemEcosDoSilencio.Prototype;

public partial class CombatPrototype
{
    private enum SessionState
    {
        Playing,
        Victory,
        Defeat
    }

    private const int TargetKills = 3;

    private SessionState _sessionState = SessionState.Playing;
    private float _sessionElapsed;

    private bool IsSessionRunning => _sessionState == SessionState.Playing;

    private void StartSession()
    {
        _sessionState = SessionState.Playing;
        _sessionElapsed = 0f;
    }

    private void UpdateSession(float delta)
    {
        if (IsSessionRunning)
            _sessionElapsed += delta;
    }

    private void CompleteSession(SessionState outcome)
    {
        if (!IsSessionRunning)
            return;

        _sessionState = outcome;
        _actionLock = 0f;
        _dashRemaining = 0f;
        _enemy.Telegraphing = false;
    }

    private string FormatSessionTime()
    {
        int totalSeconds = Math.Max(0, (int)MathF.Floor(_sessionElapsed));
        return $"{totalSeconds / 60:00}:{totalSeconds % 60:00}";
    }
}
