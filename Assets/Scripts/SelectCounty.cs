using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCounty : MonoBehaviour, IPointerClickHandler
{
    public static string currentlySelectedCounty;
    public static bool hasAnArmyBeenSelected;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WorldMapLoad.countyInfoPanel.SetActive(true);
            WorldMapLoad.armyInfoPanel.SetActive(false);

            if (hasAnArmyBeenSelected == true)
            {
                WorldMapLoad.armies[int.Parse(SelectArmy.currentlySelectedArmyName)].IsArmySelected = false;

                hasAnArmyBeenSelected = false;
            }
            else
            {
                Debug.Log("No army has been selected so this is not clearing the selected army shit.");
            }


            currentlySelectedCounty = name;
            Debug.Log("Currently Selected Province: " + currentlySelectedCounty);

            UIProvincePanel.countyOwnerText.text = "Owner: " + WorldMapLoad.counties[name].ownerName;
            UIProvincePanel.countyNameText.text = "Province: " + name;

            // This is just some temp bullshit to not allow you to look at counties you don't own.
            if (WorldMapLoad.playerName == WorldMapLoad.counties[name].ownerName)
            {
                UIProvincePanel.countyPopulationText.text = "Population: " + WorldMapLoad.counties[name].population.ToString();
            }
            else
            {
                UIProvincePanel.countyPopulationText.text = "Population: Unknown";
            }
            Debug.Log("Name of Province: " + name);

            hasAnArmyBeenSelected = false;
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Has an army been selected? " + hasAnArmyBeenSelected);
            if (hasAnArmyBeenSelected == true)
            {
                var currentlySelectedArmy = WorldMapLoad.armies[int.Parse(SelectArmy.currentlySelectedArmyName)];
                Debug.Log("Is army selected? " + currentlySelectedArmy.IsArmySelected);
                if (currentlySelectedArmy.IsArmySelected == true)
                {
                    Debug.Log("Is Counting Down? " + currentlySelectedArmy.isCountingDown);
                    if (currentlySelectedArmy.isCountingDown == false)
                    {
                        currentlySelectedArmy.armyDestination = name;
                        Debug.Log("Name of right clicked county: " + currentlySelectedArmy.armyDestination);
                        currentlySelectedArmy.startTimer = true;
                    }
                    // This resets everything so the army can start counting down to move again.
                    else
                    {
                        Debug.Log("Movement has been reset.");
                        currentlySelectedArmy.startTimer = false;
                        currentlySelectedArmy.isCountingDown = false;
                        currentlySelectedArmy.armyDestination = name;
                        currentlySelectedArmy.armyMovement.isTimeToDestinationSet = false;

                        currentlySelectedArmy.armyTimerCanvasGameObject.SetActive(false);
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
