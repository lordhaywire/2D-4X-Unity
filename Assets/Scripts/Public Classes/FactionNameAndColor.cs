using System;
using UnityEngine;

[Serializable] public class FactionNameAndColor

{
    public int factionID; // Not currently used....
    public string name;
    public Color32 color32;

    public FactionNameAndColor(int factionID, string name, Color32 color32)
    {
        this.factionID = factionID;
        this.name = name;
        this.color32 = color32;
    }
}
