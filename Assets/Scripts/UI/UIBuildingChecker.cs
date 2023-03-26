using UnityEngine;

public class UIBuildingChecker : MonoBehaviour
{
    [SerializeField] private GameObject notEnoughEmployeesPanel;

    private void OnEnable()
    {
        for (int i = 0; i < WorldMapLoad.instance.possibleBuildings.Count; i++)
        {
            WorldMapLoad.instance.possibleBuildings[i].CurrentWorkersChanged += CheckEnoughPopulation;
        }

    }

    private void OnDisable()
    {
        for (int i = 0; i < WorldMapLoad.instance.possibleBuildings.Count; i++)
        {
            WorldMapLoad.instance.possibleBuildings[i].CurrentWorkersChanged -= CheckEnoughPopulation;
        }
    }

    private void CheckEnoughPopulation()
    {
        int unemployed = 0;
        for (int i = 0; i < WorldMapLoad.instance.countyPopulationDictionary[WorldMapLoad.instance.currentlySelectedCounty].Count; i++)
        {
            if (WorldMapLoad.instance.countyPopulationDictionary[WorldMapLoad.instance.currentlySelectedCounty][i].currentActivity
                == AllText.Jobs.IDLE)
            {
                unemployed++;
            }
        }
        // This is dumb.  It needs to be fixed.  If there is nothing in the If part then why even have it?
        //Debug.Log("Unemployed: " + unemployed);
        if(unemployed < WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers)
        {
            notEnoughEmployeesPanel.SetActive(true);
            WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers = unemployed;
        }
        else
        {
            Debug.Log("Everything is beautiful in CheckEnoughPopulation()");
        }
    }
}
