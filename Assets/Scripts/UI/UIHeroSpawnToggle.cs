using UnityEngine;
using UnityEngine.UI;

public class UIHeroSpawnToggle : MonoBehaviour
{
    public GameObject heroPrefab;

    public void SpawnHero()
    {
        var heroListIndex = int.Parse(gameObject.transform.parent.name);
        var countyList = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty];
        var hero = WorldMapLoad.Instance.heroes[heroListIndex];
        var heroSpawnStack = WorldMapLoad.Instance.heroStacking[WorldMapLoad.Instance.currentlySelectedCounty];

        // We don't actually need this if check because you can't click the toggle if it is already spawned.
        if (hero.isSpawned == true)
        {
            Debug.Log("Hero already spawned.");
            return;
        }
        else
        {
            //Debug.Log("Hero Index List: " + heroListIndex);

            var spawnedHeroToken = Instantiate(heroPrefab,
                WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].heroSpawnLocation.transform.position,
                Quaternion.identity);

            // This is set up this way so it gets the toggle on the GameObject.
            GetComponent<Toggle>().interactable = false;

            spawnedHeroToken.name = heroListIndex.ToString();

            // Move the game object to the Hero list in the hierarchy.
            spawnedHeroToken.transform.parent = HeroHierarchyList.Instance.gameObject.transform;

            hero.isSpawned = true;

            hero.gameObject = spawnedHeroToken;

            hero.gameObject.GetComponent<SpriteRenderer>().sprite
                = HeroTokenSprites.Instance.heroSelectedSprite;

            hero.IsSelected = true;

            // Set the hero as already selected.
            WorldMapLoad.Instance.currentlySelectedHero = spawnedHeroToken;
            HeroStacking.Instance.heroIndexNumber = heroListIndex;

            hero.heroMovement = spawnedHeroToken.GetComponent<HeroMovement>();

            // Are we using this?
            WorldMapLoad.Instance.heroes[heroListIndex].heroStackCount = spawnedHeroToken.GetComponent<HeroStackCount>();

            hero.location
                = WorldMapLoad.Instance.currentlySelectedCounty;

            WorldMapLoad.Instance.heroInfoPanel.SetActive(true);
            WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
            WorldMapLoad.Instance.armyInfoPanel.SetActive(false);

            if (countyList.spawnedHeroCount != 0)
            {
                countyList.spawnedHeroCount++;
                HeroStacking.Instance.StackHeroes();
            }
            else
            {
                countyList.spawnedHeroCount++;
                heroSpawnStack.Add(new HeroStack(hero.gameObject));
                Debug.Log("Hero Stack Game Object Name: " + heroSpawnStack[0].gameObject.name);
                hero.gameObject.GetComponent<HeroSortOrders>().heroRenderer.sortingOrder = 100;
            }
        }
    }  
}
