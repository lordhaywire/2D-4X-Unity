
public class Hero
{
    public string firstName;
    public string lastName;

    public bool isMale;
    public int age;

    public string location;
    public string activity;

    public Hero(string newFirstName, string newLastName, bool newIsMale, int newAge, string newLocation, string newActivity)
    {
        firstName = newFirstName;
        lastName = newLastName;

        isMale = newIsMale;
        age = newAge;

        location = newLocation;
        activity = newActivity;
    }
}


