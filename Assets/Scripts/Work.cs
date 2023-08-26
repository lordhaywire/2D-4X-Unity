using System.Collections.Generic;
using UnityEngine;

public class Work : MonoBehaviour
{
    private void Start()
    {
        TimeKeeper.Instance.DayStart += DayStart;
        TimeKeeper.Instance.WorkDayOver += WorkDayOverForPopulation;
    }

    private void DayStart()
    {
        AdjustPopulationActivity();
    }

    // Adjust all of the world population!
    private void AdjustPopulationActivity()
    {
        // Go through every county.
        foreach (KeyValuePair<string, List<CountyPopulation>> item in WorldMapLoad.Instance.countyPopulationDictionary)
        {
            //Debug.Log(item.Key + " " + item.Value);
            // Go through this counties population.
            for (int i = 0; i < item.Value.Count; i++)
            {
                //Debug.Log(item.Value[pop].lastName);
                item.Value[i].currentActivity = item.Value[i].nextActivity;
                item.Value[i].currentBuilding = item.Value[i].nextBuilding;

                // Checks to see if the 
                if (item.Value[i].leaderOfPeoplePerk == true && item.Value[i].isFactionLeader == true)
                {
                    WorldMapLoad.Instance.counties[item.Key].faction.Influence
                        += WorldMapLoad.Instance.dailyInfluenceGain;
                }
            }
        }
    }

    // End work for all of the world population!
    private void WorkDayOverForPopulation()
    {
        // Go through all the counties and have people building add their work to the building.
        foreach (KeyValuePair<string, List<CountyPopulation>> item in WorldMapLoad.Instance.countyPopulationDictionary)
        {
            //Debug.Log(item.Key + " " + item.Value);
            for (int pop = 0; pop < item.Value.Count; pop++)
            {
                if (item.Value[pop].currentActivity == AllText.Jobs.BUILDING)
                {
                    item.Value[pop].currentBuilding.workCompleted++;
                    // Checks to see if the building is completed.
                    if (item.Value[pop].currentBuilding.workCompleted >= item.Value[pop].currentBuilding.workCost)
                    {
                        // This is having every population working on that building set that building as built.
                        // So it is repeating the setting to true a bunch of times.  This is ineffecient code.
                        // Some of the population will be working on different buildings too....
                        item.Value[pop].currentBuilding.isBuilt = true;

                        // This needs to work with AI factions as well which don't have a UI.
                        item.Value[pop].currentBuilding.gameObject.GetComponent<UIBuildingButton>().completedTextGameObject.SetActive(true);
                    }
                }
            }
            // Go through everyone in this county again and clear out their job if their building is done.
            for (int popAgain = 0; popAgain < item.Value.Count; popAgain++)
            {
                if (item.Value[popAgain].currentBuilding != null && item.Value[popAgain].currentBuilding.isBuilt == true)
                {
                    item.Value[popAgain].nextActivity = AllText.Jobs.IDLE;
                    item.Value[popAgain].nextBuilding = null;
                }
            }
        }
    }



    private void OnDisable()
    {
        TimeKeeper.Instance.DayStart -= AdjustPopulationActivity;
        TimeKeeper.Instance.WorkDayOver -= WorkDayOverForPopulation;
    }
}
