using System;
using UnityEngine;


public class ArmyMovement : MonoBehaviour
{
    private float localMinutes;
    private int localHours;
    private int localDays;
    private bool move;

    private readonly int minutesTillArrival = 10; // This is a temp aount of hours for testing.
    private readonly int hoursTillArrival = 1; // This is a temp amount of hours.  Eventually this will be update depending on the distance to destination.
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
        //Debug.Log("Army Current Location: " + WorldMapLoad.armies[int.Parse(name)].location);
        // There has to be better fucking way to do this so that it is not in FixedUpdate.
        // This checks to make sure the army isn't in the selected county already.
        if (WorldMapLoad.armies[int.Parse(name)].location != WorldMapLoad.armies[int.Parse(name)].armyDestination)
        {
            if (WorldMapLoad.armies[int.Parse(name)].startTimer == true)
            {
                if (TimeKeeper.foreverTimer > lastCheckTime + 1) // Checking every "minute" (which is the 1 in the if statement).
                {
                    ArmyTimer();
                    //Debug.Log("Time Keeper Minutes is greater then Last Check Time");
                    lastCheckTime = TimeKeeper.foreverTimer;
                }
            }
        }
        // Cancels the move if the player right clicks on the county the army is already in.
        if(WorldMapLoad.armies[int.Parse(name)].startTimer == true && WorldMapLoad.armies[int.Parse(name)].location == 
            WorldMapLoad.armies[int.Parse(name)].armyDestination)
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

    private void StopTimer()
    {
        WorldMapLoad.armies[int.Parse(name)].startTimer = false;
        WorldMapLoad.armies[int.Parse(name)].isCountingDown = false;
        isTimeToDestinationSet = false; // This will cause the ArmyTimer to reset.
        WorldMapLoad.armies[int.Parse(name)].armyTimerCanvasGameObject.SetActive(false);
    }

    private void ArmyTimer()
    {
        
        if (isTimeToDestinationSet == false)
        {
            // This needs to be in the order of Days > Hours > Minutes so that the Getter setter works.
            LocalHours = TimeKeeper.hours + hoursTillArrival;
            LocalMinutes = TimeKeeper.minutes + minutesTillArrival;  //Cast from Float to Int.
            //Debug.Log("2st Local Hours: " + LocalHours);
            //Debug.Log("2st Local Minutes: " + LocalMinutes);

            isTimeToDestinationSet = true;
        }
        else
        {
            WorldMapLoad.armies[int.Parse(name)].isCountingDown = true;
            WorldMapLoad.armies[int.Parse(name)].armyTimerCanvasGameObject.SetActive(true);

            // We can probably remove current time at some point, but right now for testing lets leave it.
            WorldMapLoad.armies[int.Parse(name)].armyTimerText.text = string.Format("Time till arrival: Day " + localDays + " {0:00}:{1:00}" +
                "\n Current Time: Day " + TimeKeeper.days + " {2:00}:{3:00}", LocalHours, LocalMinutes, TimeKeeper.hours, (int)Math.Round(TimeKeeper.minutes));

            //Debug.Log("TimeKeeper Minutes: " + TimeKeeper.minutes);
            // This still could be broken.
            if (TimeKeeper.days == localDays && TimeKeeper.hours == LocalHours && TimeKeeper.minutes >= LocalMinutes) //  && LocalMinutes <= TimeKeeper.minutes + 1)
            {
                WorldMapLoad.armies[int.Parse(name)].startTimer = false;
                // This starts the movement for the army to move.
                WorldMapLoad.armies[int.Parse(name)].armyTimerCanvasGameObject.SetActive(false);
                WorldMapLoad.armies[int.Parse(name)].IsArmySelected = false;
                move = true;
                
                Debug.Log("Time is up!");
            }
        }
    }
    private void Move()
    {
        
        Vector2 targetPosition = WorldMapLoad.counties[WorldMapLoad.armies[int.Parse(name)].armyDestination].countyCenterGameObject.transform.position;

        float closeEnough = .1f;
        float distance = Vector2.Distance(transform.position, targetPosition);
        float smoothTime = 0.1f; // This controls how fast the army moves.  Lower is faster.
        Vector2 velocity = Vector2.zero;

        
        WorldMapLoad.armies[int.Parse(name)].gameObject.transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // If the army gets to the center of the county its move is considered done.
        if(WorldMapLoad.counties[WorldMapLoad.armies[int.Parse(name)].armyDestination].countyCenterGameObject.transform.position == 
            WorldMapLoad.armies[int.Parse(name)].gameObject.transform.position)
        {
            move = false;
            Debug.Log("Army is at its destination.  Move is: " + move);
        }
        // The army is close enough so we don't have to wait for that minscule amount of movement.
        if(distance < closeEnough)
        {
            WorldMapLoad.armies[int.Parse(name)].location = WorldMapLoad.armies[int.Parse(name)].armyDestination;
            //Debug.Log("Is Time to Destination Set: " + isTimeToDestinationSet);
            move = false;
            isTimeToDestinationSet = false;
            WorldMapLoad.armies[int.Parse(name)].isCountingDown = false;
        }
    }
}

