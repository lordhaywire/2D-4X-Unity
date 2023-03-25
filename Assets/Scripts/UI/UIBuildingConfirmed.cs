using System;
using UnityEngine;

public class UIBuildingConfirmed : MonoBehaviour
{
    public event Action BuildingConfirmed;

    public void YesButton()
    {
        BuildingConfirmed();
    }
}
