#!/usr/bin/env python3
"""Generate a deterministic starter Echo Trial grid from BPM/duration.

This is intentionally not an automatic level designer. It creates a clean timing
skeleton so authored combat can start from bars and accents instead of an empty file.
"""

from __future__ import annotations

import argparse
import json
from pathlib import Path


def build(title: str, beatmap_id: str, bpm: float, duration: float, offset: float) -> dict:
    seconds_per_beat = 60.0 / bpm
    events: list[dict] = []
    beat = 0
    time = offset

    while time <= duration:
        if beat % 4 == 0:
            events.append(
                {
                    "time": round(time, 6),
                    "type": "pulse",
                    "id": f"bar_{beat // 4:03d}",
                    "lane": 0,
                    "value": "strong",
                }
            )
        beat += 1
        time = offset + beat * seconds_per_beat

    return {
        "version": 1,
        "id": beatmap_id,
        "title": title,
        "artist": "prototype",
        "difficulty": "Echo",
        "bpm": bpm,
        "offset": offset,
        "beatsPerBar": 4,
        "duration": duration,
        "audio": "",
        "events": events,
    }


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--title", default="New Echo Trial")
    parser.add_argument("--id", default="echo_trial_new")
    parser.add_argument("--bpm", type=float, default=120.0)
    parser.add_argument("--duration", type=float, default=60.0)
    parser.add_argument("--offset", type=float, default=0.0)
    parser.add_argument("--out", default="assets/beatmaps/generated_echo_trial.json")
    args = parser.parse_args()

    if not 40 <= args.bpm <= 300:
        parser.error("--bpm must be between 40 and 300")
    if args.duration <= 0:
        parser.error("--duration must be positive")

    root = Path(__file__).resolve().parents[1]
    output = root / args.out
    output.parent.mkdir(parents=True, exist_ok=True)
    output.write_text(
        json.dumps(build(args.title, args.id, args.bpm, args.duration, args.offset), indent=2, ensure_ascii=False) + "\n",
        encoding="utf-8",
    )
    print(output.relative_to(root))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
