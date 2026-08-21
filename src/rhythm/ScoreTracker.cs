using System;

namespace RequiemEcosDoSilencio.Rhythm;

public sealed class ScoreTracker
{
    public int PerfectCount { get; private set; }
    public int GoodCount { get; private set; }
    public int FreeCount { get; private set; }
    public int MissCount { get; private set; }
    public int Combo { get; private set; }
    public int MaxCombo { get; private set; }
    public long Score { get; private set; }

    public int JudgedCount => PerfectCount + GoodCount + FreeCount + MissCount;

    public double Accuracy
    {
        get
        {
            if (JudgedCount == 0)
                return 1.0;

            double weighted = PerfectCount + GoodCount * 0.65 + FreeCount * 0.20;
            return weighted / JudgedCount;
        }
    }

    public void Reset()
    {
        PerfectCount = 0;
        GoodCount = 0;
        FreeCount = 0;
        MissCount = 0;
        Combo = 0;
        MaxCombo = 0;
        Score = 0;
    }

    public void Apply(RhythmJudgement judgement, int baseScore = 1000)
    {
        switch (judgement.Grade)
        {
            case RhythmGrade.Perfect:
                PerfectCount++;
                Combo++;
                break;
            case RhythmGrade.Good:
                GoodCount++;
                Combo++;
                break;
            case RhythmGrade.Free:
                FreeCount++;
                Combo = 0;
                break;
            default:
                MissCount++;
                Combo = 0;
                break;
        }

        MaxCombo = Math.Max(MaxCombo, Combo);
        double comboMultiplier = 1.0 + Math.Min(Combo, 100) * 0.006;
        Score += (long)Math.Round(baseScore * judgement.ScoreWeight * comboMultiplier);
    }

    public string GetRank()
    {
        double acc = Accuracy;
        if (JudgedCount >= 20 && acc >= 0.985 && MissCount == 0 && FreeCount <= 1)
            return "RÉQUIEM";
        if (acc >= 0.95)
            return "S";
        if (acc >= 0.88)
            return "A";
        if (acc >= 0.78)
            return "B";
        if (acc >= 0.65)
            return "C";
        return "D";
    }
}
