# Echo Trials

**Working mode name.** The name can change without changing the architecture.

Echo Trials are short combat performances built around a song/timeline. They are the replayable side of Réquiem, not a replacement for the authored campaign.

## Player fantasy

The first attempt is survival. Later attempts become performance.

A new player sees attacks and reacts. An experienced player recognizes the phrase, positions Nox early, holds the right card, dodges into the next beat and turns the same encounter into choreography.

## Trial structure

Recommended first target: 30–90 seconds.

Each trial has:

- one audio timeline;
- BPM and calibration offset;
- one arena identity;
- a small authored enemy set;
- semantic timeline events;
- one or more difficulty charts;
- score, accuracy, combo and rank;
- a local personal best.

## Difficulty philosophy

Difficulty should add decisions before it adds raw speed.

### Echo

Readable telegraphs, generous recovery, fewer simultaneous demands.

### Pulse

More overlaps, tighter positioning, stronger expectation that cards are chosen ahead of time.

### Requiem

Authored for mastery: compressed recovery, layered enemy phrases and optional Perfect-oriented routes. It must remain legible rather than becoming particle noise.

## Beatmap event vocabulary v1

- `pulse`: strong authored musical accent; ordinary beat boundaries still come from `PulseClock`.
- `enemy_spawn`: request an encounter system to introduce an enemy/archetype.
- `enemy_telegraph`: request a specific readable attack phrase.
- `arena_shift`: alter space, hazards or navigable emphasis.
- `card_window`: mark a designed opportunity for attack/dodge/finisher scoring.
- `accent`: non-critical audio/VFX/narrative accent.
- `checkpoint`: semantic phase boundary for debugging, restart and analytics.

The beatmap stores **intent**, not engine implementation. `enemy_telegraph = ring` is better than storing animation frame IDs in the chart.

## First playable trial

`assets/beatmaps/first_echo_trial.json` is a 32-second authored timing sketch named **Primeiro Eco de Vesper**.

Its purpose is not content quality. It proves that the format can describe:

- introduction;
- enemy entries;
- telegraphs;
- response windows;
- arena changes;
- a midpoint memory beat;
- a compact finale and return to silence.

## Future editor

The eventual chart editor should expose a waveform/timeline and semantic tracks rather than imitate osu!'s playfield.

Suggested tracks:

```text
MUSIC     |---- waveform / bars -----------------------------|
ENEMIES   | spawn      telegraph      spawn       boss phrase|
ARENA     |         shift                    clear             |
CARDS     |      attack     dodge        perfect     finish    |
ACCENTS   | bell              memory             bell          |
```

Authoring should support drag, snap-to-beat, free millisecond placement, event duplication, phase markers, live preview and validation.

## Community content — later

A public beatmap ecosystem is plausible, but only after the internal format is stable. Community maps should not be able to alter campaign canon. They live as Trials/challenges with clear authorship and music licensing boundaries.
