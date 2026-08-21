#!/usr/bin/env python3
"""Validate Réquiem Echo Trial beatmaps using only the Python standard library."""

from __future__ import annotations

import json
import sys
from pathlib import Path

KNOWN_TYPES = {
    "pulse",
    "enemy_spawn",
    "enemy_telegraph",
    "arena_shift",
    "card_window",
    "accent",
    "checkpoint",
}


def validate(path: Path) -> list[str]:
    errors: list[str] = []
    try:
        data = json.loads(path.read_text(encoding="utf-8"))
    except Exception as exc:
        return [f"invalid JSON: {exc}"]

    if data.get("version") != 1:
        errors.append("version must be 1")
    if not str(data.get("id", "")).strip():
        errors.append("id is required")
    if not str(data.get("title", "")).strip():
        errors.append("title is required")

    bpm = data.get("bpm")
    if not isinstance(bpm, (int, float)) or not 40 <= bpm <= 300:
        errors.append("bpm must be between 40 and 300")

    duration = data.get("duration")
    if not isinstance(duration, (int, float)) or duration <= 0:
        errors.append("duration must be greater than zero")
        duration = 0

    events = data.get("events")
    if not isinstance(events, list):
        return errors + ["events must be an array"]

    previous = -1.0
    ids: set[str] = set()
    for index, event in enumerate(events):
        if not isinstance(event, dict):
            errors.append(f"events[{index}] must be an object")
            continue

        time = event.get("time")
        if not isinstance(time, (int, float)):
            errors.append(f"events[{index}].time must be numeric")
            continue
        if time < 0:
            errors.append(f"events[{index}].time cannot be negative")
        if duration and time > duration + 1e-6:
            errors.append(f"events[{index}].time exceeds duration")
        if time < previous:
            errors.append(f"events[{index}] is out of chronological order")
        previous = time

        kind = event.get("type")
        if kind not in KNOWN_TYPES:
            errors.append(f"events[{index}].type '{kind}' is unknown")

        event_id = str(event.get("id", "")).strip()
        if event_id:
            if event_id in ids:
                errors.append(f"duplicate event id '{event_id}'")
            ids.add(event_id)

    return errors


def main() -> int:
    root = Path(__file__).resolve().parents[1]
    beatmap_dir = root / "assets" / "beatmaps"
    files = sorted(beatmap_dir.glob("*.json"))
    if not files:
        print("No beatmaps found.", file=sys.stderr)
        return 1

    failures = 0
    for path in files:
        errors = validate(path)
        if errors:
            failures += 1
            print(f"FAIL {path.relative_to(root)}")
            for error in errors:
                print(f"  - {error}")
        else:
            print(f"OK   {path.relative_to(root)}")

    return 1 if failures else 0


if __name__ == "__main__":
    raise SystemExit(main())
