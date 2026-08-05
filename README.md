# REQUIEM PULSE

> **Every death rewrites the music.**

A rhythm-driven 2D action roguelite where attacks, cards and an adaptive soundtrack become one combat system.

[Game Design](docs/GAME_DESIGN.md) · [Roadmap](docs/ROADMAP.md) · [Contributing](CONTRIBUTING.md)

## The game

The World Heart has shattered, leaving Vesper trapped in supernatural silence. Nox carries its last living fragment and must cross corrupted ruins, defeat the Dissonants and discover why his own heart beats in time with the machine.

Each run is a new performance of the same requiem. Death restarts the song, but changes the cards, paths and story.

## Core pillars

- **Action on the pulse** — combat works freely, but precise timing rewards damage, energy and invulnerability.
- **Build your score** — equip, upgrade and fuse technique cards instead of drawing during combat.
- **Hear your mastery** — the soundtrack gains instruments and intensity as Cadence rises.
- **Death moves the story** — characters and the sanctuary evolve between runs.
- **Readable depth** — dark hand-painted environments contrast with clean geometric combat effects.

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

- **Engine:** Godot 4.x with .NET support
- **Language:** C# / .NET
- **Architecture:** component-driven gameplay, finite-state machines and data-driven resources
- **Target:** Windows first, with Linux support planned

## Current status

**Pre-production / foundation.** The next milestone is the Pulse combat prototype.

## Running locally

1. Install Godot 4.x with .NET support.
2. Clone this repository.
3. Open `project.godot` in Godot.
4. Press F6 or F5 to run the bootstrap scene.

## Visual identity

| Role | Color |
|---|---|
| Night black | `#090B12` |
| Ivory | `#E9E2D0` |
| Crimson | `#9E1738` |
| Spectral blue | `#54C7CE` |
| Aged gold | `#C4A35A` |

The emblem is a cracked white mask crossed by a pulse line. The REQUIEM wordmark is elegant and distressed; PULSE is thin and modern.

## License

Source code is available under the [MIT License](LICENSE). Original game names, characters, narrative, visual art, music and other creative assets are reserved by the project author unless explicitly stated otherwise.
