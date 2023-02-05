using UnityEngine;

public class County
{
    public int countyID;
    public GameObject countyCenterGameObject;
    public SelectCounty selectCounty; // We may be able to remove this.
    public string ownerName;
    public string nationName;
    public int population;

    public County(int newCountyID, SelectCounty newSelectCounty, GameObject newCountyCenterGameObject, string newOwnerName, string newNationName, int newPopulation)
    {
        countyID = newCountyID;
        countyCenterGameObject = newCountyCenterGameObject;
        selectCounty = newSelectCounty;
        ownerName = newOwnerName;
        nationName = newNationName;
        population = newPopulation;
    }
}
