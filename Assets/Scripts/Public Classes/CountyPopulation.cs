
public class CountyPopulation
{
    public string firstName;
    public string lastName;

    public bool isMale;
    public int age;

    public string currentActivity;
    public string currentBuilding;
    public string nextActivity;
    public string nextBuilding;

    public CountyPopulation(string firstName, string lastName, bool isMale, 
        int age, string currentActivity, string currentBuilding, string nextActivity,  string nextBuilding)
    {
        this.firstName = firstName;
        this.lastName = lastName;

        this.isMale = isMale;
        this.age = age;

        this.currentActivity = currentActivity;
        this.currentBuilding = currentBuilding;
        this.nextActivity = nextActivity;  
        this.nextBuilding = nextBuilding;
    }
}
