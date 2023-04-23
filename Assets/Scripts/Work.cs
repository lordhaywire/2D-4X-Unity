using System.Collections.Generic;
using UnityEngine;


public class Work : MonoBehaviour
{
    private void Start()
    {
        TimeKeeper.Instance.DayStart += AdjustPopulationActivity;
        TimeKeeper.Instance.WorkOver += EndWorkForPopulation;
    }

    // End work for all of the world population!
    private void EndWorkForPopulation()
    {
        // Go through all the counties and have people building add their work to the building.
        foreach (KeyValuePair<string, List<CountyPopulation>> item in WorldMapLoad.Instance.countyPopulationDictionary)
        {
            //Debug.Log(item.Key + " " + item.Value);
            for (int i = 0; i < item.Value.Count; i++)
            {
                if (item.Value[i].currentActivity == AllText.Jobs.BUILDING)
                {
                    item.Value[i].currentBuilding.workCompleted++;
                }
            }
        }
    }

    // Adjust all of the world population!
    private void AdjustPopulationActivity()
    {
        foreach (KeyValuePair<string, List<CountyPopulation>> item in WorldMapLoad.Instance.countyPopulationDictionary)
        {
            //Debug.Log(item.Key + " " + item.Value);
            for(int i = 0;  i < item.Value.Count; i++)
            {
                //Debug.Log(item.Value[i].lastName);
                item.Value[i].currentActivity = item.Value[i].nextActivity;
                item.Value[i].currentBuilding = item.Value[i].nextBuilding;
            }
        }
    }
}
