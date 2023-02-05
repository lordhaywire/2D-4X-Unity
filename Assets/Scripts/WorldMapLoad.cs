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



    private void Awake()
    {
        // Provinces added to Provinces Dictionary.
        counties[Arrays.name[0]] = new County(0, null, null, "Enemy1", "PlayersNation", 6000);
        counties[Arrays.name[1]] = new County(1, null, null, "Player", "PlayersNation", 10000);
        counties[Arrays.name[2]] = new County(2, null, null, "Player", "EnemyNation", 5000);
        counties[Arrays.name[3]] = new County(3, null, null, "Enemy1", "EnemyNation", 7000);
        counties[Arrays.name[4]] = new County(4, null, null, "Enemy1", "EnemyNation", 1000);
        counties[Arrays.name[5]] = new County(5, null, null, "Enemy1", "EnemyNation", 2000);
        counties[Arrays.name[6]] = new County(6, null, null, "Enemy1", "EnemyNation", 3000);

        // Get game object for center of county and assign to correct county in list.
        for (int i = 0; i < counties.Count; ++i)
        {
            counties[Arrays.name[i]].countyCenterGameObject = countyListGameObject.transform.GetChild(i).GetChild(0).gameObject;
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