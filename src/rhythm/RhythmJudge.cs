using System;

namespace RequiemEcosDoSilencio.Rhythm;

public enum RhythmGrade
{
    Miss,
    Free,
    Good,
    Perfect
}

public readonly record struct RhythmJudgement(
    RhythmGrade Grade,
    double SignedErrorSeconds,
    double AbsoluteErrorSeconds,
    long NearestBeatIndex,
    double ScoreWeight);

public sealed class RhythmJudge
{
    public double PerfectWindowSeconds { get; }
    public double GoodWindowSeconds { get; }

    public RhythmJudge(double perfectWindowSeconds = 0.065, double goodWindowSeconds = 0.140)
    {
        if (perfectWindowSeconds <= 0.0)
            throw new ArgumentOutOfRangeException(nameof(perfectWindowSeconds));
        if (goodWindowSeconds < perfectWindowSeconds)
            throw new ArgumentOutOfRangeException(nameof(goodWindowSeconds));

        PerfectWindowSeconds = perfectWindowSeconds;
        GoodWindowSeconds = goodWindowSeconds;
    }

    public RhythmJudgement JudgeAction(double songTimeSeconds, double bpm, double offsetSeconds = 0.0)
    {
        double secondsPerBeat = 60.0 / Math.Max(1.0, bpm);
        double beatPosition = (songTimeSeconds - offsetSeconds) / secondsPerBeat;
        long nearestBeat = (long)Math.Round(beatPosition, MidpointRounding.AwayFromZero);
        double targetTime = offsetSeconds + nearestBeat * secondsPerBeat;
        return JudgeTarget(songTimeSeconds, targetTime, nearestBeat, allowFree: true);
    }

    public RhythmJudgement JudgeTarget(double actionTimeSeconds, double targetTimeSeconds, long targetIndex = -1, bool allowFree = false)
    {
        double signedError = actionTimeSeconds - targetTimeSeconds;
        double distance = Math.Abs(signedError);

        if (distance <= PerfectWindowSeconds)
            return new RhythmJudgement(RhythmGrade.Perfect, signedError, distance, targetIndex, 1.0);

        if (distance <= GoodWindowSeconds)
            return new RhythmJudgement(RhythmGrade.Good, signedError, distance, targetIndex, 0.65);

        if (allowFree)
            return new RhythmJudgement(RhythmGrade.Free, signedError, distance, targetIndex, 0.20);

        return new RhythmJudgement(RhythmGrade.Miss, signedError, distance, targetIndex, 0.0);
    }
}
