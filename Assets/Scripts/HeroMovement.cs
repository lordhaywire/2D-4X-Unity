using System;
using TMPro;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public static HeroMovement Instance;

    public GameObject timerCanvasGameObject;
    public TextMeshProUGUI timerText;
    public bool heroMove;
    public float speed = 1.0f; // How fast the tokens move.

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
        //Debug.Log("Destination : " + WorldMapLoad.Instance.heroes[int.Parse(name)].destination);
        var hero = WorldMapLoad.Instance.heroes[int.Parse(name)];
        /* I guess this is extra.  Leave it here for a bit just in case we do need it after all.
        if (hero.location == WorldMapLoad.Instance.currentlyRightClickedCounty)
        {
            Debug.Log($"The hero is already in {WorldMapLoad.Instance.currentlyRightClickedCounty}.");
            StopTimer();
            return;
        }*/
        if (hero.heroMovement.isTimeToDestinationSet == true && hero.destination != WorldMapLoad.Instance.currentlyRightClickedCounty)
        {
            Debug.Log($"Hero movement was set to {hero.destination} but you clicked on " +
                $"{WorldMapLoad.Instance.currentlyRightClickedCounty} so it has been canceled.");
            StopTimer();
            return;
        }
        if (hero.heroMovement.isTimeToDestinationSet == false)
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
                WorldMapLoad.Instance.heroes[int.Parse(name)].IsSelected = false;

                WorldMapLoad.Instance.currentlySelectedHero = null;

                // Timer is done, so this is false.
                isTimeToDestinationSet = false;

                ChangeHerosList(); // Do some hero maintenance once.

                //Stacking the heroes starting location tokens.
                TokenStacking.Instance.StackTokens(WorldMapLoad.Instance.heroTokens[WorldMapLoad.Instance.heroes[int.Parse(name)].location], false);

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
        var destinationCounty = WorldMapLoad.Instance.counties[hero.destination];

        //Debug.Log("Hero Token Destination: " + hero.destination);

        // Turn off hero stack count because the hero is moving.
        hero.gameObject.GetComponent<TokenComponents>().counterCanvas.enabled = false;

        // Move the token.
        Vector2 targetPosition = destinationCounty.heroSpawnLocation.transform.position;
        hero.gameObject.transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

        // If the hero gets to the hero spawn location its move is considered done.
        if (WorldMapLoad.Instance.heroes[int.Parse(name)].gameObject.transform.position
            == destinationCounty.heroSpawnLocation.transform.position)
        {
            Debug.Log("Hero has arrived at its destination.");

            heroMove = false;

            // Change the heroes current location and destination.
            WorldMapLoad.Instance.heroes[int.Parse(name)].location = WorldMapLoad.Instance.heroes[int.Parse(name)].destination;
            WorldMapLoad.Instance.heroes[int.Parse(name)].destination = null;
            WorldMapLoad.Instance.heroes[int.Parse(name)].justMoved = true;
            
            // Stack the hero tokens at destination location.
            Debug.Log("Hero Token Location: " + WorldMapLoad.Instance.heroes[int.Parse(name)].location);
            TokenStacking.Instance.StackTokens(WorldMapLoad.Instance.heroTokens[WorldMapLoad.Instance.heroes[int.Parse(name)].location], true);

            WorldMapLoad.Instance.heroes[int.Parse(name)].isCountingDown = false; // Why do we have this if he have isTimeToDestinationSet?

            UIHeroScrollView.Instance.RefreshPanel(); // This isn't working how we want it to.  Meaning that the destintion isn't openning
            // the hero list panel.
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
        var heroStackingEnding = WorldMapLoad.Instance.heroTokens[WorldMapLoad.Instance.heroes[int.Parse(name)].destination];
        var heroStackingStarting = WorldMapLoad.Instance.heroTokens[WorldMapLoad.Instance.heroes[int.Parse(name)].location];
        

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


/* Removed SmoothDamp for now.
float closeEnough = .1f;
float distance = Vector2.Distance(transform.position, targetPosition);
float smoothTime = 0.1f; // This controls how fast the army moves.  Lower is faster.
Vector2 velocity = Vector2.zero;
WorldMapLoad.Instance.heroes[int.Parse(name)].gameObject.transform.position
    = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
*/


// The army is close enough so we don't have to wait for that minscule amount of armyMovement.
// Not needed if SmoothDamp isn't used.
/*
if (distance < closeEnough)
{
    ChangeHeroList();
    WorldMapLoad.Instance.heroes[int.Parse(name)].location = WorldMapLoad.Instance.heroes[int.Parse(name)].destination;
    //Debug.Log("Is Time to Destination Set: " + isTimeToDestinationSet);
    //move = false;
    isTimeToDestinationSet = false;
    WorldMapLoad.Instance.heroes[int.Parse(name)].isCountingDown = false;
}
*/

// This checks to make sure the hero isn't in the selected county already.
/*
if (WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].location != WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].destination)
{
    if (WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].startTimer == true)
    {
        if (TimeKeeper.Instance.foreverTimer > lastCheckTime + 1) // Checking every "minute" (which is the 1 in the if statement).
        {
            HeroTimer();
            lastCheckTime = TimeKeeper.Instance.foreverTimer;
        }
    }
}


}
*/
