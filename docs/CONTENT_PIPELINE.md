# Content Pipeline

The production goal is to make adding content cheaper without making the game feel generated.

## Principle

Automate **setup, validation and repetition**. Keep **meaning, composition and final encounter decisions** authored.

## Echo Trial pipeline

```text
music / temp audio
      ↓
BPM + offset decision
      ↓
beatmap_seed.py
      ↓
bar/accent skeleton JSON
      ↓
manual semantic events
      ↓
beatmap_lint.py
      ↓
Godot live preview
      ↓
playtest notes
      ↓
polish + difficulty variants
```

`beatmap_seed.py` is intentionally deterministic. It prevents blank-page work but does not pretend to understand encounter design.

## Later audio-analysis assistant

A future offline helper may estimate BPM, onsets, sections and intensity from audio. Its output must be treated as suggestions. It should never directly publish combat events.

Possible pipeline:

```text
track
  ↓
analysis assistant
  ├── estimated BPM / offset
  ├── onset candidates
  ├── section boundaries
  └── intensity curve
  ↓
author accepts/rejects
  ↓
semantic chart
```

## Campaign content

The same tools can help campaign encounters, but campaign rooms should reference a smaller authored encounter asset instead of importing score requirements. Narrative spaces are allowed to breathe, desynchronize, fall silent or deliberately break the grid.

## Asset production

Use generated/reference art for exploration, not as a silent substitute for final consistency.

For every final asset family keep:

- source/reference intent;
- palette and scale rules;
- export size;
- animation list;
- gameplay readability requirement;
- provenance/license note.

## What the assistant can accelerate

- C# systems and refactors;
- editor utilities;
- beatmap scaffolds and validators;
- encounter data drafts;
- card/enemy balance tables;
- UI wireframes and copy;
- lore organization and continuity checks;
- concept art and visual exploration;
- CI, build scripts and documentation;
- repetitive variations after a rule is approved.

## What still needs human direction

- whether movement/combat feels good;
- whether a musical phrase creates tension or annoyance;
- final character appeal;
- emotional pacing;
- which mystery should remain unexplained;
- commercial music/license choices;
- approving final art and audio consistency.

The intended workflow is therefore closer to **creative director + playtester** than manually writing every supporting system from scratch.
