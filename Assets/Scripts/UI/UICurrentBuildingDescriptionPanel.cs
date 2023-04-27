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
        TimeKeeper.Instance.OnPanelEnable();

        WorldMapLoad.Instance.currentBuildingDescriptionPanelExpanded = true;

        UICurrentBuildingsPanel.Instance.CurrentBuildingButtonPressed += PanelRefresh;
    }

    private void OnDisable()
    {
        TimeKeeper.Instance.OnPanelDisable();

        UICurrentBuildingsPanel.Instance.CurrentBuildingButtonPressed -= PanelRefresh;
    }

    private void PanelRefresh()
    { 
        var currentBuildings =
            WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].currentBuildings[UICurrentBuildingsPanel.Instance.CurrentBuildingNumber];
        nameText.text = currentBuildings.name;
        descriptionText.text = currentBuildings.description;
        workCompletedText.text = currentBuildings.workCompleted.ToString();
        workCostText.text = currentBuildings.workCost.ToString();
        currentWorkersText.text = currentBuildings.currentWorkers.ToString();
        maxWorkersText.text = currentBuildings.maxWorkers.ToString();

        if(currentBuildings.isBuilt == true)
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
}
