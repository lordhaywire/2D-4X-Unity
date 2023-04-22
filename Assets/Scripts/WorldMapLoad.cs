using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldMapLoad : MonoBehaviour
{
    public static WorldMapLoad Instance;
    public string currentlySelectedCounty;

    [SerializeField] private int totalCapitolPop;
    [SerializeField] private int minimumCountyPop;
    [SerializeField] private int maximumCountyPop;
    [SerializeField] private GameObject countyListGameObject;
    [SerializeField] private GameObject uICanvas;

    public bool currentBuildingDescriptionPanelExpanded;
    public bool possibleBuildingDescriptionPanelExpanded;
    public bool DevView;
    public GameObject countyInfoPanel;
    public GameObject armyInfoPanel;

    // This is just temp till we do character creation.
    public string playerFaction;
    public int playerFactionID;

    //public List<ResearchItem> researchItems = new();

    //public List<PossibleBuilding> possibleBuildings = new();

    //public List<CurrentBuilding> currentBuildings = new();

    // Initialize County Dictionary List.
    public Dictionary<string, County> counties = new();

    // Initialize County Population Dictionary List, which is a Dictionary for each county that holds a list of their population.
    public Dictionary<string, List<CountyPopulation>> countyPopulationDictionary = new();

    // Initialize County Heroes/Leader Dictionary List.
    //public Dictionary<string, List<Hero>> factionHeroesDictionary = new();

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
        Instance = this;
        currentBuildingDescriptionPanelExpanded = false;
        possibleBuildingDescriptionPanelExpanded = false;

        GetNamesFromFile();
    }

    private void Start()
    {
        UIBuildingConfirmed.Instance.BuildingConfirmed += BuildCountyImprovement;

        CreateCountiesDictionary();

        CreateResearchandBuildingList();

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
        SetNextDayJob(); // Done - This needs to have the name of the building the person is working on.

        MoveBuildingToCurrentBuildingList(); // Bugged.
    }

    private void MoveBuildingToCurrentBuildingList()
    {
        var possibleBuilding = counties[currentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber];

        Debug.Log("Possible Building Number: " + UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber);

        // Create the Current Building List and add one to the County so the County knows what it has built.
        counties[currentlySelectedCounty].currentBuildings.Add(new CurrentBuilding(possibleBuilding.name,
            possibleBuilding.description, 0, possibleBuilding.workCost, possibleBuilding.CurrentWorkers,
            possibleBuilding.maxEmployees, true, false));

        Debug.Log("Current Building Length: " + counties[currentlySelectedCounty].currentBuildings.Count);
        Debug.Log("County Build Name: " + counties[currentlySelectedCounty].currentBuildings[^1].name);

        // Assigned the Possible Building UI game object to the current building list.
        counties[currentlySelectedCounty].currentBuildings[^1].gameObject = possibleBuilding.gameObject;

        // Moving the possilbe building UI game object to the current building UI game object.
        counties[currentlySelectedCounty].currentBuildings[^1].gameObject.transform.SetParent(UICurrentBuildingsPanel.Instance.currentBuildingsGroupGameObject.transform);

        // Remove the building from the possible Building list.
        counties[currentlySelectedCounty].possibleBuildings.Remove(possibleBuilding);

        // Rename building to be the same as their possible building index.
        for (int i = 0; i < UIPossibleBuildingsPanel.Instance.possibleBuildingsGroupGameObject.transform.childCount; i++)
        {
            counties[currentlySelectedCounty].possibleBuildings[i].gameObject.name = i.ToString();
        }

        // Rename building to be the same as their current building index.
        for (int i = 0; i < UICurrentBuildingsPanel.Instance.currentBuildingsGroupGameObject.transform.childCount; i++)
        {
            counties[currentlySelectedCounty].currentBuildings[i].gameObject.name = i.ToString();
        }
    }

    private void DeductCostOfBuilding()
    {
        factions[0].Influence -= counties[currentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber].influenceCost;
    }

    private void SetNextDayJob()
    {
        int numberWorkers = 0;
        for (int i = 0; i < countyPopulationDictionary[currentlySelectedCounty].Count; i++)
        {
            if (countyPopulationDictionary[currentlySelectedCounty][i].nextActivity == AllText.Jobs.IDLE
                && numberWorkers < counties[currentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber].CurrentWorkers)
            {
                countyPopulationDictionary[currentlySelectedCounty][i].nextActivity = AllText.Jobs.BUILDING;
                countyPopulationDictionary[currentlySelectedCounty][i].nextBuilding =
                    counties[currentlySelectedCounty].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber].name;
                Debug.Log($"Name: {countyPopulationDictionary[currentlySelectedCounty][i].firstName} and job: {countyPopulationDictionary[currentlySelectedCounty][i].nextBuilding}");
                numberWorkers++;
                counties[currentlySelectedCounty].currentlyWorkingPopulation++; // We need to put this number on the county info panel.
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
    }

    private void OnDisable()
    {
        UIBuildingConfirmed.Instance.BuildingConfirmed -= SetNextDayJob;

        UIBuildingConfirmed.Instance.BuildingConfirmed -= MoveBuildingToCurrentBuildingList;
    }
    private void CreateResearchandBuildingList()
    {
        // This is going to go through all of the factions and create a list of their research, which is currently all completed.
        for (int i = 0; i < factions.Count; i++)
        {
            factions[i].researchItems.Add(new ResearchItem(
                null, null, null, AllText.BuildingName.FISHERSSHACK, AllText.Descriptions.FISHERSSHACK,
                null, null, 1, true, true));
            factions[i].researchItems.Add(new ResearchItem(
                null, null, null, AllText.BuildingName.FORESTERSSHACK, AllText.Descriptions.FORESTERSSHACK,
                null, null, 1, true, true));
            factions[i].researchItems.Add(new ResearchItem(
                null, null, null, AllText.BuildingName.GARDENERSSHACK, AllText.Descriptions.GARDENERSSHACK,
                null, null, 1, true, true));
            // The isResearchDone should start out as false, just set to done as testing.
            factions[i].researchItems.Add(new ResearchItem(
                null, null, null, "Elitism", AllText.Descriptions.ELITISM,
                null, null, 1, false, true));
        }
        /*
        researchItems.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.RESEARCHSSHACK, AllText.Descriptions.RESEARCHSSHACK,
            null, null, 1, true, true, null));
        researchItems.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.SCAVENGERSSHACK, AllText.Descriptions.SCAVENGERSSHACK,
            null, null, 1, true, true, null));
        researchItems.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.STONEWORKERSSHACK, AllText.Descriptions.STONEWORKERSSHACK,
            null, null, 1, true, true, null));
        researchItems.Add(new ResearchItem(
            null, null, null, "Basic Tactics - Guns", AllText.Descriptions.BASICTACTICSGUNS,
            null, null, 1, false, false, null));
        researchItems.Add(new ResearchItem(
            null, null, null, "Basic Tactics - Melee", AllText.Descriptions.BASICTACTICSMELEE,
            null, null, 1, false, false, null));
        researchItems.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.PRIMATIVEMELEESMITHSHACK, AllText.Descriptions.PRIMATIVEMELEESMITHSHACK,
            null, null, 1, true, false, null));
        researchItems.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.PRIMATIVERANGEDSMITHSHACK, AllText.Descriptions.PRIMATIVERANGEDSMITHSHACK,
            null, null, 1, true, false, null));
        researchItems.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.PRIMATIVEGUNSMITHSHACK, AllText.Descriptions.PRIMATIVEGUNSMITHSHACK,
            null, null, 1, true, false, null));
        researchItems.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.PRIMATIVEAMMOSHACK, AllText.Descriptions.PRIMATIVEAMMOSHACK,
            null, null, 1, true, false, null));
        researchItems.Add(new ResearchItem(
            null, null, null, AllText.BuildingName.PRIMATVEGUNAMMOSHACK, AllText.Descriptions.PRIMATVEGUNAMMOSHACK,
            null, null, 1, true, false, null));
        */
        // Should we eventually just have all the buildings generated from the research list?

        foreach (KeyValuePair<string, County> item in counties)
        {
            //Debug.Log(item.Key + "   " + item.Value);
            counties[item.Key].possibleBuildings.Add(new PossibleBuilding(
            AllText.BuildingName.FISHERSSHACK, AllText.Descriptions.FISHERSSHACK, 500, 7, 0, 5));
            counties[item.Key].possibleBuildings.Add(new PossibleBuilding(
                AllText.BuildingName.FORESTERSSHACK, AllText.Descriptions.FORESTERSSHACK, 500, 2, 0, 5));
            counties[item.Key].possibleBuildings.Add(new PossibleBuilding(
                AllText.BuildingName.GARDENERSSHACK, AllText.Descriptions.GARDENERSSHACK, 500, 7, 0, 5));
        }

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
        /*
        // Do we really need this?
        researchItems[0].possibleBuildings = possibleBuildings[0];
        researchItems[1].possibleBuildings = possibleBuildings[1];
        researchItems[2].possibleBuildings = possibleBuildings[2];
        */
    }

    private void FirstRunTopInfoBar()
    {
        UITopInfoBar.Instance.Influence = factions[0].Influence;
        UITopInfoBar.Instance.Money = factions[0].money;
        UITopInfoBar.Instance.Food = factions[0].food;
        UITopInfoBar.Instance.Scrap = factions[0].scrap;
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
        counties[CountyListCreator.Instance.countiesList[0].name] = new County(
            0, true, null, null, null, factionNameAndColors[1],
            Arrays.provinceName[0], "Coast", "Forest", "Ruin", 0, 0);
        counties[CountyListCreator.Instance.countiesList[1].name] = new County(
            1, true, null, null, null, factionNameAndColors[0],
            Arrays.provinceName[1], "Ruin", "Forest", "River", 0, 1);
        counties[CountyListCreator.Instance.countiesList[2].name] = new County(
            2, false, null, null, null, factionNameAndColors[0], // Temporarily set to the player faction for testing.
            Arrays.provinceName[1], "Coast", "Forest", "Mountain", 0, 0);
        counties[CountyListCreator.Instance.countiesList[3].name] = new County(
            3, false, null, null, null, factionNameAndColors[3],
            Arrays.provinceName[1], "Coast", "Forest", "Mountain", 0, 0);
        counties[CountyListCreator.Instance.countiesList[4].name] = new County(
            4, false, null, null, null, factionNameAndColors[4],
            Arrays.provinceName[1], "Mountain", "Forest", "Farm", 0, 0);
        counties[CountyListCreator.Instance.countiesList[5].name] = new County(
            5, false, null, null, null, factionNameAndColors[5],
            Arrays.provinceName[1], "Desert", "Mountain", "Forest", 0, 0);
        counties[CountyListCreator.Instance.countiesList[6].name] = new County(
            6, false, null, null, null, factionNameAndColors[6],
            Arrays.provinceName[1], "Mountain", "Desert", "Forest", 0, 0);
        /*
        // Create and add currentBuildings list to each county so each county knows what it has built.
        for(int i = 0; i < counties.Count; i++)
        {
            counties[CountyListCreator.Instance.countiesList[i].name].currentBuilding = new CurrentBuilding();
        }
        */
    }

    private void CreatePopulation()
    {
        // Create various county specific data.
        for (int countyIndex = 0; countyIndex < counties.Count; countyIndex++)
        {
            string countyName = CountyListCreator.Instance.countiesList[countyIndex].name;
            //string factionName = factionNameAndColors[countyIndex].name;

            // There should probably be some sort of null check in here?
            // Initilizes the List in the Dictionaries for Counties.
            countyPopulationDictionary[countyName] = new List<CountyPopulation>();
            //factionHeroesDictionary[factionName] = new List<Hero>();

            // Get game object for center of county and assign to correct county in list.
            // Why did we do that here?
            counties[countyName].countyCenterGameObject =
                countyListGameObject.transform.GetChild(countyIndex).GetChild(0).gameObject;

            if (counties[countyName].isCapital == true)
            {
                //GenerateLeaders(factionName, countyIndex);
                GeneratePopulation(countyName, totalCapitolPop);
                counties[countyName].population = totalCapitolPop;
            }
            else
            {
                int normalPopulation = Random.Range(minimumCountyPop, maximumCountyPop);
                //GenerateLeaders(factionName, countyIndex);
                GeneratePopulation(countyName, normalPopulation);
                counties[countyName].population = normalPopulation;
            }
        }
    }


    private void GeneratePopulation(string countyName, int totalPopulation)
    {
        var countyPopulation = countyPopulationDictionary[countyName];
        for (int i = 0; i < totalPopulation; i++)
        {
            
            // This adds to the Dictionary List a new person.
            countyPopulation.Add(new CountyPopulation(null, null, false, false, true, false, 0, AllText.Jobs.IDLE, null, AllText.Jobs.IDLE, null));

            // Generates Persons Last Name
            int randomLastNameNumber = Random.Range(0, lastNames.Length);
            countyPopulation[i].lastName =
                lastNames[randomLastNameNumber];

            // Determine the persons sex and first name
            int randomSexNumber = Random.Range(0, 2);
            int randomFemaleNameNumber = Random.Range(0, femaleNames.Length);
            int randomMaleNameNumber = Random.Range(0, maleNames.Length);
            if (randomSexNumber == 0)
            {
                countyPopulation[i].isMale = true;
                countyPopulation[i].firstName =
                    maleNames[randomMaleNameNumber];

            }
            else
            {
                countyPopulation[i].isMale = false;
                countyPopulation[i].firstName =
                    femaleNames[randomFemaleNameNumber];
            }

            int randomAgeNumber = Random.Range(18, 61);
            countyPopulation[i].age = randomAgeNumber;

            if(i == 0)
            {
                countyPopulation[i].isFactionLeader = true;
                countyPopulation[i].isHero = true;
            }


            Debug.Log("Name: " + countyPopulationDictionary[countyName][i].firstName + " " +
            countyPopulationDictionary[countyName][i].lastName + " is Hero? " + 
            countyPopulationDictionary[countyName][i].isHero);

        }
    }
}

/*  Removed for now because we merged the leaders into the County Population.
 *     private void GenerateLeaders(string factionName, int countyIndex)
    {
        // Add Leaders to each faction.
        // This adds to the Heroes Dictionary List a new Leader.  It is jank because we want the leader in Portland to be me.
        // This will instead need to eventually check if this is a player faction vs an AI faction.
        if (playerFaction == factionName)
        {
            factionHeroesDictionary[factionName].Add(new Hero
                ("Lord", "Haywire", true, 30, CountyListCreator.Instance.countiesList[1].name, AllText.Jobs.IDLE, AllText.Jobs.IDLE));
        }
        else
        {
            factionHeroesDictionary[factionName].Add(new Hero(null, null, false, 0, CountyListCreator.Instance.countiesList[countyIndex].name, AllText.Jobs.IDLE, AllText.Jobs.IDLE));
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
            
        }
    }
*/
