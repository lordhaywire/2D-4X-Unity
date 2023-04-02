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
        if(Time.timeScale != 0)
        {
            TimeKeeper.instance.PauseandUnpause();
        }
        
        UIBuildingsPanel.instance.PossibleBuildingButtonPressed += PanelRefresh;
        //Debug.Log("UI Possible Building Number: " + UIBuildingsPanel.instance.PossibleBuildingNumber);
        for(int i = 0; i < WorldMapLoad.instance.possibleBuildings.Count; i++)
        {
            WorldMapLoad.instance.possibleBuildings[i].CurrentWorkersChanged += CurrentEmployeesRefresh;
        }
        
    }

    private void OnDisable()
    {
        TimeKeeper.instance.PauseandUnpause();
        UIBuildingsPanel.instance.PossibleBuildingButtonPressed -= PanelRefresh;
        for (int i = 0; i < WorldMapLoad.instance.possibleBuildings.Count; i++)
        {
            WorldMapLoad.instance.possibleBuildings[i].CurrentWorkersChanged -= CurrentEmployeesRefresh;
        }
    }

    private void CurrentEmployeesRefresh()
    {
        var possibleBuilding =
            WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];
        currentEmployeesText.text = possibleBuilding.CurrentWorkers.ToString();
    }
    private void PanelRefresh()
    {
        var possibleBuilding =
            WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];
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
        WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers--;
        Debug.Log("Current Workers: " + WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers);
        if (WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers < 0)
        {
            WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers = 0;

        }
    }

    public void PlusButton()
    {   
        WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers++;

        if (WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers > WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].maxEmployees)
        {
            WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers =
                WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].maxEmployees;
        }
    }

    public void MaxButton()
    {     
        WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers =
            WorldMapLoad.instance.possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].maxEmployees;
    }
}
