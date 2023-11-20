# Tree Mesh to Terrain

## Overview
"Tree Mesh to Terrain" is a Unity Editor extension designed to facilitate the process of adding tree meshes to Unity terrains. This tool allows Unity developers to easily paint tree meshes onto a terrain, streamlining the process of decorating landscapes in Unity scenes.

## Features
- **Drag and Drop Interface**: Easily add tree meshes to the list using a simple drag-and-drop GUI in the Unity Editor.
- **Automatic Terrain Detection**: The tool detects the underlying terrain for each tree mesh and paints the trees onto the terrain accordingly.
- **Easy to Use**: With a user-friendly interface, adding trees to your terrain is just a few clicks away.

## Installation
1. Copy the `TreeMeshToTerrain.cs` script into your Unity project's `Editor` folder.
2. Ensure that all required namespaces (`UnityEngine`, `UnityEditor`, etc.) are included in your project.

## How to Use
1. Open the tool window by navigating to `Tools > Tree Mesh to Terrain` in the Unity Editor menu.
2. Drag and drop your tree mesh GameObjects into the designated area in the opened window.
3. Click the "Paint Trees on Terrain" button to automatically paint the added tree meshes onto the detected terrain.

## Requirements
- Unity Editor (This tool is designed for use within the Unity Editor and is not intended for runtime use in games.)
- Tree Mesh GameObjects (The GameObjects intended to be painted onto the terrain.)

## Support
For support, questions, or contributions, please open an issue or a pull request in the repository.

---

Note: This tool is designed for use within the Unity Editor. It is not intended for runtime use in games.
