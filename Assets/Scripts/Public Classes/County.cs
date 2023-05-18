using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class County
{
    public int countyID; // This isn't used yet, and if it never is then we need to get rid of it.
    public bool isCapital;
    public GameObject countyCenterGameObject;
    public GameObject heroSpawnGameObject;
    public SpriteRenderer spriteRenderer;

    public Faction faction;
    public string province;
    public string biomePrimary;
    public string biomeSecondary;
    public string biomeTertiary;
    public List<PossibleBuilding> possibleBuildings;
    public List<CurrentBuilding> currentBuildings;
    public int currentlyWorkingPopulation; // We should put this number on the county info panel.
    public int population;

    public County(int countyID, bool isCapital, GameObject countyCenterGameObject, GameObject heroSpawnGameObject,
        SpriteRenderer spriteRenderer,  Faction faction, 
        string province, string biomePrimary, string biomeSecondary, string biomeTertiary,
         int currentlyWorkingPopulation, int population)
    {
        this.countyID = countyID;
        this.isCapital = isCapital;
        this.countyCenterGameObject = countyCenterGameObject;
        this.heroSpawnGameObject = heroSpawnGameObject;
        this.spriteRenderer = spriteRenderer;
        //this.selectCounty = selectCounty;
        this.faction = faction;
        this.province = province;
        this.biomePrimary = biomePrimary;
        this.biomeSecondary = biomeSecondary;
        this.biomeTertiary = biomeTertiary;
        possibleBuildings = new List<PossibleBuilding>(); // This initializes the list. It is not in the constructor. 
        currentBuildings = new List<CurrentBuilding>(); // This initializes the list. It is not in the constructor. 
        this.currentlyWorkingPopulation = currentlyWorkingPopulation;
        this.population = population;
    }
}
