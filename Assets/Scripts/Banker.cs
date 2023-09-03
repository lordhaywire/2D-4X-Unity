using System.Collections.Generic;
using UnityEngine;

public class Banker : MonoBehaviour
{
    public static Banker Instance { get; private set; }

    [SerializeField] private ResourceSO foodSO;

    private void Awake()
    {
        Instance = this;
    }
    public bool CheckBuildingCost(Faction faction, BuildingInfo buildingInfo)
    {
        return faction.influence >= buildingInfo.influenceCost;
    }

    public bool CheckEnoughFood(Faction faction)
    {
        return faction.food >= Globals.Instance.minimumFoodAI;
    }

    public GameObject FindFoodBuilding(GameObject faction)
    {
        List<County> counties = faction.GetComponent<FactionAI>().countiesFactionOwns;

        for (int i = 0; i < counties.Count; i++)
        {
            Transform possibleBuildingsParent = counties[i].gameObject.GetComponent<CountyInfo>().possibleBuildingsParent;

            for (int j = 0; j < possibleBuildingsParent.childCount; j++)
            {
                BuildingInfo buildingInfo = possibleBuildingsParent.GetChild(j).GetComponent<BuildingInfo>();
                if(buildingInfo.isBeingBuilt == true || buildingInfo.isBuilt == true)
                {
                    Debug.Log(buildingInfo.buildingName + " is being built or built.");

                }
                else
                {
                    if (buildingInfo.resourceSO.name == foodSO.name)
                    {
                        GameObject foodBuilding = possibleBuildingsParent.GetChild(j).gameObject;
                        Debug.Log($"Found {foodBuilding.name} in {counties[i].gameObject.name}");

                        return foodBuilding;
                    }
                }
            }
        }
        Debug.Log("No food building found.");
        return null;
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
