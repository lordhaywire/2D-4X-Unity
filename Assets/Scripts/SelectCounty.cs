using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCounty : MonoBehaviour, IPointerClickHandler
{
    public static bool hasAnArmyBeenSelected;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WorldMapLoad.Instance.currentlySelectedCounty = name;

            CloseBuildingDescriptionPanels();

            PanelChanges();

            FillCountyInfoPanel();

            RefreshBuildingsPanels();

            DeselectArmyOnCountyLeftClick();

            //hasAnArmyBeenSelected = false; // Why the fuck is this here, if it is already being checked above?
            // We probably can delete this, but should do some more testing.
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ArmyRightClickCounty();
        }
    }

    private void CloseBuildingDescriptionPanels()
    {
        if (WorldMapLoad.Instance.currentBuildingDescriptionPanelExpanded == true)
        {
            UICurrentBuildingDescriptionPanel.Instance.gameObject.SetActive(false);
            WorldMapLoad.Instance.possibleBuildingDescriptionPanelExpanded = false;
        }
        if (WorldMapLoad.Instance.possibleBuildingDescriptionPanelExpanded == true)
        {
            UIPossibleBuildingDescriptionPanel.Instance.gameObject.SetActive(false);
            WorldMapLoad.Instance.possibleBuildingDescriptionPanelExpanded = false;
        }
    }

    // Is this supposed to be an event that UIBuildingPanelsRefresher subscribes to and refreshes when it is triggered?
    private void RefreshBuildingsPanels()
    {
        UIBuildingPanelsRefresher.Instance.CurrentBuildingPanelsDestroyer();
        UIBuildingPanelsRefresher.Instance.CurrentBuildingPanelsRefresher();

        UIBuildingPanelsRefresher.Instance.PossibleBuildingsPanelsDestroyer();
        UIBuildingPanelsRefresher.Instance.PossibleBuildingPanelsRefresher();
    }

    private void PanelChanges()
    {
        WorldMapLoad.Instance.countyInfoPanel.SetActive(true);
        WorldMapLoad.Instance.armyInfoPanel.SetActive(false);
        UICountyPanel.Instance.heroInfoList.SetActive(false); // This was some bullshit.  This makes it so that onEnable
                                                              // resets the HeroInfoList.

        if (WorldMapLoad.Instance.playerFaction == WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name
            || WorldMapLoad.Instance.DevView == true)
        {
            UIMusterArmyButton.Instance.musterArmyButtonGameObject.SetActive(true);
            if (UICountyPanel.Instance.buildingsPanelExpanded == false)
            {
                UIExpandBuildingsButton.Instance.expandBuildingButtonGameObject.SetActive(true);
            }
        }
        else
        {
            if (UICountyPanel.Instance.buildingsPanelExpanded == true)
            {
                UIPossibleBuildingsPanel.Instance.gameObject.SetActive(false);
                UICurrentBuildingsPanel.Instance.gameObject.SetActive(false);
            }

            UIMusterArmyButton.Instance.musterArmyButtonGameObject.SetActive(false);
            UIExpandBuildingsButton.Instance.expandBuildingButtonGameObject.SetActive(false);
            UICountyPanel.Instance.buildingsPanelExpanded = false;
        }
    }

    private void ArmyRightClickCounty()
    {
        Debug.Log("Has an army been selected? " + hasAnArmyBeenSelected);
        if (hasAnArmyBeenSelected == true)
        {
            var currentlySelectedArmy = WorldMapLoad.Instance.armies[int.Parse(SelectArmy.currentlySelectedArmyName)];
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
            WorldMapLoad.Instance.armies[int.Parse(SelectArmy.currentlySelectedArmyName)].IsArmySelected = false;

            hasAnArmyBeenSelected = false;
        }
        else
        {
            //Debug.Log("No army has been selected so this is not clearing the selected army variable.");
        }
    }

    private void FillCountyInfoPanel()
    {
        UICountyPanel.Instance.countyOwnerText.text = "Owner: " + WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name;
        UICountyPanel.Instance.countyNameText.text = "County: " + name;

        // This is just some temp bullshit to not allow you to look at counties you don't own.
        //Debug.Log("County Faction : " + WorldMapLoad.Instance.counties[name].faction.name);
        //Debug.Log("Player Faction : " + WorldMapLoad.Instance.playerFaction);
        //Debug.Log("Can See County Info? " + WorldMapLoad.Instance.DevView);
        if (WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name ==
            WorldMapLoad.Instance.playerFaction || WorldMapLoad.Instance.DevView == true)
        {
            CheckForHeroes(); // Check to see if this county has any heroes in it.
        }

        CheckForArmies(); // Check to see if this county has any armies in it.

        // This is just some temp bullshit to not allow you to look at counties you don't own.
        if (WorldMapLoad.Instance.playerFaction ==
            WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name || WorldMapLoad.Instance.DevView == true)
        {
            UICountyPanel.Instance.countyPopulationText.text =
                "Population: " + WorldMapLoad.Instance.counties[name].population.ToString();
        }
        else
        {
            UICountyPanel.Instance.countyPopulationText.text = "Population: Unknown";
        }
        //Debug.Log("Name of Province: " + name);
    }

    private void CheckForArmies()
    {
        if (WorldMapLoad.Instance.armies.Count != 0)
        {
            for (int i = 0; i < WorldMapLoad.Instance.armies.Count; i++)
            {
                if (WorldMapLoad.Instance.armies[i].location == name)
                {
                    UICountyPanel.Instance.armyInfoList.SetActive(true); // This sets the vertical gameobject group that is the list of heroes to active.
                    Debug.Log("Army Button Text: " + UIVerticalArmyList.armyButtonText);

                    UIVerticalArmyList.armyButtonText.text =
                        WorldMapLoad.Instance.armies[0].name + " " + ": " + WorldMapLoad.Instance.armies[0].size;
                }
                else
                {
                    UICountyPanel.Instance.armyInfoList.SetActive(false);
                    Debug.Log("There is no armies in this county.");
                }
            }
        }
    }

    private void CheckForHeroes()
    {
        var countyPopulation = WorldMapLoad.Instance.countyPopulationDictionary[name];
        //var localHeroes = WorldMapLoad.Instance.factionHeroesDictionary[countyFaction];
        for (int i = 0; i < countyPopulation.Count; i++)
        {
            //Debug.Log("Name: " + name);
            if (countyPopulation[i].isHero == true) // || WorldMapLoad.Instance.DevView == true)
            {
                UICountyPanel.Instance.heroInfoList.SetActive(true); // This sets the vertical gameobject group that is the list of heroes to active.
                UIVerticalHeroList.leaderButtonText.text =
                    countyPopulation[i].firstName + " " + countyPopulation[i].lastName + ": " + countyPopulation[i].currentActivity;
                /*
                if (localHeroes[i].currentActivity == null)
                {

                    UIVerticalHeroList.leaderButtonText.text = localHeroes[i].firstName + " " +
                        localHeroes[i].lastName + ": Something is fucked up!";
                }
                else
                */



            }
        }
    }
}
