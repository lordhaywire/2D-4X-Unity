using UnityEngine;

public class Banker : MonoBehaviour
{

    public bool CheckEnoughFood(Faction faction)
    {
        return faction.food >= Globals.Instance.minimumFood;
    }

    public bool CheckBuildingCost(Faction faction, BuildingInfo buildingInfo)
    {
        return faction.influence >= buildingInfo.influenceCost;
    }
    public void DeductCostOfBuilding()
    {
        Debug.Log("Banker.cs DeductCostOfBuilding()");
        /*
        WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].Influence 
            -= WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyInfo>().county
            .possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber].GetComponent<BuildingInfo>().influenceCost;
        */
    }
    public void RemoveCostOfHero()
    {
        if (WorldMapLoad.Instance.costOfHero > WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].Influence)
        {
            UIPopulationDescriptionPanel.Instance.notEnoughResourcesPanel.SetActive(true);
        }
        else
        {
            WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].Influence -= WorldMapLoad.Instance.costOfHero;
        }
    }
}
