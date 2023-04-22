using System;
using UnityEngine;

public class UIPossibleBuildingsPanel : MonoBehaviour
{
    public static UIPossibleBuildingsPanel instance;

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
        instance = this;
        UICountyPanel.instance.buildingsPanelExpanded = true;
    }
    public void CollapseButton()
    {
        UICountyPanel.instance.buildingsPanelExpanded = false;
    }
}
