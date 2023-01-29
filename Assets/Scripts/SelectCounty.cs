using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCounty : MonoBehaviour, IPointerClickHandler
{
    public static string currentlySelectedCounty;
    public static bool tryingToMoveAnArmy;
    //public static string armyDestination;
    //public float speed;
    //public float smoothTime = 0.3f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WorldMapLoad.countyInfoPanel.SetActive(true);
            WorldMapLoad.armyInfoPanel.SetActive(false);

            currentlySelectedCounty = name;
            Debug.Log("Currently Selected Province: " + currentlySelectedCounty);

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
                    if(WorldMapLoad.armies[int.Parse(SelectArmy.currentlySelectedArmyName)].isCountingDown == false)
                    {
                        WorldMapLoad.armies[int.Parse(SelectArmy.currentlySelectedArmyName)].armyDestination = name;
                        Debug.Log("Name of right clicked county: " + WorldMapLoad.armies[int.Parse(SelectArmy.currentlySelectedArmyName)].armyDestination);
                        WorldMapLoad.armies[int.Parse(SelectArmy.currentlySelectedArmyName)].startTimer = true;
                    }
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
