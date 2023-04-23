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
        if (UIBuildingChecker.Instance.enoughPopulation == false && enoughInfluence == true)
        {
            notEnoughAssignedWorkersPanel.SetActive(true);
        }
        else if(UIBuildingChecker.Instance.enoughPopulation == true && enoughInfluence == false)
        {
            notEnoughResourcesPanel.SetActive(true);
        }
        else if(UIBuildingChecker.Instance.enoughPopulation == true && enoughInfluence == true)
        {
            areYouSurePanel.SetActive(true);
        }
    }

    private void CheckEnoughInfluence()
    {
        // Check for enough Influence.
        enoughInfluence = false;
        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber].influenceCost
            > WorldMapLoad.Instance.factions[0].Influence)
        {
            notEnoughResourcesPanel.SetActive(true);
        }
        else
        {
            enoughInfluence = true;
        }
    }
}
