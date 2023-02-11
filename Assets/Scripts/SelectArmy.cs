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

            // Store old color for later usage.
            //WorldMapLoad.armies[int.Parse(name)].color = WorldMapLoad.armies[int.Parse(name)].gameObject.GetComponent<SpriteRenderer>().color;
            //WorldMapLoad.armies[int.Parse(name)].gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            currentlySelectedArmyName = name;

            WorldMapLoad.armies[int.Parse(name)].IsArmySelected = true;
            SelectCounty.hasAnArmyBeenSelected = true; // How does it know which county to make this true on.

            //Debug.Log("Name of Army: " + name);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("You have right clicked on an army.");
        }
    }
}
