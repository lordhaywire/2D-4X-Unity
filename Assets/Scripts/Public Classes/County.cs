using UnityEngine;

public class County
{
    public int countyID;
    public GameObject countyCenterGameObject;
    public string ownerName;
    public string nationName;
    public int population;

    public County(int newCountyID, GameObject newCountyCenterGameObject, string newOwnerName, string newNationName, int newPopulation)
    {
        countyID = newCountyID;
        countyCenterGameObject = newCountyCenterGameObject;
        ownerName = newOwnerName;
        nationName = newNationName;
        population = newPopulation;
    }
}
