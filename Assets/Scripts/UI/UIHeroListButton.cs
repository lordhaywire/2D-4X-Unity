using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroListButton : MonoBehaviour
{
    public static UIHeroListButton Instance;

    public TextMeshProUGUI leaderButtonText;
    public Toggle heroSpawnedToggle;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        // Gives 1 frame of time before the panel refreshes, otherwise it bugs out.
        StartCoroutine(AfterWaitForOneFrame());


        leaderButtonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    IEnumerator AfterWaitForOneFrame()
    {
        yield return null;


        //Debug.Log("Name: " + gameObject.name + " Currently Selected Hero: "
        //    + WorldMapLoad.Instance.currentlySelectedHero);
        if (WorldMapLoad.Instance.heroes[int.Parse(gameObject.name)].isSpawned == true)
        {
            heroSpawnedToggle.isOn = true;
            heroSpawnedToggle.interactable = false;
        }
        else
        {
            //Debug.Log("Is Spawned is false");
        }
    }

    public void HeroListButtonClicked()
    {
        Debug.Log("Game Object Name: " + gameObject.name);
        WorldMapLoad.Instance.populationDescriptionPanel.SetActive(true);
        WorldMapLoad.Instance.currentlySelectedPopulation =
            WorldMapLoad.Instance.heroes[int.Parse(gameObject.name)].countyPopulationIndex;
        WorldMapLoad.Instance.populationDescriptionPanelOpen = true;
        WorldMapLoad.Instance.populationInfoPanelOpenedByHeroListClick = true;
    }
}
