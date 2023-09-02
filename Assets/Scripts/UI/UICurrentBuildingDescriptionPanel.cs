using TMPro;
using UnityEngine;

public class UICurrentBuildingDescriptionPanel : MonoBehaviour
{
    public static UICurrentBuildingDescriptionPanel Instance;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI workCompletedText;
    [SerializeField] private TextMeshProUGUI workCostText;
    [SerializeField] private TextMeshProUGUI currentWorkersText;
    [SerializeField] private TextMeshProUGUI maxWorkersText;

    [SerializeField] private GameObject workTillCompletedPanel;
    [SerializeField] private GameObject employeesPanel;
    [SerializeField] private GameObject completedPanel;
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        TimeKeeper.Instance.PauseTime();
        UIBuildingsPanel.Instance.possibleBuildingDescriptionPanel.SetActive(false);

        WorldMapLoad.Instance.currentBuildingDescriptionPanelExpanded = true;

        //UICurrentBuildingsPanel.Instance.CurrentBuildingButtonPressed += PanelRefresh;
    }

    private void OnDisable()
    {
        TimeKeeper.Instance.UnpauseTime();

        //UICurrentBuildingsPanel.Instance.CurrentBuildingButtonPressed -= PanelRefresh;
    }

    private void PanelRefresh()
    {
        Debug.Log("UICurrentBuildingDescriptionPanel.cs PanelRefresh()");
        /*
        BuildingInfo currentBuildingInfo =
            WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name]
            .currentBuildings[UICurrentBuildingsPanel.Instance.CurrentBuildingNumber].GetComponent<BuildingInfo>();
        
        nameText.text = currentBuildingInfo.name;
        descriptionText.text = currentBuildingInfo.description;
        workCompletedText.text = currentBuildingInfo.workCompleted.ToString();
        workCostText.text = currentBuildingInfo.workCost.ToString();
        currentWorkersText.text = currentBuildingInfo.CurrentWorkers.ToString();
        maxWorkersText.text = currentBuildingInfo.maxWorkers.ToString();

        if(currentBuildingInfo.isBuilt == true)
        {
            workTillCompletedPanel.SetActive(false);
            employeesPanel.SetActive(false);
            completedPanel.SetActive(true);
        }
        else
        {
            workTillCompletedPanel.SetActive(true);
            employeesPanel.SetActive(true);
            completedPanel.SetActive(false);
        }
        */
    }
}
