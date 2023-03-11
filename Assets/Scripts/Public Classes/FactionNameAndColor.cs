using System;
using UnityEngine;

[Serializable] public class FactionNameAndColor

{
    public string name;
    public Color32 color32;

    public FactionNameAndColor(string newName, Color32 newColor32)
    {
        name = newName;
        color32 = newColor32;
    }
}
