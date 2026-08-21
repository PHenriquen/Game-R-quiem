using Godot;
using RequiemEcosDoSilencio.Audio;
using System;

namespace RequiemEcosDoSilencio.Rhythm;

/// <summary>
/// Replays an authored combat timeline on top of the shared PulseClock.
/// It intentionally does not own enemies, cards or arena code: those systems
/// subscribe to TimelineEvent and decide how an event is represented.
/// </summary>
public partial class EchoTrialDirector : Node
{
    [Signal]
    public delegate void TrialLoadedEventHandler(string beatmapId, string title);

    [Signal]
    public delegate void TimelineEventEventHandler(string eventType, string eventId, double eventTime, int lane, string value);

    [Signal]
    public delegate void TrialFinishedEventHandler(string beatmapId);

    [Export]
    public string BeatmapPath { get; set; } = "res://assets/beatmaps/first_echo_trial.json";

    [Export]
    public NodePath PulseClockPath { get; set; } = new();

    [Export(PropertyHint.Range, "0,0.5,0.001")]
    public double LookAheadSeconds { get; set; } = 0.0;

    public BeatmapDocument? Beatmap { get; private set; }
    public bool Running { get; private set; }

    private PulseClock? _clock;
    private int _nextEventIndex;
    private bool _finishedPublished;

    public override void _Ready()
    {
        if (!PulseClockPath.IsEmpty)
            _clock = GetNodeOrNull<PulseClock>(PulseClockPath);

        LoadBeatmap(BeatmapPath);
    }

    public override void _Process(double delta)
    {
        if (!Running || Beatmap is null || _clock is null)
            return;

        double now = _clock.SongTimeSeconds;
        double dispatchThrough = now + LookAheadSeconds;

        while (_nextEventIndex < Beatmap.Events.Count && Beatmap.Events[_nextEventIndex].TimeSeconds <= dispatchThrough)
        {
            BeatmapEvent item = Beatmap.Events[_nextEventIndex++];
            EmitSignal(SignalName.TimelineEvent, item.Type, item.Id, item.TimeSeconds, item.Lane, item.Value);
        }

        if (!_finishedPublished && now >= Beatmap.DurationSeconds)
        {
            _finishedPublished = true;
            Running = false;
            EmitSignal(SignalName.TrialFinished, Beatmap.Id);
        }
    }

    public void LoadBeatmap(string path)
    {
        Beatmap = BeatmapLoader.Load(path);
        _nextEventIndex = 0;
        _finishedPublished = false;
        Running = false;

        if (_clock is not null)
        {
            _clock.Bpm = Beatmap.Bpm;
            _clock.BeatsPerBar = Beatmap.BeatsPerBar;
            _clock.ResetFallbackClock();
        }

        EmitSignal(SignalName.TrialLoaded, Beatmap.Id, Beatmap.Title);
    }

    public void StartTrial()
    {
        if (Beatmap is null)
            throw new InvalidOperationException("No beatmap loaded.");

        _nextEventIndex = 0;
        _finishedPublished = false;
        _clock?.ResetFallbackClock();
        Running = true;
    }

    public void StopTrial()
    {
        Running = false;
    }
}
