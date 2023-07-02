using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopulationDescriptionPanel : MonoBehaviour
{
    public static UIPopulationDescriptionPanel Instance;

    [SerializeField] private Button recruitButton;
    [SerializeField] private TextMeshProUGUI populationNameText;
    [SerializeField] private TextMeshProUGUI constructionSkillText;

    [SerializeField] private GameObject leaderOfPeoplePerkGameObject;
    public GameObject notEnoughResourcesPanel;

    
    private void Awake()
    {
        Instance = this;
    }
    
    private void OnEnable()
    {
        TimeKeeper.Instance.PauseTime();
        WorldMapLoad.Instance.populationInfoPanel.SetActive(false);

        PopulationDescriptionPanelRefesh();
        // Gives 1 frame of time before the panel refreshes, otherwise it bugs out.
        //StartCoroutine(AfterWaitForOneFrame());
    }

    private void PopulationDescriptionPanelRefesh()
    {
        CountyPopulation countyPopulation = WorldMapLoad.Instance.currentlySelectedCountyPopulation;
        populationNameText.text = $"{countyPopulation.firstName} {countyPopulation.lastName}";
        constructionSkillText.text = $"Construction: {countyPopulation.constructionSkill}";

        if(countyPopulation.hero != null)
        {
            recruitButton.interactable = false;
        }
        else
        {
            recruitButton.interactable = true;
        }

        if(countyPopulation.leaderOfPeoplePerk == true)
        {
            leaderOfPeoplePerkGameObject.SetActive(true);
        }
    }

    /*
    // I hate this.
    IEnumerator AfterWaitForOneFrame()
    {
        yield return null;

        PopulationDescriptionPanelRefesh();
    }
    */
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
