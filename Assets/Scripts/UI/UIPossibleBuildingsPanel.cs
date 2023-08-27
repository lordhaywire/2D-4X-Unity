using System;
using UnityEngine;

public class UIPossibleBuildingsPanel : MonoBehaviour
{
    public static UIPossibleBuildingsPanel Instance; // This is just left here so the bugs will shut the fuck up.
    private int possibleBuildingNumber;
    public int PossibleBuildingNumber
    {
        get
        {
            return possibleBuildingNumber;
        }
        set
        {
            possibleBuildingNumber = value;
            //PossibleBuildingButtonPressed?.Invoke();
        }
    }
    // This is the end of the bullshit.

    [SerializeField] UIBuildingsPanel uIBuildingsPanel;
    [SerializeField] GameObject possibleBuildingsScrollView;
    [SerializeField] GameObject buildingsParent;
    private void OnEnable()
    {
        uIBuildingsPanel.PanelRefresher(buildingsParent, possibleBuildingsScrollView);
    }
    /*
    

    public GameObject buildingDescriptionPanel;
    public GameObject possibleBuildingsGroupGameObject;

    

    public event Action PossibleBuildingButtonPressed;



    private void OnEnable() // This needs to be triggered by an event or when another county is selected.
    {
        Instance = this;
        UICountyInfoPanel.Instance.buildingsPanelExpanded = true;
    }
    public void CollapseButton()
    {
        UICountyInfoPanel.Instance.buildingsPanelExpanded = false;
    }
    */
}
