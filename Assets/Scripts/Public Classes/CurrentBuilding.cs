using UnityEngine;

public class CurrentBuilding
{
    public GameObject gameObject;
    public string name;
    public string description;

    public int workCompleted;
    public int workCost;
    public int currentWorkers;
    public int maxWorkers;

    public bool isBeingBuilt;
    public bool isBuilt;

    public CurrentBuilding(string name, string description, int workCompleted, int workCost, int currentWorkers,
        int maxWorkers, bool isBeingBuilt, bool isBuilt)
    {
        this.name = name;
        this.description = description;
        this.workCompleted = workCompleted;
        this.workCost = workCost;
        this.currentWorkers = currentWorkers;
        this.maxWorkers = maxWorkers;
        this.isBeingBuilt = isBeingBuilt;
        this.isBuilt = isBuilt;
    }
}
