
public class CountyPopulation
{
    public string firstName;
    public string lastName;

    public bool isMale;
    public int age;

    public string activity;

    public CountyPopulation(string newFirstName, string newLastName, bool newIsMale, 
        int newAge, string newActivity)
    {
        firstName = newFirstName;
        lastName = newLastName;

        isMale = newIsMale;
        age = newAge;

        activity = newActivity;
    }
}
