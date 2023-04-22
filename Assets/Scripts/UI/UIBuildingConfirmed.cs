using System;
using UnityEngine;

public class UIBuildingConfirmed : MonoBehaviour
{
    public static UIBuildingConfirmed Instance;
    public event Action BuildingConfirmed;

    private void Awake()
    {
        Instance = this;
    }

    public void YesButton()
    {
        BuildingConfirmed?.Invoke();
    }
}
