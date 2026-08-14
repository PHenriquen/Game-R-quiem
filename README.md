# RÉQUIEM: ECOS DO SILÊNCIO

> **International title: REQUIEM: ECHOES OF SILENCE**
>
> **Every death rewrites the music.**

A solitary 2D action roguelite where Nox moves freely, fights through a real-time hand of action cards and gradually returns music to a world that forgot how to sound.

[Creative Direction](docs/DIRECTION_V2.md) · [Game Design](docs/GAME_DESIGN.md) · [Combat Spec](docs/COMBAT_SPEC.md) · [Story](docs/STORY.md) · [Art Bible](docs/ART_BIBLE.md) · [Engineering](docs/ENGINEERING.md) · [Roadmap](docs/ROADMAP.md)

## The identity

Réquiem is built around three feelings:

- **alone while exploring**;
- **clever while discovering secrets**;
- **powerful while mastering combat**.

The game uses lessons, symbols and narrative secrets without turning them into lectures. The world should communicate before dialogue does.

## The world

Vesper was stabilized by the **World Heart**, an ancient structure able to transform patterns of memory into music. When its final composition — the **Last Chord** — was interrupted, parts of the world entered the **Silence**.

Silence is not simply lack of sound. It breaks continuity: places repeat moments, objects retain чуж? memories and people lose pieces of identity.

Nox awakens alone in the Sanctuary of the Last Sound with a fragment of the Heart pulsing near his own.

Some Echoes recognize him.

He recognizes none of them.

The first question is:

**Did Nox break the Heart, or was he trying to stop it from breaking?**

The larger one is:

**Why did Nox choose to forget?**

## Combat

Movement and universal evasion are direct. Offensive actions come from a **four-card hand** used in real time.

Using a card:

1. immediately executes its action;
2. sends it to the discard;
3. leaves the slot empty for a short draw delay;
4. draws the next card from a small deck.

The first prototype deck has eight cards — two copies of four actions:

| Action | Role |
|---|---|
| **Corte Breve** | fast close-range link |
| **Agulha** | linear medium-range pressure |
| **Passo Fantasma** | offensive repositioning |
| **Sino Partido** | heavy circular impact |

The goal is not to make a traditional card game. The player should make quick combat decisions from the hand while still positioning Nox freely.

## Pulse

A shared musical Pulse gives every action a timing grade:

- **Perfect**;
- **Good**;
- **Free**.

Missing the beat never blocks an action. Rhythm rewards mastery instead of replacing the action game.

The first toy runs at 100 BPM with provisional ±65 ms Perfect and ±140 ms Good windows.

## Cadence

**D → C → B → A → S → RÉQUIEM**

Cadence rises when the player maintains clean pressure and performs near the Pulse.

RÉQUIEM is not a screen-filling transformation. For a few seconds Nox appears to remember exactly how he used to fight: posture becomes more confident, effects get cleaner, the Heart fragment opens into light fractures and the soundtrack receives its missing layer.

Power should feel like **control**, not noise.

## Solitude

Nox is the only clear human presence for almost the entire game.

The Sanctuary is not a town full of shops and NPCs. It is an empty, reactive place that changes through objects, bells, Echoes and new passages.

A second person is present indirectly through the story — voice fragments, silhouettes, memories and recurring objects — and only gains physical presence near the end.

## First region — Drowned Cathedral

Theme: **attachment**.

The Cathedral once used enormous bells to synchronize part of Vesper with the World Heart. Now it is partially submerged and still attempts to finish a sequence that no longer has an ending.

Visual language:

- monumental gothic shapes;
- dark blue stone;
- black water and spectral reflections;
- broken bells and aged bronze;
- rare warm-gold memory points;
- strong negative space and readable combat silhouettes.

## Secrets

Secrets are part of the game identity, not just bonus loot.

They can be:

- small visual references;
- mechanical discoveries involving bells, cards or movement;
- hidden rooms;
- short Echoes that change the meaning of earlier scenes.

A good secret should make the player say **“I noticed that”**.

## Nox

Current design language:

- light build;
- irregular dark hair;
- short asymmetric blue-black clothing;
- restrained crimson cloth detail;
- small aged-gold bell;
- spectral-blue Heart fragment;
- narrow spectral weapon: **Agulha de Vesper**.

Signature rule: fragment + bell + crimson detail. Avoid ornamental overload.

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

## Playable prototype V2

The current branch prototype is intentionally asset-free and uses custom-drawn shapes so combat can be tested before final art.

Current target:

- free movement;
- universal evasion;
- four-card hand;
- eight-card prototype deck;
- four actions;
- Pulse;
- Cadence D–RÉQUIEM;
- one telegraphed melee enemy;
- provisional combat HUD;
- local-only project architecture.

Controls:

- `WASD` — movement;
- `Space` — evade;
- `1–4` — use cards;
- mouse click — use a card;
- `R` — reset arena.

The combat toy lives in `src/prototype/CombatPrototype.cs` and starts from `src/prototype/CombatPrototype.tscn`.

## Vertical slice target

After the toy proves the combat:

- Nox with original art;
- Sanctuary of the Last Sound;
- 5–6 authored Drowned Cathedral rooms;
- 8–12 total cards, depending on testing;
- 3 enemies;
- 1 optional elite;
- 1 boss: **Guardian of the Bells**;
- 1 adaptive music composition;
- environmental secret(s);
- short narrative Echoes;
- save data;
- keyboard + controller;
- 10–15 minute complete run.

## Technology

- **Engine:** Godot 4.7.x with .NET support;
- **Language:** C# / .NET 8;
- **Target:** Windows first;
- **Architecture direction:** component-driven gameplay, finite-state machines and data-driven definitions after prototype validation;
- **Performance:** local frame-budget monitoring;
- **Balancing:** local JSON telemetry, no network transport.

The project intentionally stays on Godot instead of migrating engines during pre-production. The current goal is to validate combat and identity, not to spend the vertical-slice budget rebuilding infrastructure.

## Status

**Pre-production / playable combat toy V2.**

The project has been upgraded from its earlier Godot 4.3 setup toward Godot 4.7.1. The new prototype code has been statically checked against the current Godot API, but still requires its first build/playtest inside a local Godot 4.7.1 .NET editor before compilation is considered verified.

## Running locally

1. Install **Godot 4.7.1 .NET**.
2. Clone this repository and checkout `work/vertical-slice-v2` while the prototype is under review.
3. Open `project.godot`.
4. Let Godot restore/build the C# project.
5. Press `F6`/`F5`.
6. Report compile errors or, if it opens, test movement and card feel before judging visuals.

## License

Source code is available under the [MIT License](LICENSE). Original game names, characters, narrative, visual art, music and other creative assets are reserved by the project author unless explicitly stated otherwise.
