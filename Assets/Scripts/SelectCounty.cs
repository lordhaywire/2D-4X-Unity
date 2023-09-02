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
            //UICurrentBuildingDescriptionPanel.Instance.gameObject.SetActive(false);
            WorldMapLoad.Instance.possibleBuildingDescriptionPanelExpanded = false;
        }
        if (WorldMapLoad.Instance.possibleBuildingDescriptionPanelExpanded == true)
        {
            //UIPossibleBuildingDescriptionPanel.Instance.gameObject.SetActive(false);
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
        UIPlayerUI.Instance.countyInfoPanel.SetActive(true);

        WorldMapLoad.Instance.heroesAndArmiesVerticalGroup.SetActive(true);

        if (WorldMapLoad.Instance.playerFaction.factionNameAndColor.name
            == WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name)
        {
            if (UICountyInfoPanel.Instance.buildingsPanelExpanded == false)
            {
                //UIExpandBuildingsButton.Instance.expandBuildingButtonGameObject.SetActive(true);
            }
        }
        else
        {
            if (UICountyInfoPanel.Instance.buildingsPanelExpanded == true)
            {
                //UIPossibleBuildingsPanel.Instance.gameObject.SetActive(false);
                //UICurrentBuildingsPanel.Instance.gameObject.SetActive(false);
            }
            //UIExpandBuildingsButton.Instance.expandBuildingButtonGameObject.SetActive(false);
            UICountyInfoPanel.Instance.buildingsPanelExpanded = false;
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
        if(UIBuildingsPanel.Instance != null)
        {
            UIBuildingsPanel.Instance.PanelRefresher();
        }   
    }

    private void FillCountyInfoPanel()
    {
        UICountyInfoPanel.Instance.countyOwnerText.text = "Owner: " + WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name;
        UICountyInfoPanel.Instance.countyNameText.text = "County: " + name;

        // This is just some temp bullshit to not allow you to look at counties you don't own.
        if (WorldMapLoad.Instance.playerFaction.factionNameAndColor.name ==
            WorldMapLoad.Instance.counties[name].faction.factionNameAndColor.name)
        {
            UICountyInfoPanel.Instance.countyPopulationText.text =
                "Population: " + WorldMapLoad.Instance.counties[name].population.ToString();
        }
        else
        {
            UICountyInfoPanel.Instance.countyPopulationText.text = "Population: Unknown";
        }
    }
}
