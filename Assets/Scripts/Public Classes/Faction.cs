using System;
using UnityEngine;

[Serializable] public class Faction
{
    public string name;
    public Color32 color32;

    public Faction(string newName, Color32 newColor32)
    {
        name = newName;
        color32 = newColor32;
    }
}
