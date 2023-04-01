using UnityEngine;

public class UIBuildingChecker : MonoBehaviour
{
    [SerializeField] private GameObject notEnoughWorkersPanel;

    public static UIBuildingChecker instance;

    public bool enoughPopulation;
    public int unemployed;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        for (int i = 0; i < WorldMapLoad.instance.possibleBuildings.Count; i++)
        {
            WorldMapLoad.instance.possibleBuildings[i].CurrentWorkersChanged += CheckEnoughUnemployed;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < WorldMapLoad.instance.possibleBuildings.Count; i++)
        {
            WorldMapLoad.instance.possibleBuildings[i].CurrentWorkersChanged -= CheckEnoughUnemployed;
        }
    }

    private void CheckEnoughUnemployed()
    {
        // Check for Population.  This doesn't include the leader.
        unemployed = 0;
        enoughPopulation = false;
        

        for (int i = 0; i < WorldMapLoad.instance.countyPopulationDictionary[WorldMapLoad.instance.currentlySelectedCounty].Count; i++)
        {
            if (WorldMapLoad.instance.countyPopulationDictionary[WorldMapLoad.instance.currentlySelectedCounty][i].currentActivity
                == AllText.Jobs.IDLE)
            {
                unemployed++;
            }
        }
        //Debug.Log("Unemployed: " + unemployed);
        if (unemployed < WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers)
        {
            notEnoughWorkersPanel.SetActive(true);
            WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers = unemployed;
        }
        else if(WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers != 0)
        {
            enoughPopulation = true;
        } 
    }
}
