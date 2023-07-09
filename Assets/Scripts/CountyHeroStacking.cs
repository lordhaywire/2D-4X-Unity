using System.Collections.Generic;
using UnityEngine;

public class CountyHeroStacking : MonoBehaviour
{
    public List<GameObject> spawnedTokenList = new();

    public void StackTokens()
    {
        for (int i = 0; i < spawnedTokenList.Count; i++)
        {
            Hero hero = spawnedTokenList[i].GetComponent<TokenInfo>().hero;
            GameObject county = hero.location;

            // Change each token's order to be lower then the one on "top" of it.
            hero.gameObject.GetComponent<TokenInfo>().OrderInLayer = 100 - i;
            hero.gameObject.GetComponent<TokenInfo>().counterText.text = spawnedTokenList.Count.ToString();

            if (spawnedTokenList.Count > 1)
            {
                spawnedTokenList[0].GetComponent<TokenInfo>().counterGameObject.SetActive(true);
            }
            else
            {
                spawnedTokenList[0].GetComponent<TokenInfo>().counterGameObject.SetActive(false);
            }

            if (i == 0)
            {
                spawnedTokenList[i].GetComponentInChildren<TokenInfo>().nameGameObject.SetActive(true);

                spawnedTokenList[i].transform.position = county.GetComponent<CountyInfo>().heroSpawn.transform.position;

                WorldMapLoad.Instance.CurrentlySelectedHero = spawnedTokenList[i];
            }
            else
            {
                spawnedTokenList[i].GetComponentInChildren<TokenInfo>().nameGameObject.SetActive(false);
                spawnedTokenList[i].GetComponent<TokenInfo>().counterGameObject.SetActive(false);
                spawnedTokenList[i].transform.position
                    = new Vector2(county.GetComponent<CountyInfo>().heroSpawn.transform.position.x + (i * 0.1f)
                    , county.GetComponent<CountyInfo>().heroSpawn.transform.position.y);
            }

        }
    }

}
