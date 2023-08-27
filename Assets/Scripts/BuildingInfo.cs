using System;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    //public Building building;
    public event Action CurrentWorkersChanged;

    public GameObject uIGameObject;
    public string buildingName;
    public string description;

    // We will eventually be adding resource costs as well.
    public int workCompleted;
    public int influenceCost;
    public int workCost;
    private int currentWorkers = 0;
    public int maxWorkers;

    public bool isBeingBuilt;
    public bool isBuilt;
    public GameObject completedTextGameObject; // What is this dumb shit?

    public int CurrentWorkers
    {
        get { return currentWorkers; }
        set
        {
            currentWorkers = value;
            CurrentWorkersChanged?.Invoke();
        }
    }
    public ResourceSO resourceSO;
}
