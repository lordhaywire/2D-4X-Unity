using UnityEngine;

public class UIBuildCountyImprovementButton : MonoBehaviour
{
    [SerializeField] private GameObject notEnoughResourcesPanel;
    [SerializeField] private GameObject notEnoughAssignedWorkersPanel;
    [SerializeField] private GameObject areYouSurePanel;

    public bool enoughInfluence;

    public void BuildCountyImprovementButton()
    {

        CheckEnoughInfluence();
        if (UIBuildingChecker.instance.enoughPopulation == false && enoughInfluence == true)
        {
            notEnoughAssignedWorkersPanel.SetActive(true);
        }
        else if(UIBuildingChecker.instance.enoughPopulation == true && enoughInfluence == false)
        {
            notEnoughResourcesPanel.SetActive(true);
        }
        else if(UIBuildingChecker.instance.enoughPopulation == true && enoughInfluence == true)
        {
            areYouSurePanel.SetActive(true);
        }
    }

    private void CheckEnoughInfluence()
    {
        // Check for enough Influence.
        enoughInfluence = false;
        if (WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].influenceCost
            > WorldMapLoad.instance.factions[0].Influence)
        {
            notEnoughResourcesPanel.SetActive(true);
        }
        else
        {
            enoughInfluence = true;
        }
    }
}
