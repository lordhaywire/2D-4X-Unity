public class Province
{
    public int ID;
    public string ownerName;
    public string nationName;
    public int population;

    public Province(int newID, string newOwnerName, string newNationName, int newPopulation)
    {
        ID = newID;
        ownerName = newOwnerName;
        nationName = newNationName;
        population = newPopulation;
    }
}
