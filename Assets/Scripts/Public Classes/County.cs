using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class County
{
    public int countyID; // This isn't used yet, and if it never is then we need to get rid of it.
    public GameObject gameObject; // This is the game object that is the county.
    public bool isCapital;
    public bool isIndependentCapital;
    public SpriteRenderer spriteRenderer;
    public BuildImprovements buildImprovements;
    public Faction faction;
    public string province;
    public string biomePrimary;
    public string biomeSecondary;
    public string biomeTertiary;
    public List<CountyPopulation> countyPopulation;
    //public List<GameObject> possibleBuildings;
    //public List<GameObject> currentBuildings;

    public int spawnedHeroCount; // Count of all the heroes in the county.
    public int currentlyWorkingPopulation; // We should put this number on the county info panel.
    public int population;

    public County(int countyID, GameObject gameObject, bool isCapital, bool isIndependentCaptial,  
        SpriteRenderer spriteRenderer, BuildImprovements buildImprovements, Faction faction, 
        string province, string biomePrimary, string biomeSecondary, string biomeTertiary,
        int spawnedHeroCount, int currentlyWorkingPopulation, int population)
    {
        this.countyID = countyID;
        this.gameObject = gameObject;
        this.isCapital = isCapital;
        this.isIndependentCapital = isIndependentCaptial;

        this.spriteRenderer = spriteRenderer;
        this.buildImprovements = buildImprovements;
        this.faction = faction;
        this.province = province;
        this.biomePrimary = biomePrimary;
        this.biomeSecondary = biomeSecondary;
        this.biomeTertiary = biomeTertiary;
        //possibleBuildings = new List<GameObject>(); // This initializes the list. It is not in the constructor. 
        //currentBuildings = new List<GameObject>(); // This initializes the list. It is not in the constructor. 
        this.spawnedHeroCount = spawnedHeroCount;
        this.currentlyWorkingPopulation = currentlyWorkingPopulation;
        this.population = population;
    }
}
