using UnityEngine;

public class Banker : MonoBehaviour
{
    public static Banker Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DeductCostOfBuilding()
    {
        WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].Influence 
            -= WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyInfo>().county
            .possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber].influenceCost;
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
