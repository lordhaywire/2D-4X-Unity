using UnityEngine;
public class County
{
    public int countyID; // This isn't used yet, and if it never is then we need to get rid of it.
    public bool isCapital;
    public GameObject countyCenterGameObject;
    public Color color32;
    public SelectCounty selectCounty; // We may be able to remove this.
    public Faction faction;
    public string province;
    public int population;

    public County(int newCountyID, bool newIsCapital, GameObject newCountyCenterGameObject, Color newColor32,
        SelectCounty newSelectCounty, Faction newFaction, string newProvince, int newPopulation)
    {
        countyID = newCountyID;
        isCapital = newIsCapital;
        countyCenterGameObject = newCountyCenterGameObject;
        color32 = newColor32;
        selectCounty = newSelectCounty;
        faction = newFaction;
        province = newProvince;
        population = newPopulation;
    }
}
