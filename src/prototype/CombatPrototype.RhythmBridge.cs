using Godot;
using RequiemEcosDoSilencio.Audio;
using RequiemEcosDoSilencio.Rhythm;

namespace RequiemEcosDoSilencio.Prototype;

/// <summary>
/// Safe bridge between the existing combat toy and the new rhythm platform.
///
/// This intentionally runs in "shadow scoring" mode: it observes successful card
/// actions and scores them against the authored beat grid without changing damage,
/// cadence, movement or enemy behavior yet. That lets the timing platform be
/// playtested in the real combat scene before it becomes authoritative gameplay.
/// </summary>
public partial class CombatPrototype
{
    private CombatRhythmBridge? _combatRhythmBridge;

    public override void _EnterTree()
    {
        _combatRhythmBridge = new CombatRhythmBridge(this)
        {
            Name = "CombatRhythmBridge",
            ZIndex = 100
        };
        AddChild(_combatRhythmBridge);
    }

    private sealed partial class CombatRhythmBridge : Node2D
    {
        private readonly CombatPrototype _owner;
        private readonly RhythmJudge _judge = new();
        private readonly ScoreTracker _score = new();
        private readonly Font _font = ThemeDB.FallbackFont;

        private PulseClock? _clock;
        private EchoTrialDirector? _director;
        private int _observedActions;
        private string _cue = "timeline pronta";
        private float _cueLife;
        private RhythmJudgement _lastJudgement;
        private bool _hasJudgement;

        public CombatRhythmBridge(CombatPrototype owner)
        {
            _owner = owner;
        }

        public override void _Ready()
        {
            _observedActions = _owner._actions;

            _clock = new PulseClock
            {
                Name = "CombatPulseClock"
            };
            AddChild(_clock);

            _director = new EchoTrialDirector
            {
                Name = "CombatEchoTrialDirector",
                BeatmapPath = "res://assets/beatmaps/first_echo_trial.json",
                PulseClockPath = _clock.GetPath()
            };

            _director.TrialLoaded += OnTrialLoaded;
            _director.TimelineEvent += OnTimelineEvent;
            _director.TrialFinished += OnTrialFinished;
            AddChild(_director);
            _director.StartTrial();
        }

        public override void _Process(double delta)
        {
            _cueLife = Mathf.Max(0f, _cueLife - (float)delta);

            // Fallback in case an action was triggered by a path we did not observe
            // in _Input (for example future controller/UI bindings).
            if (_owner._actions > _observedActions)
                CaptureSuccessfulActions();

            QueueRedraw();
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventKey key && key.Pressed && !key.Echo)
            {
                Key physical = key.PhysicalKeycode;
                if (physical is (Key)49 or (Key)50 or (Key)51 or (Key)52)
                    CallDeferred(MethodName.CaptureSuccessfulActions);

                if (physical == (Key)82) // R
                    CallDeferred(MethodName.RestartTrial);
            }
            else if (@event is InputEventMouseButton mouse && mouse.Pressed && mouse.ButtonIndex == MouseButton.Left)
            {
                CallDeferred(MethodName.CaptureSuccessfulActions);
            }
        }

        public override void _Draw()
        {
            if (_clock is null)
                return;

            Vector2 size = GetViewportRect().Size;
            Rect2 panel = new(size.X - 335f, 52f, 300f, 118f);
            DrawRect(panel, new Color(0.02f, 0.025f, 0.04f, 0.90f));
            DrawRect(panel, new Color("28313E"), false, 1.5f);

            DrawString(_font, panel.Position + new Vector2(14f, 24f), "ECHO TRIAL // SHADOW", HorizontalAlignment.Left, 270f, 13, new Color("9FA8B5"));
            DrawString(_font, panel.Position + new Vector2(14f, 50f), $"{_score.GetRank()}   {_score.Accuracy * 100.0:0.0}%   combo {_score.Combo}", HorizontalAlignment.Left, 270f, 18, new Color("E9E2D0"));
            DrawString(_font, panel.Position + new Vector2(14f, 73f), $"score {_score.Score:N0}   beat {_clock.BeatIndex}", HorizontalAlignment.Left, 270f, 13, new Color("54C7CE"));

            string feedback = _hasJudgement
                ? $"{_lastJudgement.Grade.ToString().ToUpperInvariant()}  {_lastJudgement.SignedErrorSeconds * 1000.0:+0;-0;0} ms"
                : "jogue uma carta";
            DrawString(_font, panel.Position + new Vector2(14f, 96f), feedback, HorizontalAlignment.Left, 270f, 13, new Color("C4A35A"));

            if (_cueLife > 0f)
                DrawString(_font, panel.Position + new Vector2(14f, 116f), _cue, HorizontalAlignment.Left, 270f, 11, new Color("687385"));
        }

        private void CaptureSuccessfulActions()
        {
            if (_clock is null)
                return;

            while (_observedActions < _owner._actions)
            {
                _lastJudgement = _judge.JudgeAction(
                    _clock.JudgementTimeSeconds,
                    _clock.Bpm,
                    _clock.BeatOffsetSeconds);

                _score.Apply(_lastJudgement);
                _observedActions++;
                _hasJudgement = true;
            }
        }

        private void RestartTrial()
        {
            _score.Reset();
            _hasJudgement = false;
            _observedActions = _owner._actions;
            _cue = "trial reiniciado";
            _cueLife = 1.5f;
            _director?.StartTrial();
        }

        private void OnTrialLoaded(string beatmapId, string title)
        {
            _cue = title;
            _cueLife = 2.5f;
        }

        private void OnTimelineEvent(string eventType, string eventId, double eventTime, int lane, string value)
        {
            if (eventType == BeatmapEventTypes.Pulse)
                return;

            string detail = string.IsNullOrWhiteSpace(eventId) ? value : eventId;
            _cue = string.IsNullOrWhiteSpace(detail) ? eventType : $"{eventType}: {detail}";
            _cueLife = 1.35f;
        }

        private void OnTrialFinished(string beatmapId)
        {
            _cue = $"fim · {_score.GetRank()} · {_score.Accuracy * 100.0:0.0}%";
            _cueLife = 4f;
        }
    }
}
