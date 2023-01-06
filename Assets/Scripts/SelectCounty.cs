using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCounty : MonoBehaviour, IPointerClickHandler
{
    public static bool isArmySelected;
    public static string currentlySelectedProvince;
    public float speed;

    //private bool rightClick = false;
    //private Vector2 velocity = Vector2.zero;
    public float smoothTime = 0.3f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WorldMapLoad.countyInfoPanel.SetActive(true);
            WorldMapLoad.armyInfoPanel.SetActive(false);

            currentlySelectedProvince = name;
            Debug.Log("Currently Selected Province: " + currentlySelectedProvince);

            UIProvincePanel.countyOwnerText.text = "Owner: " + WorldMapLoad.counties[name].ownerName;
            UIProvincePanel.countyNameText.text = "Province: " + name;

            if (WorldMapLoad.playerName == WorldMapLoad.counties[name].ownerName)
            {
                UIProvincePanel.countyPopulationText.text = "Population: " + WorldMapLoad.counties[name].population.ToString();
            }
            else
            {
                UIProvincePanel.countyPopulationText.text = "Population: Unknown";
            }
            Debug.Log("Name of Province: " + name);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {

            if (isArmySelected == true)
            {

            }
            else
            {
                Debug.Log("No army is selected.");

            }


        }
    }
}
