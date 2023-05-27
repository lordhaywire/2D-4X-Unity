using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopulationDescriptionPanel : MonoBehaviour
{
    public static UIPopulationDescriptionPanel Instance;

    public Button recruitButton;
    public TextMeshProUGUI populationNameText;
    public TextMeshProUGUI constructionSkillText;

    public GameObject leaderOfPeoplePerkGameObject;
    public GameObject notEnoughResourcesPanel;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        TimeKeeper.Instance.PauseTime();
        WorldMapLoad.Instance.populationInfoPanel.SetActive(false);

        // Gives 1 frame of time before the panel refreshes, otherwise it bugs out.
        StartCoroutine(AfterWaitForOneFrame());
    }

    private void PanelRefesh()
    {
        //Debug.Log("Currently Selected Hero in Description Panel: " + WorldMapLoad.Instance.currentlySelectedPopulation);
        var currentPerson =
        WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.currentlySelectedCounty][WorldMapLoad.Instance.currentlySelectedPopulation];
        populationNameText.text = $"{currentPerson.firstName} {currentPerson.lastName}";
        constructionSkillText.text = $"Construction: {currentPerson.constructionSkill}";

        if(currentPerson.isHero == true)
        {
            recruitButton.interactable = false;
        }
        else
        {
            recruitButton.interactable = true;
        }

        if(currentPerson.leaderOfPeoplePerk == true)
        {
            leaderOfPeoplePerkGameObject.SetActive(true);
        }
    }

    // I hate this.
    IEnumerator AfterWaitForOneFrame()
    {
        yield return null;


        PanelRefesh();
    }
    private void OnDisable()
    {
        TimeKeeper.Instance.UnpauseTime();
        leaderOfPeoplePerkGameObject.SetActive(false);

        if(WorldMapLoad.Instance.populationInfoPanelOpenedByHeroListClick == false)
        {
            UIPopulationInfoPanel.Instance.gameObject.SetActive(true);
        }
        else
        {
            WorldMapLoad.Instance.populationInfoPanelOpenedByHeroListClick = false;
        }
        
    }
}
