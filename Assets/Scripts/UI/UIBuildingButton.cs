using TMPro;
using UnityEngine;

public class UIBuildingButton : MonoBehaviour
{
    public TextMeshProUGUI buildingNameText;
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
            //UICurrentBuildingDescriptionPanel.Instance.PanelRefresh();
        }

        
    }
}
