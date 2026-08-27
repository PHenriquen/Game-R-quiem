# Rhythm combat integration — shadow scoring pass

## Why this pass exists

The combat toy already has a responsive action loop. The rhythm platform now has a separate musical clock, beatmap timeline, judgement model and score tracker. The risky move would be replacing the combat toy's timing and enemy behavior before we have a real playtest.

This pass therefore uses **shadow scoring**: the existing combat remains authoritative, while the Echo Trial layer observes successful card actions and scores them against the authored beat grid. The player can see rank, accuracy, combo, score and timing error without the rhythm layer blocking or changing attacks.

That gives us real evidence about whether the rhythm layer feels good before it becomes a gameplay dependency.

## Transferable principles researched

### Hi-Fi Rush

Official development retrospective:
- https://news.xbox.com/en-us/2023/10/04/hi-fi-rush-exclusive-oral-history/

Useful principles:
- action remained the larger share of the experience (the team repeatedly described roughly a 70/30 action/rhythm balance);
- the game should always feel rhythmic, but responsiveness and player freedom should not be sacrificed;
- enemy attacks and animation logic can be authored from beat rules rather than guessed after music is added.

For Réquiem this means the first integration should **measure and reward rhythm before it controls permission to act**.

### Godot audio synchronization

Stable documentation:
- https://docs.godotengine.org/en/stable/tutorials/audio/sync_with_audio.html

Useful principles:
- raw `AudioStreamPlayer.GetPlaybackPosition()` advances in chunks;
- adding `AudioServer.GetTimeSinceLastMix()` improves timing precision;
- subtracting output latency better approximates what reaches the speakers;
- the resulting clock can jitter backwards, so gameplay time should be monotonic.

`PulseClock` already follows this model and remains the single timing source for Echo Trials.

### osu! beatmap architecture

File format reference:
- https://osu.ppy.sh/wiki/en/Client/File_formats/osu_(file_format)

Useful principle:
- authored timing data belongs to the map, while player/device compensation is a different concern.

Réquiem follows the same separation: beatmap BPM/offset stays authored data; input calibration adjusts judgement only.

## What the shadow bridge does

`src/prototype/CombatPrototype.RhythmBridge.cs`:

1. creates a `PulseClock` inside the combat scene;
2. loads `first_echo_trial.json` through `EchoTrialDirector`;
3. observes successful card actions from the existing toy;
4. judges those actions with `RhythmJudge`;
5. records accuracy/combo/score/rank with `ScoreTracker`;
6. renders a compact debug overlay;
7. surfaces non-pulse timeline events as temporary cues;
8. resets the Echo Trial when the combat toy is reset.

It deliberately does **not** change:
- card damage;
- cadence gains;
- movement or dash;
- enemy attack timing;
- campaign state;
- Noah canon or visual direction.

## Playtest questions

During the first local Godot test, answer these before making rhythm authoritative:

- Does the score feel like it reflects intentional play rather than random proximity to the beat?
- Are Perfect/Good windows readable while also moving and choosing cards?
- Is the overlay useful without distracting from combat?
- Do players naturally start syncing attacks without being forced?
- Does timing error consistently skew early/late, indicating calibration needs?
- Does the Echo Trial timeline feel like a useful composition layer for future enemy telegraphs?

## Promotion gate

Only after a successful local build and playtest should the next pass replace the prototype's old `_elapsed`-based timing with the shared `PulseClock`, then migrate enemy telegraphs to semantic beatmap events one behavior at a time.
