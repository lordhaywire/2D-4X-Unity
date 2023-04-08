using TMPro;
using UnityEngine;

public class UICurrentBuildingDescriptionPanel : MonoBehaviour
{
    public static UICurrentBuildingDescriptionPanel instance;

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
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings;

        if (Time.timeScale != 0)
        {
            TimeKeeper.instance.PauseandUnpause();
        }
        
        UIBuildingsPanel.instance.PossibleBuildingButtonPressed += PanelRefresh;
        //Debug.Log("UI Possible Building Number: " + UIBuildingsPanel.instance.PossibleBuildingNumber);
        for(int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildings[i].CurrentWorkersChanged += CurrentEmployeesRefresh;
        }
        
    }

    private void OnDisable()
    {
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings;

        if (Time.timeScale != 0)
        {
            TimeKeeper.instance.PauseandUnpause();
        }
        
        UIBuildingsPanel.instance.PossibleBuildingButtonPressed -= PanelRefresh;
        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildings[i].CurrentWorkersChanged -= CurrentEmployeesRefresh;
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
        var possibleBuildings =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];
        nameText.text = possibleBuildings.name;
        descriptionText.text = possibleBuildings.description;

        moneyCostText.text = $"{possibleBuildings.influenceCost} Influence";
        //resourcesCostText.text = possibleBuildings. // We aren't using the resources part yet.
        timeText.text = possibleBuildings.daysToBuild.ToString();

        // Reset current employees to 0.
        possibleBuildings.CurrentWorkers = 0;
        currentEmployeesText.text = possibleBuildings.CurrentWorkers.ToString();
        maxEmployeesText.text = possibleBuildings.maxEmployees.ToString();

        confirmBuildText.text = $"Are you sure you want to build {possibleBuildings.name}?";
    }

    public void MinusButton()
    {
        var possibleBuildings =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];

        possibleBuildings.CurrentWorkers--;
        Debug.Log("Current Workers: " + possibleBuildings.CurrentWorkers);
        if (possibleBuildings.CurrentWorkers < 0)
        {
            possibleBuildings.CurrentWorkers = 0;

        }
    }

    public void PlusButton()
    {
        var possibleBuildings =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];

        possibleBuildings.CurrentWorkers++;

        if (possibleBuildings.CurrentWorkers > possibleBuildings.maxEmployees)
        {
            possibleBuildings.CurrentWorkers =
                possibleBuildings.maxEmployees;
        }
    }

    public void MaxButton()
    {
        var possibleBuildings =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];

        possibleBuildings.CurrentWorkers =
            possibleBuildings.maxEmployees;
    }
}
