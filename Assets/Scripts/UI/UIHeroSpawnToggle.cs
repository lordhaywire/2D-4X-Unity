using UnityEngine;
using UnityEngine.UI;

public class UIHeroSpawnToggle : MonoBehaviour
{
    public GameObject heroPrefab;

    public void SpawnHero()
    {
        Hero hero = GetComponentInParent<UIHeroListButton>().hero;
        County countyList = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.CurrentlySelectedCounty];
        var heroSpawnStack = WorldMapLoad.Instance.countyHeroTokens[WorldMapLoad.Instance.CurrentlySelectedCounty];
        
        // I think I need this shit here.  With out this every time the hero list is refresh it respawns the heroes.
        if (hero.gameObject != null)
        {
            Debug.Log("Hero already spawned.");
            return;
        }
        else
        {
            // Maybe have the heroes spawn as a child of the county in the hierarchy?
            var spawnedHeroToken = Instantiate(heroPrefab, countyList.heroSpawnLocation.transform.position,
                Quaternion.identity, HeroHierarchyList.Instance.gameObject.transform);

            // This is set up this way so it gets the toggle on the GameObject.
            GetComponent<Toggle>().interactable = false;

            hero.gameObject = spawnedHeroToken;
            hero.location = WorldMapLoad.Instance.CurrentlySelectedCounty;
            spawnedHeroToken.GetComponent<TokenInfo>().hero = hero;

            //spawnedHeroToken.name = heroListIndex.ToString();

            // This isn't needed. Move the game object to the Hero list in the hierarchy.
            //spawnedHeroToken.transform.parent = HeroHierarchyList.Instance.gameObject.transform;



            // I would like this to be controlled by the selected gameObject's script
            //hero.gameObject.GetComponent<SpriteRenderer>().sprite
            //    = HeroTokenSprites.Instance.heroSelectedSprite;

            //hero.IsSelected = true;

            // Set the hero as already selected.
            WorldMapLoad.Instance.CurrentlySelectedHero = spawnedHeroToken;
            //TokenStacking.Instance.heroIndexNumber = heroListIndex;



            WorldMapLoad.Instance.heroInfoPanel.SetActive(true);
            WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
            WorldMapLoad.Instance.armyInfoPanel.SetActive(false);

            // Add the hero to the hero stack list, and increment the spawned hero count.
            countyList.spawnedHeroCount++;
            heroSpawnStack.Insert(0, new SpawnedTokenList(hero.gameObject));

            Debug.Log("Hero Stack Game Object Name: " + heroSpawnStack[0].gameObject.name);
            TokenStacking.Instance.StackTokens(heroSpawnStack);

            UIHeroScrollViewRefresher.Instance.RefreshPanel();
        }
    }
}
