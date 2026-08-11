# RÉQUIEM: ECOS DO SILÊNCIO

> **International title: REQUIEM: ECHOES OF SILENCE**
>
> **Every death rewrites the music.**

A rhythm-driven 2D action roguelite where attacks, cards and an adaptive soundtrack become one combat system.

[Game Design](docs/GAME_DESIGN.md) · [Engineering](docs/ENGINEERING.md) · [Roadmap](docs/ROADMAP.md) · [Contributing](CONTRIBUTING.md)

## The game

The World Heart has shattered, leaving Vesper trapped in supernatural silence. Nox carries its last living fragment and must cross corrupted ruins, defeat the Dissonants and discover why his own heart beats in time with the machine.

Each run is a new performance of the same requiem. Death restarts the song, but changes the cards, paths and story.

## Core pillars

- **Action on the pulse** — combat works freely, but precise timing rewards damage, energy and invulnerability.
- **Build your score** — equip, upgrade and fuse technique cards instead of drawing during combat.
- **Hear your mastery** — the soundtrack gains instruments and intensity as Cadence rises.
- **Death moves the story** — characters and the sanctuary evolve between runs.
- **Readable depth** — dark, melancholic environments contrast with clean geometric combat effects.

## Signature systems

### Pulse

A subtle ring around Nox communicates the beat. Attacking, dodging or defending inside its timing window creates a Perfect action. Missing never stops the player; mastery enhances the action instead of gating it.

### Cadence

**D → C → B → A → S → REQUIEM**

Long perfect sequences raise Cadence. Higher ranks add musical layers, improve rewards and unlock card effects. Repeated misses cause temporary Dissonance.

### Score and cards

A Score holds up to eight cards. Three cards from one family create a Harmony; combining families creates hybrid builds.

| Family | Identity | Playstyle |
|---|---|---|
| Blood | Crimson | damage, sacrifice, lifesteal |
| Veil | Violet | speed, clones, evasion |
| Bell | Aged gold | guard, impact, counters |
| Grave | Pale blue | curses, spirits, control |
| Noise | Broken white | unstable rhythm manipulation |

## Vertical slice

- 1 playable character and 1 weapon;
- 24 cards across five families;
- 8–10 rooms in the Drowned Cathedral;
- 4 regular enemies, 1 miniboss and 1 boss;
- a 15-minute complete run;
- adaptive music, progression and save data;
- keyboard and controller support.

## Technology

- **Engine:** Godot 4.x with .NET support;
- **Language:** C# / .NET;
- **Architecture:** component-driven gameplay, finite-state machines and data-driven resources;
- **Performance:** explicit runtime frame-budget monitoring;
- **Data:** local gameplay telemetry exported to JSON for balancing/profiling;
- **Target:** Windows first, with Linux support planned.

## Engineering layer

Réquiem is also the portfolio project used to demonstrate engineering specific to real-time games.

`src/engineering/PerformanceBudget.cs` tracks average/worst frame time against a configurable FPS target. `src/engineering/RunTelemetry.cs` records local combat, Cadence and room events so balancing decisions can be based on actual run data rather than intuition alone.

```text
gameplay -> telemetry local -> JSON -> análise/balanceamento
        \
         -> frame budget -> profiling/optimization
```

The telemetry layer has no network transport by design. Details are documented in [`docs/ENGINEERING.md`](docs/ENGINEERING.md).

## Visual identity

The visual direction is evolving toward **modern pixel-art readability with a darker, melancholic and mystical identity**. The objective is to combine authored 2D sprites with controlled lighting, spectral particles and clean combat feedback, rather than chase expensive 3D realism.

| Role | Color |
|---|---|
| Night black | `#090B12` |
| Ivory | `#E9E2D0` |
| Crimson | `#9E1738` |
| Spectral blue | `#54C7CE` |
| Aged gold | `#C4A35A` |

The emblem remains a cracked white mask crossed by a pulse line. **Réquiem** is the central title, connecting death, repetition, music and the REQUIEM cadence rank. **Vesper** remains the name of the world.

## Current status

**Pre-production / foundation.** The next milestone remains the Pulse combat prototype, now with performance and local telemetry foundations ready to accompany gameplay development.

## Running locally

1. Install Godot 4.x with .NET support.
2. Clone this repository.
3. Open `project.godot` in Godot.
4. Press F6 or F5 to run the bootstrap scene.

## Portfolio coverage

Réquiem adds a technical surface that the other projects do not cover directly:

- game loops and real-time logic;
- C# / .NET;
- Godot;
- gameplay/state-machine architecture;
- rhythm/audio synchronization;
- profiling and optimization;
- data-driven balancing and telemetry.

## License

Source code is available under the [MIT License](LICENSE). Original game names, characters, narrative, visual art, music and other creative assets are reserved by the project author unless explicitly stated otherwise.
