using Godot;
using RequiemEcosDoSilencio.Audio;
using System.Collections.Generic;

namespace RequiemEcosDoSilencio.Rhythm.Debug;

public partial class EchoTrialDebug : Node2D
{
    private sealed class EventFlash
    {
        public string Type = string.Empty;
        public string Id = string.Empty;
        public double Time;
        public float Life = 2.5f;
    }

    private readonly Font _font = ThemeDB.FallbackFont;
    private readonly List<EventFlash> _recent = new();
    private PulseClock? _clock;
    private EchoTrialDirector? _director;
    private string _title = "Echo Trial";

    public override void _Ready()
    {
        _clock = new PulseClock();
        _clock.Name = "PulseClock";
        AddChild(_clock);

        _director = new EchoTrialDirector
        {
            Name = "EchoTrialDirector",
            BeatmapPath = "res://assets/beatmaps/first_echo_trial.json",
            PulseClockPath = _clock.GetPath()
        };
        AddChild(_director);

        _director.TrialLoaded += OnTrialLoaded;
        _director.TimelineEvent += OnTimelineEvent;
        _director.TrialFinished += OnTrialFinished;
        _director.StartTrial();

        GD.Print("Echo Trial debug running. R = restart.");
    }

    public override void _Process(double delta)
    {
        float dt = (float)delta;
        for (int i = _recent.Count - 1; i >= 0; i--)
        {
            _recent[i].Life -= dt;
            if (_recent[i].Life <= 0f)
                _recent.RemoveAt(i);
        }

        QueueRedraw();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey key && key.Pressed && !key.Echo && key.PhysicalKeycode == Key.R)
        {
            _recent.Clear();
            _director?.StartTrial();
        }
    }

    public override void _Draw()
    {
        Vector2 size = GetViewportRect().Size;
        DrawRect(new Rect2(Vector2.Zero, size), new Color("080A10"));

        DrawString(_font, new Vector2(52, 70), "RÉQUIEM / ECHO TRIAL DEBUG", HorizontalAlignment.Left, -1, 24, new Color("E9E2D0"));
        DrawString(_font, new Vector2(52, 108), _title, HorizontalAlignment.Left, -1, 31, new Color("54C7CE"));

        double songTime = _clock?.SongTimeSeconds ?? 0.0;
        long beat = _clock?.BeatIndex ?? -1;
        double phase = _clock?.NormalizedBeatPhase() ?? 0.0;
        DrawString(_font, new Vector2(52, 148), $"time {songTime,6:0.000}s   beat {beat,3}   phase {phase:0.00}", HorizontalAlignment.Left, -1, 16, new Color("9FA8B5"));

        float x = 52f;
        float y = 190f;
        float width = size.X - 104f;
        DrawLine(new Vector2(x, y), new Vector2(x + width, y), new Color("28313E"), 2f);
        DrawLine(new Vector2(x + (float)phase * width, y - 14), new Vector2(x + (float)phase * width, y + 14), new Color("C4A35A"), 3f);

        DrawString(_font, new Vector2(52, 245), "Recent semantic events", HorizontalAlignment.Left, -1, 18, new Color("E9E2D0"));
        int visible = 0;
        for (int i = _recent.Count - 1; i >= 0 && visible < 9; i--, visible++)
        {
            EventFlash item = _recent[i];
            Color color = EventColor(item.Type);
            float rowY = 280f + visible * 35f;
            DrawCircle(new Vector2(61, rowY - 5), 5f, color);
            DrawString(_font, new Vector2(80, rowY), $"{item.Time,6:0.00}  {item.Type,-18} {item.Id}", HorizontalAlignment.Left, -1, 16, color);
        }

        DrawString(_font, new Vector2(52, size.Y - 42), "R restart · fallback clock (audio not required)", HorizontalAlignment.Left, -1, 14, new Color("687385"));
    }

    private void OnTrialLoaded(string beatmapId, string title)
    {
        _title = $"{title} / {beatmapId}";
    }

    private void OnTimelineEvent(string eventType, string eventId, double eventTime, int lane, string value)
    {
        _recent.Add(new EventFlash { Type = eventType, Id = string.IsNullOrWhiteSpace(eventId) ? value : eventId, Time = eventTime });
        GD.Print($"[EchoTrial] {eventTime:0.000} {eventType} {eventId} lane={lane} value={value}");
    }

    private void OnTrialFinished(string beatmapId)
    {
        _recent.Add(new EventFlash { Type = "finished", Id = beatmapId, Time = _clock?.SongTimeSeconds ?? 0.0 });
    }

    private static Color EventColor(string type)
    {
        return type switch
        {
            BeatmapEventTypes.EnemySpawn => new Color("E05268"),
            BeatmapEventTypes.EnemyTelegraph => new Color("F0A65A"),
            BeatmapEventTypes.CardWindow => new Color("54C7CE"),
            BeatmapEventTypes.ArenaShift => new Color("6651A6"),
            BeatmapEventTypes.Accent => new Color("C4A35A"),
            BeatmapEventTypes.Checkpoint => new Color("E9E2D0"),
            _ => new Color("8194AC")
        };
    }
}
