using System.Collections.Generic;
using UnityEngine;

public class FactionAI : MonoBehaviour
{
    public Faction faction;
    public List<County> countiesFactionOwns;

    private void Start()
    {
        TimeKeeper.Instance.FirstRun += CheckForBuildingBuildings;
        TimeKeeper.Instance.DayStart += CheckForBuildingBuildings;
    }

    private void CheckForBuildingBuildings()
    {
        //Debug.Log(faction.factionNameAndColor.name + " checking to build buildings!");

        // Is there enough food? If not build a food building.
        if (Banker.Instance.CheckEnoughFood(faction) == false)
        {
            GameObject foodBuilding = Banker.Instance.FindFoodBuilding(gameObject);
            if (foodBuilding != null)
            {
                //Debug.Log("Building to be built: " + foodBuilding.name);
                foodBuilding.GetComponent<BuildingInfo>().county.buildImprovements.BuildBuilding(faction, foodBuilding);
            }
            else
            {
                Debug.Log("Food building is null");
            }
            

            {
                //Debug.Log($"{faction.factionNameAndColor.name} doesn't have enough influence to build " +
                //   $"{counties[i].possibleBuildings[2].name} or some shit.");
            }


        }
        else
        {
            Debug.Log(faction.factionNameAndColor.name + " has enough food.");
        }

    }
}
