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

            UICountyPanel.heroInfoList.SetActive(false);

            currentlySelectedCounty = name;
            Debug.Log("Currently Selected County: " + currentlySelectedCounty);

            UICountyPanel.countyOwnerText.text = "Owner: " + WorldMapLoad.counties[name].factionName;
            UICountyPanel.countyNameText.text = "County: " + name;

            Debug.Log("County Faction Name: " + WorldMapLoad.counties[name].factionName);
            if (WorldMapLoad.counties[name].factionName == 
                WorldMapLoad.playerFaction || WorldMapLoad.canSeeCountyInfo == true)
            {
                //Debug.Log("County Faction Name: " + WorldMapLoad.counties[name].factionName);
                CheckForHeroes(); // Check to see if this county has any heroes in it.
            }
            
            CheckForArmies(); // Check to see if this county has any armies in it.

            // If an army has been selected and we left click on a county it clears the army of being selected.
            if (hasAnArmyBeenSelected == true)
            {
                WorldMapLoad.armies[int.Parse(SelectArmy.currentlySelectedArmyName)].IsArmySelected = false;

                hasAnArmyBeenSelected = false;
            }
            else
            {
                //Debug.Log("No army has been selected so this is not clearing the selected army variable.");
            }

            // This is just some temp bullshit to not allow you to look at counties you don't own.
            if (WorldMapLoad.playerFaction == 
                WorldMapLoad.counties[name].factionName || WorldMapLoad.canSeeCountyInfo == true)
            {
                UICountyPanel.countyPopulationText.text = "Population: " + WorldMapLoad.counties[name].population.ToString();
            }
            else
            {
                UICountyPanel.countyPopulationText.text = "Population: Unknown";
            }
            //Debug.Log("Name of Province: " + name);

            hasAnArmyBeenSelected = false; // Why the fuck is this here, if it is already being checked above?
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

    private void CheckForArmies()
    {
        if (WorldMapLoad.armies.Count != 0)
        {
            for (int i = 0; i < WorldMapLoad.armies.Count; i++)
            {
                if (WorldMapLoad.armies[i].location == name)
                {
                    UICountyPanel.armyInfoList.SetActive(true); // This sets the vertical gameobject group that is the list of heroes to active.
                    Debug.Log("Army Button Text: " + UIVerticalArmyList.armyButtonText);

                    UIVerticalArmyList.armyButtonText.text =
                        WorldMapLoad.armies[0].name + " " + ": " + WorldMapLoad.armies[0].size;
                }
                else
                {
                    UICountyPanel.armyInfoList.SetActive(false);
                    Debug.Log("There is no armies in this county.");
                }
            }
        }
    }

    private void CheckForHeroes()
    {
        var countyFaction = WorldMapLoad.counties[name].factionName;
        var localHeroes = WorldMapLoad.factionHeroesDictionary[countyFaction];
        for (int i = 0; i < localHeroes.Count; i++)
        {
            Debug.Log("Name: " + name);
            if (localHeroes[i].location == name || WorldMapLoad.canSeeCountyInfo == true)
            {
                UICountyPanel.heroInfoList.SetActive(true); // This sets the vertical gameobject group that is the list of heroes to active.

                //Debug.Log("Leader Button Text: " + UIVerticalHeroList.leaderButtonText);
                if (localHeroes[i].activity == null)
                {
                    //Debug.Log("Leader Button Text: " + UIVerticalHeroList.leaderButtonText);
                    UIVerticalHeroList.leaderButtonText.text = localHeroes[i].firstName + " " + localHeroes[i].lastName + ": Doing Nothing!";
                }
                else
                {
                    UIVerticalHeroList.leaderButtonText.text =
                        localHeroes[i].firstName + " " + localHeroes[i].lastName + ": " + localHeroes[i].activity;
                }
            }
        }
    }
}
