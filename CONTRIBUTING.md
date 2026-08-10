# Contributing

**VESPER: SILENT REQUIEM** (pt-BR: **Véspera: Ecos do Silêncio**) is in early pre-production. Contributions should keep the Pulse prototype small, testable and data-driven.

## Workflow

1. Open or choose an issue.
2. Create a focused branch from `main`.
3. Keep gameplay code independent from final assets.
4. Test the project in the Godot .NET editor.
5. Open a pull request describing behavior, validation and visual changes.

## Conventions

- C# classes and public members use PascalCase.
- Private fields use `_camelCase`.
- Prefer typed signals and small components.
- Store configurable gameplay values in Resources rather than hard-coding them.
- Use English for code and commits; design documentation may be in Portuguese.
- Do not commit generated `.godot`, `.mono`, `bin`, `obj` or export folders.

## Commit examples

- `feat: add beat clock prototype`
- `fix: keep dodge timing stable after pause`
- `docs: describe card harmony rules`
- `test: cover cadence rank thresholds`

Original art, music and narrative contributions require explicit approval before inclusion.
