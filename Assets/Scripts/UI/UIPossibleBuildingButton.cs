using UnityEngine;

public class UIPossibleBuildingButton : MonoBehaviour
{
    public void PossibleBuildingButton()
    {
        UIPossibleBuildingsPanel.instance.buildingDescriptionPanel.SetActive(true);
        UIPossibleBuildingsPanel.instance.PossibleBuildingButtonClicked = int.Parse(name);
    }
}
