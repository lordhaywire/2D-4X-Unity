using System;
using UnityEngine;

public class UIBuildingConfirmed : MonoBehaviour
{
    public static UIBuildingConfirmed Instance;
    //public event Action BuildingConfirmed;

    private void Awake()
    {
        Instance = this;
    }

    public void YesButton()
    {
        BuildImprovements buildImprovements = WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<BuildImprovements>();
        //buildImprovements.BuildBuilding(WorldMapLoad.Instance.playerFaction, )
        //BuildingConfirmed?.Invoke();
    }
}
