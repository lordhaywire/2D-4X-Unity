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
        var heroSpawnStack = WorldMapLoad.Instance.heroTokens[WorldMapLoad.Instance.currentlySelectedCounty];


        Debug.Log("Hero Index List: " + heroListIndex);

        // Maybe have the heroes spawn as a child of the county in the hierarchy?
        var spawnedHeroToken = Instantiate(heroPrefab,
            WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].heroSpawnLocation.transform.position,
            Quaternion.identity, HeroHierarchyList.Instance.gameObject.transform);

        // This is set up this way so it gets the toggle on the GameObject.
        GetComponent<Toggle>().interactable = false;

        spawnedHeroToken.name = heroListIndex.ToString();

        // This isn't needed. Move the game object to the Hero list in the hierarchy.
        //spawnedHeroToken.transform.parent = HeroHierarchyList.Instance.gameObject.transform;

        hero.isSpawned = true;

        hero.gameObject = spawnedHeroToken;

        hero.gameObject.GetComponent<SpriteRenderer>().sprite
            = HeroTokenSprites.Instance.heroSelectedSprite;

        hero.IsSelected = true;

        // Set the hero as already selected.
        WorldMapLoad.Instance.currentlySelectedHero = spawnedHeroToken;
        TokenStacking.Instance.heroIndexNumber = heroListIndex;

        hero.heroMovement = spawnedHeroToken.GetComponent<HeroMovement>();

        // Are we using this?
        WorldMapLoad.Instance.heroes[heroListIndex].tokenComponents = spawnedHeroToken.GetComponent<TokenComponents>();

        hero.location
            = WorldMapLoad.Instance.currentlySelectedCounty;

        WorldMapLoad.Instance.heroInfoPanel.SetActive(true);
        WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
        WorldMapLoad.Instance.armyInfoPanel.SetActive(false);

        // Add the hero to the hero stack list, and increment the spawned hero count.
        countyList.spawnedHeroCount++;
        heroSpawnStack.Insert(0, new SpawnedTokenList(hero.gameObject));

        Debug.Log("Hero Stack Game Object Name: " + heroSpawnStack[0].gameObject.name);
        TokenStacking.Instance.StackTokens(heroSpawnStack, true);

    }
}
