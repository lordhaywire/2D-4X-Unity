using TMPro;
using UnityEngine;

public class UIRecruitHeroConfirm : MonoBehaviour
{
    public static UIRecruitHeroConfirm Instance;

    [SerializeField] private TextMeshProUGUI areYouSureHeroText;
    private void OnEnable()
    {
        var population = WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.currentlySelectedCounty][WorldMapLoad.Instance.currentlySelectedPopulation];

        areYouSureHeroText.text = $"{AllText.UIText.AREYOUSUREHERO} {population.firstName} {population.lastName}";
    }

    public void ConfirmHeroRecruitment()
    {
        var population = WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.currentlySelectedCounty][WorldMapLoad.Instance.currentlySelectedPopulation];
        
        // We should really make this a method that is used here and in WorldMapLoad.
        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].faction.factionNameAndColor.name
            == WorldMapLoad.Instance.playerFaction)
        {
            Debug.Log("Heroes List Count : " + WorldMapLoad.Instance.heroes.Count);
            var heroList = WorldMapLoad.Instance.heroes;
            heroList.Add(new Hero(null, null, null, false, 100, WorldMapLoad.Instance.playerFaction,
                $"{population.firstName} {population.lastName}", heroList.Count,
                WorldMapLoad.Instance.currentlySelectedPopulation, WorldMapLoad.Instance.currentlySelectedCounty,
                false, null, false, false));
            Debug.Log("Hero List Index: " + heroList[^1].heroIndex);
            Debug.Log("Heroes List Count2 : " + WorldMapLoad.Instance.heroes.Count);
        }
        else
        {
            Debug.Log("You don't own this county, mother fucker.");
        }

        UICountyPanel.Instance.heroScrollView.SetActive(true);
        UIHeroScrollView.Instance.DestoryPanel();
        UIHeroScrollView.Instance.RefreshPanel();
    }
}
