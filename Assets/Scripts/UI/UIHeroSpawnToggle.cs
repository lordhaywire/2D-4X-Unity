using UnityEngine;
using UnityEngine.UI;

public class UIHeroSpawnToggle : MonoBehaviour
{
    public GameObject heroPrefab;
    public Toggle heroSpawnToggle;

    public void SpawnHero()
    {
        heroSpawnToggle.isOn = true;

        var heroListIndex = gameObject.transform.parent.name;
        Debug.Log("Hero Index List: " + heroListIndex);
        var spawnedHeroToken = Instantiate(heroPrefab,
            WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].heroSpawnLocation.transform.position, 
            Quaternion.identity);

        spawnedHeroToken.name = heroListIndex;
        // Move the game object to the Hero list in the hierarchy.
        spawnedHeroToken.transform.parent = HeroHierarchyList.Instance.gameObject.transform;

        spawnedHeroToken.GetComponent<SpriteRenderer>().color = Color.yellow;

        WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].gameObject = spawnedHeroToken;

        WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].IsSelected = true;

        // Set the hero as already selected.
        WorldMapLoad.Instance.currentlySelectedHero = spawnedHeroToken;

        WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].heroMovement = spawnedHeroToken.GetComponent<HeroMovement>();

        WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].location 
            = WorldMapLoad.Instance.currentlySelectedCounty;
    }
}
