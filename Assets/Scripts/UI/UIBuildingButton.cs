using TMPro;
using UnityEngine;

public class UIBuildingButton : MonoBehaviour
{
    public TextMeshProUGUI buildingNameText;
    public GameObject underConstructionGameObject;
    public GameObject completedTextGameObject;
    public GameObject actualBuilding;

    public void BuildingButton()
    {
        BuildingInfo buildingInfo = actualBuilding.GetComponent<BuildingInfo>();
        WorldMapLoad.Instance.currentlySelectedBuilding = actualBuilding;
        if(buildingInfo.isBeingBuilt == false || buildingInfo.isBuilt == false)
        {
            UIBuildingsPanel.Instance.possibleBuildingDescriptionPanel.SetActive(true);
            UIPossibleBuildingDescriptionPanel.Instance.PanelRefresh();
        }
        else
        {
            UIBuildingsPanel.Instance.currentBuildingDescriptionPanel.SetActive(true);

            // This needs to be uncommented when we fix the Current Building Description.
            //UICurrentBuildingDescriptionPanel.Instance.PanelRefresh();
        }
    }
}
