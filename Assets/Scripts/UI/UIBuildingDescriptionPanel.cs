using TMPro;
using UnityEngine;

public class UIBuildingDescriptionPanel : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI moneyCostText;
    [SerializeField] private TextMeshProUGUI resourcesCostText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI employeesText;

    [SerializeField] private TextMeshProUGUI confirmBuildText;


    private void OnEnable()
    {
        UIPossibleBuildingsPanel.instance.PossibleBuildingButtonPressed += PanelRefresh;
    }

    private void PanelRefresh()
    {
        var possibleBuilding = 
            WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingButtonClicked];
        nameText.text = possibleBuilding.name;
        descriptionText.text = possibleBuilding.description;
        
        moneyCostText.text = possibleBuilding.influenceCost.ToString();
        //resourcesCostText.text = possibleBuilding.
        timeText.text = possibleBuilding.daysToBuild.ToString();
        employeesText.text = possibleBuilding.maxEmployees.ToString();

        confirmBuildText.text = $"Are you sure you want to build {possibleBuilding.name}?";
        
    }
}
