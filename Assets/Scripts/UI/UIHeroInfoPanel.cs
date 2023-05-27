using TMPro;
using UnityEngine;

public class UIHeroInfoPanel : MonoBehaviour
{
    public static UIHeroInfoPanel Instance;

    public TextMeshProUGUI heroOwnerText;
    public TextMeshProUGUI heroNameText;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        heroOwnerText.text 
            = WorldMapLoad.Instance.heroes[int.Parse(WorldMapLoad.Instance.currentlySelectedHero.name)].owner;
        heroNameText.text 
            = WorldMapLoad.Instance.heroes[int.Parse(WorldMapLoad.Instance.currentlySelectedHero.name)].name; 
    }
}
