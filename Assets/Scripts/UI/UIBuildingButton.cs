using UnityEngine;

public class UIBuildingButton : MonoBehaviour
{
    public void BuildingButton()
    {
        UIBuildingsPanel.instance.buildingDescriptionPanel.SetActive(true);
        if(transform.parent == UIBuildingsPanel.instance.possibleBuildingsGroupGameObject.transform)
        {
            Debug.Log("Possible Building.");
            UIBuildingsPanel.instance.PossibleBuildingNumber = int.Parse(name);
        }
        else
        {
            Debug.Log("Current Building.");
            UIBuildingsPanel.instance.CurrentBuildingNumber = int.Parse(name);
        }
    }
}
