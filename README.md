# Unity Essentials

This module is part of the Unity Essentials ecosystem and follows the same lightweight, editor-first approach.
Unity Essentials is a lightweight, modular set of editor utilities and helpers that streamline Unity development. It focuses on clean, dependency-free tools that work well together.

All utilities are under the `UnityEssentials` namespace.

```csharp
using UnityEssentials;
```

## Installation

Install the Unity Essentials entry package via Unity's Package Manager, then install modules from the Tools menu.

- Add the entry package (via Git URL)
    - Window → Package Manager
    - "+" → "Add package from git URL…"
    - Paste: `https://github.com/CanTalat-Yakan/UnityEssentials.git`

- Install or update Unity Essentials packages
    - Tools → Install & Update UnityEssentials
    - Install all or select individual modules; run again anytime to update

---

# Read Only Attribute

> Quick overview: Display any serialized field as a non-editable value in the Inspector. Renders a label-only view of the field’s current value with word wrapping, ideal for diagnostics and derived data.

Show values in the Inspector without allowing edits. Apply `[ReadOnly]` to a serialized field to render its value as text. Great for IDs, computed summaries, or debug info you don’t want to change from the editor.

![screenshot](Documentation/Screenshot.png)

## Features
- Non-editable rendering for any serialized field (primitives, enums, vectors, colors, UnityEngine.Object, etc.)
- Word-wrapped value text for long strings
- Preserves the field’s display name and indentation
- Editor-only; no runtime overhead

## Requirements
- Unity Editor 6000.0+ (Editor-only; attribute lives in Runtime for convenience)
- Depends on the Unity Essentials Inspector Hooks module (used for property value resolution and handling)

Tip: If the value label doesn’t appear, ensure the Inspector Hooks package is installed and active.

## Usage
Basic

```csharp
using UnityEngine;
using UnityEssentials;

public class Example : MonoBehaviour
{
    [ReadOnly] public int id = 42;
    [ReadOnly] public string version = "1.0.0";
}
```

Vectors and objects

```csharp
public class DebugInfo : MonoBehaviour
{
    [ReadOnly] public Vector3 lastPosition;
    [ReadOnly] public Transform target;
}
```

Dynamic text (updated from code)

```csharp
public class Status : MonoBehaviour
{
    [ReadOnly, TextArea] public string summary;

    void Update()
    {
        summary = $"FPS: {1f / Time.deltaTime:F1}\nActive: {enabled}";
    }
}
```

## How It Works
- A custom drawer replaces the default field UI with two labels: the field label and the value text
- The value is obtained via the Inspector Hooks utility (`GetPropertyValue`) and converted with `ToString()`
- The property is marked as handled so Unity doesn’t draw the default editable control
- Height is calculated from the wrapped text to avoid clipping

## Notes and Limitations
- Read-only by design: the field cannot be edited in the Inspector while `[ReadOnly]` is applied
- Complex types (structs, arrays, lists) are shown via their `ToString()` result; nested children are not drawn
- UnityEngine.Object values are displayed as text (e.g., `MyPrefab (GameObject)`); object ping/select controls are not shown
- Drawer conflicts: Unity applies one drawer per field; combining with other drawers isn’t supported
- Multi-object editing: the value label reflects each inspected target independently

## Files in This Package
- `Runtime/ReadOnlyAttribute.cs` – `[ReadOnly]` attribute marker
- `Editor/ReadOnlyDrawer.cs` – PropertyDrawer (label-only rendering, word wrap, value resolution)
- `Runtime/UnityEssentials.ReadOnlyAttribute.asmdef` – Runtime assembly definition
- `Editor/UnityEssentials.ReadOnlyAttribute.Editor.asmdef` – Editor assembly definition

## Tags
unity, unity-editor, attribute, propertydrawer, readonly, label, inspector, ui, tools, workflow
