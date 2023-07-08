using System;
using UnityEngine;

[Serializable]
public class Hero
{
    /*
    public int heroIndex;
    public int heroStackIndex;
    */



    //public HeroMovement heroMovement; // We might be able to get rid of this because we are assigning the gameobject
    // to currentlySelectedHero;
    //public List<HeroStack> heroStacks;
    //public TokenInfo tokenInfo; // We don't need this here, I don't think.
    //public bool isSpawned;

    public GameObject gameObject;
    public CountyPopulation countyPopulation;
    public Faction faction;
    public GameObject location;
    public GameObject destination;

    public bool justMoved;
    public bool startTimer;
    public bool isCountingDown;

    public Hero(GameObject gameObject, CountyPopulation countyPopulation, Faction faction, GameObject location
        , GameObject destination, bool justMoved, bool startTimer, bool isCountingDown)
    {
        this.gameObject = gameObject;
        this.countyPopulation = countyPopulation;

        this.faction = faction;
        this.location = location;
        this.destination = destination;

        this.justMoved = justMoved;
        this.startTimer = startTimer;
        this.isCountingDown = isCountingDown;

        /*
        this.gameObject = gameObject;
        this.heroMovement = heroMovement;
        this.tokenInfo = tokenComponents;
        this.isSpawned = isSpawned;
        */

        /*
        this.heroIndex = heroIndex;
        this.heroStackIndex = heroStackIndex;
        this.countyPopulationIndex = countyPopulationIndex;
        */

    }
}
