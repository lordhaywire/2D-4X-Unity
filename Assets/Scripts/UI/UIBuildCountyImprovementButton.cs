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
        enoughInfluence = false;
        if (WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyInfo>().county
            .possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber].GetComponent<BuildingInfo>().influenceCost
            > WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].Influence)
        {
            notEnoughResourcesPanel.SetActive(true);
        }
        else
        {
            enoughInfluence = true;
        }
    }
}
