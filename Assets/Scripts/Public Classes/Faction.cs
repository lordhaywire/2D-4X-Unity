using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Faction
{
    public event Action InfluenceChanged;

    public bool isPlayer;
    public FactionNameAndColor factionNameAndColor;
    public List<ResearchItem> researchItems;
    public CountyPopulation factionLeader;

    [Header("Resources")]
    
    [SerializeField] private int influence;
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

    public Faction(bool isPlayer, FactionNameAndColor factionNameAndColor, CountyPopulation factionLeader, 
        int influence, int money, int food, int scrap)
    {
        this.isPlayer = isPlayer;
        this.factionNameAndColor = factionNameAndColor;
        researchItems = new List<ResearchItem>();
        this.factionLeader = factionLeader;

        Influence = influence;
        this.money = money;
        this.food = food;
        this.scrap = scrap;
    }
}
