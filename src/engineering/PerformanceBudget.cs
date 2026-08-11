using Godot;
using System;

namespace Requiem.Engineering;

/// <summary>
/// Lightweight runtime performance budget monitor.
/// It keeps the game-performance concern explicit without depending on an
/// external profiler in release/debug builds.
/// </summary>
public partial class PerformanceBudget : Node
{
    [Export] public double TargetFps { get; set; } = 60.0;
    [Export] public double SampleIntervalSeconds { get; set; } = 1.0;
    [Export] public bool PrintWarnings { get; set; } = true;

    public double LastAverageFrameMs { get; private set; }
    public double WorstFrameMs { get; private set; }
    public long FramesSampled { get; private set; }

    private double _elapsed;
    private double _frameMsTotal;
    private double _worstFrameMs;
    private long _frames;

    public override void _Process(double delta)
    {
        var frameMs = delta * 1000.0;
        _elapsed += delta;
        _frameMsTotal += frameMs;
        _worstFrameMs = Math.Max(_worstFrameMs, frameMs);
        _frames++;

        if (_elapsed < SampleIntervalSeconds)
            return;

        LastAverageFrameMs = _frames > 0 ? _frameMsTotal / _frames : 0.0;
        WorstFrameMs = _worstFrameMs;
        FramesSampled = _frames;

        var targetFrameMs = 1000.0 / Math.Max(TargetFps, 1.0);
        if (PrintWarnings && (LastAverageFrameMs > targetFrameMs || WorstFrameMs > targetFrameMs * 1.75))
        {
            GD.PushWarning(
                $"Performance budget exceeded: avg={LastAverageFrameMs:F2}ms " +
                $"worst={WorstFrameMs:F2}ms target={targetFrameMs:F2}ms"
            );
        }

        _elapsed = 0.0;
        _frameMsTotal = 0.0;
        _worstFrameMs = 0.0;
        _frames = 0;
    }
}
