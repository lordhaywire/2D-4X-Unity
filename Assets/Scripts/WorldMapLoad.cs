using System.Collections.Generic;
using UnityEngine;

public class WorldMapLoad : MonoBehaviour
{
    [SerializeField] GameObject countyListGameObject; // This is the gameObject that all the counties are under in the inspector.
    [SerializeField] string countyInfoPanelName;
    [SerializeField] string armyPanelName;

    public static GameObject countyInfoPanel;
    public static GameObject armyInfoPanel;

    // This is just temp till we do character creation.
    public static string playerName;
    public static string enemyName;

    // Initialize County Dictionary.
    public static Dictionary<string, County> counties = new();

    // Initialize Army List.
    public static List<Army> armies = new();

    // Array of county names.
    public new string[] name =
    {
        "Cowlitz Washington",
        "Portland Oregon",
        "Tillamook Oregon",
        "Douglas Oregon",
        "Wasco Oregon",
        "Harney Oregon",
        "Umatilla Oregon",
    };


    private void Awake()
    {
        // Provinces added to Provinces Dictionary.
        counties[name[0]] = new County(0, null, "Enemy1", "PlayersNation", 6000);
        counties[name[1]] = new County(1, null, "Player", "PlayersNation", 10000);
        counties[name[2]] = new County(2, null, "Player", "EnemyNation", 5000);
        counties[name[3]] = new County(3, null, "Enemy1", "EnemyNation", 7000);
        counties[name[4]] = new County(4, null, "Enemy1", "EnemyNation", 1000);
        counties[name[5]] = new County(5, null, "Enemy1", "EnemyNation", 2000);
        counties[name[6]] = new County(6, null, "Enemy1", "EnemyNation", 3000);

        for (int i = 0; i < counties.Count; ++i)
        {
            counties[name[i]].countyCenterGameObject = countyListGameObject.transform.GetChild(i).GetChild(0).gameObject;
        }



        // This is just temp till we do character creation.
        playerName = "Player";
        enemyName = "Enemy1";

        // This is set up this way so it can be static.
        countyInfoPanel = GameObject.Find(countyInfoPanelName);
        countyInfoPanel.SetActive(false);
        armyInfoPanel = GameObject.Find(armyPanelName);
        armyInfoPanel.SetActive(false);
    }
}
