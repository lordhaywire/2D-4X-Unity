
public class CountyPopulation
{
    public string firstName;
    public string lastName;

    public bool isMale;
    public int age;

    public string currentActivity;
    public string nextActivity;

    public CountyPopulation(string firstName, string lastName, bool isMale, 
        int age, string currentActivity, string nextActivity)
    {
        this.firstName = firstName;
        this.lastName = lastName;

        this.isMale = isMale;
        this.age = age;

        this.currentActivity = currentActivity;
        this.nextActivity = nextActivity;
    }
}
