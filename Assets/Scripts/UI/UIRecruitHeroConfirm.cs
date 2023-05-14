using System;
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
}
