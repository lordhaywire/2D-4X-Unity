using UnityEngine;

public class UIBuildingButton : MonoBehaviour
{
    public static UIBuildingButton Instance;

    public GameObject completedTextGameObject;
    private void Awake()
    {
        Instance = this;
    }

    public void BuildingButton()
    {
        /*
        if(transform.parent == UIPossibleBuildingsPanel.Instance.possibleBuildingsGroupGameObject.transform)
        {
            //Debug.Log("Possible Building.");
            //UIPossibleBuildingsPanel.Instance.buildingDescriptionPanel.SetActive(true);
            UICurrentBuildingsPanel.Instance.buildingDescriptionPanel.SetActive(false);
            UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber = int.Parse(name);
        }
        else
        {
            //Debug.Log("Current Building.");
            UICurrentBuildingsPanel.Instance.buildingDescriptionPanel.SetActive(true);
            //UIPossibleBuildingsPanel.Instance.buildingDescriptionPanel.SetActive(false);
            UICurrentBuildingsPanel.Instance.CurrentBuildingNumber = int.Parse(name);
        }
        */
    }
}
