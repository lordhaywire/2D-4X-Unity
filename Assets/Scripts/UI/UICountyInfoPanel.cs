using TMPro;
using UnityEngine;

public class UICountyInfoPanel : MonoBehaviour
{
    public static UICountyInfoPanel Instance;

    [SerializeField] private GameObject expandBuildingsPanelButton;

    public bool buildingsPanelExpanded;

    public TextMeshProUGUI countyOwnerText;
    public TextMeshProUGUI countyNameText;
    public TextMeshProUGUI countyPopulationText;
    private void Awake()
    {
        Instance = this;
        buildingsPanelExpanded = false;
    }

    private void OnEnable()
    {
        UIPlayerUI.Instance.heroInfoPanel.SetActive(false);
    }
    public void PopulationButton()
    {
        // Only allowed to look at your own population.
        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].faction.factionNameAndColor.name ==
            WorldMapLoad.Instance.playerFaction.factionNameAndColor.name)
        {
            UIPlayerUI.Instance.populationListPanel.SetActive(true);
        }
    }


}
