using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldMapLoad : MonoBehaviour
{
    public static WorldMapLoad instance;
    public string currentlySelectedCounty;

    [SerializeField] private int totalCapitolPop;
    [SerializeField] private GameObject countyListGameObject;
    [SerializeField] private GameObject uICanvas;

    public bool DevView;
    public GameObject countyInfoPanel;
    public GameObject armyInfoPanel;

    // This is just temp till we do character creation.
    public string playerFaction;

    public List<ResearchItem> researchItemsTier1 = new();

    public List<PossibleBuilding> possibleBuildings = new();
    public List<CurrentBuilding> currentBuildings = new();

    // Initialize County Dictionary List.
    public Dictionary<string, County> counties = new();

    // Initialize County Population Dictionary List.
    public Dictionary<string, List<CountyPopulation>> countyPopulationDictionary = new();

    // Initialize County Heroes/Leader Dictionary List.
    public Dictionary<string, List<Hero>> factionHeroesDictionary = new();

    // Initialize Army List.
    public List<Army> armies = new();

    // Initialize Factions list that will be used with the counties.
    public List<FactionNameAndColor> factionNameAndColors = new();

    public List<Faction> factions = new();

    // Arrays for County Population generation.
    private string[] maleNames;
    private string[] femaleNames;
    private string[] lastNames;

    private void Awake()
    {
        DevView = false;
        instance = this;

        GetNamesFromFile();


    }

    private void Start()
    {
        CreateResearchandBuildingList();
        CreateCountiesDictionary();

        // This is just temp till we do character creation.
        playerFaction = factionNameAndColors[0].name;

        CreatePopulation();

        AssignFactionNameAndColorToFaction();

        FirstRunTopInfoBar();

        // Get rid of extra variables.
        lastNames = null;
        femaleNames = null;
        maleNames = null;
    }

    private void CreateResearchandBuildingList()
    {
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.FISHERSSHACK, AllText.Descriptions.FISHERSSHACK,
            null, null, 1, true, true, null));
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.FORESTERSSHACK, AllText.Descriptions.FORESTERSSHACK,
            null, null, 1, true, true, null));
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.GARDENERSSHACK, AllText.Descriptions.GARDENERSSHACK,
            null, null, 1, true, true, null));
        /*
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.RESEARCHSSHACK, AllText.Descriptions.RESEARCHSSHACK,
            null, null, 1, true, true, null));
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.SCAVENGERSSHACK, AllText.Descriptions.SCAVENGERSSHACK,
            null, null, 1, true, true, null));
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.STONEWORKERSSHACK, AllText.Descriptions.STONEWORKERSSHACK,
            null, null, 1, true, true, null));
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, "Basic Tactics - Guns", AllText.Descriptions.BASICTACTICSGUNS,
            null, null, 1, false, false, null));
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, "Basic Tactics - Melee", AllText.Descriptions.BASICTACTICSMELEE,
            null, null, 1, false, false, null));
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.PRIMATIVEMELEESMITHSHACK, AllText.Descriptions.PRIMATIVEMELEESMITHSHACK,
            null, null, 1, true, false, null));
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.PRIMATIVERANGEDSMITHSHACK, AllText.Descriptions.PRIMATIVERANGEDSMITHSHACK,
            null, null, 1, true, false, null));
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.PRIMATIVEGUNSMITHSHACK, AllText.Descriptions.PRIMATIVEGUNSMITHSHACK,
            null, null, 1, true, false, null));
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.PRIMATIVEAMMOSHACK, AllText.Descriptions.PRIMATIVEAMMOSHACK,
            null, null, 1, true, false, null));
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.PRIMATVEGUNAMMOSHACK, AllText.Descriptions.PRIMATVEGUNAMMOSHACK,
            null, null, 1, true, false, null));
        - Nationalism
        */
        // The isResearchDone should start out as false, just set to done as testing.
        researchItemsTier1.Add(new ResearchItem(
            null, null, null, "Elitism", AllText.Descriptions.ELITISM,
            null, null, 1, false, true, null));

        possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.FISHERSSHACK, AllText.Descriptions.FISHERSSHACK, 500, 7, 0, 5));
        possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.FORESTERSSHACK, AllText.Descriptions.FORESTERSSHACK, 500, 1, 0, 5));
        possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.GARDENERSSHACK, AllText.Descriptions.GARDENERSSHACK, 500, 7, 0, 5));
        /*
        possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.RESEARCHSSHACK, AllText.Descriptions.RESEARCHSSHACK, 500, 7, 0, 5));
        possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.SCAVENGERSSHACK, AllText.Descriptions.SCAVENGERSSHACK, 500, 7, 0, 5));
        possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.STONEWORKERSSHACK, AllText.Descriptions.STONEWORKERSSHACK, 500, 7, 0, 5));
        possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.PRIMATIVEMELEESMITHSHACK, AllText.Descriptions.PRIMATIVEMELEESMITHSHACK, 500, 7, 0, 5));
        possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.PRIMATIVERANGEDSMITHSHACK, AllText.Descriptions.PRIMATIVERANGEDSMITHSHACK, 500, 7, 0, 5));
        possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.PRIMATIVEGUNSMITHSHACK, AllText.Descriptions.PRIMATIVEGUNSMITHSHACK, 500, 7, 0, 5));
        possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.PRIMATIVEAMMOSHACK, AllText.Descriptions.PRIMATIVEAMMOSHACK, 500, 7, 0, 5));
        possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.PRIMATVEGUNAMMOSHACK, AllText.Descriptions.PRIMATVEGUNAMMOSHACK, 500, 7, 0, 5));
        */

        researchItemsTier1[0].possibleBuildings = possibleBuildings[0];
        researchItemsTier1[1].possibleBuildings = possibleBuildings[1];
        researchItemsTier1[2].possibleBuildings = possibleBuildings[2];

    }

    private void FirstRunTopInfoBar()
    {
        UITopInfoBar.instance.Influence = factions[0].influence;
        UITopInfoBar.instance.Money = factions[0].money;
        UITopInfoBar.instance.Food = factions[0].food;
        UITopInfoBar.instance.Scrap = factions[0].scrap;
    }

    private void AssignFactionNameAndColorToFaction()
    {
        for (int i = 0; i < factions.Count; i++)
        {
            factions[i].factionNameAndColor = factionNameAndColors[i];
        }
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
        // Types of biomes - Coast, Desert, Farm, Forest, Mountain, Ruin
        counties[CountyListCreator.instance.countiesList[0].name] = new County(
            0, true, null, null, null, factionNameAndColors[1],
            Arrays.provinceName[0], "Coast", "Forest", "Ruin", 0);
        counties[CountyListCreator.instance.countiesList[1].name] = new County(
            1, true, null, null, null, factionNameAndColors[0],
            Arrays.provinceName[1], "Ruin", "Forest", "Farm", 1);
        counties[CountyListCreator.instance.countiesList[2].name] = new County(
            2, false, null, null, null, factionNameAndColors[2],
            Arrays.provinceName[1], "Coast", "Forest", "Mountain", 0);
        counties[CountyListCreator.instance.countiesList[3].name] = new County(
            3, false, null, null, null, factionNameAndColors[3],
            Arrays.provinceName[1], "Coast", "Forest", "Mountain", 0);
        counties[CountyListCreator.instance.countiesList[4].name] = new County(
            4, false, null, null, null, factionNameAndColors[4],
            Arrays.provinceName[1], "Mountain", "Forest", "Farm", 0);
        counties[CountyListCreator.instance.countiesList[5].name] = new County(
            5, false, null, null, null, factionNameAndColors[5],
            Arrays.provinceName[1], "Desert", "Mountain", "Forest", 0);
        counties[CountyListCreator.instance.countiesList[6].name] = new County(
            6, false, null, null, null, factionNameAndColors[6],
            Arrays.provinceName[1], "Mountain", "Desert", "Forest", 0);
    }

    private void CreatePopulation()
    {
        // Create various county specific data.
        for (int countyIndex = 0; countyIndex < counties.Count; countyIndex++)
        {
            string countyName = CountyListCreator.instance.countiesList[countyIndex].name;
            string factionName = factionNameAndColors[countyIndex].name;

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
            factionHeroesDictionary[factionName].Add(new Hero
                ("Lord", "Haywire", true, 30, CountyListCreator.instance.countiesList[1].name, AllText.Jobs.IDLE, AllText.Jobs.IDLE));
        }
        else
        {
            factionHeroesDictionary[factionName].Add(new Hero(null, null, false, 0, CountyListCreator.instance.countiesList[countyIndex].name, AllText.Jobs.IDLE, AllText.Jobs.IDLE));
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
            countyPopulationDictionary[countyName].Add(new CountyPopulation(null, null, false, 0, AllText.Jobs.IDLE, AllText.Jobs.IDLE));

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
