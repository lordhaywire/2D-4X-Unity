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
                BuildingInfo buildingInfo = foodBuilding.GetComponent<BuildingInfo>();
                // Diving the total population of the county by 2 (and because it is an int it always rounds down).
                int numberOfWorkers = buildingInfo.county.population / 2;
                if (numberOfWorkers > buildingInfo.maxWorkers)
                {
                    numberOfWorkers = buildingInfo.maxWorkers;
                }
                buildingInfo.CurrentWorkers = numberOfWorkers;
                //Debug.Log("Building to be built: " + foodBuilding.name);
                if (Banker.Instance.CheckBuildingCost(faction, buildingInfo) == true
                && Banker.Instance.CheckEnoughIdleWorkers(buildingInfo) == true
                && Banker.Instance.CheckForWorkersAssigned(buildingInfo) == true)
                {
                    buildingInfo.county.buildImprovements.BuildBuilding(faction, foodBuilding);
                }
                else
                {
                    Debug.Log($"{faction.factionNameAndColor.name} has failed one of its resource checks.");
                    buildingInfo.CurrentWorkers = 0;
                }
            }
            else
            {
                Debug.Log("Food building is null");
            }
        }
        else
        {
            Debug.Log(faction.factionNameAndColor.name + " has enough food.");
        }

    }
}
