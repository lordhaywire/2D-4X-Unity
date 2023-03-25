using System;

public class PossibleBuilding
{
    public string name;
    public string description;

    // We will eventually be adding resource costs as well.
    public int influenceCost;
    public int daysToBuild;
    public int currentEmployees;
    public int maxEmployees;

    public PossibleBuilding(string name, string description, int influenceCost,
        int daysToBuild, int currentEmployees, int maxEmployees)
    {
        this.name = name;
        this.description = description;
        this.influenceCost = influenceCost;
        this.daysToBuild = daysToBuild;
        this.currentEmployees = currentEmployees;
        this.maxEmployees = maxEmployees;
    }
}
