using System.Collections.Generic;
using UnityEngine;

public class CountyHeroStacking : MonoBehaviour
{
    // This generates the list needed for the dictionary list in WorldMapLoad
    private void Start()
    {
        WorldMapLoad.Instance.countyHeroTokens[name] = new List<SpawnedTokenList>();
    }
}
