using System.Collections.Generic;
using UnityEngine;

public class RandomColorFaction : MonoBehaviour
{
    void Start()
    {
        if (Arrays.colors.Length < WorldMapLoad.Instance.factionNameAndColors.Count)
        {
            Debug.LogError("Not enough color options for all Sprite Renderers!");
            return;
        }

        // Create a list of available color32 options
        List<Color32> availableColors = new(Arrays.colors);

        // Loop through each factionNameAndColors and assign a random color32 from available options
        for (int i = 0; i < WorldMapLoad.Instance.factionNameAndColors.Count; i++)
        {
            int randomIndex = Random.Range(0, availableColors.Count);
            WorldMapLoad.Instance.factionNameAndColors[i].color32 = availableColors[randomIndex];
            availableColors.RemoveAt(randomIndex);
        }

        // Go through each county, assign their Sprite Renderer and their color.
        foreach (KeyValuePair<string, County> item in WorldMapLoad.Instance.counties)
        {
            //Debug.Log("Random Color Faction: " + item.Key + "   " + item.Value);
            var county = WorldMapLoad.Instance.counties[item.Key];
            county.spriteRenderer =
                CountyListCreator.Instance.countiesList[county.countyID].gameObject.GetComponent<SpriteRenderer>();

            county.spriteRenderer.color = county.faction.color32;
        }
    }
}


