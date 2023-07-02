using System.Collections.Generic;
using UnityEngine;

public class UIHeroScrollViewRefresher : MonoBehaviour
{
    public static UIHeroScrollViewRefresher Instance;

    [SerializeField] private GameObject prefabHeroButton;
    [SerializeField] private GameObject parentHeroListGroup;
    [SerializeField] private GameObject heroScrollView;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if (WorldMapLoad.Instance.playerFaction == WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty].faction.factionNameAndColor.name)
        {
            RefreshPanel();
        }
        else
        {
            Debug.Log("Not your county, mother fucker.");
        }

    }

    public void RefreshPanel()
    {
        var heroesList = WorldMapLoad.Instance.heroes;
        //var heroTokens = WorldMapLoad.Instance.countyHeroTokens;

        DestroyPanel();

        //Debug.Log("Refresh Panel");


        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty].faction.factionNameAndColor.name
            == WorldMapLoad.Instance.playerFaction)
        {
            //Debug.Log("Refresh Panel Currently Selected County: " + WorldMapLoad.Instance.CurrentlySelectedCounty);
            for (int i = 0; i < heroesList.Count; i++)
            {
                if (heroesList[i].location == WorldMapLoad.Instance.CurrentlySelectedCounty)
                {

                    GameObject UIhero = Instantiate(prefabHeroButton, parentHeroListGroup.transform);

                    UIhero.GetComponent<UIHeroListButton>().hero = heroesList[i];

                    // We need to set the name text of the UIHeroListButton.

                    //$"{heroesList[i].countyPopulation.firstName} {heroesList[i].countyPopulation.lastName}";
                }
            }
            
            int childCount = parentHeroListGroup.transform.childCount;
            //Debug.Log("Child count: " + childCount);
            if(childCount == 0)
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
            Debug.Log("Destroying: " + child.gameObject.name);
            Destroy(child.gameObject);
        }
        Debug.Log("Everything destroyed?");
    }

    private void OnDisable()
    {
        DestroyPanel();
    }
}
