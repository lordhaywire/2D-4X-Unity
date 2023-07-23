using UnityEngine;

public class UIHeroScrollViewRefresher : MonoBehaviour
{
    public static UIHeroScrollViewRefresher Instance;

    [SerializeField] private GameObject prefabHeroButton;
    [SerializeField] private GameObject parentHeroList;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {

        RefreshPanel();
    }

    public void RefreshPanel()
    {
        var countyHeroes = WorldMapLoad.Instance.countyHeroes[WorldMapLoad.Instance.CurrentlySelectedCounty.name];

        DestroyPanel();

        for (int i = 0; i < countyHeroes.Count; i++)
        {
            GameObject uIHeroList = Instantiate(prefabHeroButton, parentHeroList.transform);

            uIHeroList.GetComponent<UIHeroListButton>().countyPopulation = countyHeroes[i];

        }

        if (countyHeroes.Count == 0)
        {
            WorldMapLoad.Instance.heroScrollView.SetActive(false);
        }
    }

    public void DestroyPanel()
    {
        foreach (Transform child in parentHeroList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnDisable()
    {
        DestroyPanel();
    }
}
