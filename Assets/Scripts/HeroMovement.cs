using System;
using TMPro;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public static HeroMovement Instance;

    public GameObject timerCanvasGameObject;
    public TextMeshProUGUI timerText;

    private float localMinutes;
    private int localHours;
    private int localDays;
    public bool move;

    // Eventually this will be update depending on the distance to destination.
    private readonly int minutesTillArrival = 10; // This is a temp aount of Hours for testing.
    private readonly int hoursTillArrival = 2; // This is a temp amount of Hours.  
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
        //var heroToken = WorldMapLoad.Instance.currentlySelectedHero;
        if (WorldMapLoad.Instance.heroes[int.Parse(name)].location
            != WorldMapLoad.Instance.heroes[int.Parse(name)].destination)
        {
            Debug.Log("Starting Hero Movement!");
            SetInitialTime();
        }
        else
        {
            Debug.Log("The hero is already in that County.");
        }
    }
    private void FixedUpdate()
    {
        if (isTimeToDestinationSet == true)
        {
            HeroTimer();
        }
        if (move == true)
        {
            Move();
        }
    }

    private void SetInitialTime()
    {
        if (isTimeToDestinationSet == false)
        {
            // This needs to be in the order of Days > Hours > Minutes so that the Getter setter works.
            LocalHours = TimeKeeper.Instance.Hours + hoursTillArrival;
            LocalMinutes = TimeKeeper.Instance.minutes + minutesTillArrival;  //Cast from Float to Int.
            localDays = TimeKeeper.Instance.days + daysTillArrival;
            Debug.Log("2st Local Hours: " + LocalHours);
            Debug.Log("2st Local Minutes: " + LocalMinutes);

            isTimeToDestinationSet = true;

            WorldMapLoad.Instance.heroes[int.Parse(name)].isCountingDown = true;

            timerCanvasGameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Time to Destionation is not set.");
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
            // This still could be broken.
            if (TimeKeeper.Instance.days == localDays && TimeKeeper.Instance.Hours 
                == LocalHours && TimeKeeper.Instance.minutes >= LocalMinutes) //  && LocalMinutes <= TimeKeeper.minutes + 1)
            {
                // Why is this here?
                WorldMapLoad.Instance.heroes[int.Parse(name)].startTimer = false;

                // This starts the heroMovement for the hero to move.
                timerCanvasGameObject.SetActive(false);
                WorldMapLoad.Instance.heroes[int.Parse(name)].IsSelected = false;
                WorldMapLoad.Instance.currentlySelectedHero = null;
                move = true;

                Debug.Log("Time is up!");
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
    private void Move()
    {
        var county = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.heroes[int.Parse(name)].destination];

        Vector2 targetPosition = county.heroSpawnLocation.transform.position;

        float closeEnough = .1f;
        float distance = Vector2.Distance(transform.position, targetPosition);
        float smoothTime = 0.1f; // This controls how fast the army moves.  Lower is faster.
        Vector2 velocity = Vector2.zero;


        WorldMapLoad.Instance.heroes[int.Parse(name)].gameObject.transform.position
            = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // If the hero gets to the center of the county its move is considered done.
        if (WorldMapLoad.Instance.heroes[int.Parse(name)].gameObject.transform.position
            == county.countyCenterGameObject.transform.position)
        {
            move = false;
            //Debug.Log("SpawnedArmy is at its destination.  Move is: " + move);
        }
        // The army is close enough so we don't have to wait for that minscule amount of armyMovement.
        if (distance < closeEnough)
        {
            WorldMapLoad.Instance.heroes[int.Parse(name)].location = WorldMapLoad.Instance.heroes[int.Parse(name)].destination;
            //Debug.Log("Is Time to Destination Set: " + isTimeToDestinationSet);
            move = false;
            isTimeToDestinationSet = false;
            WorldMapLoad.Instance.heroes[int.Parse(name)].isCountingDown = false;
        }
        // Cancels the move if the player right clicks on the county the army is already in.
        if (WorldMapLoad.Instance.heroes[int.Parse(name)].startTimer == true
            && WorldMapLoad.Instance.heroes[int.Parse(name)].location ==
            WorldMapLoad.Instance.heroes[int.Parse(name)].destination)
        {
            StopTimer();
        }
    }
}

// There has to be better fucking way to do this so that it is not in FixedUpdate.  I don't think there is.
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
// Cancels the move if the player right clicks on the county the army is already in.
if (WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].startTimer == true && WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].location ==
    WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].destination)
{
    StopTimer();
}

}
*/
