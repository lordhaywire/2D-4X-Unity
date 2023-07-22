using UnityEngine;

public class UIHeroScrollViewRefresher : MonoBehaviour
{
    public static UIHeroScrollViewRefresher Instance;

    [SerializeField] private GameObject prefabHeroButton;
    [SerializeField] private GameObject parentHeroListGroup;
    [SerializeField] private GameObject heroScrollView;

    [SerializeField] private string countyFactionName;
    [SerializeField] private string playerFactionName;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        countyFactionName = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].faction.factionNameAndColor.name;
        playerFactionName = WorldMapLoad.Instance.playerFaction.factionNameAndColor.name;

        if (countyFactionName == playerFactionName)
        {
            RefreshPanel();
        }
    }

    public void RefreshPanel()
    {
        DestroyPanel();

        var countyPopulationDictionary 
            = WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.CurrentlySelectedCounty.name];

        if (countyFactionName == playerFactionName)
        {
            for (int i = 0; i < countyPopulationDictionary.Count; i++)
            {
                if (countyPopulationDictionary[i].location.name == WorldMapLoad.Instance.CurrentlySelectedCounty.name)
                {

                    GameObject uIHeroList = Instantiate(prefabHeroButton, parentHeroListGroup.transform);

                    uIHeroList.GetComponent<UIHeroListButton>().countyPopulation = countyPopulationDictionary[i];
                }
            }

            int childCount = parentHeroListGroup.transform.childCount;
            if (childCount == 0)
            {
                heroScrollView.SetActive(false);
            }
        }
        else
        {
            heroScrollView.SetActive(false);
        }
    }

    public void DestroyPanel()
    {
        foreach (Transform child in parentHeroListGroup.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnDisable()
    {
        DestroyPanel();
    }
}
