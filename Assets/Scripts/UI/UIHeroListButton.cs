using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroListButton : MonoBehaviour
{
    public Hero hero;
    public Toggle heroSpawnedToggle;

    private void OnEnable()
    {
        StartCoroutine(TextUpdate());
        
        CheckIfHeroSpawned();
    }

    IEnumerator TextUpdate()
    {
        yield return null;
        GetComponent<TextMeshProUGUI>().text = $"{hero.countyPopulation.firstName} {hero.countyPopulation.lastName}";
    }

    public void HeroListButtonClicked()
    {
        // Closes the Description Panel then reopens it so it refreshes.
        WorldMapLoad.Instance.populationDescriptionPanel.SetActive(false);
        WorldMapLoad.Instance.populationDescriptionPanel.SetActive(true);

        WorldMapLoad.Instance.currentlySelectedCountyPopulation =
            hero.countyPopulation;
        WorldMapLoad.Instance.populationDescriptionPanelOpen = true;
        WorldMapLoad.Instance.populationInfoPanelOpenedByHeroListClick = true;
    }

    private void CheckIfHeroSpawned()
    {
        // This is the code that is causing us to have to have an if statement in UIHeroSpawnToggle
        if (hero.gameObject != null)
        {
            heroSpawnedToggle.isOn = true;
            heroSpawnedToggle.interactable = false;
        }
    }
}
