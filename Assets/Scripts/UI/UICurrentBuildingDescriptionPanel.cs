using TMPro;
using UnityEngine;

public class UICurrentBuildingDescriptionPanel : MonoBehaviour
{
    public static UICurrentBuildingDescriptionPanel instance;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI resourcesCostText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI currentWorkersText;
    [SerializeField] private TextMeshProUGUI maxWorkersText;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        TimeKeeper.instance.OnPanelEnable();

        WorldMapLoad.instance.currentBuildingDescriptionPanelExpanded = true;

        UICurrentBuildingsPanel.instance.CurrentBuildingButtonPressed += PanelRefresh;
        /*
        var currentBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].currentBuildings;
        for(int i = 0; i < currentBuildings.Count; i++)
        {
            currentBuildings[i].CurrentWorkersChanged += CurrentEmployeesRefresh;
        }
        */
    }

    private void OnDisable()
    {
        TimeKeeper.instance.OnPanelDisable();

        UICurrentBuildingsPanel.instance.CurrentBuildingButtonPressed -= PanelRefresh;
        /*
        var currentBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].currentBuildings;
        for (int i = 0; i < currentBuildings.Count; i++)
        {
            currentBuildings[i].CurrentWorkersChanged -= CurrentEmployeesRefresh;
        }
        */
    }

    /*
    private void CurrentEmployeesRefresh()
    {
        var currentBuilding =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].currentBuildings[UICurrentBuildingsPanel.instance.CurrentBuildingNumber];
        currentWorkersText.text = currentBuilding.currentWorkers.ToString();
    }*/
    private void PanelRefresh()
    { 
        var currentBuildings =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].currentBuildings[UICurrentBuildingsPanel.instance.CurrentBuildingNumber];
        nameText.text = currentBuildings.name;
        descriptionText.text = currentBuildings.description;
        timeText.text = currentBuildings.daysToBuild.ToString();
        currentWorkersText.text = currentBuildings.currentWorkers.ToString();
        maxWorkersText.text = currentBuildings.maxWorkers.ToString();

    }

    /*
    public void MinusButton()
    {
        var currentBuildings =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].currentBuildings[UICurrentBuildingsPanel.instance.PossibleBuildingNumber];

        currentBuildings.CurrentWorkers--;
        Debug.Log("Current Workers: " + currentBuildings.CurrentWorkers);
        if (currentBuildings.CurrentWorkers < 0)
        {
            currentBuildings.CurrentWorkers = 0;

        }
    }
    */

    /*
    public void PlusButton()
    {
        var currentBuildings =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].currentBuildings[UICurrentBuildingsPanel.instance.PossibleBuildingNumber];

        currentBuildings.CurrentWorkers++;

        if (currentBuildings.CurrentWorkers > currentBuildings.maxWorkers)
        {
            currentBuildings.CurrentWorkers =
                currentBuildings.maxWorkers;
        }
    }
    */
    /*
    public void MaxButton()
    {
        var currentBuildings =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].currentBuildings[UICurrentBuildingsPanel.instance.PossibleBuildingNumber];

        currentBuildings.CurrentWorkers =
            currentBuildings.maxWorkers;
    }
    */
}
