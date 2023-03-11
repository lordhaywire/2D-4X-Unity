using System.Collections.Generic;
using UnityEngine;

public class RandomColorFaction : MonoBehaviour
{
    void Start()
    {
        if (Arrays.colors.Length < WorldMapLoad.instance.factionNameAndColors.Count)
        {
            Debug.LogError("Not enough color options for all Sprite Renderers!");
            return;
        }

        // Create a list of available color32 options
        List<Color32> availableColors = new(Arrays.colors);

        // Loop through each factionNameAndColors and assign a random color32 from available options
        for (int i = 0; i < WorldMapLoad.instance.factionNameAndColors.Count; i++)
        {
            int randomIndex = Random.Range(0, availableColors.Count);
            WorldMapLoad.instance.factionNameAndColors[i].color32 = availableColors[randomIndex];

            // Assign the new faction color to the county.
            CountyListCreator.instance.countiesList[i].gameObject.GetComponent<SpriteRenderer>().color =
                WorldMapLoad.instance.factionNameAndColors[i].color32;
            availableColors.RemoveAt(randomIndex);
        }
    }
}


