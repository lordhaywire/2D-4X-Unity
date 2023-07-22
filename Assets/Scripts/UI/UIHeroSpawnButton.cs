using UnityEngine;
using UnityEngine.UI;

public class UIHeroSpawnButton : MonoBehaviour
{
    [SerializeField] private GameObject heroPrefab;
    [SerializeField] private GameObject heroSpawnButton;

    private void OnEnable()
    {
        CountyPopulation countyPopulation = WorldMapLoad.Instance.currentlySelectedCountyPopulation;
        if (countyPopulation.isHero == true)
        {
            heroSpawnButton.SetActive(true);
        }
        else
        {
            heroSpawnButton.SetActive(false);
        }
    }

    public void SpawnHero()
    {
        CountyPopulation countyPopulation = WorldMapLoad.Instance.currentlySelectedCountyPopulation;
        var spawnedTokenList 
            = WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyHeroStacking>().spawnedTokenList;
        GameObject spawnLocation = WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyInfo>().tokenSpawn;

        // Maybe have the heroes spawn as a child of the county in the hierarchy?
        GameObject spawnedHeroToken = Instantiate(heroPrefab, spawnLocation.transform.position,
            Quaternion.identity, HeroHierarchyList.Instance.gameObject.transform);

        spawnedHeroToken.name = countyPopulation.firstName + " " + countyPopulation.lastName;
        countyPopulation.isSpawned = true;
        
        /*spawnedHeroToken.GetComponent<TokenInfo>().countyPopulationID
            = WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.CurrentlySelectedCounty.name]
            .IndexOf(countyPopulation);

        Debug.Log("County Population Index: " + spawnedHeroToken.GetComponent<TokenInfo>().countyPopulationID);
        */
        countyPopulation.location = WorldMapLoad.Instance.CurrentlySelectedCounty;
        
        // Set the hero as already selected.
        WorldMapLoad.Instance.CurrentlySelectedToken = spawnedHeroToken;
        WorldMapLoad.Instance.heroInfoPanel.SetActive(true);
        WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
        WorldMapLoad.Instance.armyInfoPanel.SetActive(false);

        // Add the hero to the hero stack list, and increment the spawned hero count.
        WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyInfo>().county.spawnedHeroCount++;
        spawnedTokenList.Insert(0, spawnedHeroToken);

        WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyHeroStacking>().StackTokens();

        UIHeroScrollViewRefresher.Instance.RefreshPanel();

        // I think I need this shit here.  With out this every time the hero list is refresh it respawns the heroes.
        // I know why this happens.  Every time the list is refreshed it checks marks the Toggle button automatically.
        // If we change how that works, then we can get rid of this if statement, but not the else.
        /*
        if (hero.gameObject != null)
        {
            Debug.Log("Hero already spawned.");
            return;
        }
        
        else
        {
        */

        //}
    }
}
