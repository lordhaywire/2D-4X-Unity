using System;
using UnityEngine;

[Serializable]
public class Faction
{
    public FactionNameAndColor factionNameAndColor;

    [Header("Resources")]
    
    public int influence;
    public int money;
    public int food;
    public int scrap;

    // Is this better then saying variable = newVariable
    public Faction(FactionNameAndColor factionNameAndColor, int influence, int money, int food, int scrap)
    {
        this.factionNameAndColor = factionNameAndColor;
        this.influence = influence;
        this.money = money;
        this.food = food;
        this.scrap = scrap;
    }
}
