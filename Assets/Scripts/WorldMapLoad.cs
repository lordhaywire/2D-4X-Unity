using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldMapLoad : MonoBehaviour
{
    public static WorldMapLoad instance;
    public string currentlySelectedCounty;

    [SerializeField] private int totalCapitolPop;
    [SerializeField] private int minimumCountyPop;
    [SerializeField] private int maximumCountyPop;
    [SerializeField] private GameObject countyListGameObject;
    [SerializeField] private GameObject uICanvas;

    public bool DevView;
    public GameObject countyInfoPanel;
    public GameObject armyInfoPanel;

    // This is just temp till we do character creation.
    public string playerFaction;
    public int playerFactionID;

    public List<ResearchItem> researchItemsTier1 = new();

    public List<PossibleBuilding> possibleBuildings = new();

    public List<CurrentBuilding> currentBuildings = new();

    // Initialize County Dictionary List.
    public Dictionary<string, County> counties = new();

    // Initialize County Population Dictionary List, which is a Dictionary for each county that holds a list of their population.
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
        //DevView = false;
        instance = this;

        GetNamesFromFile();
    }

    private void Start()
    {
        UIBuildingConfirmed.instance.BuildingConfirmed += BuildCountyImprovement;

        CreateResearchandBuildingList();
        CreateCountiesDictionary();

        // This is just temp till we do character creation.
        playerFaction = factionNameAndColors[0].name;
        playerFactionID = 0;

        CreatePopulation();

        AssignFactionNameAndColorToFaction();

        FirstRunTopInfoBar();

        // Get rid of extra variables.
        lastNames = null;
        femaleNames = null;
        maleNames = null;
    }

    private void BuildCountyImprovement()
    {
        DeductCostOfBuilding(); // Done - for Influence costs only.
        SetNextDayJob(); // Done

        MoveBuildingToCurrentBuildingList(); // Not done at all.  
    }

    private void MoveBuildingToCurrentBuildingList()
    {
        var possibleBuilding = possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber];

        Debug.Log("Possible Building Number: " + UIBuildingsPanel.instance.PossibleBuildingNumber);

        // Create the Current Building List and add one to the County so the County knows what it has built.
        // I don't know what the fuck this is, but it works.  Thanks Visual Studio!
        counties[currentlySelectedCounty].buildings = new List<CurrentBuilding>
        {
            new CurrentBuilding(
            possibleBuilding.name, possibleBuilding.description, possibleBuilding.daysToBuild, possibleBuilding.CurrentWorkers,
            possibleBuilding.maxEmployees, true, false)
        };
        Debug.Log("Current Building Length: " + counties[currentlySelectedCounty].buildings.Count);
        Debug.Log("County Build Name: " + counties[currentlySelectedCounty].buildings[^1].name);

        // Assigned the Possible Building UI game object to the current building list.
        counties[currentlySelectedCounty].buildings[^1].gameObject = possibleBuilding.gameObject;

        // Moving the possilbe building UI game object to the current building UI game object.
        counties[currentlySelectedCounty].buildings[^1].gameObject.transform.SetParent(UICurrentBuildingsPanel.instance.currentBuildingsGroupGameObject.transform);

        // Remove the building from the possible Building list.
        possibleBuildings.Remove(possibleBuilding);

        // Rename building to be the same as their possible building index.
        for(int i = 0; i < UIBuildingsPanel.instance.possibleBuildingsGroupGameObject.transform.childCount; i++)
        {
            possibleBuildings[i].gameObject.name = i.ToString();
        }

        // Rename building to be the same as their current building index.
        for (int i = 0; i < UICurrentBuildingsPanel.instance.currentBuildingsGroupGameObject.transform.childCount; i++)
        {
            counties[currentlySelectedCounty].buildings[i].gameObject.name = i.ToString();
        }

    }

    private void DeductCostOfBuilding()
    {
        factions[0].Influence -= possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].influenceCost;
    }

    private void SetNextDayJob()
    {
        // This is where we left off - I think if we could build 3 buildings we could run out of unemployeed workers and
        // everythign would break.
        int numberWorkers = 0;
        for(int i = 0; i < countyPopulationDictionary[currentlySelectedCounty].Count; i++)
        {
            if(countyPopulationDictionary[currentlySelectedCounty][i].nextActivity == AllText.Jobs.IDLE
                && numberWorkers < possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers)
            {
                countyPopulationDictionary[currentlySelectedCounty][i].nextActivity = AllText.Jobs.BUILDING;
                numberWorkers++;
                counties[currentlySelectedCounty].currentlyWorkingPopulation++; // We could put this number on the county info panel.
                /*
                Debug.Log("Currently Working Population: " + counties[currentlySelectedCounty].currentlyWorkingPopulation);
                Debug.Log("First Name: " + countyPopulationDictionary[currentlySelectedCounty][i].firstName);
                Debug.Log("Activity: " + countyPopulationDictionary[currentlySelectedCounty][i].nextActivity);
                */
    }
    /*
    else
    {
         Debug.Log("Set Next Day Job got to Else.");
    }
    */
}

        //possibleBuildings[UIBuildingsPanel.instance.PossibleBuildingNumber].CurrentWorkers
    }

    private void OnDisable()
    {
        UIBuildingConfirmed.instance.BuildingConfirmed -= SetNextDayJob;

        UIBuildingConfirmed.instance.BuildingConfirmed -= MoveBuildingToCurrentBuildingList;
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

        // Do we really need this?
        researchItemsTier1[0].possibleBuildings = possibleBuildings[0];
        researchItemsTier1[1].possibleBuildings = possibleBuildings[1];
        researchItemsTier1[2].possibleBuildings = possibleBuildings[2];

    }

    private void FirstRunTopInfoBar()
    {
        UITopInfoBar.instance.Influence = factions[0].Influence;
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
        // Types of biomes - Coast, Desert, Farm, Forest, Mountain, Ruin, River
        counties[CountyListCreator.instance.countiesList[0].name] = new County(
            0, true, null, null, null, factionNameAndColors[1],
            Arrays.provinceName[0], "Coast", "Forest", "Ruin", null, 0, 0);
        counties[CountyListCreator.instance.countiesList[1].name] = new County(
            1, true, null, null, null, factionNameAndColors[0],
            Arrays.provinceName[1], "Ruin", "Forest", "River", null, 0, 1);
        counties[CountyListCreator.instance.countiesList[2].name] = new County(
            2, false, null, null, null, factionNameAndColors[2],
            Arrays.provinceName[1], "Coast", "Forest", "Mountain", null, 0, 0);
        counties[CountyListCreator.instance.countiesList[3].name] = new County(
            3, false, null, null, null, factionNameAndColors[3],
            Arrays.provinceName[1], "Coast", "Forest", "Mountain", null, 0, 0);
        counties[CountyListCreator.instance.countiesList[4].name] = new County(
            4, false, null, null, null, factionNameAndColors[4],
            Arrays.provinceName[1], "Mountain", "Forest", "Farm", null, 0, 0);
        counties[CountyListCreator.instance.countiesList[5].name] = new County(
            5, false, null, null, null, factionNameAndColors[5],
            Arrays.provinceName[1], "Desert", "Mountain", "Forest", null, 0, 0);
        counties[CountyListCreator.instance.countiesList[6].name] = new County(
            6, false, null, null, null, factionNameAndColors[6],
            Arrays.provinceName[1], "Mountain", "Desert", "Forest", null, 0, 0);
        /*
        // Create and add currentBuildings list to each county so each county knows what it has built.
        for(int i = 0; i < counties.Count; i++)
        {
            counties[CountyListCreator.instance.countiesList[i].name].currentBuilding = new CurrentBuilding();
        }
        */
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
                GenerateLeaders(factionName, countyIndex);
                GeneratePopulation(countyName, totalCapitolPop);
                counties[countyName].population = totalCapitolPop + 1; // At beginning of game every faction will always just have 1 hero.
            }
            else
            {
                int normalPopulation = Random.Range(minimumCountyPop, maximumCountyPop);
                GenerateLeaders(factionName, countyIndex);
                GeneratePopulation(countyName, normalPopulation);
                counties[countyName].population = normalPopulation
                    + 1; // At beginning of game every faction will always just have 1 hero.
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

            
            //Debug.Log("Name: " + countyPopulationDictionary[countyName][populationIndex].firstName + " " +
            //countyPopulationDictionary[countyName][populationIndex].lastName);
            
        }
    }
}
