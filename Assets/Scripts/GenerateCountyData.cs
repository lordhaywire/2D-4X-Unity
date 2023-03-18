using UnityEngine;


public class GenerateCountyData : MonoBehaviour
{
    public County county;

    private void Start()
    {
        WorldMapLoad.instance.counties[name] = county;
        Debug.Log("County Primary Biome: " + WorldMapLoad.instance.counties[name].biomePrimary);
    }
}
