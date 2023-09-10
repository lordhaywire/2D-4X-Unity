using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class County
{
    public event Action IdleWorkersChanged;

    public int countyID; // I believe this is being used now.
    public GameObject gameObject; // This is the game object that is the county.
    public bool isCapital;
    public bool isIndependentCapital;
    public SpriteRenderer spriteRenderer;
    public BuildImprovements buildImprovements;
    public Faction faction;
    public string province;
    public string biomePrimary;
    public string biomeSecondary;
    public string biomeTertiary;
    public List<CountyPopulation> countyPopulation;
    //public List<GameObject> possibleBuildings;
    //public List<GameObject> currentBuildings;

    public int spawnedHeroCount; // Count of all the heroes in the county.
    public int population;
    [SerializeField] private int idleWorkers;

    public int IdleWorkers
    {
        get { return idleWorkers; }
        set
        {
            idleWorkers = value;
            //Debug.Log("Invoke Mother Fucker!");
            IdleWorkersChanged?.Invoke();
        }
    }

    public County(int countyID, GameObject gameObject, bool isCapital, bool isIndependentCaptial,
        SpriteRenderer spriteRenderer, BuildImprovements buildImprovements, Faction faction,
        string province, string biomePrimary, string biomeSecondary, string biomeTertiary,
        int spawnedHeroCount, int population, int idleWorkers)
    {
        this.countyID = countyID;
        this.gameObject = gameObject;
        this.isCapital = isCapital;
        this.isIndependentCapital = isIndependentCaptial;

        this.spriteRenderer = spriteRenderer;
        this.buildImprovements = buildImprovements;
        this.faction = faction;
        this.province = province;
        this.biomePrimary = biomePrimary;
        this.biomeSecondary = biomeSecondary;
        this.biomeTertiary = biomeTertiary;
        //possibleBuildings = new List<GameObject>(); // This initializes the list. It is not in the constructor. 
        //currentBuildings = new List<GameObject>(); // This initializes the list. It is not in the constructor. 
        this.spawnedHeroCount = spawnedHeroCount;
        this.population = population;
        IdleWorkers = idleWorkers;
    }
}
