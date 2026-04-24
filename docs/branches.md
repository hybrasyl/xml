# Active Branch Overview

Snapshot of the topic branches produced by splitting the original `feature/element-expansion` work, plus two follow-up refactors. All six branch off `main` and merge into `main` cleanly in any order (no conflicts, 3-way merge handles overlapping files).

## feature/map-dynamic-lighting

Adds a `DynamicLighting` boolean attribute (default `false`) to `Map`, signaling whether a map supports dynamic lighting on the client.

- [src/Objects/Map.cs](../src/Objects/Map.cs)
- [src/XSD/Map.xsd](../src/XSD/Map.xsd)

## refactor/castable-uselevel-rename

Renames `Castable.CastableLevel` → `UseLevel`. The old name was ambiguous with a castable's own level; `UseLevel` more clearly names the player's trained rank with that castable.

- [src/Extensions/Castable.cs](../src/Extensions/Castable.cs)

## feature/npc-role-schema-expansion

Extends the NPC role XSD to support per-nation cost adjustments and cookie-gated role access.

- New `NpcRoleCostAdjustment` shared type (float value + `Nation` attr)
- `ExceptCookie` and `OnlyCookie` attributes on all five role types (`Bank`, `Post`, `Repair`, `Vend`, `Train`)
- `CostAdjustment` child element on all five role types
- `Nation` attribute on `NpcRolePost`
- `NpcRolePost.Surcharge` `minOccurs` relaxed from `1` to `0`

Files: [src/XSD/Common.xsd](../src/XSD/Common.xsd)

## feature/element-expansion

End-to-end overhaul of the element system: expanded enum, corrected defaults, explicit random-set resolution, and a variable-size element table.

**Enum & schema ([src/Objects/ElementType.cs](../src/Objects/ElementType.cs), [src/XSD/Common.xsd](../src/XSD/Common.xsd)):**
- `None` → `Force` (new unaspected/default sentinel)
- `Wood` → `Nature`
- Added combat elements: `Flesh, Spirit, Nature, Time, Stasis, Life, Arcane, Void` — 17 total
- Added meta-resolution types: `RandomClassic`, `RandomAll`

**Default propagation:**
- XSD attributes that defaulted to `"None"` now default to `"Force"` — [Common.xsd](../src/XSD/Common.xsd), [ElementTable.xsd](../src/XSD/ElementTable.xsd)
- List-typed `Elements` attributes in [Spawns.xsd](../src/XSD/Spawns.xsd) and [Castable.xsd](../src/XSD/Castable.xsd) drop their default (a scalar default was never a valid list member)
- C# consumers ([StatModifiers.cs](../src/Objects/StatModifiers.cs), [Item.cs](../src/Extensions/Item.cs), [Variant.cs](../src/Extensions/Variant.cs), [Spawn.cs](../src/Extensions/Spawn.cs), [Castable.cs](../src/Extensions/Castable.cs)) updated `ElementType.None` → `ElementType.Force`

**Random meta-element resolution** — new [src/Extensions/ElementType.cs](../src/Extensions/ElementType.cs) with `ElementTypeExtensions.Resolve()`; replaces prior ordinal-range logic (which broke with the reordered enum) with explicit whitelists:
- `RandomTemuair` → `{Fire, Water, Wind, Earth}`
- `RandomClassic` → `{Fire, Water, Wind, Earth, Light, Dark}`
- `RandomExpanded` → `{Arcane, Void, Life, Metal}`
- `RandomAll` → all 16 combat elements except `Undead`

Callers (`Spawn.OffensiveElement/DefensiveElement`, `Castable.Element`) now invoke `.Resolve()`. Also fixes a pre-existing copy-paste bug where `Spawn.DefensiveElement` was reading `_damage.Elements` inside the `_defense.Elements` switch.

**ElementTable shape ([src/XSD/ElementTable.xsd](../src/XSD/ElementTable.xsd)):** `Source`/`Target` changed from hardcoded 9×9 to variable-size (`minOccurs="1" maxOccurs="unbounded"`). With 17 elements, a fixed 9×9 matrix could no longer represent the full interaction table; tables now declare only the pairs they cover.

## refactor/xsd-self-resolving-includes

IDE tooling cleanup only — no runtime change. Each leaf XSD references shared types (`hyb:String8`, `hyb:ElementType`, etc.) defined in [Common.xsd](../src/XSD/Common.xsd). The umbrella [Hybrasyl.xsd](../src/XSD/Hybrasyl.xsd) stitches everything together via `xs:include`, but IDEs validating a leaf in isolation can't resolve those references and flag spurious `src-resolve` errors.

Adds `<xs:include schemaLocation="Common.xsd" />` to every leaf XSD, plus `<xs:include schemaLocation="Spawns.xsd" />` to [Map.xsd](../src/XSD/Map.xsd) (which references `hyb:SpawnGroup`). Duplicate includes that arise via the umbrella schema are deduplicated by XSD processors on `schemaLocation`.

Touches: Castable, Creature, ElementTable, Item, Localization, Loot, Map, Nation, Recipe, ServerConfig, Spawns, Status XSDs.

## refactor/test-worlddir-env-var

Makes the test world-data directory configurable via environment variable. The path was previously hardcoded in [tests/xmltest-settings.json](../tests/xmltest-settings.json) to `c:\HybrasylWorld\ceridwen\xml`, causing `XmlManagerFixture` to throw `FileNotFoundException` — and with it drop ~45 fixture-dependent tests — on any machine where the data lives elsewhere.

[tests/Settings.cs](../tests/Settings.cs) now checks `HYBRASYL_TEST_WORLD_DIR` first and falls back to the JSON value when unset. CI and existing setups keep working without changes; local developers can point at their own checkout.

## Merge guidance

Four pairs touch overlapping files:

| Pair | Shared file(s) |
| --- | --- |
| `castable-uselevel-rename` × `element-expansion` | `src/Extensions/Castable.cs` |
| `npc-role-schema-expansion` × `element-expansion` | `src/XSD/Common.xsd` |
| `map-dynamic-lighting` × `xsd-self-resolving-includes` | `src/XSD/Map.xsd` |
| `element-expansion` × `xsd-self-resolving-includes` | `src/XSD/Castable.xsd`, `ElementTable.xsd`, `Spawns.xsd` |

All merge cleanly — the edits sit in non-overlapping regions of each shared file. A sequential merge of all six into `main` was verified to produce a clean build.

Safety tag `backup/element-expansion-pre-split` preserves the original branch tip before the split.
