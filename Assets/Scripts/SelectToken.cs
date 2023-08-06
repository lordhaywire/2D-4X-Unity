using UnityEngine;
using UnityEngine.EventSystems;

public class SelectToken : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (WorldMapLoad.Instance.CurrentlySelectedToken == gameObject)
            {
                string location =
                WorldMapLoad.Instance.CurrentlySelectedToken.GetComponent<TokenInfo>().countyPopulation.location.name;
                var spawnedTokenList =
                WorldMapLoad.Instance.counties[location].gameObject.GetComponent<CountyHeroStacking>().spawnedTokenList;

                // Move the top token to the bottom.
                spawnedTokenList.Insert(spawnedTokenList.Count(), gameObject);
                spawnedTokenList.RemoveAt(0);
    
                //countyHeroStacking.spawnedTokenList.RemoveAt(0);
            }
            else
            {
                WorldMapLoad.Instance.CurrentlySelectedToken = gameObject;
            }
                

            


        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("You have right clicked on a hero.");
        }
    }
}
/*
if (gameObject == WorldMapLoad.Instance.CurrentlySelectedToken)
{
    CountyPopulation countyPopulation
        = WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.CurrentlySelectedCounty.name]
        [gameObject.GetComponent<TokenInfo>().countyPopulationID];
    CountyHeroStacking countyHeroStacking
        = countyPopulation.location.GetComponent<CountyHeroStacking>();

    // Move the top token to the bottom.
    countyHeroStacking.spawnedTokenList.Insert(countyHeroStacking.spawnedTokenList.Count, countyHeroStacking.spawnedTokenList[0]);
    countyHeroStacking.spawnedTokenList.RemoveAt(0);
    countyHeroStacking.StackTokens();
}
else
{
    WorldMapLoad.Instance.CurrentlySelectedToken = gameObject;

    WorldMapLoad.Instance.heroInfoPanel.SetActive(true);
    WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
    WorldMapLoad.Instance.armyInfoPanel.SetActive(false);
}
*/
