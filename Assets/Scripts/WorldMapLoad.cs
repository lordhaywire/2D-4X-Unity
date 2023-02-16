using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldMapLoad : MonoBehaviour
{
    public static bool canSeeCountyInfo = true;

    [SerializeField] private GameObject countyListGameObject; // This is the gameObject that all the counties are under in the inspector.
    [SerializeField] private string countyInfoPanelName;
    [SerializeField] private string armyPanelName;

    [SerializeField] private GameObject uICanvas;

    public static GameObject countyInfoPanel;
    public static GameObject armyInfoPanel;

    // This is just temp till we do character creation.
    public static string playerName;
    public static string enemyName;

    // Initialize County Dictionary.
    public static Dictionary<string, County> counties = new();

    public static List<CountyPopulation> testList = new();

    // Initialize County Population Dictionary List.
    public static Dictionary<string, List<CountyPopulation>> countyPopulationDictionary = new();

    // Array for countyName creation.

    private string[] maleNames;
    private string[] femaleNames;
    private string[] lastNames;

    // Initialize Heroes/Leader List.
    public static List<Hero> heroes = new();

    // Initialize Army List.
    public static List<Army> armies = new();

    private void Awake()
    {
        // Counties added to counties Dictionary.
        counties[Arrays.countyName[0]] = new County(0, true, null, null, "Enemy1", "PlayersNation", 6000);
        counties[Arrays.countyName[1]] = new County(1, true, null, null, "Player", "PlayersNation", 10000);
        counties[Arrays.countyName[2]] = new County(2, false, null, null, "Player", "EnemyNation", 5000);
        counties[Arrays.countyName[3]] = new County(3, false, null, null, "Enemy1", "EnemyNation", 7000);
        counties[Arrays.countyName[4]] = new County(4, false, null, null, "Enemy1", "EnemyNation", 1000);
        counties[Arrays.countyName[5]] = new County(5, false, null, null, "Enemy1", "EnemyNation", 2000);
        counties[Arrays.countyName[6]] = new County(6, false, null, null, "Enemy1", "EnemyNation", 3000);

        // Create various county specific data.
        
        for (int countyIndex = 0; countyIndex < counties.Count; countyIndex++)
        {
            string countyName = Arrays.countyName[countyIndex];
            countyPopulationDictionary[countyName] = new List<CountyPopulation>();

            // Get game object for center of county and assign to correct county in list.
            counties[countyName].countyCenterGameObject = 
                countyListGameObject.transform.GetChild(countyIndex).GetChild(0).gameObject;

            if (counties[countyName].isCapital == true)
            {
                int totalPopulation = 10;
                GeneratePopulation(countyName,totalPopulation);
                counties[countyName].population = totalPopulation;
            }
            else
            {
                int totalPopulation = Random.Range(3, 9);
                GeneratePopulation(countyName,totalPopulation);
                counties[countyName].population = totalPopulation;
            }
        }

        // This is just temp till we do character creation.
        playerName = "Player";
        enemyName = "Enemy1";

        // This is set up this way so it can be a static variable.
        countyInfoPanel = uICanvas.transform.GetChild(1).gameObject;
        armyInfoPanel = uICanvas.transform.GetChild(2).gameObject;

        // Leader getting added - This is just temp till we do character creation.
        heroes.Add(new Hero("Lord", "Haywire", Arrays.countyName[1],  null));

    }

    private void GeneratePopulation(string countyName, int totalPopulation)
    {
        lastNames = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "Lists", "Last Names.txt"));
        femaleNames = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "Lists", "Female Names.txt"));
        maleNames = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "Lists", "Male Names.txt"));

        for (int populationIndex = 0; populationIndex < totalPopulation; populationIndex++)
        {
            //Debug.Log("County Name Variable: " + countyName);

            // This adds to the Dictionary List a new person.
            countyPopulationDictionary[countyName].Add(new CountyPopulation(null, null, false, 0));

            // Generates Persons Last Name
            int randomLastNameNumber = Random.Range(0, lastNames.Length);
            countyPopulationDictionary[countyName][populationIndex].lastName =
                lastNames[randomLastNameNumber];

            // Determine the persons sex and first name
            int randomSexNumber = Random.Range(0, 2);
            int randomFemaleNameNumber = Random.Range(0, femaleNames.Length);
            int randomMaleNameNumber = Random.Range(0, maleNames.Length);
            if (randomSexNumber == 0)
            {
                countyPopulationDictionary[countyName][populationIndex].isMale = true;
                countyPopulationDictionary[countyName][populationIndex].firstName =
                    maleNames[randomMaleNameNumber];
            }
            else
            {
                countyPopulationDictionary[countyName][populationIndex].isMale = false;
                countyPopulationDictionary[countyName][populationIndex].firstName =
                    femaleNames[randomFemaleNameNumber];
            }

            int randomAgeNumber = Random.Range(18, 61);
            countyPopulationDictionary[countyName][populationIndex].age = randomAgeNumber;

            /*
            Debug.Log("Random Person " +
                countyPopulationDictionary[countyName][populationIndex].isMale + " " + 
                countyPopulationDictionary[countyName][populationIndex].firstName + " " +
                countyPopulationDictionary[countyName][populationIndex].lastName + " Age: " +
                countyPopulationDictionary[countyName][populationIndex].age);
            */
        }
    }
}
