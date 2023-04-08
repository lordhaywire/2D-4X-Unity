using TMPro;
using UnityEngine;

public class UIPossibleBuildingDescriptionPanel : MonoBehaviour
{
    public static UIPossibleBuildingDescriptionPanel instance;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI moneyCostText;
    [SerializeField] private TextMeshProUGUI resourcesCostText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI currentEmployeesText;
    [SerializeField] private TextMeshProUGUI maxEmployeesText;
    [SerializeField] private TextMeshProUGUI confirmBuildText;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        if (Time.timeScale != 0)
        {
            TimeKeeper.instance.PauseandUnpause();
        }
        
        UIBuildingsPanel.instance.PossibleBuildingButtonPressed += PanelRefresh;
        //Debug.Log("UI Possible Building Number: " + UIBuildingsPanel.instance.PossibleBuildingNumber);
        for(int i = 0; i < WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings.Count; i++)
        {
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[i].CurrentWorkersChanged += CurrentEmployeesRefresh;
        }
    }

    private void OnDisable()
    { 
        if (Time.timeScale != 0)
        {
            TimeKeeper.instance.PauseandUnpause();
        }
        UIBuildingsPanel.instance.PossibleBuildingButtonPressed -= PanelRefresh;
        for (int i = 0; i < WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings.Count; i++)
        {
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[i].CurrentWorkersChanged -= CurrentEmployeesRefresh;
        }
    }

    private void CurrentEmployeesRefresh()
    {
        var possibleBuilding =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];
        currentEmployeesText.text = possibleBuilding.CurrentWorkers.ToString();
    }
    private void PanelRefresh()
    {
        var possibleBuilding =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];
        nameText.text = possibleBuilding.name;
        descriptionText.text = possibleBuilding.description;

        moneyCostText.text = $"{possibleBuilding.influenceCost} Influence";
        //resourcesCostText.text = possibleBuilding. // We aren't using the resources part yet.
        timeText.text = possibleBuilding.daysToBuild.ToString();

        // Reset current employees to 0.
        possibleBuilding.CurrentWorkers = 0;
        currentEmployeesText.text = possibleBuilding.CurrentWorkers.ToString();
        maxEmployeesText.text = possibleBuilding.maxEmployees.ToString();

        confirmBuildText.text = $"Are you sure you want to build {possibleBuilding.name}?";
    }

    public void MinusButton()
    {
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];
        possibleBuildings.CurrentWorkers--;
        Debug.Log("Current Workers: " + possibleBuildings.CurrentWorkers);
        if (possibleBuildings.CurrentWorkers < 0)
        {
            possibleBuildings.CurrentWorkers = 0;

        }
    }

    public void PlusButton()
    {
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];

        possibleBuildings.CurrentWorkers++;

        if (possibleBuildings.CurrentWorkers > possibleBuildings.maxEmployees)
        {
            possibleBuildings.CurrentWorkers =
                possibleBuildings.maxEmployees;
        }
    }

    public void MaxButton()
    {
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];

        possibleBuildings.CurrentWorkers =
            possibleBuildings.maxEmployees;
    }
}
