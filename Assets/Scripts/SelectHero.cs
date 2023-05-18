using UnityEngine;
using UnityEngine.EventSystems;

public class SelectHero : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WorldMapLoad.Instance.currentlySelectedHero = gameObject;
            Debug.Log("Currently Selected Hero: " + WorldMapLoad.Instance.currentlySelectedHero);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("You have right clicked on an hero.");
        }
    }
}
