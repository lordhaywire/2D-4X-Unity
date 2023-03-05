using UnityEngine;

public class CountySpawn : MonoBehaviour
{
    private void Start()
    {
        // Set the Select County script to be the same as the script attached to the county.
        WorldMapLoad.Instance.counties[name].selectCounty = GetComponent<SelectCounty>();

        WorldMapLoad.Instance.counties[name].color32 = WorldMapLoad.Instance.counties[name].faction.color32;
        GetComponent<SpriteRenderer>().color = WorldMapLoad.Instance.counties[name].color32;

    }
}
