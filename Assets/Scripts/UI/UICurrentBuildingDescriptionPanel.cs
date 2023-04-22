using TMPro;
using UnityEngine;

public class UICurrentBuildingDescriptionPanel : MonoBehaviour
{
    public static UICurrentBuildingDescriptionPanel instance;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI workCompletedText;
    [SerializeField] private TextMeshProUGUI workCostText;
    [SerializeField] private TextMeshProUGUI currentWorkersText;
    [SerializeField] private TextMeshProUGUI maxWorkersText;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        TimeKeeper.instance.OnPanelEnable();

        WorldMapLoad.instance.currentBuildingDescriptionPanelExpanded = true;

        UICurrentBuildingsPanel.instance.CurrentBuildingButtonPressed += PanelRefresh;
    }

    private void OnDisable()
    {
        TimeKeeper.instance.OnPanelDisable();

        UICurrentBuildingsPanel.instance.CurrentBuildingButtonPressed -= PanelRefresh;

    }

    private void PanelRefresh()
    { 
        var currentBuildings =
            WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].currentBuildings[UICurrentBuildingsPanel.instance.CurrentBuildingNumber];
        nameText.text = currentBuildings.name;
        descriptionText.text = currentBuildings.description;
        workCompletedText.text = currentBuildings.workCompleted.ToString();
        workCostText.text = currentBuildings.workCost.ToString();
        currentWorkersText.text = currentBuildings.currentWorkers.ToString();
        maxWorkersText.text = currentBuildings.maxWorkers.ToString();
    }
}
