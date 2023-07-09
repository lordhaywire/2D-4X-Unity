using UnityEngine;
using UnityEngine.EventSystems;

public class SelectHero : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (GetComponent<HeroMovement>().heroMove == false)
            {
                if(gameObject == WorldMapLoad.Instance.CurrentlySelectedHero)
                {
                    CountyHeroStacking countyHeroStacking = gameObject.GetComponent<TokenInfo>().hero.location.GetComponent<CountyHeroStacking>();
                    
                    // Move the top token to the bottom.
                    countyHeroStacking.spawnedTokenList.Insert(countyHeroStacking.spawnedTokenList.Count , countyHeroStacking.spawnedTokenList[0]);
                    countyHeroStacking.spawnedTokenList.RemoveAt(0);
                    countyHeroStacking.StackTokens();
                }
                else
                {
                    WorldMapLoad.Instance.CurrentlySelectedHero = gameObject;

                    WorldMapLoad.Instance.heroInfoPanel.SetActive(true);
                    WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
                    WorldMapLoad.Instance.armyInfoPanel.SetActive(false);
                }

            }
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("You have right clicked on a hero.");
        }
    }
}
