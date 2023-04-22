using TMPro;
using UnityEngine;

public class UICountyPanel : MonoBehaviour
{
    public static UICountyPanel Instance;

    [SerializeField] private GameObject populationInfoPanel;
    [SerializeField] private GameObject expandBuildingsPanelButton;

    public bool buildingsPanelExpanded;

    public GameObject heroInfoList;
    public GameObject armyInfoList;

    public TextMeshProUGUI countyOwnerText;
    public TextMeshProUGUI countyNameText;
    public TextMeshProUGUI countyPopulationText;
    private void Awake()
    {
        Instance = this;
        buildingsPanelExpanded = false;
    }
    public void PopulationButton()
    {
        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].faction.name ==
            WorldMapLoad.Instance.playerFaction || WorldMapLoad.Instance.DevView == true)
        {
            populationInfoPanel.SetActive(true);
        }
        else
        {
            Debug.Log("You don't own this county, fuck brain.");
        }
    }
}
