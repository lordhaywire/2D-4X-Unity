using System.Collections.Generic;
using UnityEngine;

public class WorldMapLoad : MonoBehaviour
{
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

        /*
        countyPopulationDictionary[Arrays.countyName[0]] = new List<CountyPopulation>();
        countyPopulationDictionary[Arrays.countyName[0]].Add(new CountyPopulation(null, null, false));
        countyPopulationDictionary[Arrays.countyName[0]][0].lastName = "First One";
        Debug.Log("0Last Name: " + countyPopulationDictionary[Arrays.countyName[0]][0].lastName);
        countyPopulationDictionary[Arrays.countyName[0]].Add(new CountyPopulation(null, null, false));
        countyPopulationDictionary[Arrays.countyName[0]][1].lastName = "Seconds One";
        Debug.Log("1Last Name: " + countyPopulationDictionary[Arrays.countyName[0]][1].lastName);
        */
        // Create various county specific data.
        
        for (int countyIndex = 0; countyIndex < counties.Count; countyIndex++)
        {
            string name = Arrays.countyName[countyIndex];
            countyPopulationDictionary[name] = new List<CountyPopulation>();

            // Get game object for center of county and assign to correct county in list.
            counties[name].countyCenterGameObject = 
                countyListGameObject.transform.GetChild(countyIndex).GetChild(0).gameObject;

            if (counties[name].isCapital == true)
            {
                int totalPopulation = 10;
                GeneratePopulation(name,totalPopulation);
            }
            else
            {
                int totalPopulation = Random.Range(3, 9);
                GeneratePopulation(name,totalPopulation);
            }
        }

        // This is just temp till we do character creation.
        playerName = "Player";
        enemyName = "Enemy1";

        // This is set up this way so it can be a static variable.
        countyInfoPanel = uICanvas.transform.GetChild(1).gameObject;
        Debug.Log("County Info Panel: " + countyInfoPanel);
        armyInfoPanel = uICanvas.transform.GetChild(2).gameObject;
        Debug.Log("Army Info Panel: " + armyInfoPanel);

        /*
        countyInfoPanel = GameObject.Find(countyInfoPanelName);
        countyInfoPanel.SetActive(false);
        armyInfoPanel = GameObject.Find(armyPanelName);
        armyInfoPanel.SetActive(false);
        */
        // Leader getting added - This is just temp till we do character creation.
        heroes.Add(new Hero("Lord", "Haywire", Arrays.countyName[1],  null));

        Debug.Log("Leader Name: " + heroes[0].firstName + " " + heroes[0].lastName);

        //Debug.Log("Test County Population Name Portland: " +
        //    countyPopulationDictionary["Portland Oregon"][6].lastName);
    }

    private void GeneratePopulation(string name, int totalPopulation)
    {   
        for (int populationIndex = 0; populationIndex < totalPopulation; populationIndex++)
        {
            Debug.Log("Population Index: " + populationIndex);
            Debug.Log("Name Variable: " + name);

            countyPopulationDictionary[name].Add(new CountyPopulation(null, null, false));

            int randomIndex = Random.Range(0, Arrays.lastName.Length);
            countyPopulationDictionary[name][populationIndex].lastName =
                Arrays.lastName[randomIndex];
            Debug.Log("Random Name? " +
                countyPopulationDictionary[name][populationIndex].lastName);
        }
    }
}
