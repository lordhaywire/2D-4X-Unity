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
            WorldMapLoad.Instance.currentlySelectedCounty = name;

            CloseDescriptionPanels();

            PanelChanges();

            FillCountyInfoPanel();

            RefreshBuildingsPanels();

            DeselectArmyOnCountyLeftClick();

            //hasAnArmyBeenSelected = false; // Why the fuck is this here, if it is already being checked above?
            // We probably can delete this, but should do some more testing.
        }

        // Right Click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            WorldMapLoad.Instance.selectedHerosDestination = name;
            ArmyRightClickCounty();
            HeroRightClickCounty();
        }
    }

    private void HeroRightClickCounty()
    {
        
        if(WorldMapLoad.Instance.currentlySelectedHero != null)
        {
            WorldMapLoad.Instance.currentlySelectedHero.GetComponent<HeroMovement>().StartHeroMovement();
        }
        else
        {
            Debug.Log("Currently Selected Hero is null.");
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
        if(WorldMapLoad.Instance.populationDescriptionPanelOpen == true)
        {
            UIPopulationDescriptionPanel.Instance.gameObject.SetActive(false);
            WorldMapLoad.Instance.populationDescriptionPanelOpen = false;
        }
        if(WorldMapLoad.Instance.populationInfoPanelOpen == true)
        {
            UIPopulationInfoPanel.Instance.gameObject.SetActive(false);
            WorldMapLoad.Instance.populationInfoPanelOpen = false;
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
        UICountyPanel.Instance.heroScrollView.SetActive(false); // This was some bullshit.  This makes it so that onEnable
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
                    UICountyPanel.Instance.armyScrollView.SetActive(true); // This sets the vertical gameobject group that is the list of heroes to active.
                    Debug.Log("Army Button Text: " + UIVerticalArmyList.armyButtonText);

                    UIVerticalArmyList.armyButtonText.text =
                        WorldMapLoad.Instance.armies[0].name + " " + ": " + WorldMapLoad.Instance.armies[0].size;
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
            if (countyPopulation[i].isHero == true)
            {
                numberOfHeros++;
            }
        }

        if(numberOfHeros > 0 )
        {
            UICountyPanel.Instance.heroScrollView.SetActive(true);
        }
        else
        {
            UICountyPanel.Instance.heroScrollView.SetActive(false);
        }

    }
}
