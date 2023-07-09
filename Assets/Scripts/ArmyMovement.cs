using System;
using UnityEngine;

public class ArmyMovement : MonoBehaviour
{
    /*
    private float localMinutes;
    private int localHours;
    private int localDays;
    private bool move;

    // Eventually this will be update depending on the distance to destination.
    private readonly int minutesTillArrival = 10; // This is a temp aount of Hours for testing.
    private readonly int hoursTillArrival = 1; // This is a temp amount of Hours.  
    private readonly int daysTillArrival = 1; // This is a temp amount of days.  

    private float lastCheckTime = 0;

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

    private void FixedUpdate()
    {
        // There has to be better fucking way to do this so that it is not in FixedUpdate. I don't think there is.
        // This checks to make sure the army isn't in the selected county already.
        if (WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].location != WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].destination)
        {
            if (WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].startTimer == true)
            {
                if (TimeKeeper.Instance.foreverTimer > lastCheckTime + 1) // Checking every "minute" (which is the 1 in the if statement).
                {
                    ArmyTimer();
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

        // I think we want a coroutine in the if statement below. Why?
        // There has to be better fucking way to do this so that it is not in FixedUpdate.
        if (move == true)
        {
            Move();
        }
    }

    private void ArmyTimer()
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
        }
        else
        {
            WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].isCountingDown = true;
            // This is turned off just for testing.
            //WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].timerCanvasGameObject.SetActive(true);

            // We can probably remove current time at some point, but right now for testing lets leave it.
            // Double check if we can replace string.Format with just $.
            WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].timerText.text = string.Format("Time till arrival: Day " + localDays + " {0:00}:{1:00}" +
                "\n Current Time: Day " + TimeKeeper.Instance.days +
                " {2:00}:{3:00}", LocalHours, LocalMinutes, TimeKeeper.Instance.Hours, (int)Math.Round(TimeKeeper.Instance.minutes));

            //Debug.Log("TimeKeeper Minutes: " + TimeKeeper.minutes);
            // This still could be broken.
            if (TimeKeeper.Instance.days == localDays && TimeKeeper.Instance.Hours == LocalHours && TimeKeeper.Instance.minutes >= LocalMinutes) //  && LocalMinutes <= TimeKeeper.minutes + 1)
            {
                WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].startTimer = false;
                // This starts the armyMovement for the army to move.
                WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].timerCanvasGameObject.SetActive(false);
                WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].IsSelected = false;
                move = true;

                //Debug.Log("Time is up!");
            }
        }
    }

    private void StopTimer()
    {
        WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].startTimer = false;
        WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].isCountingDown = false;
        isTimeToDestinationSet = false; // This will cause the HeroTimer to reset.
        WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].timerCanvasGameObject.SetActive(false);
    }
    private void Move()
    {
        /*
        Vector2 targetPosition = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].destination].countyCenterGameObject.transform.position;

        float closeEnough = .1f;
        float distance = Vector2.Distance(transform.position, targetPosition);
        float smoothTime = 0.1f; // This controls how fast the army moves.  Lower is faster.
        Vector2 velocity = Vector2.zero;


        WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].gameObject.transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // If the army gets to the center of the county its move is considered done.
        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].destination].countyCenterGameObject.transform.position ==
            WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].gameObject.transform.position)
        {
            move = false;
            //Debug.Log("SpawnedArmy is at its destination.  Move is: " + move);
        }
        // The army is close enough so we don't have to wait for that minscule amount of armyMovement.
        if (distance < closeEnough)
        {
            WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].location = WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].destination;
            //Debug.Log("Is Time to Destination Set: " + isTimeToDestinationSet);
            move = false;
            isTimeToDestinationSet = false;
            WorldMapLoad.Instance.spawnedArmies[int.Parse(name)].isCountingDown = false;
        }
        
    }
    */
}

