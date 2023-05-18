using UnityEngine;
using UnityEngine.EventSystems;

public class SelectArmy : MonoBehaviour, IPointerClickHandler
{
    // This public static string seems like a disaster in the works.  I think we should move this to WorldMapLoad.
    public static string currentlySelectedArmyName;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WorldMapLoad.Instance.armyInfoPanel.SetActive(true);
            WorldMapLoad.Instance.countyInfoPanel.SetActive(false);

            UIArmyPanel.Instance.armyOwnerText.text = "Owner: " + WorldMapLoad.Instance.armies[int.Parse(name)].owner;
            UIArmyPanel.Instance.armyNameText.text = "Name: " + WorldMapLoad.Instance.armies[int.Parse(name)].name;
            UIArmyPanel.Instance.armySizeText.text = "Size: " + WorldMapLoad.Instance.armies[int.Parse(name)].size.ToString();

            currentlySelectedArmyName = name;

            WorldMapLoad.Instance.armies[int.Parse(name)].IsArmySelected = true;
            SelectCounty.hasAnArmyBeenSelected = true; // How does it know which county to make this true on.

            //Debug.Log("Name of Army: " + name);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("You have right clicked on an army.");
        }
    }
}
