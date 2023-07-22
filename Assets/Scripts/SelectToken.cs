using UnityEngine;
using UnityEngine.EventSystems;

public class SelectToken : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WorldMapLoad.Instance.CurrentlySelectedToken = gameObject;

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

        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("You have right clicked on a hero.");
        }
    }
}
