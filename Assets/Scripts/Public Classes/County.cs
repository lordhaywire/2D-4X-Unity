using UnityEngine;
public class County
{
    public int countyID; // This isn't used yet, and if it never is then we need to get rid of it.
    public bool isCapital;
    public GameObject countyCenterGameObject;
    public SelectCounty selectCounty; // We may be able to remove this.
    public string province;
    public string factionName;
    public int population;

    public County(int newCountyID, bool newIsCapital, GameObject newCountyCenterGameObject, SelectCounty newSelectCounty,  
        string newProvince, string newFactionName, int newPopulation)
    {
        countyID = newCountyID;
        isCapital = newIsCapital;
        countyCenterGameObject = newCountyCenterGameObject;
        selectCounty = newSelectCounty;
        province = newProvince;
        factionName = newFactionName;
        population = newPopulation;
    }
}
