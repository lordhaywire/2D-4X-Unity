using UnityEngine;

public class UIBuildingChecker : MonoBehaviour
{
    [SerializeField] private GameObject notEnoughWorkersPanel;

    public static UIBuildingChecker Instance;

    public bool enoughPopulation;
    public int unemployed;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        var possibleBuildings = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty].possibleBuildings;
        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildings[i].CurrentWorkersChanged += CheckEnoughUnemployed;
        }
    }

    private void CheckEnoughUnemployed()
    {
        // Check for Population.
        unemployed = 0;
        enoughPopulation = false;
        var possibleBuildings = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber];


        for (int i = 0; i < WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.CurrentlySelectedCounty].Count; i++)
        {
            if (WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.CurrentlySelectedCounty][i].nextActivity
                == AllText.Jobs.IDLE)
            {
                unemployed++; // Can we get rid of the incrementing?
                //Debug.Log("Unemployed: " + unemployed);
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

    private void OnDisable()
    {
        var possibleBuildings = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty].possibleBuildings;
        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildings[i].CurrentWorkersChanged -= CheckEnoughUnemployed;
        }
    }
}
