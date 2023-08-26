using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionGameObjectGeneration : MonoBehaviour
{

    [SerializeField] private GameObject factionPrefab;
    private void Start()
    {
        StartCoroutine(CreateFactionGameObjects());
    }

    IEnumerator CreateFactionGameObjects()
    {
        yield return 0;
        var factions = WorldMapLoad.Instance.factions;
         
        for (int i = 0; i < factions.Count; i++)
        {
            if (factions[i].isPlayer == false)
            {
                var counties = WorldMapLoad.Instance.counties;

                GameObject gameObject = Instantiate(factionPrefab, transform);
                gameObject.name = factions[i].factionNameAndColor.name;
                FactionAI factionAI = gameObject.GetComponent<FactionAI>();
                factionAI.faction = factions[i];

                foreach (KeyValuePair<string, County> item in counties)
                {
                    if (factions[i] == counties[item.Key].faction)
                    {
                        factionAI.countiesFactionOwns.Add(counties[item.Key]);
                    }
                }
            }
        }
    }
}
