using UnityEngine;
using UnityEngine.EventSystems;

public class SelectArmy : MonoBehaviour, IPointerClickHandler
{
    public static string currentlySelectedArmyName;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WorldMapLoad.armyInfoPanel.SetActive(true);
            WorldMapLoad.countyInfoPanel.SetActive(false);

            UIArmyPanel.armyOwnerText.text = "Owner: " + WorldMapLoad.armies[int.Parse(name)].owner;
            UIArmyPanel.armyNameText.text = "Name: " + WorldMapLoad.armies[int.Parse(name)].name;
            UIArmyPanel.armySizeText.text = "Size: " + WorldMapLoad.armies[int.Parse(name)].size.ToString();

            WorldMapLoad.armies[int.Parse(name)].isArmySelected = true;
            currentlySelectedArmyName = name;

            Debug.Log("Name of Army: " + name);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("You have right clicked on an army.");
        }
    }
}
