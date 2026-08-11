# Engineering Notes

Réquiem is the portfolio project focused on game-development engineering rather than web/backend breadth. Its technical goal is to demonstrate C#/.NET, real-time systems, gameplay architecture, performance discipline and data-driven balancing inside Godot.

## Runtime performance budget

`src/engineering/PerformanceBudget.cs` measures average and worst frame time over a configurable interval and warns when the game exceeds the target frame budget.

For a 60 FPS target, the practical budget is about 16.67 ms per frame. The monitor is not a replacement for the Godot profiler; it makes performance expectations explicit during normal gameplay development.

Future extensions:

- per-system timing for combat, particles and AI;
- object-count budgets;
- memory/GC allocation snapshots;
- automated performance scenes in CI.

## Local gameplay telemetry

`src/engineering/RunTelemetry.cs` records local events such as combat actions, Perfect timing, Cadence progression, room duration and damage taken. It can export a run to JSON for balancing and offline analysis.

```text
run -> local events -> JSON -> balancing notebook/tool -> gameplay decisions
```

There is deliberately no network transport in this layer. Player data should not be uploaded without an explicit future opt-in design.

## Architecture direction

The project remains centered on:

- component-driven gameplay;
- finite-state machines;
- data-driven cards/resources;
- deterministic run seeds where possible;
- local save/progression;
- adaptive audio synchronized with combat;
- profiling before premature optimization.

## Visual engineering direction

The art direction is moving toward modern 2D pixel-art readability with a darker, melancholic identity: restrained silhouettes, strong lighting, spectral effects and clean combat feedback. The target is a style that remains feasible for a small project while still looking authored rather than generic.

Rendering work should prioritize:

1. readable silhouettes and hit effects;
2. stable pixel scale/camera movement;
3. controlled particles and bloom;
4. lighting that supports gameplay instead of obscuring it;
5. a consistent performance budget on target hardware.

## Portfolio coverage

Réquiem demonstrates a different engineering surface from the other projects:

- C# / .NET;
- Godot and real-time game loops;
- gameplay architecture;
- state machines and data-driven content;
- audio/rhythm systems;
- profiling and optimization;
- local telemetry for balancing.
