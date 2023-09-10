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
        GenerateLeaderInfluence();
    }

    private void GenerateLeaderInfluence()
    {
        
        for (int i = 0; i < WorldMapLoad.Instance.factions.Count; i++) 
        {
            Faction faction = WorldMapLoad.Instance.factions[i];
            if(faction.factionLeader.leaderOfPeoplePerk == true)
            {
                faction.Influence += Globals.Instance.dailyInfluenceGain;
            }
            else
            {
                Debug.Log($"The leader of {faction.factionNameAndColor.name} isn't a leader of people.");
            }
        }     
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
            }
        }
    }

    // End work for all of the world population!
    private void WorkDayOverForPopulation()
    {
        // We should rewrite this.  I think there is a better way to do this without using the Dictionary bullshit.
        // Go through all the counties and have people building add their work to the building.
        foreach (KeyValuePair<string, List<CountyPopulation>> item in WorldMapLoad.Instance.countyPopulationDictionary)
        {
            //Debug.Log(item.Key + " " + item.Value);
            for (int pop = 0; pop < item.Value.Count; pop++)
            {
                if (item.Value[pop].currentActivity == AllText.Jobs.BUILDING)
                {
                    BuildingInfo buildingInfo = item.Value[pop].currentBuilding.GetComponent<BuildingInfo>();
                    buildingInfo.workCompleted++;
                    // Checks to see if the building is completed.
                    if (buildingInfo.workCompleted >= buildingInfo.workCost)
                    {
                        // This is having every population working on that building set that building as built.
                        // So it is repeating the setting to true a bunch of times.  This is ineffecient code.
                        // Some of the population will be working on different buildings too....
                        buildingInfo.isBuilt = true;
                        buildingInfo.isBeingBuilt = false;
                        buildingInfo.uIGameObject.GetComponent<UIBuildingButton>().underConstructionGameObject.SetActive(false);
                    }
                }
            }

            // Go through everyone in this county again and clear out their job if their building is done.
            for (int popAgain = 0; popAgain < item.Value.Count; popAgain++)
            {
                if (item.Value[popAgain].currentBuilding != null && item.Value[popAgain].currentBuilding.GetComponent<BuildingInfo>().isBuilt == true)
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
