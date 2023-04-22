using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Faction
{
    public event Action InfluenceChanged;
    
    public FactionNameAndColor factionNameAndColor;
    public List<ResearchItem> researchItems;

    [Header("Resources")]
    
    public int influence;
    public int money;
    public int food;
    public int scrap;

    public int Influence
    {
        get { return influence; }
        set 
        {
            influence = value;
            InfluenceChanged?.Invoke();
        }
    }

    // Is this better then saying variable = newVariable
    public Faction(FactionNameAndColor factionNameAndColor, int influence, int money, int food, int scrap)
    {
        this.factionNameAndColor = factionNameAndColor;
        researchItems = new List<ResearchItem>();
        this.Influence = influence;
        this.money = money;
        this.food = food;
        this.scrap = scrap;
    }
}
