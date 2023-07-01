using System.Collections.Generic;
using UnityEngine;

public class UIHeroScrollViewRefresher : MonoBehaviour
{
    public static UIHeroScrollViewRefresher Instance;

    public GameObject prefabHeroButton;
    public GameObject parentHeroListGroup;

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
        DestroyPanel();

        //Debug.Log("Refresh Panel");
        var heroesList = WorldMapLoad.Instance.heroes;

        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty].faction.factionNameAndColor.name
            == WorldMapLoad.Instance.playerFaction) //|| WorldMapLoad.Instance.DevView == true
        {
            Debug.Log("Refresh Panel Currently Selected County: " + WorldMapLoad.Instance.CurrentlySelectedCounty);
            for (int i = 0; i < WorldMapLoad.Instance.heroes.Count; i++)
            {
                if (heroesList[i].location == WorldMapLoad.Instance.CurrentlySelectedCounty)
                {
                    int childCount = parentHeroListGroup.transform.childCount;
                    Debug.Log("Child count: " + childCount);

                    GameObject UIhero = Instantiate(prefabHeroButton, parentHeroListGroup.transform);

                    UIhero.name = heroesList[i].heroIndex.ToString();
                    UIhero.GetComponent<UIHeroListButton>().leaderButtonText.text =
                        heroesList[i].name.ToString();
                }
            }
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
