using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    //public Building building;
    //public event Action CurrentWorkersChanged;

    public GameObject uIGameObject;
    public County county;
    public string buildingName;
    public string description;

    // We will eventually be adding resource costs as well.
    public int workCompleted;
    public int influenceCost;
    public int workCost;
    [SerializeField] private int currentWorkers = 0;
    public int maxWorkers;

    public bool isBeingBuilt;
    public bool isBuilt;

    public int CurrentWorkers // This doesn't need to be a getter setter currently.
    {
        get { return currentWorkers; }
        set
        {
            currentWorkers = value;
            //CurrentWorkersChanged?.Invoke();
        }
    }
    public ResourceSO resourceSO;
}
