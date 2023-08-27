using UnityEngine;

public class BuildImprovements : MonoBehaviour
{
    public void CheckIfBuildingAlreadyBuilt(Faction faction, GameObject building)
    {

    }
    public void BuildBuilding(Faction faction, GameObject building)
    {
        Debug.Log(faction.factionNameAndColor.name + " is building " + building.name);
        BuildingInfo buildingInfo = building.GetComponent<BuildingInfo>();

        County county = GetComponent<CountyInfo>().county;
        int numberOfWorkers;

        // Removes the cost of building.
        faction.influence -= buildingInfo.influenceCost;

        // We need to set this before it is moved because we don't know which currentBuilding it is going to be.
        buildingInfo.isBeingBuilt = true;

        // Diving the total population of the county by 2 (and because it is an int it always rounds down).
        numberOfWorkers = county.population / 2;
        if (numberOfWorkers > buildingInfo.maxWorkers)
        {
            numberOfWorkers = buildingInfo.maxWorkers;
        }

        for (int i = 0; i < numberOfWorkers; i++)
        {
            // This will make it so the leader is not used for building shit.
            if (county.countyPopulation[i].isFactionLeader == false)
            {
                county.countyPopulation[i].nextActivity = AllText.Jobs.BUILDING;
                county.countyPopulation[i].nextBuilding = building;
            }
        }
        // Moves the building to the current building list.
        county.currentBuildings.Add(building);
        county.possibleBuildings.Remove(building);
    }
}
