#!/usr/bin/env python3
"""Validate source-level contracts that protect the playable prototype loop."""

from __future__ import annotations

import re
import sys
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]


class ContractError(Exception):
    pass


def read(relative_path: str) -> str:
    path = ROOT / relative_path
    if not path.is_file():
        raise ContractError(f"missing required file: {relative_path}")
    return path.read_text(encoding="utf-8")


def require(condition: bool, message: str) -> None:
    if not condition:
        raise ContractError(message)


def require_in_order(source: str, tokens: tuple[str, ...], contract: str) -> None:
    cursor = -1
    for token in tokens:
        position = source.find(token, cursor + 1)
        require(position >= 0, f"{contract}: missing `{token}`")
        require(position > cursor, f"{contract}: `{token}` is out of order")
        cursor = position


def validate_input_contract(input_source: str, combat_source: str, bridge_source: str) -> int:
    actions = dict(re.findall(r'public const string (\w+) = "([^"]+)";', input_source))
    require(len(actions) >= 13, "input: expected at least 13 prototype actions")
    require(len(set(actions.values())) == len(actions), "input: action names must be unique")

    for constant in actions:
        require(f"EnsureAction({constant}," in input_source, f"input: {constant} has no default binding")

    combined = combat_source + bridge_source
    forbidden = ("IsPhysicalKeyPressed", "PhysicalKeycode ==", "(Key)49", "(Key)82")
    for token in forbidden:
        require(token not in combined, f"input: raw physical binding returned: {token}")

    require("Input.GetVector(" in combat_source, "input: movement must consume mapped actions")
    require("PrototypeInput.CardActions" in bridge_source, "input: rhythm capture must consume card actions")
    return len(actions)


def validate_pause_contract(combat_source: str, bridge_source: str) -> None:
    require_in_order(
        combat_source,
        ("if (_prototypePaused || IsSessionBriefing)", "_elapsed += dt;"),
        "pause",
    )
    require("_clock?.SetProcess(!paused);" in bridge_source, "pause: Pulse clock is not frozen")
    require("_director?.SetProcess(!paused);" in bridge_source, "pause: trial timeline is not frozen")


def validate_reset_contract(combat_source: str, bridge_source: str) -> None:
    reset_start = combat_source.find("private void ResetArena(bool showBriefing)")
    require(reset_start >= 0, "reset: ResetArena boundary is missing")
    reset_source = combat_source[reset_start:]
    require_in_order(
        reset_source,
        (
            "_elapsed = 0f;",
            "StartSession(showBriefing);",
            "RestartForArena();",
            "SetPrototypePaused(showBriefing);",
        ),
        "reset",
    )
    require("if (@event.IsActionPressed(PrototypeInput.Restart" not in bridge_source, "reset: restart has multiple input owners")
    require("private void RestartTrial()" in bridge_source, "reset: rhythm restart implementation is missing")


def validate_briefing_contract(session_source: str, combat_source: str, draw_source: str) -> None:
    require("SessionState.Briefing" in session_source, "briefing: state is missing")
    require("BeginBriefedSession()" in combat_source, "briefing: input cannot start the trial")
    require("_combatRhythmBridge?.SetPrototypePaused(false);" in session_source, "briefing: Pulse does not resume on confirmation")
    require("DrawBriefingOverlay();" in draw_source, "briefing: overlay is not drawn")
    require("if (IsSessionRunning || IsSessionBriefing)" in draw_source, "briefing: result overlay can cover the briefing")


def main() -> int:
    try:
        input_source = read("src/prototype/PrototypeInput.cs")
        combat_source = read("src/prototype/CombatPrototype.cs")
        bridge_source = read("src/prototype/CombatPrototype.RhythmBridge.cs")
        session_source = read("src/prototype/CombatPrototype.Session.cs")
        draw_source = read("src/prototype/CombatPrototype.Draw.cs")

        action_count = validate_input_contract(input_source, combat_source, bridge_source)
        validate_pause_contract(combat_source, bridge_source)
        validate_reset_contract(combat_source, bridge_source)
        validate_briefing_contract(session_source, combat_source, draw_source)
    except (ContractError, OSError, UnicodeError) as error:
        print(f"ERROR prototype contract: {error}", file=sys.stderr)
        return 1

    print(f"OK   prototype contracts ({action_count} actions, pause, briefing, synchronized reset)")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
