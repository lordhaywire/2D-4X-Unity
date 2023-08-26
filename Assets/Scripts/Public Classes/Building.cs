using System;
using UnityEngine;

[Serializable]
public class Building
{
    public event Action CurrentWorkersChanged;

    public GameObject gameObject;
    public string name;
    public string description;

    // We will eventually be adding resource costs as well.
    public int workCompleted;
    public int influenceCost;
    public int workCost;
    private int currentWorkers;
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

    public Building(GameObject gameObject, string name, string description, int workCompleted, int influenceCost,
        int workCost, int currentEmployees, int maxWorkers, bool isBeingBuilt, bool isBuilt
        , GameObject completedTextGameObject)
    {
        this.gameObject = gameObject;
        this.name = name;
        this.description = description;

        this.workCompleted = workCompleted;
        this.influenceCost = influenceCost;
        this.workCost = workCost;
        CurrentWorkers = currentEmployees;
        this.maxWorkers = maxWorkers;

        this.isBeingBuilt = isBeingBuilt;
        this.isBuilt = isBuilt;
        this.completedTextGameObject = completedTextGameObject;
    }
}
