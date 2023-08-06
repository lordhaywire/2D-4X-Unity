using System.Collections.Generic;
using UnityEngine;

public class CountyHeroStacking : MonoBehaviour
{
    public List<GameObject> spawnedTokenList = new();

    public void StackTokens()
    {
        if(spawnedTokenList.Count > 1)
        {
            for (int i = 0; i < spawnedTokenList.Count; i++)
            {
                TokenInfo tokenInfo = spawnedTokenList[i].GetComponent<TokenInfo>();
                GameObject tokenLocation = tokenInfo.countyPopulation.location;

                // Change each token's order to be lower then the one on "top" of it.
                tokenInfo.OrderInLayer = 100 - i;
                tokenInfo.counterText.text = spawnedTokenList.Count.ToString();

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

                    spawnedTokenList[i].transform.position = tokenLocation.GetComponent<CountyInfo>().tokenSpawn.transform.position;

                    WorldMapLoad.Instance.CurrentlySelectedToken = spawnedTokenList[i];
                }
                else
                {
                    spawnedTokenList[i].GetComponentInChildren<TokenInfo>().nameGameObject.SetActive(false);
                    spawnedTokenList[i].GetComponent<TokenInfo>().counterGameObject.SetActive(false);
                    spawnedTokenList[i].transform.position
                        = new Vector2(tokenLocation.GetComponent<CountyInfo>().tokenSpawn.transform.position.x + (i * 0.1f)
                        , tokenLocation.GetComponent<CountyInfo>().tokenSpawn.transform.position.y);
                }

            }
        }
        
    }

}
