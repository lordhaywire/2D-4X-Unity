using TMPro;
using UnityEngine;

public class UICountyInfoPanel : MonoBehaviour
{
    public static UICountyInfoPanel Instance;

    [SerializeField] private GameObject expandBuildingsPanelButton;

    public bool buildingsPanelExpanded;

    [SerializeField] private TextMeshProUGUI countyOwnerText;
    public string CountyOwnerText
    {
        get { return countyOwnerText.text; }
        set 
        { 
            countyOwnerText.text = value;
        }
    }

    [SerializeField] private TextMeshProUGUI countyNameText;
    public string CountyNameText
    {
        get { return countyNameText.text; }
        set
        {
            countyNameText.text = value;
        }
    }

    [SerializeField] private TextMeshProUGUI countyPopulationNumberText;

    public string CountyPopulationNumberText
    {
        get { return countyPopulationNumberText.text; }
        set
        {
            countyPopulationNumberText.text = value;
        }
    }

    [SerializeField] private TextMeshProUGUI countyIdleWorkersNumberText;
    public string CountyIdleWorkersNumberText
    {
        get { return countyIdleWorkersNumberText.text; }
        set
        {
            countyIdleWorkersNumberText.text = value;
        }
    }
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
