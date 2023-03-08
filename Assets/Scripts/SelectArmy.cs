using UnityEngine;
using UnityEngine.EventSystems;

public class SelectArmy : MonoBehaviour, IPointerClickHandler
{
    public static string currentlySelectedArmyName;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WorldMapLoad.instance.armyInfoPanel.SetActive(true);
            WorldMapLoad.instance.countyInfoPanel.SetActive(false);

            UIArmyPanel.instance.armyOwnerText.text = "Owner: " + WorldMapLoad.instance.armies[int.Parse(name)].owner;
            UIArmyPanel.instance.armyNameText.text = "Name: " + WorldMapLoad.instance.armies[int.Parse(name)].name;
            UIArmyPanel.instance.armySizeText.text = "Size: " + WorldMapLoad.instance.armies[int.Parse(name)].size.ToString();

            currentlySelectedArmyName = name;

            WorldMapLoad.instance.armies[int.Parse(name)].IsArmySelected = true;
            SelectCounty.hasAnArmyBeenSelected = true; // How does it know which county to make this true on.

            //Debug.Log("Name of Army: " + name);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("You have right clicked on an army.");
        }
    }
}
