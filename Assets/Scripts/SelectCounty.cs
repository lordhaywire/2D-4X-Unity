using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCounty : MonoBehaviour, IPointerClickHandler
{
    public static bool hasAnArmyBeenSelected;

    // Left Click
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Select County Left Clicked.");
            WorldMapLoad.Instance.CurrentlySelectedCounty = name;
            Debug.Log("Currently Selected County: " + WorldMapLoad.Instance.CurrentlySelectedCounty);

            CloseDescriptionPanels();

            PanelChanges();

            FillCountyInfoPanel();

            RefreshBuildingsPanels();

            DeselectHeroOnCountyClick();

            DeselectArmyOnCountyLeftClick();

            //hasAnArmyBeenSelected = false; // Why the fuck is this here, if it is already being checked above?
            // We probably can delete this, but should do some more testing.
        }

        // Right Click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("County Name: " + name);
            WorldMapLoad.Instance.currentlyRightClickedCounty = name;

            HeroRightClickCounty();

            /*
            // Army movement isn't going to work because of this.  This is disabled until we get to fixing Army
            // Movement.
            {
                ArmyRightClickCounty();
            }
            */


        }
    }

    private void CloseDescriptionPanels()
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
        if (WorldMapLoad.Instance.populationDescriptionPanelOpen == true)
        {
            UIPopulationDescriptionPanel.Instance.gameObject.SetActive(false);
            WorldMapLoad.Instance.populationDescriptionPanelOpen = false;
        }
        if (WorldMapLoad.Instance.populationInfoPanelOpen == true)
        {
            UIPopulationInfoPanel.Instance.gameObject.SetActive(false);
            WorldMapLoad.Instance.populationInfoPanelOpen = false;
        }
    }

    private void PanelChanges()
    {
        WorldMapLoad.Instance.countyInfoPanel.SetActive(true);
        WorldMapLoad.Instance.heroInfoPanel.SetActive(false);
        WorldMapLoad.Instance.armyInfoPanel.SetActive(false);

        if (WorldMapLoad.Instance.playerFaction == WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name) //|| WorldMapLoad.Instance.DevView == true
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
        Debug.Log("We have gotten to the end of PanelChanges()");
    }

    private void HeroRightClickCounty()
    {
        var currentHero = WorldMapLoad.Instance.CurrentlySelectedHero;

        if (currentHero != null)
        {
            Debug.Log("Right Clicked on a county while a hero is selected.");
            currentHero.GetComponent<HeroMovement>().StartHeroMovement();
        }
        else
        {
            Debug.Log("Currently Selected Hero is null.");
        }
    }

    private void ArmyRightClickCounty()
    {
        Debug.Log("Has an army been selected? " + hasAnArmyBeenSelected);
        if (hasAnArmyBeenSelected == true)
        {
            var currentlySelectedArmy = WorldMapLoad.Instance.spawnedArmies[int.Parse(SelectArmy.currentlySelectedArmyName)];
            Debug.Log("Is army selected? " + currentlySelectedArmy.IsSelected);
            if (currentlySelectedArmy.IsSelected == true)
            {
                Debug.Log("Is Counting Down? " + currentlySelectedArmy.isCountingDown);
                if (currentlySelectedArmy.isCountingDown == false)
                {
                    currentlySelectedArmy.destination = name;
                    Debug.Log("Name of right clicked county: " + currentlySelectedArmy.destination);
                    currentlySelectedArmy.startTimer = true;
                }
                // This resets everything so the army can start counting down to move again.
                else
                {
                    Debug.Log("Movement has been reset.");
                    currentlySelectedArmy.startTimer = false;
                    currentlySelectedArmy.isCountingDown = false;
                    currentlySelectedArmy.destination = name;
                    currentlySelectedArmy.armyMovement.isTimeToDestinationSet = false;

                    currentlySelectedArmy.timerCanvasGameObject.SetActive(false);
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

    private void DeselectHeroOnCountyClick()
    {
        if (WorldMapLoad.Instance.CurrentlySelectedHero != null)
        {
            WorldMapLoad.Instance.CurrentlySelectedHero = null;
        }
        else
        {
            Debug.Log("Currently Selected Hero is null.");
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


    private void DeselectArmyOnCountyLeftClick()
    {
        // If an army has been selected and we left click on a county it clears the army of being selected.
        // The if statement is needed if there just in case there are no spawnedArmies created yet.
        if (hasAnArmyBeenSelected == true)
        {
            WorldMapLoad.Instance.spawnedArmies[int.Parse(SelectArmy.currentlySelectedArmyName)].IsSelected = false;

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
            WorldMapLoad.Instance.playerFaction)
        {
            CheckForHeroes(); // Check to see if this county has any heroes in it.
        }

        //CheckForArmies(); // Check to see if this county has any spawnedArmies in it.

        // This is just some temp bullshit to not allow you to look at counties you don't own.
        if (WorldMapLoad.Instance.playerFaction ==
            WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name)
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
        if (WorldMapLoad.Instance.spawnedArmies.Count != 0)
        {
            for (int i = 0; i < WorldMapLoad.Instance.spawnedArmies.Count; i++)
            {
                if (WorldMapLoad.Instance.spawnedArmies[i].location == name)
                {
                    UICountyPanel.Instance.armyScrollView.SetActive(true); // This sets the vertical gameobject group that is the list of heroes to active.
                    Debug.Log("Army Button Text: " + UIVerticalArmyList.armyButtonText);

                    UIVerticalArmyList.armyButtonText.text =
                        WorldMapLoad.Instance.spawnedArmies[0].name + " " + ": " + WorldMapLoad.Instance.spawnedArmies[0].size;
                }
                else
                {
                    UICountyPanel.Instance.armyScrollView.SetActive(false);
                    Debug.Log("There is no armies in this county.");
                }
            }
        }
    }

    private void CheckForHeroes()
    {
        var countyPopulation = WorldMapLoad.Instance.countyPopulationDictionary[name];
        int numberOfHeros = 0;

        for (int i = 0; i < countyPopulation.Count; i++)
        {
            //Debug.Log("Name: " + name);
            // This is going to need to turn off the Hero Scroll View at some point.
            if (countyPopulation[i].hero != null)
            {
                numberOfHeros++;
            }
        }

        if (numberOfHeros > 0)
        {
            UICountyPanel.Instance.heroScrollView.SetActive(true);
        }
        else
        {
            UICountyPanel.Instance.heroScrollView.SetActive(false);
        }

    }
}
