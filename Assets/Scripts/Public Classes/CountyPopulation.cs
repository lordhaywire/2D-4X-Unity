using System;
using UnityEngine;

[Serializable]
public class CountyPopulation
{
    public GameObject location;
    public GameObject destination;

    public string faction;

    public string firstName;
    public string lastName;
    public bool isMale;
    public int age;

    public bool isFactionLeader;
    public bool isHero;
    public bool isWorker;

    [Header("Perks")]
    public bool leaderOfPeoplePerk;

    [Header("Attributes")]

    [Header("Skills")]
    public int constructionSkill;

    [Header("Activities")]
    public string currentActivity;
    public Building currentBuilding; // What this person is currently building that day.
    public string nextActivity;
    public Building nextBuilding;

    [Header("Token")]
    public bool isSpawned;
    
    public CountyPopulation(GameObject location, GameObject destination, string faction
        , string firstName, string lastName, bool isMale, int age, bool isFactionLeader, bool isHero, bool isWorker
         , bool leaderOfPeoplePerk, int constructionSkill, string currentActivity
        , Building currentBuilding, string nextActivity, Building nextBuilding, bool isSpawned)
    {
        this.location = location;
        this.destination = destination;
        this.faction = faction;

        this.firstName = firstName;
        this.lastName = lastName;
        this.isMale = isMale;
        this.age = age;

        this.isFactionLeader = isFactionLeader;
        this.isHero = isHero;
        this.isWorker = isWorker;

        this.leaderOfPeoplePerk = leaderOfPeoplePerk;

        this.constructionSkill = constructionSkill;

        this.currentActivity = currentActivity;
        this.currentBuilding = currentBuilding;
        this.nextActivity = nextActivity;
        this.nextBuilding = nextBuilding;

        this.isSpawned = isSpawned;
    }
}
