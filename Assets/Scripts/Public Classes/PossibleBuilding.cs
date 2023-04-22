using System;
using UnityEngine;

public class PossibleBuilding
{
    public event Action CurrentWorkersChanged;

    public GameObject gameObject;
    public string name;
    public string description;

    // We will eventually be adding resource costs as well.
    public int influenceCost;
    public int workCost;
    private int currentWorkers;
    public int maxEmployees;

    public int CurrentWorkers
    {
        get { return currentWorkers; }
        set
        {
            currentWorkers = value;
            CurrentWorkersChanged?.Invoke();
        }
    }

    public PossibleBuilding(string name, string description, int influenceCost,
        int workCost, int currentEmployees, int maxEmployees)
    {
        this.name = name;
        this.description = description;
        this.influenceCost = influenceCost;
        this.workCost = workCost;
        CurrentWorkers = currentEmployees;
        this.maxEmployees = maxEmployees;
    }
}
