using TMPro;
using UnityEngine;

public class UICountyPanel : MonoBehaviour
{
    public static UICountyPanel instance;

    [SerializeField] private GameObject populationInfoPanel;
    [SerializeField] private GameObject expandBuildingsPanel;
    public GameObject heroInfoList;
    public GameObject armyInfoList;

    public TextMeshProUGUI countyOwnerText;
    public TextMeshProUGUI countyNameText;
    public TextMeshProUGUI countyPopulationText;
    private void Awake()
    {
        instance = this;
    }
    public void PopulationButton()
    {
        if (WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].faction.name ==
            WorldMapLoad.instance.playerFaction || WorldMapLoad.instance.DevView == true)
        {
            populationInfoPanel.SetActive(true);
        }
        else
        {
            Debug.Log("You don't own this county, fuck brain.");
        }
    }
}
