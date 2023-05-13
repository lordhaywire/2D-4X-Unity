using UnityEngine;

public class UIHeroListButton : MonoBehaviour
{
    public static UIHeroListButton Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void HeroListButtonClicked()
    {
        Debug.Log("Game Object Name: " + gameObject.name);
        WorldMapLoad.Instance.currentlySelectedPopulation = int.Parse(gameObject.name);
        WorldMapLoad.Instance.populationDescriptionPanelOpen = true;
        WorldMapLoad.Instance.populationInfoPanelOpenedByHeroListClick = true;
    }
}
