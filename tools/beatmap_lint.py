#!/usr/bin/env python3
"""Validate Réquiem Echo Trial charts and mapset manifests using only stdlib."""

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


def read_json(path: Path) -> tuple[dict | None, list[str]]:
    try:
        data = json.loads(path.read_text(encoding="utf-8"))
    except Exception as exc:
        return None, [f"invalid JSON: {exc}"]
    if not isinstance(data, dict):
        return None, ["root must be an object"]
    return data, []


def validate_chart(path: Path) -> tuple[dict | None, list[str]]:
    data, errors = read_json(path)
    if data is None:
        return None, errors

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
        return data, errors + ["events must be an array"]

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

    return data, errors


def res_path_to_repo(root: Path, value: str) -> Path | None:
    prefix = "res://"
    if not value.startswith(prefix):
        return None
    return root / value[len(prefix) :]


def validate_set(path: Path, root: Path, charts_by_id: dict[str, tuple[Path, dict]]) -> list[str]:
    data, errors = read_json(path)
    if data is None:
        return errors

    if data.get("version") != 1:
        errors.append("version must be 1")

    set_id = str(data.get("id", "")).strip()
    if not set_id:
        errors.append("id is required")
    if not str(data.get("title", "")).strip():
        errors.append("title is required")
    if not str(data.get("audio", "")).strip():
        errors.append("audio is required")

    preview = data.get("previewTime", 0)
    if not isinstance(preview, (int, float)) or preview < 0:
        errors.append("previewTime must be a non-negative number")

    difficulties = data.get("difficulties")
    if not isinstance(difficulties, list) or not difficulties:
        return errors + ["difficulties must be a non-empty array"]

    seen_ids: set[str] = set()
    seen_names: set[str] = set()
    for index, difficulty in enumerate(difficulties):
        if not isinstance(difficulty, dict):
            errors.append(f"difficulties[{index}] must be an object")
            continue

        chart_id = str(difficulty.get("id", "")).strip()
        name = str(difficulty.get("name", "")).strip()
        chart_path = str(difficulty.get("chart", "")).strip()
        rating = difficulty.get("rating")

        if not chart_id:
            errors.append(f"difficulties[{index}].id is required")
        elif chart_id in seen_ids:
            errors.append(f"duplicate difficulty id '{chart_id}'")
        seen_ids.add(chart_id)

        if not name:
            errors.append(f"difficulties[{index}].name is required")
        elif name.casefold() in seen_names:
            errors.append(f"duplicate difficulty name '{name}'")
        seen_names.add(name.casefold())

        if not isinstance(rating, (int, float)) or not 0 <= rating <= 10:
            errors.append(f"difficulties[{index}].rating must be between 0 and 10")

        resolved = res_path_to_repo(root, chart_path)
        if resolved is None:
            errors.append(f"difficulties[{index}].chart must use a res:// path")
        elif not resolved.exists():
            errors.append(f"difficulties[{index}].chart does not exist: {chart_path}")

        chart_entry = charts_by_id.get(chart_id)
        if chart_entry is None:
            errors.append(f"difficulty id '{chart_id}' does not match a chart id")
            continue

        _, chart = chart_entry
        if str(chart.get("setId", "")).strip() != set_id:
            errors.append(f"chart '{chart_id}' setId must be '{set_id}'")
        if chart_path and resolved is not None and chart_entry[0].resolve() != resolved.resolve():
            errors.append(f"difficulty '{chart_id}' chart path does not point to its chart file")
        if str(chart.get("difficulty", "")).strip() != name:
            errors.append(f"chart '{chart_id}' difficulty must match mapset name '{name}'")
        if str(chart.get("audio", "")).strip() != str(data.get("audio", "")).strip():
            errors.append(f"chart '{chart_id}' audio must match mapset audio")

    return errors


def main() -> int:
    root = Path(__file__).resolve().parents[1]
    beatmap_dir = root / "assets" / "beatmaps"
    chart_files = sorted(path for path in beatmap_dir.glob("*.json") if not path.name.endswith(".set.json"))
    set_files = sorted(beatmap_dir.glob("*.set.json"))

    if not chart_files:
        print("No beatmaps found.", file=sys.stderr)
        return 1

    failures = 0
    charts_by_id: dict[str, tuple[Path, dict]] = {}

    for path in chart_files:
        data, errors = validate_chart(path)
        if data is not None:
            chart_id = str(data.get("id", "")).strip()
            if chart_id:
                if chart_id in charts_by_id:
                    errors.append(f"duplicate chart id '{chart_id}' across files")
                else:
                    charts_by_id[chart_id] = (path, data)

        if errors:
            failures += 1
            print(f"FAIL {path.relative_to(root)}")
            for error in errors:
                print(f"  - {error}")
        else:
            print(f"OK   {path.relative_to(root)}")

    for path in set_files:
        errors = validate_set(path, root, charts_by_id)
        if errors:
            failures += 1
            print(f"FAIL {path.relative_to(root)}")
            for error in errors:
                print(f"  - {error}")
        else:
            print(f"OK   {path.relative_to(root)}")

    if not set_files:
        print("WARN no mapset manifests found")

    return 1 if failures else 0


if __name__ == "__main__":
    raise SystemExit(main())
