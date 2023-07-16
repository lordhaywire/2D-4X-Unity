using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldMapLoad : MonoBehaviour
{
    public static WorldMapLoad Instance { get; private set; }

    public event Action RefreshBuildingPanels;

    public GameObject currentlyRightClickedCounty;
    public CountyPopulation currentlySelectedCountyPopulation;

    [SerializeField] private GameObject currentlySelectedCounty;
    [SerializeField] private GameObject countyInfoPanelGameObject;
    public GameObject CurrentlySelectedCounty
    {
        get
        {
            return currentlySelectedCounty;
        }
        set
        {
            currentlySelectedCounty = value;
            if (currentlySelectedCounty != null)
            {
                countyInfoPanel.GetComponent<UIHeroScrollViewRefresher>().RefreshPanel();
            }
            else
            {
                countyInfoPanelGameObject.SetActive(false);
            }
        }
    }

    [SerializeField] private GameObject currentlySelectedHero;
    public GameObject CurrentlySelectedToken
    {
        get
        {
            return currentlySelectedHero;
        }
        set
        {
            if (currentlySelectedHero != null)
            {
                currentlySelectedHero.GetComponent<SpriteRenderer>().sprite = HeroTokenSprites.Instance.heroUnselectedSprite;
            }
            if (value == null)
            {
                currentlySelectedHero.GetComponent<SpriteRenderer>().sprite = HeroTokenSprites.Instance.heroUnselectedSprite;
            }
            currentlySelectedHero = value;
            if (currentlySelectedHero != null)
            {
                currentlySelectedHero.GetComponent<SpriteRenderer>().sprite = HeroTokenSprites.Instance.heroSelectedSprite;
            }
        }
    }

    [SerializeField] private int totalCapitolPop;
    [SerializeField] private int minimumCountyPop;
    [SerializeField] private int maximumCountyPop;


    [SerializeField] private GameObject countyListGameObject;
    [SerializeField] private GameObject uICanvas;

    public bool currentBuildingDescriptionPanelExpanded;
    public bool possibleBuildingDescriptionPanelExpanded;
    public bool populationDescriptionPanelOpen;
    public bool populationInfoPanelOpen;
    public bool populationInfoPanelOpenedByHeroListClick;

    //public bool DevView;

    public GameObject countyInfoPanel;
    public GameObject heroInfoPanel;
    public GameObject armyInfoPanel;

    public GameObject populationInfoPanel;

    // Because the button that opens this is in a Prefab.
    public GameObject populationDescriptionPanel;

    // This is just temp bullshit.
    public Faction playerFaction;
    public int playerFactionID;
    public int dailyInfluenceGain;
    public int costOfHero;

    // Initialize County Dictionary List.
    public Dictionary<string, County> counties = new();

    // A Dictionary for each county that holds a list of their population.
    public Dictionary<string, List<CountyPopulation>> countyPopulationDictionary = new();

    // Initialize army list of spawned spawnedArmies.
    public List<SpawnedArmy> spawnedArmies = new();

    // Initialize army list of spawned heroes.
    public List<Hero> heroes = new();

    // List of all spawned tokens in a county.
    //public Dictionary<string, List<SpawnedTokenList>> countyHeroTokens = new();

    // Initialize Factions list that will be used with the counties.
    public List<FactionNameAndColor> factionNameAndColors = new();

    public List<Faction> factions = new();

    // Arrays for County Population generation.
    private string[] maleNames;
    private string[] femaleNames;
    private string[] lastNames;

    private void Awake()
    {
        //currentlySelectedCountyPopulation = 57; // This is just a test number for when there is more then 1 hero.

        Instance = this;
        currentBuildingDescriptionPanelExpanded = false;
        possibleBuildingDescriptionPanelExpanded = false;

        GetNamesFromFile();
    }

    private void Start()
    {
        UIBuildingConfirmed.Instance.BuildingConfirmed += BuildCountyImprovement;

        AssignFactionNameAndColorToFaction();

        CreateCountiesDictionary();

        CreateResearchandBuildingList();

        // This is just temp till we do character creation.
        playerFaction = factions[0];
        playerFactionID = 0;

        CreatePopulation();

        FirstRunTopInfoBar();

        // Get rid of extra variables.
        lastNames = null;
        femaleNames = null;
        maleNames = null;
    }

    private void BuildCountyImprovement()
    {
        Banker.Instance.DeductCostOfBuilding();
        MoveBuildingToCurrentBuildingList();
        SetNextDayJob();
    }

    private void MoveBuildingToCurrentBuildingList()
    {
        var possibleBuilding = counties[CurrentlySelectedCounty.name].possibleBuildings[UIPossibleBuildingsPanel.Instance.PossibleBuildingNumber];

        // Create the Current Building List and add list to the County so the County knows what it has built.
        counties[CurrentlySelectedCounty.name].currentBuildings.Add(new CurrentBuilding(possibleBuilding.name,
            possibleBuilding.description, 0, possibleBuilding.workCost, possibleBuilding.CurrentWorkers,
            possibleBuilding.maxEmployees, true, false, null));

        // This makes is so that the population gets the correct building to build.
        UICurrentBuildingsPanel.Instance.CurrentBuildingNumber =
            counties[CurrentlySelectedCounty.name].currentBuildings.Count - 1;

        // Remove the building from the possible Building list.
        counties[CurrentlySelectedCounty.name].possibleBuildings.Remove(possibleBuilding);

        RefreshBuildingPanels?.Invoke();
    }

    private void SetNextDayJob()
    {
        int numberWorkers = 0;
        for (int i = 0; i < countyPopulationDictionary[CurrentlySelectedCounty.name].Count; i++)
        {
            if (countyPopulationDictionary[CurrentlySelectedCounty.name][i].nextActivity == AllText.Jobs.IDLE
                && numberWorkers <
                counties[CurrentlySelectedCounty.name].currentBuildings[UICurrentBuildingsPanel.Instance.CurrentBuildingNumber].currentWorkers)
            {
                countyPopulationDictionary[CurrentlySelectedCounty.name][i].nextActivity = AllText.Jobs.BUILDING;
                countyPopulationDictionary[CurrentlySelectedCounty.name][i].nextBuilding =
                    counties[CurrentlySelectedCounty.name].currentBuildings[UICurrentBuildingsPanel.Instance.CurrentBuildingNumber];

                numberWorkers++; // Why is this incrementing when it should match the i in the for loop?
                counties[CurrentlySelectedCounty.name].currentlyWorkingPopulation++; // We need to put this number on the county info panel.
            }
        }
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


    }

    private void FirstRunTopInfoBar()
    {
        UITopInfoBar.Instance.Influence = factions[playerFactionID].Influence;
        UITopInfoBar.Instance.Money = factions[playerFactionID].money;
        UITopInfoBar.Instance.Food = factions[playerFactionID].food;
        UITopInfoBar.Instance.Scrap = factions[playerFactionID].scrap;
    }

    private void AssignFactionNameAndColorToFaction()
    {
        for (int i = 0; i < factions.Count; i++)
        {
            factions[i].factionNameAndColor = factionNameAndColors[i];
            //Debug.Log("Faction Names: " + factions[i].factionNameAndColor.name);
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
        // Why aren't these counties.Add or why is counyPopulation below countyPopulation.Add instead of = new.
        // I believe this is because the counties dictionary list has only been initialized and it needs the name (string) added as well.
        // I think this idea might be wrong and it is really because it is a public class, not a list.
        // Counties added to counties Dictionary.
        // Types of biomes - Coast, Desert, Farm, Forest, Mountain, Ruin, River
        // The first county is the player county.
        
        counties[CountyListCreator.Instance.countiesList[0].name] = new County(
            0, true, CountyListCreator.Instance.countiesList[0].gameObject, null, factions[1],
            Arrays.provinceName[0], "Coast", "Forest", "Ruin", 0, 0, 0);
        counties[CountyListCreator.Instance.countiesList[1].name] = new County(
            1, true, CountyListCreator.Instance.countiesList[1].gameObject, null, factions[0],
            Arrays.provinceName[1], "Ruin", "Forest", "River", 0, 0, 1);
        counties[CountyListCreator.Instance.countiesList[2].name] = new County(
            2, false, CountyListCreator.Instance.countiesList[2].gameObject, null, factions[0], // Temporarily set to the player faction for testing.
            Arrays.provinceName[1], "Coast", "Forest", "Mountain", 0, 0, 0);
        counties[CountyListCreator.Instance.countiesList[3].name] = new County(
            3, false, CountyListCreator.Instance.countiesList[3].gameObject, null, factions[2],
            Arrays.provinceName[1], "Coast", "Forest", "Mountain", 0, 0, 0);
        counties[CountyListCreator.Instance.countiesList[4].name] = new County(
            4, false, CountyListCreator.Instance.countiesList[4].gameObject, null, factions[3],
            Arrays.provinceName[1], "Mountain", "Forest", "Farm", 0, 0, 0);
        counties[CountyListCreator.Instance.countiesList[5].name] = new County(
            5, false, CountyListCreator.Instance.countiesList[5].gameObject, null, factions[4],
            Arrays.provinceName[1], "Desert", "Mountain", "Forest", 0, 0, 0);
        counties[CountyListCreator.Instance.countiesList[6].name] = new County(
            6, false, CountyListCreator.Instance.countiesList[6].gameObject, null, factions[5],
            Arrays.provinceName[1], "Mountain", "Desert", "Forest", 0, 0, 0);

        // We should expand this for loop if possible.
        for(int i = 0; i < CountyListCreator.Instance.countiesList.Count; i++)
        {
            CountyListCreator.Instance.countiesList[i].gameObject.GetComponent<CountyInfo>().county =
                counties[CountyListCreator.Instance.countiesList[i].name];
        }
    }

    private void CreatePopulation()
    {
        // Create various county specific data.
        for (int i = 0; i < counties.Count; i++)
        {
            string countyName = CountyListCreator.Instance.countiesList[i].name;

            // There should probably be some sort of null check in here?
            // Initilizes the List in the Dictionaries for Counties.
            countyPopulationDictionary[countyName] = new List<CountyPopulation>();

            if (counties[countyName].isCapital == true)
            {
                GeneratePopulation(countyName, totalCapitolPop);
                counties[countyName].population = totalCapitolPop;
            }
            else
            {
                int normalPopulation = UnityEngine.Random.Range(minimumCountyPop, maximumCountyPop);
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
            countyPopulation.Add(new CountyPopulation(0, null, null, false, null, true, false, 0, false, 0, AllText.Jobs.IDLE, null, AllText.Jobs.IDLE, null));

            // Assign the County Populations ID
            countyPopulation[i].countyPopulationID = i;

            // Generates Persons Last Name
            int randomLastNameNumber = UnityEngine.Random.Range(0, lastNames.Length);
            countyPopulation[i].lastName =
                lastNames[randomLastNameNumber];

            // Determine the persons sex and first name
            int randomSexNumber = UnityEngine.Random.Range(0, 2);
            int randomFemaleNameNumber = UnityEngine.Random.Range(0, femaleNames.Length);
            int randomMaleNameNumber = UnityEngine.Random.Range(0, maleNames.Length);
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

            int randomAgeNumber = UnityEngine.Random.Range(18, 61);
            countyPopulation[i].age = randomAgeNumber;

            if (counties[countyName].faction.factionLeader == null)
            {
                if (i == 0)
                {
                    counties[countyName].faction.factionLeader = countyPopulation[i];
                    countyPopulation[i].isFactionLeader = true;

                    countyPopulation[i].leaderOfPeoplePerk = true;

                    if (counties[countyName].faction.factionNameAndColor.name == playerFaction.factionNameAndColor.name)
                    {
                        Hero hero = new(null, countyPopulation[i], playerFaction,
                            counties[countyName].gameObject, null, false, false, false);

                        heroes.Add(hero);

                        countyPopulation[i].hero = hero;
                    }
                }
            }

            // Generate random skill level for each population.
            var randomConstructionSkill = UnityEngine.Random.Range(20, 81);
            countyPopulation[i].constructionSkill = randomConstructionSkill;
        }
    }
    private void OnDisable()
    {
        UIBuildingConfirmed.Instance.BuildingConfirmed -= BuildCountyImprovement;
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
