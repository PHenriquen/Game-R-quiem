# RÉQUIEM: ECOS DO SILÊNCIO

> **International title: REQUIEM: ECHOES OF SILENCE**
>
> **Every death rewrites the music.**

A solitary 2D action roguelite where Nox moves freely, fights through a real-time hand of action cards and gradually returns music to a world that forgot how to sound.

[Creative Direction](docs/DIRECTION_V2.md) · [Game Design](docs/GAME_DESIGN.md) · [Combat Spec](docs/COMBAT_SPEC.md) · [Level Slice](docs/LEVEL_SLICE.md) · [Story](docs/STORY.md) · [Art Bible](docs/ART_BIBLE.md) · [Audio](docs/AUDIO_DIRECTION.md) · [Engineering](docs/ENGINEERING.md) · [Roadmap](docs/ROADMAP.md) · [Assets](docs/ASSET_REGISTRY.md)

## Identity

Réquiem is built around three feelings:

- **alone while exploring**;
- **clever while discovering secrets**;
- **powerful while mastering combat**.

The game uses lessons, symbols and narrative secrets without turning them into lectures. The world should communicate before dialogue does.

## World

Vesper was stabilized by the **World Heart**, an ancient structure able to transform patterns of memory into music. When its final composition — the **Last Chord** — was interrupted, parts of the world entered the **Silence**.

Silence is not simply lack of sound. It breaks continuity: places repeat moments, objects retain memories that are not theirs and people lose pieces of identity.

Nox awakens alone in the Sanctuary of the Last Sound with a fragment of the Heart pulsing near his own.

Some Echoes recognize him. He recognizes none of them.

The first question is:

**Did Nox break the Heart, or was he trying to stop it from breaking?**

The larger one is:

**Why did Nox choose to forget?**

## Combat

Movement and universal evasion are direct. Offensive actions come from a **four-card hand** used in real time.

Using a card immediately executes its action, sends it to the discard and opens that slot for a short draw delay. A small deck keeps the current hand readable enough for fast decisions.

The first combat toy uses eight cards — two copies of four actions:

| Action | Role |
|---|---|
| **Corte Breve** | fast close-range link |
| **Agulha** | linear medium-range pressure |
| **Passo Fantasma** | offensive repositioning |
| **Sino Partido** | heavy circular impact |

This is not intended to play like a turn-based deckbuilder or a mobile gacha. The hand changes the choices available **inside** an action game.

## Pulse

A shared musical Pulse gives actions one of three timing grades:

- **Perfect**;
- **Good**;
- **Free**.

Missing the beat never blocks an action. Rhythm rewards mastery instead of replacing the action game.

The first toy uses 100 BPM with provisional ±65 ms Perfect and ±140 ms Good windows.

`src/audio/PulseClock.cs` is the direction for the production clock: once real music is playing, gameplay timing should derive from the audio playback clock and compensate for output latency instead of every system maintaining an independent timer.

## Cadence

**D → C → B → A → S → RÉQUIEM**

Cadence rises when the player maintains clean pressure and performs near the Pulse.

RÉQUIEM is not a screen-filling transformation. For a few seconds Nox appears to remember exactly how he used to fight: posture becomes calmer, effects get cleaner, the Heart fragment opens into light fractures and the soundtrack receives its missing layer.

Power should feel like **control**, not noise.

## Solitude

Nox is the only clear human presence for almost the entire game.

The Sanctuary is not a town full of shops and NPCs. It is an empty, reactive place that changes through objects, bells, Echoes and new passages.

A second person is present indirectly through voice fragments, silhouettes, memories and recurring objects, gaining physical presence only near the end.

## First region — Drowned Cathedral

Theme: **attachment**.

The Cathedral once used enormous bells to synchronize part of Vesper with the World Heart. Now it is partially submerged and still attempts to finish a sequence that no longer has an ending.

The first vertical-slice route is designed as:

**Sanctuary → Silent Nave → Gallery of Four Bells → Submerged Cloister → optional elite → Corridor Without Echo → Bell Guardian.**

See [`docs/LEVEL_SLICE.md`](docs/LEVEL_SLICE.md).

