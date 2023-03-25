using System;
using TMPro;
using UnityEngine;

public class UIBuildingDescriptionPanel : MonoBehaviour
{
    public static UIBuildingDescriptionPanel instance;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI moneyCostText;
    [SerializeField] private TextMeshProUGUI resourcesCostText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI currentEmployeesText;
    [SerializeField] private TextMeshProUGUI maxEmployeesText;
    [SerializeField] private TextMeshProUGUI confirmBuildText;

    public event Action CurrentEmployeesChanged;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        TimeKeeper.instance.PauseandUnpause();
        UIPossibleBuildingsPanel.instance.PossibleBuildingButtonPressed += PanelRefresh;
        CurrentEmployeesChanged += CurrentEmployeesRefresh;
    }

    private void OnDisable()
    {
        TimeKeeper.instance.PauseandUnpause();
        UIPossibleBuildingsPanel.instance.PossibleBuildingButtonPressed -= PanelRefresh;
        CurrentEmployeesChanged -= CurrentEmployeesRefresh;
    }

    private void CurrentEmployeesRefresh()
    {
        var possibleBuilding =
            WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber];
        currentEmployeesText.text = possibleBuilding.currentEmployees.ToString();
    }
    private void PanelRefresh()
    {
        var possibleBuilding =
            WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber];
        nameText.text = possibleBuilding.name;
        descriptionText.text = possibleBuilding.description;

        moneyCostText.text = possibleBuilding.influenceCost.ToString();
        //resourcesCostText.text = possibleBuilding. // We aren't using the resources part yet.
        timeText.text = possibleBuilding.daysToBuild.ToString();

        // Reset current employees to 0.
        possibleBuilding.currentEmployees = 0;
        currentEmployeesText.text = possibleBuilding.currentEmployees.ToString();
        maxEmployeesText.text = possibleBuilding.maxEmployees.ToString();

        confirmBuildText.text = $"Are you sure you want to build {possibleBuilding.name}?";

    }

    public void MinusButton()
    {
        WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].currentEmployees--;
        if (WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].currentEmployees < 0)
        {
            WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].currentEmployees = 0;

        }
        CurrentEmployeesChanged();
    }

    public void PlusButton()
    {

        WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].currentEmployees++;

        if (WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].currentEmployees > WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].maxEmployees)
        {

            WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].currentEmployees =
                WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].maxEmployees;
        }
        CurrentEmployeesChanged();
    }

    public void MaxButton()
    {     
        WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].currentEmployees = WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].maxEmployees;
        CurrentEmployeesChanged();
    }
}
