using System.Collections.Generic;
using UnityEngine;

public class TokenStacking : MonoBehaviour
{
    public static TokenStacking Instance;

    public int heroIndexNumber;

    private void Awake()
    {
        Instance = this;
    }
     
    public void StackTokens(List<SpawnedTokenList> tokenList, bool isSelected)
    {   
        for (int i = 0; i < tokenList.Count; i++)
        {
            var hero = WorldMapLoad.Instance.heroes[int.Parse(tokenList[i].gameObject.name)];
            var county = WorldMapLoad.Instance.counties[hero.location];

            Debug.Log("Hero Spawn Stack Game Object Name: " + tokenList[i].gameObject.name);

            hero.gameObject.GetComponent<TokenComponents>().spriteRenderer.sortingOrder = 100 - i;
            hero.gameObject.GetComponent<TokenComponents>().timerCanvas.sortingOrder = 100 - i;
            hero.gameObject.GetComponent<TokenComponents>().counterCanvas.sortingOrder = 100 - i;
            hero.gameObject.GetComponent<TokenComponents>().nameCanvas.sortingOrder = 100 - i;

            hero.tokenComponents.counterText.text = tokenList.Count.ToString();

            hero.heroStackIndex = i;
            Debug.Log("Hero Stack Index: " + hero.heroStackIndex);

            if (i == 0)
            {
                tokenList[i].gameObject.GetComponentInChildren<HeroName>().heroNameGameObject.SetActive(true);
                if(tokenList.Count == 1)
                {                   
                    tokenList[i].gameObject.GetComponent<TokenComponents>().counterGameObject.SetActive(false);                   
                }
                else
                {
                    tokenList[i].gameObject.GetComponent<TokenComponents>().counterGameObject.SetActive(true);
                }   
                tokenList[i].gameObject.transform.position = county.heroSpawnLocation.transform.position;

                if(isSelected == true)
                {
                    hero.IsSelected = true;
                    WorldMapLoad.Instance.currentlySelectedHero = tokenList[i].gameObject;
                }
                else
                {
                    hero.IsSelected = false;
                }
            }
            else
            {
                tokenList[i].gameObject.GetComponentInChildren<HeroName>().heroNameGameObject.SetActive(false);
                tokenList[i].gameObject.GetComponent<TokenComponents>().counterGameObject.SetActive(false);
                tokenList[i].gameObject.transform.position 
                    = new Vector2(county.heroSpawnLocation.transform.position.x + (i * 0.1f)
                    , county.heroSpawnLocation.transform.position.y);
                hero.IsSelected = false;
            }
        }
    }
}
