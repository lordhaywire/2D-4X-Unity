using System.Collections.Generic;
using UnityEngine;

public class FactionAI : MonoBehaviour
{
    public Faction faction;
    public Banker banker;
    public List<County> countiesFactionOwns;

    private void Awake()
    {
        banker = GetComponent<Banker>();
    }

    private void Start()
    {
        TimeKeeper.Instance.FirstRun += CheckForBuildingBuildings;
        //TimeKeeper.Instance.DayStart += BuildBuildings;
    }

    private void CheckForBuildingBuildings()
    {
        Debug.Log(faction.factionNameAndColor.name  + " checking to build buildings!");

        // Is there enough food? If not build a Garden Shack.

        if (banker.CheckEnoughFood(faction) == false)
        {
            // We need to figure out a less error prone way to do this because knowing which building is which in
            for (int i = 0; i < countiesFactionOwns.Count; i++)
            {
                //Debug.Log("Influence Cost: " + countiesFactionOwns[i].possibleBuildings[2].influenceCost);
                if(countiesFactionOwns[i].possibleBuildings[2] != null)
                {
                    if (faction.influence >= countiesFactionOwns[i].possibleBuildings[2].GetComponent<BuildingInfo>().influenceCost)
                    //&& countiesFactionOwns[i].currentBuildings[2].isBeingBuilt == false
                    //&& countiesFactionOwns[i].currentBuildings[2].isBuilt == false)
                    {
                        countiesFactionOwns[i].gameObject.GetComponent<BuildImprovements>()
                            .BuildBuilding(faction, countiesFactionOwns[i].possibleBuildings[2]);
                    }
                    else
                    {
                        Debug.Log($"{faction.factionNameAndColor.name} doesn't have enough influence to build " +
                            $"{countiesFactionOwns[i].possibleBuildings[2].name} or some shit.");
                    }
                }            
            }
        }
        else
        {
            Debug.Log(faction.factionNameAndColor.name + " has enough food.");
        }

    }
}
