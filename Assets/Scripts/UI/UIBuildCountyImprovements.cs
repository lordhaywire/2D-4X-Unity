using UnityEngine;

public class UIBuildCountyImprovements : MonoBehaviour
{
    //[SerializeField] private Banker banker;
    [SerializeField] private GameObject notEnoughResourcesPanel;
    //[SerializeField] private GameObject notEnoughAssignedWorkersPanel;
    [SerializeField] private GameObject areYouSurePanel;
    private Faction faction;
    private BuildingInfo buildingInfo;
    private BuildImprovements buildImprovements;
    //public bool enoughInfluence;


    private void Awake()
    {
        faction = WorldMapLoad.Instance.playerFaction;
    }
    public void YesConfirmButton()
    {
        buildImprovements = buildingInfo.county.buildImprovements;

        buildImprovements.BuildBuilding(faction, buildingInfo.gameObject);
        UIBuildingsPanel.Instance.PanelRefresher();

    }
    public void BuildButton()
    {
        buildingInfo = WorldMapLoad.Instance.currentlySelectedBuilding.GetComponent<BuildingInfo>();
        if (Banker.Instance.CheckBuildingCost(faction, buildingInfo) == true
            && Banker.Instance.CheckEnoughIdleWorkers(buildingInfo) == true
            && Banker.Instance.CheckForWorkersAssigned(buildingInfo) == true)
        {
            Debug.Log("There is enough influence, workers assigned, and idle workers.");
            areYouSurePanel.SetActive(true);
        }
        else
        {
            Debug.Log("You don't have enough influence, workers or idle workers.");
            notEnoughResourcesPanel.SetActive(true);
        }
    }
}
