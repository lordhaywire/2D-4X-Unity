using System.Collections.Generic;
using UnityEngine;

public class WorldMapLoad  : MonoBehaviour
{
    [SerializeField] GameObject provinceListGameObject;
    [SerializeField] string provincePanelName;
    [SerializeField] string armyPanelName;
    
    public static GameObject provincePanel;
    public static GameObject armyPanel;

   

    // This is just temp till we do character creation.
    public static string playerName;
    public static string enemyName;

    // Initialize Province Dictionary.
    public static Dictionary<string, List<Province>> provinces = new();

    // Initialize Army List.
    public static List<Army> armies = new();

    private void Awake()
    {
        // Provinces added to Provinces Dictionary.
        provinces[AllText.Counties.NAME_1] = new List<Province>
        {
        new Province (0, null, "Enemy1", "PlayersNation", 6000)
        };
        provinces[AllText.Counties.NAME_2] = new List<Province>
        {
        new Province (1, null, "Player", "PlayersNation", 10000)
        };

        provinces[AllText.Counties.NAME_3] = new List<Province>
        {
        new Province (2, null, "Player", "EnemyNation", 5000)
        };
        provinces[AllText.Counties.NAME_4] = new List<Province>
        {
        new Province (3, null, "Enemy1", "EnemyNation", 7000)
        };

        provinces[AllText.Counties.NAME_1][0].provinceCenterGameObject = provinceListGameObject.transform.GetChild(0).GetChild(0).gameObject;
        provinces[AllText.Counties.NAME_2][0].provinceCenterGameObject = provinceListGameObject.transform.GetChild(1).GetChild(0).gameObject;
        provinces[AllText.Counties.NAME_3][0].provinceCenterGameObject = provinceListGameObject.transform.GetChild(2).GetChild(0).gameObject;
        provinces[AllText.Counties.NAME_4][0].provinceCenterGameObject = provinceListGameObject.transform.GetChild(3).GetChild(0).gameObject;
        /*
        Debug.Log(provinces[AllText.Counties.NAME_1][0].provinceCenterGameObject);
        Debug.Log(provinces[AllText.Counties.NAME_2][0].provinceCenterGameObject);
        Debug.Log(provinces[AllText.Counties.NAME_3][0].provinceCenterGameObject);
        Debug.Log(provinces[AllText.Counties.NAME_4][0].provinceCenterGameObject);
        */
        // Armies added to Armies list
        armies.Add(new Army(null, "Player","Fuck Stick", 10000));

        // This is just temp till we do character creation.
        playerName = "Player";
        enemyName = "Enemy1";

        provincePanel = GameObject.Find(provincePanelName);
        provincePanel.SetActive(false);
        armyPanel = GameObject.Find(armyPanelName);
        armyPanel.SetActive(false);
    }
}
