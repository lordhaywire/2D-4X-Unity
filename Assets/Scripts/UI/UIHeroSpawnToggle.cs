using System.Collections.Generic;
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

            hero.heroMovement = spawnedHeroToken.GetComponent<HeroMovement>();

            // Are we using this?
            WorldMapLoad.Instance.heroes[heroListIndex].heroStackCount = spawnedHeroToken.GetComponent<HeroStackCount>();

            hero.location
                = WorldMapLoad.Instance.currentlySelectedCounty;

            WorldMapLoad.Instance.heroInfoPanel.SetActive(true);
            WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
            WorldMapLoad.Instance.armyInfoPanel.SetActive(false);
            /*
             *         counties[CountyListCreator.Instance.countiesList[0].name] = new County(
            0, true, null, null, null, factions[1],
            Arrays.provinceName[0], "Coast", "Forest", "Ruin",0, 0, 0);
            */


            if (countyList.spawnedHeroCount != 0)
            {
                MultipleHeroesStacked();
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

    private void MultipleHeroesStacked()
    {
        var heroSpawnStack = WorldMapLoad.Instance.heroStacking[WorldMapLoad.Instance.currentlySelectedCounty];
         
        heroSpawnStack.Add(new HeroStack(WorldMapLoad.Instance.heroes[int.Parse(gameObject.transform.parent.name)].gameObject));
        //Debug.Log("Hero Spawn Stack Count: " + heroSpawnStack.Count);
        heroSpawnStack.Insert(0, heroSpawnStack[^1]);
        //Debug.Log("Hero Spawn Stack Count: " + heroSpawnStack.Count);
        heroSpawnStack.RemoveAt(heroSpawnStack.Count - 1);
        //Debug.Log("Hero Spawn Stack Count: " + heroSpawnStack.Count);

        for(int i = 0; i < heroSpawnStack.Count; i++)
        {
            var hero = WorldMapLoad.Instance.heroes[int.Parse(heroSpawnStack[i].gameObject.name)];

            Debug.Log("Hero Spawn Stack Game Object Name: " + heroSpawnStack[i].gameObject.name);
            Debug.Log("Position: " + i + " " + heroSpawnStack[i].gameObject.transform.position.x);

            hero.gameObject.GetComponent<HeroSortOrders>().heroRenderer.sortingOrder = 100 - i;
            hero.gameObject.GetComponent<HeroSortOrders>().heroCanvasTimerRenderer.sortingOrder = 100 - i;
            hero.gameObject.GetComponent<HeroSortOrders>().heroStackCountRenderer.sortingOrder = 100 - i;
            hero.gameObject.GetComponent<HeroSortOrders>().heroNameTextRenderer.sortingOrder = 100 - i;
            // Reduce duplicate code.
            if (i == 0)
            {
                heroSpawnStack[i].gameObject.GetComponentInChildren<HeroName>().heroNameGameObject.SetActive(true);
                heroSpawnStack[i].gameObject.GetComponent<HeroStackCount>().heroCountGameObject.SetActive(true);
                // We should turn this into a list.

                hero.isSelected = true;
                
            }
            else if(i == 1)
            {
                heroSpawnStack[i].gameObject.GetComponentInChildren<HeroName>().heroNameGameObject.SetActive(false);
                heroSpawnStack[i].gameObject.GetComponent<HeroStackCount>().heroCountGameObject.SetActive(false);
                heroSpawnStack[i].gameObject.transform.position = new Vector2(heroSpawnStack[i].gameObject.transform.position.x + 0.2f, heroSpawnStack[i].gameObject.transform.position.y);
                hero.isSelected = false;
            }
            else
            {
                heroSpawnStack[i].gameObject.GetComponentInChildren<HeroName>().heroNameGameObject.SetActive(false);
                heroSpawnStack[i].gameObject.GetComponent<HeroStackCount>().heroCountGameObject.SetActive(false);
                heroSpawnStack[i].gameObject.transform.position = new Vector2(heroSpawnStack[i].gameObject.transform.position.x - 0.2f, heroSpawnStack[i].gameObject.transform.position.y);
                hero.isSelected = false;
            }
        }
        //var heroList = WorldMapLoad.Instance.heroes;

        /*
        for (int i = 0; i < hero.Count; i++)
        {
            if (hero[i].isSpawned == true && hero[i].location == WorldMapLoad.Instance.currentlySelectedCounty)
            {
                countyList.spawnedHeroCount++;
            }
        }
        
        hero[int.Parse(gameObject.transform.parent.name)].heroStacking.heroCountText.text = countyList.spawnedHeroCount.ToString();
        hero[int.Parse(gameObject.transform.parent.name)].OrderLayer = 99 + countyList.spawnedHeroCount;
        */
    }
    }
