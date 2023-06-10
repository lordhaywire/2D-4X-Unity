using System.Collections.Generic;
using UnityEngine;

public class CountyHeroStacking : MonoBehaviour
{

    private void Start()
    {
        WorldMapLoad.Instance.heroStacking[name] = new List<HeroStack>();
    }
}
