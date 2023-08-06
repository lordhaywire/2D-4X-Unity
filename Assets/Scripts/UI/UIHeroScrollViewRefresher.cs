using UnityEngine;
using UnityEngine.UI;

public class UIHeroScrollViewRefresher : MonoBehaviour
{
    public static UIHeroScrollViewRefresher Instance;

    [SerializeField] private GameObject uIHeroList;
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
        DestroyPanel();
        var countyHeroes = WorldMapLoad.Instance.countyHeroes[WorldMapLoad.Instance.CurrentlySelectedCounty.name];
        //Debug.Log("Currently Selected County: " + WorldMapLoad.Instance.CurrentlySelectedCounty.name);

        for (int i = 0; i < countyHeroes.Count; i++)
        {
            GameObject uIHero = Instantiate(prefabHeroButton, parentHeroList.transform);

            uIHero.name = $"UI {countyHeroes[i].firstName} {countyHeroes[i].lastName}";
            uIHero.GetComponent<UIHeroListButton>().countyPopulation = countyHeroes[i];
            if (countyHeroes[i].isSpawned == true)
            {
                uIHero.GetComponentInChildren<Toggle>().isOn = true;
            }
            else
            {
                uIHero.GetComponentInChildren<Toggle>().isOn = false;
            }
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
