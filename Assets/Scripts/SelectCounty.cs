using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCounty : MonoBehaviour, IPointerClickHandler
{
    public static bool hasAnArmyBeenSelected;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        { 
            WorldMapLoad.instance.currentlySelectedCounty = name;

            PanelChanges();

            FillCountyInfoPanel();

            DeselectArmyOnCountyLeftClick();

            //hasAnArmyBeenSelected = false; // Why the fuck is this here, if it is already being checked above?
            // We probably can delete this, but should do some more testing.
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ArmyRightClickCounty();
        }

    }

    private void PanelChanges()
    {
        WorldMapLoad.instance.countyInfoPanel.SetActive(true);
        WorldMapLoad.instance.armyInfoPanel.SetActive(false);

        UICountyPanel.instance.heroInfoList.SetActive(false); // This was some bullshit.  This makes it so that onEnable
                                                              // resets the HeroInfoList.

        if (WorldMapLoad.instance.playerFaction == WorldMapLoad.instance.counties[name].faction.name)
        {
            UIMusterArmyButton.instance.musterArmyButtonGameObject.SetActive(true);
            UIExpandBuildingsPanel.instance.expandBuildingButtonGameObject.SetActive(true);


                    
        }
        else
        {
            UIMusterArmyButton.instance.musterArmyButtonGameObject.SetActive(false);
            UIExpandBuildingsPanel.instance.expandBuildingButtonGameObject.SetActive(false);
        }
    }

    private void ArmyRightClickCounty()
    {
        Debug.Log("Has an army been selected? " + hasAnArmyBeenSelected);
        if (hasAnArmyBeenSelected == true)
        {
            var currentlySelectedArmy = WorldMapLoad.instance.armies[int.Parse(SelectArmy.currentlySelectedArmyName)];
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

    private void DeselectArmyOnCountyLeftClick()
    {
        // If an army has been selected and we left click on a county it clears the army of being selected.
        // The if statement is needed if there just in case there are no armies created yet.
        if (hasAnArmyBeenSelected == true)
        {
            WorldMapLoad.instance.armies[int.Parse(SelectArmy.currentlySelectedArmyName)].IsArmySelected = false;

            hasAnArmyBeenSelected = false;
        }
        else
        {
            //Debug.Log("No army has been selected so this is not clearing the selected army variable.");
        }
    }

    private void FillCountyInfoPanel()
    {
        UICountyPanel.instance.countyOwnerText.text = "Owner: " + WorldMapLoad.instance.counties[name].faction.name;
        UICountyPanel.instance.countyNameText.text = "County: " + name;

        // This is just some temp bullshit to not allow you to look at counties you don't own.
        //Debug.Log("County Faction : " + WorldMapLoad.instance.counties[name].faction.name);
        //Debug.Log("Player Faction : " + WorldMapLoad.instance.playerFaction);
        //Debug.Log("Can See County Info? " + WorldMapLoad.instance.DevView);
        if (WorldMapLoad.instance.counties[name].faction.name ==
            WorldMapLoad.instance.playerFaction || WorldMapLoad.instance.DevView == true)
        {
            CheckForHeroes(); // Check to see if this county has any heroes in it.
        }

        CheckForArmies(); // Check to see if this county has any armies in it.

        // This is just some temp bullshit to not allow you to look at counties you don't own.
        if (WorldMapLoad.instance.playerFaction ==
            WorldMapLoad.instance.counties[name].faction.name || WorldMapLoad.instance.DevView == true)
        {
            UICountyPanel.instance.countyPopulationText.text =
                "Population: " + WorldMapLoad.instance.counties[name].population.ToString();
        }
        else
        {
            UICountyPanel.instance.countyPopulationText.text = "Population: Unknown";
        }
        //Debug.Log("Name of Province: " + name);
    }

    private void CheckForArmies()
    {
        if (WorldMapLoad.instance.armies.Count != 0)
        {
            for (int i = 0; i < WorldMapLoad.instance.armies.Count; i++)
            {
                if (WorldMapLoad.instance.armies[i].location == name)
                {
                    UICountyPanel.instance.armyInfoList.SetActive(true); // This sets the vertical gameobject group that is the list of heroes to active.
                    Debug.Log("Army Button Text: " + UIVerticalArmyList.armyButtonText);

                    UIVerticalArmyList.armyButtonText.text =
                        WorldMapLoad.instance.armies[0].name + " " + ": " + WorldMapLoad.instance.armies[0].size;
                }
                else
                {
                    UICountyPanel.instance.armyInfoList.SetActive(false);
                    Debug.Log("There is no armies in this county.");
                }
            }
        }
    }

    private void CheckForHeroes()
    {
        var countyFaction = WorldMapLoad.instance.counties[name].faction.name;
        var localHeroes = WorldMapLoad.instance.factionHeroesDictionary[countyFaction];
        for (int i = 0; i < localHeroes.Count; i++)
        {
            //Debug.Log("Name: " + name);
            if (localHeroes[i].location == name || WorldMapLoad.instance.DevView == true)
            {
                UICountyPanel.instance.heroInfoList.SetActive(true); // This sets the vertical gameobject group that is the list of heroes to active.

                //Debug.Log("Leader Button Text: " + UIVerticalHeroList.leaderButtonText);
                if (localHeroes[i].currentActivity == null)
                {
                    //Debug.Log("Leader Button Text: " + UIVerticalHeroList.leaderButtonText);
                    UIVerticalHeroList.leaderButtonText.text = localHeroes[i].firstName + " " +
                        localHeroes[i].lastName + ": Something is fucked up!";
                }
                else
                {
                    UIVerticalHeroList.leaderButtonText.text =
                        localHeroes[i].firstName + " " + localHeroes[i].lastName + ": " + localHeroes[i].currentActivity;
                }
            }
        }
    }
}
