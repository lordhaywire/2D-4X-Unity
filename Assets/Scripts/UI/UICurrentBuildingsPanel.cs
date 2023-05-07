using System;
using UnityEngine;

public class UICurrentBuildingsPanel : MonoBehaviour
{
    public static UICurrentBuildingsPanel Instance;

    public GameObject buildingDescriptionPanel;
    public GameObject currentBuildingsGroupGameObject;

    private int currentBuildingNumber;
    public event Action CurrentBuildingButtonPressed;

    public int CurrentBuildingNumber
    {
        get
        {
            return currentBuildingNumber;
        }
        set
        {
            currentBuildingNumber = value;
            //WorldMapLoad.Instance.currentlySelectedBuilding = currentBuildingNumber;
            //Debug.Log("UI Current Building Number: " + CurrentBuildingNumber);
            CurrentBuildingButtonPressed?.Invoke(); // What is this doing?  Did we ever update this when we copied Possible Building?
        }
    }

    private void Awake()
    {
        Instance = this;
    }

}
