using UnityEngine;
using UnityEngine.EventSystems;

public class SelectHero : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("You have left CLICKED!!!!");
            if (GetComponent<HeroMovement>().heroMove == false)
            {
                if(gameObject == WorldMapLoad.Instance.CurrentlySelectedHero)
                {
                    var heroSpawnedTokens = WorldMapLoad.Instance.countyHeroTokens[WorldMapLoad.Instance.heroes[int.Parse(name)].location];
                    // Move the top token to the bottom.
                    heroSpawnedTokens.Insert(heroSpawnedTokens.Count , heroSpawnedTokens[0]);
                    heroSpawnedTokens.RemoveAt(0);
                    TokenStacking.Instance.StackTokens(WorldMapLoad.Instance.countyHeroTokens[WorldMapLoad.Instance.heroes[int.Parse(name)].location]);
                }
                else
                {
                    WorldMapLoad.Instance.CurrentlySelectedHero = gameObject;

                    WorldMapLoad.Instance.heroInfoPanel.SetActive(true);
                    WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
                    WorldMapLoad.Instance.armyInfoPanel.SetActive(false);

                    Debug.Log("Hero Game Object Name: " + name);
                    //Debug.Log("Currently Selected Hero: " + WorldMapLoad.Instance.currentlySelectedHero);
                }

            }
            else
            {
                Debug.Log("Hero is moving.");
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("You have right clicked on a hero.");
        }
    }
}
