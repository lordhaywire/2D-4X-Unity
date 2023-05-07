using System.Collections;
using TMPro;
using UnityEngine;

public class UIHeroDescriptionPanel : MonoBehaviour
{
    public TextMeshProUGUI heroNameText;
    public TextMeshProUGUI constructionSkillText;

    public GameObject leaderOfPeoplePerkGameObject;

    private void OnEnable()
    {
        TimeKeeper.Instance.PauseTime();

        // Call a function after the coroutine has finished
        StartCoroutine(AfterWaitForOneFrame());


    }

    private void PanelRefesh()
    {
        Debug.Log("Currently Selected Hero in Description Panel: " + WorldMapLoad.Instance.currentlySelectedHero);
        var currentHero =
        WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.currentlySelectedCounty][WorldMapLoad.Instance.currentlySelectedHero];
        heroNameText.text = $"{currentHero.firstName} {currentHero.lastName}";
        constructionSkillText.text = $"Construction: {currentHero.constructionSkill}";

        if(currentHero.leaderOfPeoplePerk == true)
        {
            leaderOfPeoplePerkGameObject.SetActive(true);
        }
    }

    IEnumerator AfterWaitForOneFrame()
    {
        // Wait for one frame
        yield return null;

        PanelRefesh();

    }


    private void OnDisable()
    {
        TimeKeeper.Instance.UnpauseTime();
        leaderOfPeoplePerkGameObject.SetActive(false);
    }
}
