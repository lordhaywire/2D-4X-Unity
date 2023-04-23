using UnityEngine;


public class GenerateCountyData : MonoBehaviour
{
    public County county;

    private void Start()
    {
        WorldMapLoad.Instance.counties[name] = county;
        Debug.Log("County Primary Biome: " + WorldMapLoad.Instance.counties[name].biomePrimary);
    }
}
