public class Hero
{
    public string firstName;
    public string lastName;

    public bool isMale;
    public int age;

    public string location;
    public string currentActivity;
    public string nextActivity;


    public Hero(string firstName, string lastName, bool isMale, int age, string location, string currentActivity, string nextActivity)
    {
        this.firstName = firstName;
        this.lastName = lastName;

        this.isMale = isMale;
        this.age = age;

        this.location = location;
        this.currentActivity = currentActivity;
        this.nextActivity = nextActivity;
    }
}


