using System.Collections.Generic;
using UnityEngine;

public class UIHeroScrollView : MonoBehaviour
{
    public static UIHeroScrollView Instance;

    public List<GameObject> heroListClones = new();

    public GameObject prefabHeroButton;
    public GameObject parentHeroListGroup;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        // I hate this bullshit.
        DestoryPanel();
        RefreshPanel();
    }

    public void RefreshPanel()
    {
        var population = WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.currentlySelectedCounty];
        Debug.Log("Population Count " + population.Count);
        int numberOfHeroes = 0;
        for (int i = 0; i < population.Count; i++)
        {
            if (population[i].isHero == true)
            {

                heroListClones.Add(Instantiate(prefabHeroButton, parentHeroListGroup.transform));
                heroListClones[numberOfHeroes].name = numberOfHeroes.ToString();
                heroListClones[numberOfHeroes].GetComponent<UIHeroListButton>().leaderButtonText.text =
                $"{population[i].firstName} {population[i].lastName}";
                numberOfHeroes++;
            }
            else
            {
                //Debug.Log("Not a Hero.");
            }
        }
    }

    public void DestoryPanel()
    {
        for (int i = 0; i < heroListClones.Count; i++)
        {
            Destroy(heroListClones[i]);
        }
        heroListClones.Clear();
    }

    private void OnDisable()
    {
        DestoryPanel();
    }
}
