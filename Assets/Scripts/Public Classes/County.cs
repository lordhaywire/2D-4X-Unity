using UnityEngine;

public class County
{
    public int countyID;
    public bool isCapital;
    public GameObject countyCenterGameObject;
    public SelectCounty selectCounty; // We may be able to remove this.
    public string ownerName;
    public string nationName;
    public int population;

    public County(int newCountyID, bool newIsCapital, SelectCounty newSelectCounty, GameObject newCountyCenterGameObject, 
        string newOwnerName, string newNationName, int newPopulation)
    {
        countyID = newCountyID;
        isCapital = newIsCapital;
        countyCenterGameObject = newCountyCenterGameObject;
        selectCounty = newSelectCounty;
        ownerName = newOwnerName;
        nationName = newNationName;
        population = newPopulation;
    }
}
