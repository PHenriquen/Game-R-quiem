# Rhythm Platform Foundation

Réquiem uses rhythm as a shared technical substrate for combat, music and replayability without becoming an osu! clone.

## Two layers

### Authored campaign

The campaign remains finite and narrative-led. Encounters can synchronize telegraphs, accents and environmental events to the music, but level design is authored around story, place and pacing.

### Echo Trials

Echo Trials are compact replayable combat scores. They reuse the same movement, cards and combat language while adding explicit accuracy, combo, score, ranks, difficulty variants and authored timelines.

The inspiration taken from score-focused rhythm games is the loop:

`music -> authored chart -> precision -> combo -> score -> mastery -> replay`

The input language remains Réquiem's combat.

## Runtime architecture

```text
AudioStreamPlayer
      |
      v
PulseClock --------------------------+
  BPM / offset / latency             |
      |                               |
      +--> RhythmJudge                |
      |      Perfect / Good / Free    |
      |                               |
      +--> EchoTrialDirector <--- BeatmapLoader <--- JSON beatmap
                 |
                 +--> enemy systems
                 +--> arena events
                 +--> VFX / accents
                 +--> card opportunities

player actions --> RhythmJudge --> ScoreTracker --> score / accuracy / rank
```

## Responsibilities

### `PulseClock`

Single source of musical time. It can follow actual audio playback, compensates output latency, understands BPM, beat offset and bars, and exposes beat boundaries.

### `RhythmJudge`

Pure timing math. It can grade a free combat action against the nearest beat or grade a required chart target against a specific timestamp.

### `ScoreTracker`

Keeps accuracy, combo, maximum combo, result counts, score and the D/C/B/A/S/RÉQUIEM mastery rank.

### `BeatmapLoader`

Loads versioned JSON and rejects malformed maps before gameplay depends on them.

### `EchoTrialDirector`

Walks through an authored timeline using `PulseClock`. It emits semantic events; it does not know how to spawn an enemy or draw an arena. That separation lets campaign encounters and Trials share infrastructure.

## Timing language

Initial windows:

- Perfect: ±65 ms
- Good: ±140 ms
- Free: outside Good when an ordinary combat action is allowed
- Miss: used only when an authored Echo Trial object explicitly expected a response

These values are starting points, not canon. They must be tuned through playtesting and include a user calibration setting before release.

## Score philosophy

Score should reward expression without making optimal play unreadable.

- Perfect and Good preserve combo.
- Free actions remain legal but break score combo.
- Miss is reserved for chart requirements.
- accuracy matters more than raw action count;
- combo adds pressure but should not dwarf precision;
- RÉQUIEM rank should be rare enough to feel earned.

The implementation intentionally borrows the separation between accuracy and combo common in modern rhythm-game scoring systems, but uses original values and combat-specific judgements.

## What is deliberately not in this foundation

- online leaderboards;
- workshop/community distribution;
- automatic enemy choreography;
- procedural campaign encounters;
- a full visual chart editor;
- licensed/commercial music ingestion.

Those only become worth building after one 30–90 second Echo Trial feels good in the actual Godot build.
