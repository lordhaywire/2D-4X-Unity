using System.Collections.Generic;
using UnityEngine;

public class WorldMapLoad  : MonoBehaviour
{   
    public static Dictionary<string, List<Province>> provinces = new();

    // This is just temp till we do character creation.
    public static string playerName;
    public static string enemyName;

    private void Awake()
    {

        provinces["North"] = new List<Province>
        {
        new Province (1, "Independent", "PlayersNation", 10000)
        };
        provinces["West"] = new List<Province>
        {
        new Province (2, "Player", "PlayersNation", 10000)
        };

        provinces["East"] = new List<Province>
        {
        new Province (3, "Enemy", "EnemyNation", 5000)
        };

        // This is just temp till we do character creation.
        playerName = "Player";
        enemyName = "Enemy";
    }
}
