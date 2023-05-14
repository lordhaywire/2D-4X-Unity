using TMPro;
using UnityEngine;

public class UIHeroListButton : MonoBehaviour
{
    public static UIHeroListButton Instance;

    public TextMeshProUGUI leaderButtonText;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        leaderButtonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void HeroListButtonClicked()
    {
        Debug.Log("Game Object Name: " + gameObject.name);
        WorldMapLoad.Instance.populationDescriptionPanel.SetActive(true);
        WorldMapLoad.Instance.currentlySelectedPopulation = int.Parse(gameObject.name);
        WorldMapLoad.Instance.populationDescriptionPanelOpen = true;
        WorldMapLoad.Instance.populationInfoPanelOpenedByHeroListClick = true;
    }
}
