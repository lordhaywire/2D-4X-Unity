using System;
using TMPro;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    //public static HeroMovement Instance;

    public GameObject timerCanvasGameObject;
    public TextMeshProUGUI timerText;
    public bool heroMove;
    private float speed; // How fast the tokens move.

    private float localMinutes;
    private int localHours;
    private int localDays;


    // Eventually this will be update depending on the distance to destination.
    private readonly int minutesTillArrival = 10; // This is a temp aount of Hours for testing.
    private readonly int hoursTillArrival = 4; // This is a temp amount of Hours.  
    private readonly int daysTillArrival = 0; // This is a temp amount of days.  

    //private float lastCheckTime = 0;

    public bool isTimeToDestinationSet;

    public float LocalMinutes
    {
        get
        {
            return localMinutes;
        }
        set
        {
            localMinutes = value;
            //Debug.Log("Local Minutes Getter Setter Local Hours: " + LocalHours);
            //Debug.Log("Local Minutes Getter Setter Local Minutes: " + LocalMinutes);
            if (localMinutes > 60)
            {
                LocalHours++;
                localMinutes -= 60;
            }
        }
    }

    public int LocalHours
    {
        get
        {
            return localHours;
        }
        set
        {
            localHours = value;
            //Debug.Log("Local Hours Getter Setter Local Hours: " + LocalHours);
            //Debug.Log("Local Hours Getter Setter Local Minutes: " + LocalMinutes);
            if (localHours >= 24)
            {
                localDays++;
                localHours -= 24;
            }
        }
    }

    private void Awake()
    {
        speed = WorldMapLoad.Instance.tokenSpeed;
    }

    private void FixedUpdate()
    {
        if (isTimeToDestinationSet == true)
        {
            HeroTimer();
        }
        if (heroMove == true)
        {
            HeroMove();
        }
    }
    public void StartHeroMovement()
    {
        GameObject destination = WorldMapLoad.Instance.CurrentlySelectedHero.GetComponent<TokenInfo>().hero.destination;

        if (isTimeToDestinationSet == true && destination != WorldMapLoad.Instance.currentlyRightClickedCounty)
        {
            StopTimer();
            return;
        }
        if (isTimeToDestinationSet == false)
        {
            WorldMapLoad.Instance.CurrentlySelectedHero.GetComponent<TokenInfo>().hero.destination 
                = WorldMapLoad.Instance.currentlyRightClickedCounty;
            SetInitialTime(); // This is the start of the timer, not hero movement.       
        }
    }

    private void SetInitialTime()
    {
        if (isTimeToDestinationSet == false)
        {
            // This needs to be in the order of Days > Hours > Minutes so that the Getter setter works.
            localDays = TimeKeeper.Instance.days + daysTillArrival;
            LocalHours = TimeKeeper.Instance.Hours + hoursTillArrival;
            LocalMinutes = TimeKeeper.Instance.minutes + minutesTillArrival;  //Cast from Float to Int.

            isTimeToDestinationSet = true;

            timerCanvasGameObject.SetActive(true);
        }
    }

    private void HeroTimer()
    {
        {
            // We can probably remove current time at some point, but right now for testing lets leave it.
            // Reformat this code at some point.
            timerText.text = string.Format("Time till arrival: Day " + localDays + " {0:00}:{1:00}" +
                "\n Current Time: Day " + TimeKeeper.Instance.days +
                " {2:00}:{3:00}", LocalHours, LocalMinutes, TimeKeeper.Instance.Hours, (int)Math.Round(TimeKeeper.Instance.minutes));

            // This still is be broken.
            if (TimeKeeper.Instance.days == localDays && TimeKeeper.Instance.Hours
                == LocalHours && TimeKeeper.Instance.minutes >= LocalMinutes)
            {
                // Why is this here?
                GetComponent<TokenInfo>().hero.startTimer = false;

                // This starts the heroMovement for the hero to move.
                timerCanvasGameObject.SetActive(false);

                WorldMapLoad.Instance.CurrentlySelectedHero = null;

                // Timer is done, so this is false.
                isTimeToDestinationSet = false;

                // This needs to happen the minute the hero starts moving. Stacking the heroes starting location tokens.
                GetComponent<TokenInfo>().hero.location.GetComponent<CountyHeroStacking>().StackTokens();

                // This needs to happen here because the hero starting location would still have them in it.
                ChangeSpawnedTokenList();

                // Timer is done so hero should start moving.
                heroMove = true;

                //Debug.Log("Time is up!");
            }
        }
    }

    public void StopTimer()
    {
        GetComponent<TokenInfo>().hero.startTimer = false;
        GetComponent<TokenInfo>().hero.isCountingDown = false;
        isTimeToDestinationSet = false; // This will cause the HeroTimer to reset.
        timerCanvasGameObject.SetActive(false);
    }
    private void HeroMove()
    {
        float step = speed * Time.fixedDeltaTime;
        Hero hero = GetComponent<TokenInfo>().hero;
        GameObject destination = GetComponent<TokenInfo>().hero.destination;

        // Turn off hero stack count because the hero is moving.
        GetComponent<TokenInfo>().counterGameObject.SetActive(false);

        // Move the token.
        transform.position = Vector2.MoveTowards(transform.position, destination.transform.position, step);

        // Refresh the hero scroll view immediately when the hero starts moving. What county is getting refreshed?
        UIHeroScrollViewRefresher.Instance.RefreshPanel();

        // If the hero gets to the hero spawn location its move is considered done.
        if (transform.position == destination.transform.position)
        {
            heroMove = false;

            // Change the heroes current location and destination.
            hero.location = hero.destination;
            hero.destination = null;
            hero.justMoved = true;

            // Stack the hero tokens at destination location.
            destination.GetComponent<CountyHeroStacking>().StackTokens();

            hero.isCountingDown = false; // Why do we have this if he have isTimeToDestinationSet?
        }

        // Cancels the move if the player right clicks on the county the army is already in.
        if (hero.startTimer == true && hero.location == hero.destination)
        {
            StopTimer();
        }
    }

    private void ChangeSpawnedTokenList()
    {
        Hero hero = WorldMapLoad.Instance.CurrentlySelectedHero.GetComponent<TokenInfo>().hero;
        CountyHeroStacking startingCounty = hero.location.GetComponent<CountyHeroStacking>();
        CountyHeroStacking destinationCounty = hero.destination.GetComponent<CountyHeroStacking>();

        // Add hero to the destination list.
        destinationCounty.spawnedTokenList.Insert(0, startingCounty.spawnedTokenList[0]);

        // Remove from starting list at zero index.
        startingCounty.spawnedTokenList.RemoveAt(0);
    }
}
