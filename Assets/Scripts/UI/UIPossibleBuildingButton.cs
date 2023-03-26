using UnityEngine;

public class UIPossibleBuildingButton : MonoBehaviour
{
    public void PossibleBuildingButton()
    {
        UIPossibleBuildingsPanel.instance.buildingDescriptionPanel.SetActive(true);
        UIPossibleBuildingsPanel.instance.PossibleBuildingNumber = int.Parse(name);
        Debug.Log("Possible Building Number: " + UIPossibleBuildingsPanel.instance.PossibleBuildingNumber);
    }
}
