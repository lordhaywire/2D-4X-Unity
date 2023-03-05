using UnityEngine;
public class County
{
    public int countyID;
    public bool isCapital;
    public GameObject countyCenterGameObject;
    public SelectCounty selectCounty; // We may be able to remove this.
    public string factionName;
    public int population;

    public County(int newCountyID, bool newIsCapital, SelectCounty newSelectCounty, GameObject newCountyCenterGameObject, 
        string newFactionName, int newPopulation)
    {
        countyID = newCountyID;
        isCapital = newIsCapital;
        countyCenterGameObject = newCountyCenterGameObject;
        selectCounty = newSelectCounty;
        factionName = newFactionName;
        population = newPopulation;
    }
}
