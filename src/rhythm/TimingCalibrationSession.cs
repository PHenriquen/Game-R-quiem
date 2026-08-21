using System;
using System.Collections.Generic;

namespace RequiemEcosDoSilencio.Rhythm;

/// <summary>
/// Collects tap timing errors and proposes a small input-time correction.
///
/// The recommendation uses the median instead of the mean so one accidental tap
/// does not move the calibration by a large amount. It is intentionally separate
/// from authored beatmap offset: a map describes the music, calibration describes
/// the player's device/input path.
/// </summary>
public sealed class TimingCalibrationSession
{
    private readonly List<double> _signedErrors = new();

    public int MinimumSamples { get; }
    public double MaxAcceptedErrorSeconds { get; }
    public int SampleCount => _signedErrors.Count;
    public bool IsReady => SampleCount >= MinimumSamples;

    public TimingCalibrationSession(int minimumSamples = 8, double maxAcceptedErrorSeconds = 0.180)
    {
        if (minimumSamples < 3)
            throw new ArgumentOutOfRangeException(nameof(minimumSamples));
        if (maxAcceptedErrorSeconds <= 0.0 || maxAcceptedErrorSeconds > 0.5)
            throw new ArgumentOutOfRangeException(nameof(maxAcceptedErrorSeconds));

        MinimumSamples = minimumSamples;
        MaxAcceptedErrorSeconds = maxAcceptedErrorSeconds;
    }

    public bool Add(RhythmJudgement judgement)
    {
        if (judgement.Grade == RhythmGrade.Miss || judgement.AbsoluteErrorSeconds > MaxAcceptedErrorSeconds)
            return false;

        _signedErrors.Add(judgement.SignedErrorSeconds);
        return true;
    }

    public void Reset()
    {
        _signedErrors.Clear();
    }

    public double MedianSignedErrorSeconds => Median(_signedErrors);

    public double MeanAbsoluteErrorSeconds
    {
        get
        {
            if (_signedErrors.Count == 0)
                return 0.0;

            double total = 0.0;
            foreach (double value in _signedErrors)
                total += Math.Abs(value);

            return total / _signedErrors.Count;
        }
    }

    /// <summary>
    /// Adjustment to add to the current input calibration offset.
    /// Late taps produce a negative correction; early taps produce a positive one.
    /// </summary>
    public double SuggestedAdjustmentSeconds
    {
        get
        {
            if (!IsReady)
                return 0.0;

            return Math.Clamp(-MedianSignedErrorSeconds, -0.250, 0.250);
        }
    }

    private static double Median(List<double> values)
    {
        if (values.Count == 0)
            return 0.0;

        var copy = values.ToArray();
        Array.Sort(copy);
        int middle = copy.Length / 2;

        if (copy.Length % 2 == 1)
            return copy[middle];

        return (copy[middle - 1] + copy[middle]) * 0.5;
    }
}
