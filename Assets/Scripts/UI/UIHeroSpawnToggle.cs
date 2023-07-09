using UnityEngine;
using UnityEngine.UI;

public class UIHeroSpawnToggle : MonoBehaviour
{
    public GameObject heroPrefab;

    public void SpawnHero()
    {
        Hero hero = GetComponentInParent<UIHeroListButton>().hero;
        var spawnedTokenList = WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyHeroStacking>().spawnedTokenList;
        GameObject spawnLocation = WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyInfo>().heroSpawn;
        
        // I think I need this shit here.  With out this every time the hero list is refresh it respawns the heroes.
        // I know why this happens.  Every time the list is refreshed it checks marks the Toggle button automatically.
        // If we change how that works, then we can get rid of this if statement, but not the else.
        if (hero.gameObject != null)
        {
            Debug.Log("Hero already spawned.");
            return;
        }
        else
        {
            // Maybe have the heroes spawn as a child of the county in the hierarchy?
            GameObject spawnedHeroToken = Instantiate(heroPrefab, spawnLocation.transform.position,
                Quaternion.identity, HeroHierarchyList.Instance.gameObject.transform);

            // This is set up this way so it gets the toggle on the GameObject.
            GetComponent<Toggle>().interactable = false;

            hero.gameObject = spawnedHeroToken;
            hero.location = WorldMapLoad.Instance.CurrentlySelectedCounty;
            spawnedHeroToken.GetComponent<TokenInfo>().hero = hero;

            // Set the hero as already selected.
            WorldMapLoad.Instance.CurrentlySelectedHero = spawnedHeroToken;
            WorldMapLoad.Instance.heroInfoPanel.SetActive(true);
            WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
            WorldMapLoad.Instance.armyInfoPanel.SetActive(false);

            // Add the hero to the hero stack list, and increment the spawned hero count.
            WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyInfo>().county.spawnedHeroCount++;
            spawnedTokenList.Insert(0, spawnedHeroToken);

            WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyHeroStacking>().StackTokens();

            UIHeroScrollViewRefresher.Instance.RefreshPanel();
        }
    }
}
