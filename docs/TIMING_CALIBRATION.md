# Timing calibration

## Why this exists

Rhythm scoring is only fair when three different things stay separate:

1. **authored timing** — where the song's beat grid actually begins;
2. **playback timing** — what the audio device and game clock are doing;
3. **player/input calibration** — a small correction for the player's hardware and input path.

Réquiem must never fix a player's setup by silently moving authored events. Beatmap timing remains part of the content; calibration only changes how an input is judged.

## Reference principles

The first implementation was informed by:

- osu! beatmap timing documentation: BPM, offset and timing sections are map data, and objects are authored against a timeline grid;
  https://osu.ppy.sh/wiki/en/Beatmapping/Timing
- osu! `.osu` format documentation: timing metadata and hit objects are stored separately in a human-readable, versioned map representation;
  https://osu.ppy.sh/wiki/en/Client/File_formats/osu_%28file_format%29
- osu!lazer discussions around universal/platform offset: real devices and OS audio paths can require user-side correction even when map timing is valid;
  https://github.com/ppy/osu/discussions/18328

Transferable rule: **map offset and player offset must not be the same variable**.

## Implementation

`PulseClock` now exposes:

- `SongTimeSeconds`: raw gameplay/audio timeline after the existing audio latency compensation;
- `BeatOffsetSeconds`: authored map grid offset;
- `InputCalibrationOffsetSeconds`: user/device correction;
- `JudgementTimeSeconds`: raw song time plus the input calibration correction.

Beat/bar publication and Echo Trial timeline events continue to use `SongTimeSeconds`. Only player judgement should use `JudgementTimeSeconds`.

## Calibration session

`TimingCalibrationSession` collects accepted timing errors and recommends an **incremental** correction.

Rules:

- minimum 8 accepted taps;
- ignore misses and taps farther than 180 ms from the nearest beat;
- use the **median signed error**, not the mean, so one bad tap has low influence;
- clamp one recommendation to ±250 ms;
- late taps produce a negative correction; early taps produce a positive correction.

The debug Echo Trial scene supports:

- `Space`: tap to the beat;
- `A`: apply the current suggestion when enough samples exist;
- `C`: clear calibration samples;
- `R`: restart the trial.

This is developer tooling only. A final player-facing calibration screen should use real music/click audio, a short guided sequence and explicit save/reset controls.

## Next validation gate

Do not tune final Perfect/Good windows from theory. After the Godot 4.7.1 .NET build is confirmed, run calibration on at least two output paths (for example speakers and Bluetooth/headphones if available), record the resulting offsets, then play the same Echo Trial with and without the correction.

The success criterion is not a particular millisecond value. It is that repeated taps become centered around zero without moving authored timeline events.
