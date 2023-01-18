using System;
using UnityEngine;

public class ArmyMovement : MonoBehaviour
{
    private float localMinutes;
    private int localHours;
    private int localDays;

    private int minutesTillArrival = 50; // This is a temp aount of hours for testing.
    private int hoursTillArrival = 23; // This is a temp amount of hours.  Eventually this will be update depending on the distance to destination.
    private float lastCheckTime = 0;

    public float LocalMinutes
    {
        get
        {
            return localMinutes;
        }
        set
        {
            localMinutes = value;
            Debug.Log("Local Minutes Getter Setter Local Hours: " + LocalHours);
            Debug.Log("Local Minutes Getter Setter Local Minutes: " + LocalMinutes);
            if(localMinutes > 60)
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
            Debug.Log("Local Hours Getter Setter Local Hours: " + LocalHours);
            Debug.Log("Local Hours Getter Setter Local Minutes: " + LocalMinutes);
            if (localHours >= 24)
            {
                localDays++;
                localHours -= 24;
            }
        }
    }
    bool isTimeToDestinationSet;

    private void FixedUpdate()
    {
        // There has to be better fucking way to do this so that it is not in FixedUpdate.
        if(WorldMapLoad.armies[int.Parse(name)].startTimer == true)
        {
            if (TimeKeeper.foreverTimer > lastCheckTime + 1) // Checking every "minute" (which is the 1 in the if statement).
            {
                ArmyTimer();
                //Debug.Log("Time Keeper Minutes is greater then Last Check Time");
                lastCheckTime = TimeKeeper.foreverTimer;
            }
        }
    }

    private void ArmyTimer()
    {
        if (isTimeToDestinationSet == false)
        {
            // This needs to be in the order of Days > Hours > Minutes so that the Getter setter works.
            LocalHours = TimeKeeper.hours + hoursTillArrival;
            LocalMinutes = (int)Math.Round(TimeKeeper.minutes) + minutesTillArrival;  //Cast from Float to Int.
            Debug.Log("2st Local Hours: " + LocalHours);
            Debug.Log("2st Local Minutes: " + LocalMinutes);
            

            isTimeToDestinationSet = true;
        }
        else
        {
            WorldMapLoad.armies[int.Parse(name)].armyTimerCanvasGameObject.SetActive(true);

            // We can probably remove current time at some point, but right now for testing lets leave it.
            WorldMapLoad.armies[int.Parse(name)].armyTimerText.text = string.Format("Time till arrival: Day " + localDays + " {0:00}:{1:00}" +
                "\n Current Time: Day " + TimeKeeper.days + " {2:00}:{3:00}", LocalHours, LocalMinutes, TimeKeeper.hours, (int)Math.Round(TimeKeeper.minutes));

            if(localDays == TimeKeeper.days && LocalHours == TimeKeeper.hours && LocalMinutes == (int)Math.Round(TimeKeeper.minutes))
            {
                WorldMapLoad.armies[int.Parse(name)].startTimer = false;
                Debug.Log("Time is up!");
            }
        }

        /*
         * public class MoveObject : MonoBehaviour
{
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 targetPosition = new Vector3(0, 0, 0); // target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}*/
    }
}
