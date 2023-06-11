using UnityEngine;
using UnityEngine.EventSystems;

public class SelectHero : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (WorldMapLoad.Instance.heroes[int.Parse(name)].heroMovement.move == false)
            {
                if(WorldMapLoad.Instance.heroes[int.Parse(name)].IsSelected == true)
                {
                    HeroStacking.Instance.StackHeroes();
                }
                else
                {
                    WorldMapLoad.Instance.currentlySelectedHero = gameObject;

                    WorldMapLoad.Instance.heroInfoPanel.SetActive(true);
                    WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
                    WorldMapLoad.Instance.armyInfoPanel.SetActive(false);

                    Debug.Log("Hero Game Object Name: " + name);
                    WorldMapLoad.Instance.heroes[int.Parse(name)].IsSelected = true;
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
