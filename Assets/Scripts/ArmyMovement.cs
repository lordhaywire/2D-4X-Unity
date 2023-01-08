using UnityEngine;

public class ArmyMovement : MonoBehaviour
{
    private float localMinutes;
    private int localHours;

    private int hoursTillArrival = 2; // This is a temp amount of hours.  Eventually this will be update depending on the distance to destination.
    private float lastCheckTime = 0;

    bool notFirstRun;

    private void FixedUpdate()
    {
        if (TimeKeeper.foreverCounter > lastCheckTime + 1) // Checking every "minute" (which is the 1 in the if statement).
        {
            ArmyTimer();
            //Debug.Log("Time Keeper Minutes is greater then Last Check Time");
            lastCheckTime = TimeKeeper.foreverCounter;
        }

    }

    // This is like half done.
    private void ArmyTimer()
    {

        Debug.Log("Start Timer: " + WorldMapLoad.armies[int.Parse(name)].startTimer);

        if (notFirstRun == false)
        {
            localMinutes = TimeKeeper.minutes;
            localHours = TimeKeeper.hours + hoursTillArrival;
            notFirstRun = true;
        }

        //localHours = localHours + hoursTillArrival;

        WorldMapLoad.armies[int.Parse(name)].armyTimerCanvasGameObject.SetActive(true);
        WorldMapLoad.armies[int.Parse(name)].armyTimerText.text =
            string.Format("Time till arrival: {0:00}:{1:00}", localHours, localMinutes + "\n" + "Current Time: {0:00}:{1:00}", TimeKeeper.minutes, TimeKeeper.hours);
    }
}
