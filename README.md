# RÉQUIEM: ECOS DO SILÊNCIO

> **International title: REQUIEM: ECHOES OF SILENCE**
>
> **Every death rewrites the music.**

A compact rhythm-driven 2D action roguelite where attacks, cards and an adaptive soundtrack become one combat system.

[Game Design](docs/GAME_DESIGN.md) · [Story](docs/STORY.md) · [Art Bible](docs/ART_BIBLE.md) · [Engineering](docs/ENGINEERING.md) · [Roadmap](docs/ROADMAP.md)

## The game

Vesper was once stabilized by the World Heart, an ancient machine whose music held memory, matter and time together. When it shattered, entire regions fell into supernatural silence and their inhabitants became trapped inside incomplete memories.

Nox awakens in the Sanctuary of the Last Sound with no memory of his past and a fragment of the Heart pulsing inside his chest. Each death returns him to the Sanctuary. Each run recovers new Echoes. Some of them recognize him.

The first version asks one question: **did Nox destroy the World Heart, or was he trying to save it?**

## Core pillars

- **Action first, rhythm second** — combat always works; good timing makes it stronger.
- **Simple builds** — six equipped cards, short descriptions, three families in the first version.
- **Adaptive music** — Cadence adds musical layers as the player performs better.
- **Short runs** — 10–15 minutes for the first complete slice.
- **Small story, strong mystery** — short Echoes and one clear reveal instead of long cutscenes.
- **Readable pixel art** — dark, melancholic environments with clear combat feedback.

## Signature systems

### Pulse

A visual/music pulse communicates timing. Attack or dodge close to the beat for a Good or Perfect action. Missing never blocks the command.

### Cadence

**D → C → B → A → S → REQUIEM**

Cadence rises through well-timed actions and progressively enriches music and effects.

### Score and cards

The first version has **12 cards**, **6 slots** and **3 families**:

| Family | Identity | Playstyle |
|---|---|---|
| Blood | Crimson | damage, risk, recovery |
| Veil | Violet | movement, dodge, speed |
| Bell | Aged gold | impact, defense, shockwaves |

Grave and Noise remain future expansion space.

## Nox

The only playable character in the first version.

Visual identity:

- dark irregular hair;
- blue-black short coat;
- crimson scarf;
- small aged-gold bell;
- spectral-blue Heart fragment;
- narrow spectral blade.

Nox is quiet, observant and uncertain about his own past. His identity is rebuilt through the Echoes found during runs.

## First playable slice

- 1 playable character;
- 1 weapon: **Agulha de Vesper**;
- 1 small hub: **Sanctuary of the Last Sound**;
- 1 region: **Drowned Cathedral**;
- 12 cards;
- 3 enemy types;
- 1 optional elite;
- 1 boss: **Guardian of the Bells**;
- 5–6 authored rooms in small route variations;
- 10–15 minute complete run;
- 1 adaptive music track;
- basic save data;
- keyboard and controller support.

No advanced procedural generation, multiple characters, multiple complete regions or giant progression trees in the first version.

## Technology

- **Engine:** Godot 4.x with .NET support;
- **Language:** C# / .NET;
- **Architecture:** component-driven gameplay, finite-state machines and data-driven resources;
- **Performance:** explicit runtime frame-budget monitoring;
- **Data:** local gameplay telemetry exported to JSON for balancing/profiling;
- **Target:** Windows first.

## Engineering layer

Réquiem also demonstrates engineering specific to real-time games.

`src/engineering/PerformanceBudget.cs` tracks average/worst frame time against a configurable FPS target. `src/engineering/RunTelemetry.cs` records local combat, Cadence and room events so balancing decisions can use actual run data.

```text
gameplay -> local telemetry -> JSON -> balancing
        \
         -> frame budget -> profiling/optimization
```

Telemetry has no network transport by design.

## Visual identity

Direction: **modern pixel art, melancholic fantasy, strong silhouettes, restrained lighting and spectral effects**.

| Role | Color |
|---|---|
| Night black | `#090B12` |
| Ivory | `#E9E2D0` |
| Crimson | `#9E1738` |
| Spectral blue | `#54C7CE` |
| Aged gold | `#C4A35A` |
| Veil violet | `#6651A6` |

The recurring symbols are bell, pulse, fracture, Echo and the ivory mask as a secondary narrative emblem.

See [`docs/ART_BIBLE.md`](docs/ART_BIBLE.md).

## Current status

**Pre-production / focused vertical slice.** The project is intentionally reducing scope before expanding content. The next objective is a polished Pulse combat prototype with Nox, one room, one enemy and adaptive music before building the complete 10–15 minute slice.

## Running locally

1. Install Godot 4.x with .NET support.
2. Clone this repository.
3. Open `project.godot` in Godot.
4. Press F6 or F5 to run the bootstrap scene.

## License

Source code is available under the [MIT License](LICENSE). Original game names, characters, narrative, visual art, music and other creative assets are reserved by the project author unless explicitly stated otherwise.
