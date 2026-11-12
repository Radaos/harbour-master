# Harbour Master

Harbour Master is a Valheim BepInEx mod focused on harbour planning, navigation surveying, channel design, and future dredging operations.

## Current Status

Early prototype (v0.1).

Current functionality:

- Loads as a BepInEx plugin.
- Supports configurable activation key.
- Supports navigation profiles.
- Provides terrain targeting through raycasting.
- Includes harbour survey groundwork.
- Designed for future channel and basin planning.
- Designed for future terrain modification support.

## Project Goals

Harbour Master aims to provide tools for:

- Surveying water depth.
- Planning safe navigation channels.
- Designing turning basins.
- Supporting vanilla and future vessels.
- Supporting mod-added vessels through profiles.
- Future dredging and harbour construction workflows.

## Current Controls

### Default Controls

| Key | Function |
|------|----------|
| F8 | Toggle Harbour Master |
| Page Up | Next navigation profile |
| Page Down | Previous navigation profile |
| Ctrl + Mouse Wheel | Adjust survey radius |
| Esc | Exit Harbour Master |

When Harbour Master is disabled the mod remains inactive.

## Navigation Profiles

Profiles currently included:

### Longship

- Safe Depth: 4.5m
- Channel Width: 8m
- Turning Radius: 15m

### Drakkar

- Safe Depth: 7m
- Channel Width: 16m
- Turning Radius: 25m

Future vessel profiles can be added without changing the planning architecture.

## Project Structure

```
HarbourMaster/
│
├── HarbourMasterPlugin.cs
├── HarbourMasterTool.cs
│
├── Models/
├── Profiles/
├── Services/
├── Selection/
└── Rendering/
```

## Build Requirements

- Visual Studio 2022
- .NET Framework 4.7.2
- BepInEx
- Harmony
- Valheim assembly references

Required references typically include:

- assembly_valheim.dll
- UnityEngine.CoreModule.dll
- UnityEngine.InputLegacyModule.dll
- UnityEngine.PhysicsModule.dll

## Installation

Copy the compiled DLL to:

```
BepInEx/plugins/HarbourMaster/
```

Then start Valheim.

## Planned Features

### Survey Mode

Display:

- Current water depth
- Required navigation depth
- Required dredging depth

### Channel Planning

Generate:

- Navigation channels
- Entrance channels
- Marker routes

### Basin Planning

Generate:

- Turning basins
- Anchorage zones
- Docking approaches

### Terrain Editing

Future versions are intended to integrate directly with Valheim terrain systems using TerrainComp, Heightmap and ownership-aware terrain updates.

## License

Private development project.
