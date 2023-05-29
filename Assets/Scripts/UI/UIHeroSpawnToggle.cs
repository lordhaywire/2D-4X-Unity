using UnityEngine;
using UnityEngine.UI;

public class UIHeroSpawnToggle : MonoBehaviour
{
    public GameObject heroPrefab;
    
    public void SpawnHero()
    {
        var heroListIndex = gameObject.transform.parent.name;
        if (WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].isSpawned == true)
        {
            Debug.Log("Hero already spawned.");
            return;
        }
        else
        {
            Debug.Log("Hero Index List: " + heroListIndex);

            
            var spawnedHeroToken = Instantiate(heroPrefab,
                WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].heroSpawnLocation.transform.position,
                Quaternion.identity);

            // This is set up this way so it gets the toggle on the GameObject.
            GetComponent<Toggle>().interactable = false;

            spawnedHeroToken.name = heroListIndex;

            // Move the game object to the Hero list in the hierarchy.
            spawnedHeroToken.transform.parent = HeroHierarchyList.Instance.gameObject.transform;

            WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].isSpawned = true;

            WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].gameObject = spawnedHeroToken;

            WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].gameObject.GetComponent<SpriteRenderer>().sprite
                = HeroTokenSprites.Instance.heroSelectedSprite;

            WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].IsSelected = true;

            // Set the hero as already selected.
            WorldMapLoad.Instance.currentlySelectedHero = spawnedHeroToken;

            WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].heroMovement = spawnedHeroToken.GetComponent<HeroMovement>();

            WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].heroStacking = spawnedHeroToken.GetComponent<HeroStacking>();

            WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)].location
                = WorldMapLoad.Instance.currentlySelectedCounty;

            WorldMapLoad.Instance.heroInfoPanel.SetActive(true);
            WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
            WorldMapLoad.Instance.armyInfoPanel.SetActive(false);

            HeroStacking();
        }   
    }

    private void HeroStacking()
    {
        var hero = WorldMapLoad.Instance.heroes;
        WorldMapLoad.Instance.testHeroCount = 0;
        for (int i = 0; i < hero.Count; i++)
        {
            if (hero[i].isSpawned == true && hero[i].location == WorldMapLoad.Instance.currentlySelectedCounty)
            {
                WorldMapLoad.Instance.testHeroCount++;
            }
        }
        hero[int.Parse(gameObject.transform.parent.name)].heroStacking.heroCountCanvas.SetActive(true);
        hero[int.Parse(gameObject.transform.parent.name)].heroStacking.heroCountText.text = WorldMapLoad.Instance.testHeroCount.ToString();
        hero[int.Parse(gameObject.transform.parent.name)].OrderLayer = 99 + WorldMapLoad.Instance.testHeroCount;
    }
}
