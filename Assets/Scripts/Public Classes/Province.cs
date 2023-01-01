using UnityEngine;

public class Province
{
    public int ID;
    public GameObject provinceCenterGameObject;
    public string ownerName;
    public string nationName;
    public int population;

    public Province(int newID, GameObject newProvinceCenterGameObject, string newOwnerName, string newNationName, int newPopulation)
    {
        ID = newID;
        provinceCenterGameObject = newProvinceCenterGameObject;
        ownerName = newOwnerName;
        nationName = newNationName;
        population = newPopulation;
    }
}
