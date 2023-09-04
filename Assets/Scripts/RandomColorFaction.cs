using System.Collections.Generic;
using UnityEngine;

public class RandomColorFaction : MonoBehaviour
{
    void Start()
    {
        var factionNameAndColors = WorldMapLoad.Instance.factionNameAndColors;
        if (Arrays.colors.Length < factionNameAndColors.Count)
        {
            Debug.LogError("Not enough color options for all Sprite Renderers!");
            return;
        }

        // Create a list of available color32 options
        List<Color32> availableColors = new(Arrays.colors);

        // Loop through each factionNameAndColors and assign a random color32 from available options
        for (int i = 0; i < factionNameAndColors.Count; i++)
        {
            int randomIndex = Random.Range(0, availableColors.Count);
            factionNameAndColors[i].color32 = availableColors[randomIndex];
            availableColors.RemoveAt(randomIndex);
        }

        // Go through each county, assign their Sprite Renderer, their color and their Build Improvements script.
        foreach (KeyValuePair<string, County> item in WorldMapLoad.Instance.counties)
        {
            //Debug.Log("Random Color Faction: " + item.Key + "   " + item.Value);
            var county = WorldMapLoad.Instance.counties[item.Key];
            county.spriteRenderer =
                CountyListCreator.Instance.countiesList[county.countyID].gameObject.GetComponent<SpriteRenderer>();
            county.buildImprovements
                = CountyListCreator.Instance.countiesList[county.countyID].gameObject.GetComponent<BuildImprovements>();
            //Debug.Log(county.gameObject.name + " building improvements: " + county.buildImprovements);

            county.spriteRenderer.color = county.faction.factionNameAndColor.color32;
        }

        // Assign the faction's factionID
        for(int i = 0; i < factionNameAndColors.Count; i++)
        {
            factionNameAndColors[i].factionID = i;
            //Debug.Log("Faction ID: " + factionNameAndColors[i].factionID);
        }
    }
}


