using Godot;
using System;

namespace RequiemEcosDoSilencio.Audio;

public enum PulseGrade
{
    Free,
    Good,
    Perfect
}

/// <summary>
/// Shared musical clock for gameplay systems.
///
/// The combat toy can run without audio, but once a music player is assigned this
/// clock derives its time from the actual playback position and compensates for
/// audio mix/output latency. Cards, enemies and VFX should consult this source
/// instead of maintaining independent beat timers.
/// </summary>
public partial class PulseClock : Node
{
    [Signal]
    public delegate void BeatEventHandler(long beatIndex);

    [Signal]
    public delegate void BarEventHandler(long barIndex);

    [Export(PropertyHint.Range, "40,240,0.1")]
    public double Bpm { get; set; } = 100.0;

    [Export(PropertyHint.Range, "1,12,1")]
    public int BeatsPerBar { get; set; } = 4;

    [Export(PropertyHint.Range, "0.01,0.25,0.001")]
    public double PerfectWindowSeconds { get; set; } = 0.065;

    [Export(PropertyHint.Range, "0.02,0.35,0.001")]
    public double GoodWindowSeconds { get; set; } = 0.140;

    [Export]
    public NodePath MusicPlayerPath { get; set; } = new();

    [Export]
    public bool CompensateOutputLatency { get; set; } = true;

    public double SongTimeSeconds { get; private set; }
    public long BeatIndex { get; private set; } = -1;
    public long BarIndex { get; private set; } = -1;

    public double SecondsPerBeat => 60.0 / Math.Max(1.0, Bpm);

    private AudioStreamPlayer? _musicPlayer;
    private double _fallbackTime;
    private double _lastRawPlayback;
    private double _loopOffset;
    private double _lastClockTime;

    public override void _Ready()
    {
        if (!MusicPlayerPath.IsEmpty)
            _musicPlayer = GetNodeOrNull<AudioStreamPlayer>(MusicPlayerPath);
    }

    public override void _Process(double delta)
    {
        _fallbackTime += delta;
        SongTimeSeconds = ReadClockTime();
        PublishBoundaries();
    }

    public PulseGrade GradeCurrentMoment()
    {
        return GradeTime(SongTimeSeconds);
    }

    public PulseGrade GradeTime(double songTimeSeconds)
    {
        double beatDuration = SecondsPerBeat;
        double phase = PositiveModulo(songTimeSeconds, beatDuration);
        double distance = Math.Min(phase, beatDuration - phase);

        if (distance <= PerfectWindowSeconds)
            return PulseGrade.Perfect;

        if (distance <= GoodWindowSeconds)
            return PulseGrade.Good;

        return PulseGrade.Free;
    }

    public double SecondsUntilNextBeat()
    {
        double phase = PositiveModulo(SongTimeSeconds, SecondsPerBeat);
        return phase <= 0.000001 ? 0.0 : SecondsPerBeat - phase;
    }

    public double NormalizedBeatPhase()
    {
        return PositiveModulo(SongTimeSeconds, SecondsPerBeat) / SecondsPerBeat;
    }

    public void ResetFallbackClock()
    {
        _fallbackTime = 0.0;
        _lastRawPlayback = 0.0;
        _loopOffset = 0.0;
        _lastClockTime = 0.0;
        SongTimeSeconds = 0.0;
        BeatIndex = -1;
        BarIndex = -1;
    }

    private double ReadClockTime()
    {
        if (_musicPlayer is null || !_musicPlayer.Playing)
            return _fallbackTime;

        double raw = _musicPlayer.GetPlaybackPosition() + AudioServer.GetTimeSinceLastMix();

        if (CompensateOutputLatency)
            raw -= AudioServer.GetOutputLatency();

        raw = Math.Max(0.0, raw);

        // A looping AudioStreamPlayer jumps its playback position back near zero.
        // Preserve a monotonic gameplay clock across loops when stream length is known.
        if (raw + 0.10 < _lastRawPlayback)
        {
            double length = _musicPlayer.Stream?.GetLength() ?? 0.0f;
            if (length > 0.0)
                _loopOffset += length;
        }

        _lastRawPlayback = raw;

        double clock = _loopOffset + raw;

        // Audio callbacks and render frames are asynchronous. Never allow a tiny
        // backward jitter to make gameplay publish the same beat twice.
        if (clock < _lastClockTime)
            clock = _lastClockTime;

        _lastClockTime = clock;
        return clock;
    }

    private void PublishBoundaries()
    {
        long newBeat = (long)Math.Floor(SongTimeSeconds / SecondsPerBeat);
        if (newBeat != BeatIndex)
        {
            BeatIndex = newBeat;
            EmitSignal(SignalName.Beat, BeatIndex);
        }

        int safeBeatsPerBar = Math.Max(1, BeatsPerBar);
        long newBar = BeatIndex < 0 ? -1 : BeatIndex / safeBeatsPerBar;
        if (newBar != BarIndex)
        {
            BarIndex = newBar;
            EmitSignal(SignalName.Bar, BarIndex);
        }
    }

    private static double PositiveModulo(double value, double modulus)
    {
        if (modulus <= 0.0)
            return 0.0;

        double result = value % modulus;
        return result < 0.0 ? result + modulus : result;
    }
}
