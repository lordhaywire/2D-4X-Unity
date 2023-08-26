using UnityEngine;

public class BuildImprovements : MonoBehaviour
{
    public void BuildBuilding(Faction faction, Building building)
    {
        Debug.Log(faction.factionNameAndColor.name + " is building " + building.name);

        County county = GetComponent<CountyInfo>().county;
        int numberOfWorkers;

        // Removes the cost of building.
        faction.influence -= building.influenceCost;

        // We need to set this before it is moved because we don't know which currentBuilding it is going to be.
        building.isBeingBuilt = true;

        // Diving the total population of the county by 2 (and because it is an int it always rounds down).
        numberOfWorkers = county.population / 2;
        if (numberOfWorkers > building.maxWorkers)
        {
            numberOfWorkers = building.maxWorkers;
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
