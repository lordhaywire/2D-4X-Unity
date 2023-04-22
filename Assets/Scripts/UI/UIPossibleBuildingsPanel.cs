using System;
using UnityEngine;

public class UIPossibleBuildingsPanel : MonoBehaviour
{
    public static UIPossibleBuildingsPanel Instance;

    public GameObject buildingDescriptionPanel;
    public GameObject possibleBuildingsGroupGameObject;

    private int possibleBuildingNumber;

    public event Action PossibleBuildingButtonPressed;


    public int PossibleBuildingNumber
    {
        get
        {
            return possibleBuildingNumber;
        }
        set
        {
            possibleBuildingNumber = value;
            PossibleBuildingButtonPressed?.Invoke();
        }
    }

    private void OnEnable() // This needs to be triggered by an event or when another county is selected.
    {
        Instance = this;
        UICountyPanel.Instance.buildingsPanelExpanded = true;
    }
    public void CollapseButton()
    {
        UICountyPanel.Instance.buildingsPanelExpanded = false;
    }
}