## Secrets

Secrets are part of the identity, not just bonus loot.

They can be visual references, mechanical discoveries, hidden rooms or short Echoes that change the meaning of earlier scenes.

A good secret should make the player think:

> **“I noticed that.”**

## Nox

Current design language:

- light/athletic build;
- irregular dark hair;
- short asymmetric blue-black clothing;
- restrained crimson cloth detail;
- small aged-gold bell;
- spectral-blue Heart fragment;
- narrow spectral weapon: **Agulha de Vesper**.

Signature rule: **fragment + bell + crimson detail**. Avoid ornamental overload.

## Visual identity

Direction: **modern readable pixel art, melancholic fantasy, monumental environments and restrained spectral effects**.

| Role | Color |
|---|---|
| Night black | `#090B12` |
| Ivory | `#E9E2D0` |
| Spectral blue | `#54C7CE` |
| Aged gold | `#C4A35A` |
| Crimson | `#9E1738` |
| Veil violet | `#6651A6` |

Color carries meaning and is not used everywhere at once.

## Adaptive audio prototype

`tools/generate_prototype_audio.py` creates original procedural placeholder stems without third-party Python packages:

- cathedral_ambient.wav;
- cathedral_memory.wav;
- cathedral_pulse.wav;
- cathedral_bells.wav;
- cathedral_requiem.wav;
- cathedral_full_preview.wav.

They share the same 100 BPM / 8-bar timeline and exist only to validate Pulse, mixing and Cadence before final music is composed.

Run:

```bash
python tools/generate_prototype_audio.py
```

Generated files are placed under `assets/audio/prototype/`.

## Playable combat toy V2

The prototype is intentionally asset-free and uses shapes drawn by code so combat can be judged before final art.

Current implementation includes:

- free movement;
- universal evasion;
- four-card hand;
- eight-card prototype deck;
- four actions;
- Pulse;
- Cadence D–RÉQUIEM;
- telegraphed melee enemy;
- provisional HUD;
- placeholder arena composition.

Controls:

- `WASD` — movement;
- `Space` — evade;
- `1–4` — use cards;
- mouse click — use a card;
- `R` — reset arena.

Main files:

- `src/prototype/CombatPrototype.cs`;
- `src/prototype/CombatPrototype.tscn`;
- `src/audio/PulseClock.cs`.

## Vertical slice target

After the combat toy proves the core feel:

- Nox with original art;
- Sanctuary of the Last Sound;
- 5–6 authored Drowned Cathedral rooms;
- 8–12 total cards depending on testing;
- 3 enemies;
- 1 optional elite;
- 1 boss: **Guardian of the Bells**;
- adaptive music;
- environmental secrets;
- short narrative Echoes;
- basic save;
- keyboard + controller;
- 10–15 minute complete run.

## Technology

- **Engine:** Godot 4.7.x .NET;
- **Language:** C# / .NET 8;
- **Target:** Windows first;
- **Architecture direction:** component-driven gameplay, finite-state machines and data-driven definitions after the combat toy validates feel;
- **Performance:** local frame-budget monitoring;
- **Balancing:** local JSON telemetry with no network transport.

The project stays on Godot instead of migrating engines during pre-production. The current budget is better spent validating combat and identity than rebuilding infrastructure.

## Status

**Pre-production / playable combat toy V2.**

The branch has been upgraded from its earlier Godot 4.3 setup toward Godot 4.7.1. The new code has been statically checked against relevant Godot APIs, but compilation is not considered verified until the first local build/playtest in a Godot 4.7.1 .NET editor.

## Running locally

1. Install **Godot 4.7.1 .NET**.
2. Clone this repository.
3. Checkout `work/vertical-slice-v2` while the prototype is under review.
4. Optionally run `python tools/generate_prototype_audio.py` to generate temporary stems.
5. Open `project.godot`.
6. Let Godot restore/build the C# project.
7. Press `F5`.
8. Test movement/card feel before judging placeholder visuals.

## License

Source code is available under the [MIT License](LICENSE). Original game names, characters, narrative, visual art, music and other creative assets are reserved by the project author unless explicitly stated otherwise.
