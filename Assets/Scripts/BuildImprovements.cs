using System.Collections;
using UnityEngine;

public class BuildImprovements : MonoBehaviour
{
    [SerializeField] private County county;
    [SerializeField] private CountyInfo countyInfo;
    private void Start()
    {
        StartCoroutine(WaitForOneFrame());
    }

    IEnumerator WaitForOneFrame()
    {
        yield return null;

        countyInfo = GetComponent<CountyInfo>();
        county = countyInfo.county;
    }
        public void BuildBuilding(Faction faction, GameObject building)
    {
        Debug.Log(faction.factionNameAndColor.name + " is building " + building.name);
        BuildingInfo buildingInfo = building.GetComponent<BuildingInfo>();

        int numberOfWorkers;

        // THe Banker removes the cost of building.
        Banker.Instance.ChargeForBuilding(faction, buildingInfo);

        // Diving the total population of the county by 2 (and because it is an int it always rounds down).
        numberOfWorkers = county.population / 2;
        if (numberOfWorkers > buildingInfo.maxWorkers)
        {
            numberOfWorkers = buildingInfo.maxWorkers;
        }

        for (int i = 0; i < numberOfWorkers; i++)
        {
            county.countyPopulation[i].nextActivity = AllText.Jobs.BUILDING;
            county.countyPopulation[i].nextBuilding = building;

        }

        buildingInfo.isBeingBuilt = true;

        // Moves the building to the current building list.
        building.transform.SetParent(countyInfo.currentBuildingsParent);
    }
}
