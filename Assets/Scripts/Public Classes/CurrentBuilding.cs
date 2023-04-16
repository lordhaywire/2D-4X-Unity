using UnityEngine;

public class CurrentBuilding
{
    public GameObject gameObject;
    public string name;
    public string description;

    public int daysToBuild;
    public int currentWorkers;
    public int maxWorkers;

    public bool isBeingBuilt;
    public bool isBuilt;

    public CurrentBuilding(string name, string description, int daysToBuild, int currentWorkers,
        int maxWorkers, bool isBeingBuilt, bool isBuilt)
    {
        this.name = name;
        this.description = description;
        this.daysToBuild = daysToBuild;
        this.currentWorkers = currentWorkers;
        this.maxWorkers = maxWorkers;
        this.isBeingBuilt = isBeingBuilt;
        this.isBuilt = isBuilt;
    }
}
