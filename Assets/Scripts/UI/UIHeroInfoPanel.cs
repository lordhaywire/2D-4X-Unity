using TMPro;
using UnityEngine;

public class UIHeroInfoPanel : MonoBehaviour
{
    public TextMeshProUGUI heroOwnerText;
    public TextMeshProUGUI heroNameText;

    private void OnEnable()
    {
        UIPlayerUI.Instance.countyInfoPanel.SetActive(false);
        CountyPopulation countyPopulation = WorldMapLoad.Instance.currentlySelectedCountyPopulation;
        heroOwnerText.text = countyPopulation.faction.ToString();
        heroNameText.text = $"{countyPopulation.firstName} {countyPopulation.lastName}"; 
    }
}
