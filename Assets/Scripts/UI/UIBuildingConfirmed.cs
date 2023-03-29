using System;
using UnityEngine;

public class UIBuildingConfirmed : MonoBehaviour
{
    public static UIBuildingConfirmed instance;
    public event Action BuildingConfirmed;

    private void Awake()
    {
        instance = this;
    }

    public void YesButton()
    {
        BuildingConfirmed?.Invoke();
    }
}
