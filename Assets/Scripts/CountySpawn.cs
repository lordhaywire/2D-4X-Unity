using UnityEngine;

public class CountySpawn : MonoBehaviour
{

    private void Start()
    {
        // Set the Select County script to be the same as the script attached to the county.
        WorldMapLoad.counties[name].selectCounty = GetComponent<SelectCounty>();
        //Debug.Log("Select Country Script: " + WorldMapLoad.counties[name].selectCounty);
    }
}
