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
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings;
        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildings[i].CurrentWorkersChanged += CheckEnoughUnemployed;
        }
    }

    private void OnDisable()
    {
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings;
        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildings[i].CurrentWorkersChanged -= CheckEnoughUnemployed;
        }
    }

    private void CheckEnoughUnemployed()
    {
        // Check for Population.  This doesn't include the leader.
        unemployed = 0;
        enoughPopulation = false;
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];


        for (int i = 0; i < WorldMapLoad.instance.countyPopulationDictionary[WorldMapLoad.instance.currentlySelectedCounty].Count; i++)
        {
            if (WorldMapLoad.instance.countyPopulationDictionary[WorldMapLoad.instance.currentlySelectedCounty][i].currentActivity
                == AllText.Jobs.IDLE)
            {
                unemployed++;
            }
        }
        //Debug.Log("Unemployed: " + unemployed);
        if (unemployed < possibleBuildings.CurrentWorkers)
        {
            notEnoughWorkersPanel.SetActive(true);
            possibleBuildings.CurrentWorkers = unemployed;
        }
        else if(possibleBuildings.CurrentWorkers != 0)
        {
            enoughPopulation = true;
        } 
    }
}
