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
            WorldMapLoad.Instance.CurrentlySelectedCounty = gameObject;

            CloseDescriptionPanels();

            PanelChanges();

            FillCountyInfoPanel();

            RefreshBuildingsPanels();

            DeselectHeroOnCountyClick();

            //DeselectArmyOnCountyLeftClick();
        }

        // Right Click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            WorldMapLoad.Instance.currentlyRightClickedCounty = gameObject;

            if (WorldMapLoad.Instance.CurrentlySelectedToken != null)
            {
                GameObject heroDestination
                    = WorldMapLoad.Instance.CurrentlySelectedToken.GetComponent<TokenInfo>().countyPopulation.destination;

                if (heroDestination == null)
                {
                    // Sets token destination in hero.
                    WorldMapLoad.Instance.CurrentlySelectedToken.GetComponent<TokenInfo>().countyPopulation.destination
                        = WorldMapLoad.Instance.currentlyRightClickedCounty;

                    TokenMoveToCounty();
                }
                else
                {
                    // Return token to their starting location if the player clicks on a county that isn't the destination.
                    if (WorldMapLoad.Instance.CurrentlySelectedToken.GetComponent<TokenMovement>().Move == true
                        && WorldMapLoad.Instance.currentlyRightClickedCounty.GetComponent<CountyInfo>().tokenSpawn
                        != heroDestination.GetComponent<CountyInfo>().tokenSpawn)
                    {

                        WorldMapLoad.Instance.CurrentlySelectedToken.GetComponent<TokenInfo>().countyPopulation.destination
                            = WorldMapLoad.Instance.CurrentlySelectedToken.GetComponent<TokenInfo>().countyPopulation.location;
                    }
                }
            }
        }
    }

    private void TokenMoveToCounty()
    {
        WorldMapLoad.Instance.CurrentlySelectedToken.GetComponent<TokenMovement>().Move = true;
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

        UIPlayerUI.Instance.populationListPanel.SetActive(false);

    }

    private void PanelChanges()
    {
        WorldMapLoad.Instance.countyInfoPanel.SetActive(true);
        WorldMapLoad.Instance.heroInfoPanel.SetActive(false);
        WorldMapLoad.Instance.armyInfoPanel.SetActive(false);

        WorldMapLoad.Instance.heroesAndArmiesVerticalGroup.SetActive(true);

        if (WorldMapLoad.Instance.playerFaction.factionNameAndColor.name
            == WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name)
        {
            //UIMusterArmyButton.Instance.musterArmyButtonGameObject.SetActive(true);


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

    private void DeselectHeroOnCountyClick()
    {
        if (WorldMapLoad.Instance.CurrentlySelectedToken != null)
        {
            WorldMapLoad.Instance.CurrentlySelectedToken = null;
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

    private void FillCountyInfoPanel()
    {
        UICountyPanel.Instance.countyOwnerText.text = "Owner: " + WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name;
        UICountyPanel.Instance.countyNameText.text = "County: " + name;

        // This is just some temp bullshit to not allow you to look at counties you don't own.
        if (WorldMapLoad.Instance.playerFaction.factionNameAndColor.name ==
            WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name)
        {
            UICountyPanel.Instance.countyPopulationText.text =
                "Population: " + WorldMapLoad.Instance.counties[name].population.ToString();
        }
        else
        {
            UICountyPanel.Instance.countyPopulationText.text = "Population: Unknown";
        }
    }

    /*
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
*/

    /*
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
    */

    /*
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
    */


}
