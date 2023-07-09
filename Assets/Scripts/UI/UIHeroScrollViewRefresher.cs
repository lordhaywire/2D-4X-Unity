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
        if (WorldMapLoad.Instance.playerFaction.factionNameAndColor.name 
            == WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].faction.factionNameAndColor.name)
        {
            RefreshPanel();
        }
    }

    public void RefreshPanel()
    {
        var heroesList = WorldMapLoad.Instance.heroes;

        DestroyPanel();

        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty.name].faction.factionNameAndColor.name
            == WorldMapLoad.Instance.playerFaction.factionNameAndColor.name)
        {
            for (int i = 0; i < heroesList.Count; i++)
            {
                if (heroesList[i].location.name == WorldMapLoad.Instance.CurrentlySelectedCounty.name)
                {

                    GameObject UIhero = Instantiate(prefabHeroButton, parentHeroListGroup.transform);

                    UIhero.GetComponent<UIHeroListButton>().hero = heroesList[i];
                }
            }
            
            int childCount = parentHeroListGroup.transform.childCount;
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
            Destroy(child.gameObject);
        }
    }

    private void OnDisable()
    {
        DestroyPanel();
    }
}
