using UnityEngine;

public class UIBuildCountyImprovements : MonoBehaviour
{
    //[SerializeField] private Banker banker;
    [SerializeField] private GameObject notEnoughResourcesPanel;
    [SerializeField] private GameObject notEnoughAssignedWorkersPanel;
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
        if (Banker.Instance.CheckBuildingCost(faction, buildingInfo) == true)
        {
            Debug.Log("There is enough influence.");
            if (Banker.Instance.CheckForWorkers(buildingInfo) == true)
            {
                Debug.Log("You have enough workers.");
                areYouSurePanel.SetActive(true);
            }
            else
            {
                Debug.Log("You don't have enough workers.");
                notEnoughAssignedWorkersPanel.SetActive(true);
            }
        }
        else
        {
            Debug.Log("There is not enough influence.");
            notEnoughResourcesPanel.SetActive(true);
        }

        //notEnoughAssignedWorkersPanel.SetActive(true);
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
}
