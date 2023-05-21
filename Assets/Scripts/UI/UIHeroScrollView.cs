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
        var heroes = WorldMapLoad.Instance.heroes;
        int numberOfHeroes = 0;
        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].faction.factionNameAndColor.name
            == WorldMapLoad.Instance.playerFaction)
        {
            for (int i = 0; i < WorldMapLoad.Instance.heroes.Count; i++)
            {
                if (heroes[i].location == WorldMapLoad.Instance.currentlySelectedCounty)
                {
                    heroListClones.Add(Instantiate(prefabHeroButton, parentHeroListGroup.transform));
                    heroListClones[numberOfHeroes].name = heroes[i].heroIndex.ToString();

                    heroListClones[numberOfHeroes].GetComponent<UIHeroListButton>().leaderButtonText.text =
                        heroes[i].name.ToString();
                    numberOfHeroes++;
                }
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
