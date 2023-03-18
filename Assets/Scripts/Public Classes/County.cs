using System;
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
    public int population;

    public County(int newCountyID, bool newIsCapital, GameObject newCountyCenterGameObject, 
        SpriteRenderer newSpriteRendered, SelectCounty newSelectCounty, FactionNameAndColor newFaction, 
        string newProvince, string newBiomePrimary, string newBiomeSecondary, string newBiomeTertiary,
        int newPopulation)
    {
        countyID = newCountyID;
        isCapital = newIsCapital;
        countyCenterGameObject = newCountyCenterGameObject;
        spriteRenderer = newSpriteRendered;
        selectCounty = newSelectCounty;
        faction = newFaction;
        province = newProvince;
        biomePrimary = newBiomePrimary;
        biomeSecondary = newBiomeSecondary;
        biomeTertiary = newBiomeTertiary;
        population = newPopulation;
    }
}
