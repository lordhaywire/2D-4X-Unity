using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class County
{
    public int countyID; // This isn't used yet, and if it never is then we need to get rid of it.
    public bool isCapital;
    public GameObject countyCenterGameObject;
    public SpriteRenderer spriteRenderer;
    public SelectCounty selectCounty; // We may be able to remove this.
    public FactionNameAndColor faction;
    public string province;
    public string biomePrimary;
    public string biomeSecondary;
    public string biomeTertiary;
    public List<CurrentBuilding> buildings;
    public int currentlyWorkingPopulation; // We could put this number on the county info panel.
    public int population;

    public County(int countyID, bool isCapital, GameObject countyCenterGameObject, 
        SpriteRenderer spriteRendered, SelectCounty selectCounty, FactionNameAndColor faction, 
        string province, string biomePrimary, string biomeSecondary, string biomeTertiary,
        List<CurrentBuilding> buildings, int currentlyWorkingPopulation, int population)
    {
        this.countyID = countyID;
        this.isCapital = isCapital;
        this.countyCenterGameObject = countyCenterGameObject;
        this.spriteRenderer = spriteRendered;
        this.selectCounty = selectCounty;
        this.faction = faction;
        this.province = province;
        this.biomePrimary = biomePrimary;
        this.biomeSecondary = biomeSecondary;
        this.biomeTertiary = biomeTertiary;
        this.buildings = buildings;
        this.currentlyWorkingPopulation = currentlyWorkingPopulation;
        this.population = population;
    }
}
