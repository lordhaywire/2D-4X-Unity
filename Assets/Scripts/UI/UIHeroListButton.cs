using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroListButton : MonoBehaviour
{
    public static UIHeroListButton Instance;

    public Hero hero;
    public Toggle heroSpawnedToggle;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        // Gives 1 frame of time before the panel refreshes, otherwise it bugs out.
        // This gameobject is getterin Instantiated then the Hero list is being assigned to hero and I think it is happening
        // after this is "enabled."
        //StartCoroutine(AfterWaitForOneFrame());
        TextUpdate();
        CheckIfHeroSpawned();
    }

    private void TextUpdate()
    {
        GetComponent<TextMeshProUGUI>().text = $"{hero.countyPopulation.firstName} {hero.countyPopulation.lastName}";
    }
    IEnumerator AfterWaitForOneFrame()
    {
        yield return null;

        //Debug.Log("Name: " + gameObject.name + " Currently Selected Hero: "
        //    + WorldMapLoad.Instance.currentlySelectedHero);

    }

    private void CheckIfHeroSpawned()
    {
        if (hero.gameObject != null)
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
        WorldMapLoad.Instance.currentlySelectedCountyPopulation =
            WorldMapLoad.Instance.heroes[int.Parse(gameObject.name)].countyPopulation;
        WorldMapLoad.Instance.populationDescriptionPanelOpen = true;
        WorldMapLoad.Instance.populationInfoPanelOpenedByHeroListClick = true;
    }
}
