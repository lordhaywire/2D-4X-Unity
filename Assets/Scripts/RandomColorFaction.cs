using System.Collections.Generic;
using UnityEngine;

public class RandomColorFaction : MonoBehaviour
{
    void Start()
    {
        if (Arrays.colors.Length < WorldMapLoad.Instance.factions.Count)
        {
            Debug.LogError("Not enough color options for all Sprite Renderers!");
            return;
        }

        // Create a list of available color32 options
        List<Color32> availableColors = new(Arrays.colors);

        // Loop through each Sprite Renderer and assign a random color32 from available options
        for (int i = 0; i < WorldMapLoad.Instance.factions.Count; i++)
        {
            int randomIndex = Random.Range(0, availableColors.Count);
            WorldMapLoad.Instance.factions[i].color32 = availableColors[randomIndex];
            availableColors.RemoveAt(randomIndex);
        }
    }
}


