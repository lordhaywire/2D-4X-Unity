using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class UICountyPanel : MonoBehaviour
{
    public static UICountyPanel instance;

    [SerializeField] private GameObject populationInfoPanel;
    public GameObject heroInfoList;
    public GameObject armyInfoList;

    public TextMeshProUGUI countyOwnerText;
    public TextMeshProUGUI countyNameText;
    public TextMeshProUGUI countyPopulationText;

    public void PopulationButton()
    {
        if (WorldMapLoad.instance.counties[SelectCounty.currentlySelectedCounty].faction.name ==
            WorldMapLoad.instance.playerFaction || WorldMapLoad.instance.DevView == true)
        {
            populationInfoPanel.SetActive(true);
        }
        else
        {
            Debug.Log("You don't own this county, fuck brain.");
        }
    }
    private void Awake()
    {
        instance = this;
    }
}
