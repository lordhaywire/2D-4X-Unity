using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroListButton : MonoBehaviour
{
    [SerializeField] private Toggle heroSpawnedToggle;
    public CountyPopulation countyPopulation;

    private void OnEnable()
    {
        StartCoroutine(TextUpdate());

        CheckIfHeroSpawned();
    }

    IEnumerator TextUpdate()
    {
        yield return null;
        GetComponent<TextMeshProUGUI>().text = $"{countyPopulation.firstName} {countyPopulation.lastName}";
    }

    public void HeroListButtonClicked()
    {
        // Closes the Description Panel then reopens it so it refreshes.
        WorldMapLoad.Instance.populationDescriptionPanel.SetActive(false);
        WorldMapLoad.Instance.populationDescriptionPanel.SetActive(true);

        WorldMapLoad.Instance.currentlySelectedCountyPopulation = countyPopulation;
        WorldMapLoad.Instance.populationDescriptionPanelOpen = true;
        WorldMapLoad.Instance.populationInfoPanelOpenedByHeroListClick = true;
    }

    
    private void CheckIfHeroSpawned()
    {
        if (countyPopulation.isSpawned == true)
        {
           heroSpawnedToggle.isOn = true;
        }
    }
}
