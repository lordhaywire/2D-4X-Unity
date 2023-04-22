using System;
using UnityEngine;

public class UICurrentBuildingsPanel : MonoBehaviour
{
    public static UICurrentBuildingsPanel instance;

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
            CurrentBuildingButtonPressed?.Invoke();
        }
    }

    private void Awake()
    {
        instance = this;
    }
}
