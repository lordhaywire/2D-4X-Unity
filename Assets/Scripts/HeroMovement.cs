using System;
using TMPro;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public static HeroMovement Instance;

    public GameObject timerCanvasGameObject;
    public TextMeshProUGUI timerText;
    public bool heroMove;
    public float speed; // How fast the tokens move.

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

    public void StartHeroMovement()
    {
        var hero = WorldMapLoad.Instance.heroes[int.Parse(name)];

        if (isTimeToDestinationSet == true && hero.destination != WorldMapLoad.Instance.currentlyRightClickedCounty)
        {
            Debug.Log($"Hero movement was set to {hero.destination} but you clicked on " +
                $"{WorldMapLoad.Instance.currentlyRightClickedCounty} so it has been canceled.");
            StopTimer();
            return;
        }
        if (isTimeToDestinationSet == false)
        {
            hero.destination = WorldMapLoad.Instance.currentlyRightClickedCounty;
            Debug.Log("Hero Location: " + hero.location + " Hero Destination: " + hero.destination);
            Debug.Log("Set Initial Time!");
            SetInitialTime(); // This is the start of the timer, not hero movement.
            
        }
        else
        {
            Debug.Log("Hero is already set to move.");
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

            Debug.Log("2st Local Hours: " + LocalHours);
            Debug.Log("2st Local Minutes: " + LocalMinutes);

            isTimeToDestinationSet = true;

            //WorldMapLoad.Instance.heroes[int.Parse(name)].isCountingDown = true;

            timerCanvasGameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Time to Destionation is set.  WTF?"); // This debug log doesn't make any sense.
        }
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

        // Cancels the move if the player right clicks on the county the army is already in.
        if (isTimeToDestinationSet == true && WorldMapLoad.Instance.heroes[int.Parse(name)].location ==
            WorldMapLoad.Instance.heroes[int.Parse(name)].destination)
        {
            Debug.Log("Location: " + WorldMapLoad.Instance.heroes[int.Parse(name)].location + " Destination: " +
                WorldMapLoad.Instance.heroes[int.Parse(name)].destination);
            StopTimer();
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

            //Debug.Log("TimeKeeper Minutes: " + TimeKeeper.minutes);
            // This still is be broken.
            if (TimeKeeper.Instance.days == localDays && TimeKeeper.Instance.Hours
                == LocalHours && TimeKeeper.Instance.minutes >= LocalMinutes)
            {
                // Why is this here?
                WorldMapLoad.Instance.heroes[int.Parse(name)].startTimer = false;

                // This starts the heroMovement for the hero to move.
                timerCanvasGameObject.SetActive(false);

                WorldMapLoad.Instance.CurrentlySelectedHero = null;

                // Timer is done, so this is false.
                isTimeToDestinationSet = false;

                ChangeHerosList(); // Do some hero maintenance once.

                //Stacking the heroes starting location tokens.
                TokenStacking.Instance.StackTokens(WorldMapLoad.Instance.countyHeroTokens[WorldMapLoad.Instance.heroes[int.Parse(name)].location]);

                // Timer is done so hero should start moving.
                heroMove = true;

                //Debug.Log("Time is up!");
            }
        }
    }

    private void StopTimer()
    {
        WorldMapLoad.Instance.heroes[int.Parse(name)].startTimer = false;
        WorldMapLoad.Instance.heroes[int.Parse(name)].isCountingDown = false;
        isTimeToDestinationSet = false; // This will cause the HeroTimer to reset.
        timerCanvasGameObject.SetActive(false);
    }
    private void HeroMove()
    {
        float step = speed * Time.fixedDeltaTime;
        var hero = WorldMapLoad.Instance.heroes[int.Parse(name)];
        County destinationCounty = WorldMapLoad.Instance.counties[hero.destination];

        //Debug.Log("Hero Token Destination: " + hero.destination);

        // Turn off hero stack count because the hero is moving.
        GetComponent<TokenInfo>().counterGameObject.SetActive(false);

        // Move the token.
        Vector2 targetPosition = destinationCounty.heroSpawnLocation.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

        // Refresh the hero scroll view immediately when the hero starts moving.
        UIHeroScrollViewRefresher.Instance.RefreshPanel();

        // If the hero gets to the hero spawn location its move is considered done.
        if (transform.position == destinationCounty.heroSpawnLocation.transform.position)
        {
            Debug.Log("Hero has arrived at its destination.");

            heroMove = false;

            // Change the heroes current location and destination.
            WorldMapLoad.Instance.heroes[int.Parse(name)].location = WorldMapLoad.Instance.heroes[int.Parse(name)].destination;
            WorldMapLoad.Instance.heroes[int.Parse(name)].destination = null;
            WorldMapLoad.Instance.heroes[int.Parse(name)].justMoved = true;
            
            // Stack the hero tokens at destination location.
            Debug.Log("Hero Token Location: " + WorldMapLoad.Instance.heroes[int.Parse(name)].location);
            Debug.Log("Hero List Count: " + WorldMapLoad.Instance.countyHeroTokens[WorldMapLoad.Instance.heroes[int.Parse(name)].location].Count);
            TokenStacking.Instance.StackTokens(WorldMapLoad.Instance.countyHeroTokens[WorldMapLoad.Instance.heroes[int.Parse(name)].location]);

            WorldMapLoad.Instance.heroes[int.Parse(name)].isCountingDown = false; // Why do we have this if he have isTimeToDestinationSet?


        }

        // Cancels the move if the player right clicks on the county the army is already in.
        if (WorldMapLoad.Instance.heroes[int.Parse(name)].startTimer == true
            && WorldMapLoad.Instance.heroes[int.Parse(name)].location ==
            WorldMapLoad.Instance.heroes[int.Parse(name)].destination)
        {
            StopTimer();
        }
    }

    private void ChangeHerosList()
    {
        var heroStackingEnding = WorldMapLoad.Instance.countyHeroTokens[WorldMapLoad.Instance.heroes[int.Parse(name)].destination];
        var heroStackingStarting = WorldMapLoad.Instance.countyHeroTokens[WorldMapLoad.Instance.heroes[int.Parse(name)].location];
        

        Debug.Log("Hero Destination: " + WorldMapLoad.Instance.heroes[int.Parse(name)].destination);
        Debug.Log("Hero Token Name: " + name);

        // Add to destination list from zero index to zero index.
        heroStackingEnding.Insert(0, heroStackingStarting[0]);
        Debug.Log("Hero Ending List Count: " + heroStackingEnding.Count);

        // Remove from starting list at zero index.
        heroStackingStarting.RemoveAt(0);
        Debug.Log("Hero Starting List Count: " + heroStackingStarting.Count);
    }
}
