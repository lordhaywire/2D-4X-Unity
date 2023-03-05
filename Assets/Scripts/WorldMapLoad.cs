using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldMapLoad : MonoBehaviour
{
    public static WorldMapLoad Instance;

    [SerializeField] private int totalCapitolPop;
    [SerializeField] private GameObject countyListGameObject;
    [SerializeField] private GameObject uICanvas;

    public bool canSeeCountyInfo = true;
    public GameObject countyInfoPanel;
    public GameObject armyInfoPanel;

    // This is just temp till we do character creation.
    public string playerFaction;

    // Initialize County Dictionary.
    public Dictionary<string, County> counties = new();

    // Initialize County Population Dictionary List.
    public Dictionary<string, List<CountyPopulation>> countyPopulationDictionary = new();

    // Initialize County Heroes/Leader Dictionary List.
    public Dictionary<string, List<Hero>> factionHeroesDictionary = new();

    // Initialize Army List.
    public List<Army> armies = new();

    // Initialize Factions list that will be used with the counties.
    public List<Faction> factions = new();

    // Arrays for County Population generation.
    private string[] maleNames;
    private string[] femaleNames;
    private string[] lastNames;

    private void Awake()
    {
        Instance = this;

        GetNamesFromFile();

        CreateCountiesDictionary();

        // This is just temp till we do character creation.
        playerFaction = factions[1].name;

        // This is set up this way so it can be a static variable - This will be changed when we set up The Instance.
        //countyInfoPanel = uICanvas.transform.GetChild(1).gameObject;
        //armyInfoPanel = uICanvas.transform.GetChild(2).gameObject;

        CreatePopulation();
    }



    private void Start()
    {
        // Get rid of extra variables.
        lastNames = null;
        femaleNames = null;
        maleNames = null;
    }

    private void GetNamesFromFile()
    {
        // Get names for population and leader generation.
        lastNames = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "Lists", "Last Names.txt"));
        femaleNames = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "Lists", "Female Names.txt"));
        maleNames = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "Lists", "Male Names.txt"));
    }

    private void CreateCountiesDictionary()
    {
        // Counties added to counties Dictionary.
        counties[Arrays.countyName[0]] = new County(0, true, null, Color.clear, null, factions[0], Arrays.provinceName[0], 0);
        counties[Arrays.countyName[1]] = new County(1, true, null, Color.clear, null, factions[1], Arrays.provinceName[1], 1);
        counties[Arrays.countyName[2]] = new County(2, false, null, Color.clear, null, factions[2], Arrays.provinceName[1], 0);
        counties[Arrays.countyName[3]] = new County(3, false, null, Color.clear, null, factions[3], Arrays.provinceName[1], 0);
        counties[Arrays.countyName[4]] = new County(4, false, null, Color.clear, null, factions[4], Arrays.provinceName[1], 0);
        counties[Arrays.countyName[5]] = new County(5, false, null, Color.clear, null, factions[5], Arrays.provinceName[1], 0);
        counties[Arrays.countyName[6]] = new County(6, false, null, Color.clear, null, factions[6], Arrays.provinceName[1], 0);
    }

    private void CreatePopulation()
    {
        // Create various county specific data.
        for (int countyIndex = 0; countyIndex < counties.Count; countyIndex++)
        {
            string countyName = Arrays.countyName[countyIndex];
            string factionName = factions[countyIndex].name;

            // There should probably be some sort of null check in here?
            // Initilizes the List in the Dictionaries for Counties and Heroes.
            countyPopulationDictionary[countyName] = new List<CountyPopulation>();
            factionHeroesDictionary[factionName] = new List<Hero>();

            // Get game object for center of county and assign to correct county in list.
            counties[countyName].countyCenterGameObject =
                countyListGameObject.transform.GetChild(countyIndex).GetChild(0).gameObject;

            if (counties[countyName].isCapital == true)
            {
                int normalPopulation = totalCapitolPop;
                GenerateLeaders(factionName, countyIndex);
                GeneratePopulation(countyName, normalPopulation);
                counties[countyName].population = normalPopulation;
            }
            else
            {
                int normalPopulation = Random.Range(3, 9);
                GenerateLeaders(factionName, countyIndex);
                GeneratePopulation(countyName, normalPopulation);
                counties[countyName].population = normalPopulation;
            }
        }
    }

    private void GenerateLeaders(string factionName, int countyIndex)
    {
        // Add Leaders to each faction.
        // This adds to the Heroes Dictionary List a new Leader.  It is jank because we want the leader in Portland to be me.
        // This will instead need to eventually check if this is a player faction vs an AI faction.
        if (playerFaction == factionName)
        {
            factionHeroesDictionary[factionName].Add(new Hero("Lord", "Haywire", true, 30, Arrays.countyName[1], "Scavenging"));
        }
        else
        {
            factionHeroesDictionary[factionName].Add(new Hero(null, null, false, 0, Arrays.countyName[countyIndex], "Scavenging"));
            int randomLastNameNumber = Random.Range(0, lastNames.Length);
            factionHeroesDictionary[factionName][0].lastName = lastNames[randomLastNameNumber];

            // Determine the persons sex and first name
            int randomSexNumber = Random.Range(0, 2);
            int randomFemaleNameNumber = Random.Range(0, femaleNames.Length);
            int randomMaleNameNumber = Random.Range(0, maleNames.Length);
            if (randomSexNumber == 0)
            {
                factionHeroesDictionary[factionName][0].isMale = true;
                factionHeroesDictionary[factionName][0].firstName =
                    maleNames[randomMaleNameNumber];
            }
            else
            {
                factionHeroesDictionary[factionName][0].isMale = false;
                factionHeroesDictionary[factionName][0].firstName =
                    femaleNames[randomFemaleNameNumber];
            }

            int randomAgeNumber = Random.Range(30, 61);
            factionHeroesDictionary[factionName][0].age = randomAgeNumber;

            /*
            Debug.Log("First Name: " + factionHeroesDictionary[factionName][0].firstName + " " +
                factionHeroesDictionary[factionName][0].lastName + " County: "
                + factionHeroesDictionary[factionName][0].location);
            */
        }
    }
    private void GeneratePopulation(string countyName, int totalPopulation)
    {
        for (int populationIndex = 0; populationIndex < totalPopulation; populationIndex++)
        {
            // This adds to the Dictionary List a new person.
            countyPopulationDictionary[countyName].Add(new CountyPopulation(null, null, false, 0, "Scavenging"));

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
            Debug.Log("Name: " + countyPopulationDictionary[countyName][populationIndex].firstName + " " +
            countyPopulationDictionary[countyName][populationIndex].lastName);
            */
        }
    }
}
