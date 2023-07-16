using TMPro;
using UnityEngine;

public class UIHeroInfoPanel : MonoBehaviour
{
    public TextMeshProUGUI heroOwnerText;
    public TextMeshProUGUI heroNameText;

    private void OnEnable()
    {
        heroOwnerText.text 
            = WorldMapLoad.Instance.CurrentlySelectedToken.GetComponent<TokenInfo>().hero.faction.ToString();
        heroNameText.text 
            = $"{ WorldMapLoad.Instance.CurrentlySelectedToken.GetComponent<TokenInfo>().hero.countyPopulation.firstName}" +
            $"{WorldMapLoad.Instance.CurrentlySelectedToken.GetComponent<TokenInfo>().hero.countyPopulation.lastName}"; 
    }
}
