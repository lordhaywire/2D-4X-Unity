
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectProvince : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UIProvincePanel.provinceOwnerText.text = "Owner: " + WorldMapLoad.provinces[name][0].ownerName;
            UIProvincePanel.provinceNameText.text = "Province: " + name;

            if(WorldMapLoad.playerName == WorldMapLoad.provinces[name][0].ownerName)
            {
                UIProvincePanel.provincePopulationText.text = "Population: " + WorldMapLoad.provinces[name][0].population.ToString();
            }
            else
            {
                UIProvincePanel.provincePopulationText.text = "Population: Unknown";
            }
            Debug.Log("Name of Province: " + name);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("This is a right Click");
        }
    }
}
