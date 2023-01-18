using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCounty : MonoBehaviour, IPointerClickHandler
{
    public static string currentlySelectedProvince;
    public static bool tryingToMoveAnArmy;
    //public float speed;
    //public float smoothTime = 0.3f;

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

            tryingToMoveAnArmy = false;
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(tryingToMoveAnArmy == true)
            {
                if (WorldMapLoad.armies[int.Parse(SelectArmy.currentlySelectedArmyName)].isArmySelected == true)
                {
                    Debug.Log("Name of right clicked county: " + name);
                    WorldMapLoad.armies[int.Parse(SelectArmy.currentlySelectedArmyName)].startTimer = true;
                }
                else
                {
                    Debug.Log("No army is selected.");

                }
            }
            else
            {
                Debug.Log("Nothing is selected so right click does shit.");
            }


        }
    }
}
