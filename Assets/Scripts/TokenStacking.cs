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

    public void StackTokens(List<SpawnedTokenList> tokenList)
    {
        for (int i = 0; i < tokenList.Count; i++)
        {
            var hero = WorldMapLoad.Instance.heroes[int.Parse(tokenList[i].gameObject.name)];
            var county = WorldMapLoad.Instance.counties[hero.location];

            // Change each token's order to be lower then the one on "top" of it.
            tokenList[i].gameObject.GetComponent<TokenInfo>().OrderInLayer = 100 - i;

            tokenList[0].gameObject.GetComponent<TokenInfo>().counterText.text = tokenList.Count.ToString();

            /*
            hero.heroStackIndex = i;
            Debug.Log("Hero Stack Index: " + hero.heroStackIndex);
            */
            if (tokenList.Count > 1)
            {
                tokenList[0].gameObject.GetComponent<TokenInfo>().counterGameObject.SetActive(true);
            }
            else
            {
                tokenList[0].gameObject.GetComponent<TokenInfo>().counterGameObject.SetActive(false);
            }

            if (i == 0)
            {
                tokenList[i].gameObject.GetComponentInChildren<TokenInfo>().nameGameObject.SetActive(true);

                tokenList[i].gameObject.transform.position = county.heroSpawnLocation.transform.position;

                WorldMapLoad.Instance.CurrentlySelectedHero = tokenList[i].gameObject;
            }
            else
            {
                tokenList[i].gameObject.GetComponentInChildren<TokenInfo>().nameGameObject.SetActive(false);
                tokenList[i].gameObject.GetComponent<TokenInfo>().counterGameObject.SetActive(false);
                tokenList[i].gameObject.transform.position
                    = new Vector2(county.heroSpawnLocation.transform.position.x + (i * 0.1f)
                    , county.heroSpawnLocation.transform.position.y);
            }

        }
    }
}
