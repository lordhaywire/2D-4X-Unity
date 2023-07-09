using System;
using UnityEngine;

[Serializable]
public class Hero
{
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
    }
}
