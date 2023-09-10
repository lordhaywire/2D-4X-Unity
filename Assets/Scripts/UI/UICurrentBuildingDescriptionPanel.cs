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

        PanelRefresh();
    }


    private void PanelRefresh()
    {   
        BuildingInfo buildingInfo = WorldMapLoad.Instance.currentlySelectedBuilding.GetComponent<BuildingInfo>();

        nameText.text = buildingInfo.name;
        descriptionText.text = buildingInfo.description;
        workCompletedText.text = buildingInfo.workCompleted.ToString();
        workCostText.text = buildingInfo.workCost.ToString();
        currentWorkersText.text = buildingInfo.CurrentWorkers.ToString();
        maxWorkersText.text = buildingInfo.maxWorkers.ToString();

        if(buildingInfo.isBuilt == true)
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
        
    }
    private void OnDisable()
    {
        TimeKeeper.Instance.UnpauseTime();
    }
}
