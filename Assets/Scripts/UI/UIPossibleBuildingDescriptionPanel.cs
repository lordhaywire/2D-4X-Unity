using TMPro;
using UnityEngine;

public class UIPossibleBuildingDescriptionPanel : MonoBehaviour
{
    public static UIPossibleBuildingDescriptionPanel Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI moneyCostText;
    [SerializeField] private TextMeshProUGUI resourcesCostText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI currentEmployeesText;
    [SerializeField] private TextMeshProUGUI maxEmployeesText;
    [SerializeField] private TextMeshProUGUI confirmBuildText;

    private BuildingInfo buildingInfo;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        TimeKeeper.Instance.PauseTime();
        UIBuildingsPanel.Instance.currentBuildingDescriptionPanel.SetActive(false);

        PanelRefresh();
    }

    public void PanelRefresh()
    {
        buildingInfo = WorldMapLoad.Instance.currentlySelectedBuilding.GetComponent<BuildingInfo>();
            
        buildingNameText.text = $"{buildingInfo.county.gameObject.name}'s {buildingInfo.buildingName}";
        descriptionText.text = buildingInfo.description;

        moneyCostText.text = $"{buildingInfo.influenceCost} Influence";
        //resourcesCostText.text = possibleBuilding. // We aren't using the resources part yet.
        timeText.text = buildingInfo.workCost.ToString();

        // Reset current employees to 0.
        buildingInfo.CurrentWorkers = 0;
        currentEmployeesText.text = buildingInfo.CurrentWorkers.ToString();
        maxEmployeesText.text = buildingInfo.maxWorkers.ToString();

        confirmBuildText.text = $"Are you sure you want to build {buildingInfo.name}?";       
    }

    public void MinusButton()
    {
        buildingInfo.CurrentWorkers--;

        if (buildingInfo.CurrentWorkers < 0)
        {
            buildingInfo.CurrentWorkers = 0;
        }
        currentEmployeesText.text = buildingInfo.CurrentWorkers.ToString();
    }

    public void PlusButton()
    {
        buildingInfo.CurrentWorkers++;

        if (buildingInfo.CurrentWorkers > buildingInfo.maxWorkers)
        {
            buildingInfo.CurrentWorkers =
                buildingInfo.maxWorkers;
        }
        currentEmployeesText.text = buildingInfo.CurrentWorkers.ToString();
    }

    public void MaxButton()
    {
        buildingInfo.CurrentWorkers = buildingInfo.maxWorkers;
        currentEmployeesText.text = buildingInfo.CurrentWorkers.ToString();
    }

    private void OnDisable()
    {
        TimeKeeper.Instance.UnpauseTime();

        //UIPossibleBuildingsPanel.Instance.PossibleBuildingButtonPressed -= PanelRefresh;
    }
}

/* Saving this just in case we find a Current Workers bug.
private void CurrentEmployeesRefresh()
{

    GameObject possibleBuilding =
       WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber];
    currentEmployeesText.text = possibleBuilding.GetComponent<BuildingInfo>().CurrentWorkers.ToString();

}
*/
