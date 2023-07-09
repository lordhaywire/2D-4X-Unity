using TMPro;
using UnityEngine;

public class UICountyPanel : MonoBehaviour
{
    public static UICountyPanel Instance;

    [SerializeField] private GameObject populationInfoPanel;
    [SerializeField] private GameObject expandBuildingsPanelButton;

    public bool buildingsPanelExpanded;

    public GameObject heroScrollView;
    public GameObject armyScrollView;

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
        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].faction.factionNameAndColor.name ==
            WorldMapLoad.Instance.playerFaction.factionNameAndColor.name)
        {
            populationInfoPanel.SetActive(true);
        }
    }
}
