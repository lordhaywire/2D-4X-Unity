using UnityEngine;

public class UIBuildingButton : MonoBehaviour
{
    public void BuildingButton()
    {
        if(transform.parent == UIPossibleBuildingsPanel.instance.possibleBuildingsGroupGameObject.transform)
        {
            Debug.Log("Possible Building.");
            UIPossibleBuildingsPanel.instance.buildingDescriptionPanel.SetActive(true);
            UIPossibleBuildingsPanel.instance.PossibleBuildingNumber = int.Parse(name);
        }
        else
        {
            Debug.Log("Current Building.");
            UICurrentBuildingsPanel.instance.buildingDescriptionPanel.SetActive(true);
            UICurrentBuildingsPanel.instance.CurrentBuildingNumber = int.Parse(name);
        }
    }
}
