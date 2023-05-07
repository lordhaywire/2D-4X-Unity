
public class CountyPopulation
{
    public int countyPopulationID;
    public string firstName;
    public string lastName;

    public bool isFactionLeader;
    public bool isHero;
    public bool isWorker;
    public bool isMale;
    public int age;

    // Perks
    public bool leaderOfPeoplePerk;

    // Attributes

    // Skills
    public int constructionSkill;

    public string currentActivity;
    public CurrentBuilding currentBuilding;
    public string nextActivity;
    public CurrentBuilding nextBuilding;

    public CountyPopulation(int countyPopulationID, string firstName, string lastName, bool isFactionLeader, bool isHero, bool isWorker,
         bool isMale, int age, bool leaderOfPeoplePerk, int constructionSkill, string currentActivity, 
         CurrentBuilding currentBuilding, string nextActivity, CurrentBuilding nextBuilding)
    {
        this.countyPopulationID = countyPopulationID;
        this.firstName = firstName;
        this.lastName = lastName;

        this.isFactionLeader = isFactionLeader;
        this.isHero = isHero;
        this.isWorker = isWorker;
        this.isMale = isMale;
        this.age = age;

        this.leaderOfPeoplePerk = leaderOfPeoplePerk; 

        this.constructionSkill = constructionSkill;

        this.currentActivity = currentActivity;
        this.currentBuilding = currentBuilding;
        this.nextActivity = nextActivity;  
        this.nextBuilding = nextBuilding;
    }
}
