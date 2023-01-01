using UnityEngine;
using UnityEngine.EventSystems;

public class SelectArmy : MonoBehaviour, IPointerClickHandler
{
    //public static string provinceName;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WorldMapLoad.armyPanel.SetActive(true);
            WorldMapLoad.provincePanel.SetActive(false);

            UIArmyPanel.armyOwnerText.text = "Owner: " + WorldMapLoad.armies[0].owner;
            UIArmyPanel.armyNameText.text = "Name: " + WorldMapLoad.armies[0].name;
            UIArmyPanel.armySizeText.text = "Size: " + WorldMapLoad.armies[0].size.ToString();

            SelectProvince.isArmySelected = true;

            Debug.Log("Name of Army: " + name);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("You have right clicked on an army.");

        }
    }
}
