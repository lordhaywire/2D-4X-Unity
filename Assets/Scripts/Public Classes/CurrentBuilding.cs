

public class CurrentBuilding
{
    public string name;
    public string description;

    public int daysToBuild;
    public int currentEmployees;
    public int maxEmployees;

    public bool isBeingBuilt;
    public bool isBuilt;

    public CurrentBuilding(string name, string description, int daysToBuild, int currentEmployees,
        int maxEmployees, bool isBeingBuilt, bool isBuilt)
    {
        this.name = name;
        this.description = description;
        this.daysToBuild = daysToBuild;
        this.currentEmployees = currentEmployees;
        this.maxEmployees = maxEmployees;
        this.isBeingBuilt = isBeingBuilt;
        this.isBuilt = isBuilt;
    }
}
