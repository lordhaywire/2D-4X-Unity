using UnityEngine;
using UnityEngine.EventSystems;

public class SelectUnit : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
           
            Debug.Log("Name of Unit: " + name);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("This is a right Click");
        }
    }
}
