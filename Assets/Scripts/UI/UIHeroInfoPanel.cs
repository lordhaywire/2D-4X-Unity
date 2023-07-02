using TMPro;
using UnityEngine;

public class UIHeroInfoPanel : MonoBehaviour
{
    public TextMeshProUGUI heroOwnerText;
    public TextMeshProUGUI heroNameText;

    private void OnEnable()
    {
        heroOwnerText.text 
            = WorldMapLoad.Instance.CurrentlySelectedHero.GetComponent<TokenInfo>().hero.owner;
        heroNameText.text 
            = $"{ WorldMapLoad.Instance.CurrentlySelectedHero.GetComponent<TokenInfo>().hero.countyPopulation.firstName}" +
            $"{WorldMapLoad.Instance.CurrentlySelectedHero.GetComponent<TokenInfo>().hero.countyPopulation.lastName}"; 
    }
}
