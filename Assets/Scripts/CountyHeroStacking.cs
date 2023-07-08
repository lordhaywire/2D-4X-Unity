using System.Collections.Generic;
using UnityEngine;

public class CountyHeroStacking : MonoBehaviour
{
    private List<SpawnedTokenList> spawnedTokenList = new();

    public void StackTokens()
    {
        for (int i = 0; i < spawnedTokenList.Count; i++)
        {
            Hero hero = spawnedTokenList[i].gameObject.GetComponent<TokenInfo>().hero;
            GameObject county = hero.location;

            // Change each token's order to be lower then the one on "top" of it.
            hero.gameObject.GetComponent<TokenInfo>().OrderInLayer = 100 - i;
            hero.gameObject.GetComponent<TokenInfo>().counterText.text = spawnedTokenList.Count.ToString();

            if (spawnedTokenList.Count > 1)
            {
                spawnedTokenList[0].gameObject.GetComponent<TokenInfo>().counterGameObject.SetActive(true);
            }
            else
            {
                spawnedTokenList[0].gameObject.GetComponent<TokenInfo>().counterGameObject.SetActive(false);
            }

            if (i == 0)
            {
                spawnedTokenList[i].gameObject.GetComponentInChildren<TokenInfo>().nameGameObject.SetActive(true);

                spawnedTokenList[i].gameObject.transform.position = county.GetComponent<CountyInfo>().heroSpawn.transform.position;

                WorldMapLoad.Instance.CurrentlySelectedHero = spawnedTokenList[i].gameObject;
            }
            else
            {
                spawnedTokenList[i].gameObject.GetComponentInChildren<TokenInfo>().nameGameObject.SetActive(false);
                spawnedTokenList[i].gameObject.GetComponent<TokenInfo>().counterGameObject.SetActive(false);
                spawnedTokenList[i].gameObject.transform.position
                    = new Vector2(county.GetComponent<CountyInfo>().heroSpawn.transform.position.x + (i * 0.1f)
                    , county.GetComponent<CountyInfo>().heroSpawn.transform.position.y);
            }

        }
    }

}
