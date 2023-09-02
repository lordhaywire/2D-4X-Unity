using TMPro;
using UnityEngine;

public class UIPossibleBuildingDescriptionPanel : MonoBehaviour
{
    public static UIPossibleBuildingDescriptionPanel Instance { get; private set; }

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
        Instance = this;
    }
    private void OnEnable()
    {
        TimeKeeper.Instance.PauseTime();
        UIBuildingsPanel.Instance.currentBuildingDescriptionPanel.SetActive(false);

        PanelRefresh();

        /*
        List<GameObject> possibleBuildings 
            = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].possibleBuildings;

        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildings[i].GetComponent<BuildingInfo>().CurrentWorkersChanged += CurrentEmployeesRefresh;
        }
        */
    }

    public void PanelRefresh()
    {
        BuildingInfo buildingInfo = WorldMapLoad.Instance.currentlySelectedBuilding.GetComponent<BuildingInfo>();
            
        nameText.text = buildingInfo.buildingName;
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

    private void CurrentEmployeesRefresh()
    {
        /*
        GameObject possibleBuilding =
           WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber];
        currentEmployeesText.text = possibleBuilding.GetComponent<BuildingInfo>().CurrentWorkers.ToString();
        */
    }


    public void MinusButton()
    {
        /*
        BuildingInfo possibleBuildingInfo 
            = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber].GetComponent<BuildingInfo>();
        possibleBuildingInfo.CurrentWorkers--;
        Debug.Log("Current Workers: " + possibleBuildingInfo.CurrentWorkers);
        if (possibleBuildingInfo.CurrentWorkers < 0)
        {
            possibleBuildingInfo.CurrentWorkers = 0;

        }
        */
    }

    public void PlusButton()
    {
        /*
        BuildingInfo possibleBuildingInfo = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber].GetComponent<BuildingInfo>();

        possibleBuildingInfo.CurrentWorkers++;

        if (possibleBuildingInfo.CurrentWorkers > possibleBuildingInfo.maxWorkers)
        {
            possibleBuildingInfo.CurrentWorkers =
                possibleBuildingInfo.maxWorkers;
        }
        */
    }

    public void MaxButton()
    {
        /*
        BuildingInfo possibleBuildingInfo 
            = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber].GetComponent<BuildingInfo>();

        possibleBuildingInfo.CurrentWorkers = possibleBuildingInfo.maxWorkers;
        */
    }

    private void OnDisable()
    {
        TimeKeeper.Instance.UnpauseTime();

        //UIPossibleBuildingsPanel.Instance.PossibleBuildingButtonPressed -= PanelRefresh;

        /*
        List<GameObject> possibleBuildings 
            = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].possibleBuildings;

        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildings[i].GetComponent<BuildingInfo>().CurrentWorkersChanged -= CurrentEmployeesRefresh;
        }
        */
    }
}
