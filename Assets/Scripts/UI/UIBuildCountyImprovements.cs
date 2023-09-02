using UnityEngine;

public class UIBuildCountyImprovements : MonoBehaviour
{
    [SerializeField] private Banker banker;
    [SerializeField] private GameObject notEnoughResourcesPanel;
    [SerializeField] private GameObject notEnoughAssignedWorkersPanel;
    [SerializeField] private GameObject areYouSurePanel;

    public bool enoughInfluence;

    public void BuildButton()
    {
        Faction faction = WorldMapLoad.Instance.playerFaction;
        BuildingInfo buildingInfo = WorldMapLoad.Instance.currentlySelectedBuilding.GetComponent<BuildingInfo>();
        if(banker.CheckBuildingCost(faction, buildingInfo) == true)
        {
            Debug.Log("There is enough influence.");
            //notEnoughAssignedWorkersPanel.SetActive(true);
        }
        else
        {
            Debug.Log("There is not enough influence.");
            notEnoughResourcesPanel.SetActive(true);
        }
        

        /*
        if (UIBuildingChecker.Instance.enoughPopulation == false && enoughInfluence == true)
        {
            
        }
        else if(UIBuildingChecker.Instance.enoughPopulation == true && enoughInfluence == false)
        {
            
        }
        else if(UIBuildingChecker.Instance.enoughPopulation == true && enoughInfluence == true)
        {
            areYouSurePanel.SetActive(true);
        }
        */
    }

    private void CheckEnoughInfluence()
    {
        Debug.Log("UIBuildCountyImprovementButton CheckEnoughInfluence()");
        /*
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
        */
    }
}
