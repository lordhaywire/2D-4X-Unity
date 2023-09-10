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

        // The Banker removes the cost of building.
        Banker.Instance.ChargeForBuilding(faction, buildingInfo);
        numberOfWorkers = buildingInfo.CurrentWorkers;
        //buildingInfo.uIGameObject.GetComponent<UIBuildingButton>().underConstructionGameObject.SetActive(true);


        // Sets the next day jobs for each workers who isn't already working.
        for (int i = 0; i < numberOfWorkers; i++)
        {
            if (county.countyPopulation[i].nextBuilding == null)
            {
                county.countyPopulation[i].nextActivity = AllText.Jobs.BUILDING;
                county.countyPopulation[i].nextBuilding = building;

            }
            else
            {
                numberOfWorkers++;
            }
        }

        Banker.Instance.CountIdleWorkers(buildingInfo.county);
        buildingInfo.isBeingBuilt = true;

        // Moves the building to the current building list.
        building.transform.SetParent(countyInfo.currentBuildingsParent);
    }
}
