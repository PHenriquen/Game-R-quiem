#!/usr/bin/env python3
"""Generate original placeholder audio for Réquiem's 100 BPM combat prototype.

No third-party packages are required. The generated WAVs are deliberately simple
and are not intended as final composition or sound design.
"""
from __future__ import annotations

import math
import struct
import wave
from pathlib import Path

SAMPLE_RATE = 22_050
BPM = 100.0
BEAT = 60.0 / BPM
BARS = 8
BEATS_PER_BAR = 4
DURATION = BARS * BEATS_PER_BAR * BEAT
TAU = math.tau

ROOT = Path(__file__).resolve().parents[1]
OUTPUT = ROOT / "assets" / "audio" / "prototype"

NOTES = {
    "D2": 73.416,
    "A2": 110.000,
    "D3": 146.832,
    "D4": 293.665,
    "F4": 349.228,
    "A4": 440.000,
    "Cs5": 554.365,
    "D5": 587.330,
}
MOTIF = [NOTES["D4"], NOTES["F4"], NOTES["A4"], NOTES["Cs5"]]


def clamp(value: float, lo: float = -1.0, hi: float = 1.0) -> float:
    return max(lo, min(hi, value))


def sine(freq: float, t: float, phase: float = 0.0) -> float:
    return math.sin(TAU * freq * t + phase)


def event_envelope(t: float, start: float, attack: float, decay: float) -> float:
    age = t - start
    if age < 0.0:
        return 0.0
    if age < attack:
        return age / max(attack, 1e-6)
    return math.exp(-(age - attack) / max(decay, 1e-6))


def loop_lfo(t: float, cycles: int, phase: float = 0.0) -> float:
    return math.sin(TAU * cycles * t / DURATION + phase)


def render() -> dict[str, list[float]]:
    count = int(round(DURATION * SAMPLE_RATE))
    stems = {
        "ambient": [0.0] * count,
        "memory": [0.0] * count,
        "pulse": [0.0] * count,
        "bells": [0.0] * count,
        "requiem": [0.0] * count,
    }

    memory_events: list[tuple[float, float]] = []
    bell_events: list[tuple[float, float]] = []

    for bar in range(BARS):
        bar_start = bar * BEATS_PER_BAR * BEAT
        memory_events.append((bar_start, MOTIF[0]))
        memory_events.append((bar_start + 2.0 * BEAT, MOTIF[1 if bar % 2 == 0 else 2]))

    for bar in range(BARS):
        if bar % 2 == 0:
            continue
        bar_start = bar * BEATS_PER_BAR * BEAT
        for beat_index, freq in enumerate(MOTIF):
            bell_events.append((bar_start + beat_index * BEAT, freq / 2.0))

    for i in range(count):
        t = i / SAMPLE_RATE

        drift = 0.5 + 0.5 * loop_lfo(t, 2)
        ambient = (
            0.055 * sine(NOTES["D2"], t)
            + 0.026 * sine(NOTES["A2"], t, 0.6)
            + 0.012 * sine(NOTES["D3"], t, 1.1)
        )
        ambient *= 0.62 + 0.22 * drift
        ambient += 0.008 * loop_lfo(t, 16, 1.7) * sine(36.7, t)
        stems["ambient"][i] = ambient

        memory = 0.0
        for start, freq in memory_events:
            env = event_envelope(t, start, 0.035, 0.78)
            if env > 0.0002:
                memory += env * (
                    0.095 * sine(freq, t - start)
                    + 0.028 * sine(freq * 2.0, t - start, 0.2)
                )
        stems["memory"][i] = memory

        pulse = 0.0
        nearest_beat = round(t / BEAT) * BEAT
        beat_age = t - nearest_beat
        if 0.0 <= beat_age < 0.12:
            env = math.exp(-beat_age / 0.035)
            freq = 92.0 - 42.0 * min(1.0, beat_age / 0.12)
            pulse += 0.11 * env * sine(freq, beat_age)

        nearest_half = round(t / (BEAT / 2.0)) * (BEAT / 2.0)
        half_age = t - nearest_half
        if 0.0 <= half_age < 0.035 and int(round(nearest_half / (BEAT / 2.0))) % 2 == 1:
            pulse += 0.025 * math.exp(-half_age / 0.012) * sine(1450.0, half_age)
        stems["pulse"][i] = pulse

        bells = 0.0
        for start, freq in bell_events:
            env = event_envelope(t, start, 0.008, 1.15)
            if env > 0.00015:
                age = t - start
                bells += env * (
                    0.052 * sine(freq, age)
                    + 0.032 * sine(freq * 2.01, age, 0.4)
                    + 0.019 * sine(freq * 2.71, age, 1.0)
                    + 0.011 * sine(freq * 4.13, age, 0.2)
                )
        stems["bells"][i] = bells

        gate = 0.70 + 0.20 * loop_lfo(t, 4, 0.9)
        stems["requiem"][i] = gate * (
            0.034 * sine(NOTES["D4"], t, 0.1)
            + 0.027 * sine(NOTES["A4"], t, 0.7)
            + 0.020 * sine(NOTES["Cs5"], t, 1.2)
            + 0.012 * sine(NOTES["D5"], t, 0.4)
        )

    return stems


def normalize(samples: list[float], target_peak: float = 0.82) -> list[float]:
    peak = max((abs(value) for value in samples), default=1.0)
    if peak <= 1e-9:
        return samples
    gain = min(1.0, target_peak / peak)
    return [clamp(value * gain) for value in samples]


def write_wav(path: Path, samples: list[float]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    pcm = bytearray()
    for sample in normalize(samples):
        pcm.extend(struct.pack("<h", int(clamp(sample) * 32767.0)))

    with wave.open(str(path), "wb") as wav:
        wav.setnchannels(1)
        wav.setsampwidth(2)
        wav.setframerate(SAMPLE_RATE)
        wav.writeframes(pcm)


def main() -> None:
    stems = render()

    for name, samples in stems.items():
        write_wav(OUTPUT / f"cathedral_{name}.wav", samples)

    preview = [
        stems["ambient"][i]
        + stems["memory"][i]
        + stems["pulse"][i]
        + stems["bells"][i]
        + stems["requiem"][i]
        for i in range(len(stems["ambient"]))
    ]
    write_wav(OUTPUT / "cathedral_full_preview.wav", preview)

    (OUTPUT / "README.md").write_text(
        "# Prototype audio (generated)\n\n"
        "Generated locally by `tools/generate_prototype_audio.py`.\n\n"
        f"- BPM: {BPM:g}\n"
        f"- Bars: {BARS}\n"
        f"- Duration: {DURATION:.1f}s\n"
        "- Format: mono PCM WAV, 22.05 kHz / 16-bit\n"
        "- Purpose: timing/adaptive-mix prototype only; not final soundtrack.\n\n"
        "All generated tones are original procedural placeholders created by this repository script.\n",
        encoding="utf-8",
    )

    print(f"Generated {len(stems) + 1} WAV files in {OUTPUT}")


if __name__ == "__main__":
    main()
