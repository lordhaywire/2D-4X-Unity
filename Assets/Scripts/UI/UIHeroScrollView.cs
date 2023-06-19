using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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

        RefreshPanel();
    }

    public void RefreshPanel()
    {
        //Debug.Log("Hero List Clones: " + heroListClones.Count);
        DestroyPanel();
        //Debug.Log("Refresh Panel");
        var heroesList = WorldMapLoad.Instance.heroes;
        int numberOfHeroes = 0;
        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].faction.factionNameAndColor.name
            == WorldMapLoad.Instance.playerFaction || WorldMapLoad.Instance.DevView == true)
        {
            Debug.Log("Refresh Panel Currently Selected County: " + WorldMapLoad.Instance.currentlySelectedCounty);
            for (int i = 0; i < WorldMapLoad.Instance.heroes.Count; i++)
            {
                if (heroesList[i].location == WorldMapLoad.Instance.currentlySelectedCounty)
                {
                    heroListClones.Add(Instantiate(prefabHeroButton, parentHeroListGroup.transform));
                    heroListClones[numberOfHeroes].name = heroesList[i].heroIndex.ToString();

                    heroListClones[numberOfHeroes].GetComponent<UIHeroListButton>().leaderButtonText.text =
                        heroesList[i].name.ToString();
                    numberOfHeroes++;
                }
            }

        }
    }

    public void DestroyPanel()
    {
        for (int i = 0; i < heroListClones.Count; i++)
        {
            Destroy(heroListClones[i]);
        }
        heroListClones.Clear();
        Debug.Log("Everything destroyed?");
    }

    private void OnDisable()
    {
        DestroyPanel();
    }
}
