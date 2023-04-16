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
        TimeKeeper.instance.OnPanelEnable();

        UIPossibleBuildingsPanel.instance.PossibleBuildingButtonPressed += PanelRefresh;

        WorldMapLoad.instance.possibleBuildingDescriptionPanelExpanded = true;

        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings;

        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildings[i].CurrentWorkersChanged += CurrentEmployeesRefresh;
        }
    }

    private void OnDisable()
    {
        TimeKeeper.instance.OnPanelDisable();

        UIPossibleBuildingsPanel.instance.PossibleBuildingButtonPressed -= PanelRefresh;

        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings;

        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildings[i].CurrentWorkersChanged -= CurrentEmployeesRefresh;
        }
    }

    private void CurrentEmployeesRefresh()
    {
        var possibleBuilding =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber];
        currentEmployeesText.text = possibleBuilding.CurrentWorkers.ToString();
    }
    private void PanelRefresh()
    {
        var possibleBuilding =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber];
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
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber];
        possibleBuildings.CurrentWorkers--;
        Debug.Log("Current Workers: " + possibleBuildings.CurrentWorkers);
        if (possibleBuildings.CurrentWorkers < 0)
        {
            possibleBuildings.CurrentWorkers = 0;

        }
    }

    public void PlusButton()
    {
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber];

        possibleBuildings.CurrentWorkers++;

        if (possibleBuildings.CurrentWorkers > possibleBuildings.maxEmployees)
        {
            possibleBuildings.CurrentWorkers =
                possibleBuildings.maxEmployees;
        }
    }

    public void MaxButton()
    {
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber];

        possibleBuildings.CurrentWorkers =
            possibleBuildings.maxEmployees;
    }
}
