using UnityEngine;

public class CountyAndBuilding : MonoBehaviour
{
    public County county;
    public GameObject building;

    public CountyAndBuilding(County county, GameObject building)
    {
        this.county = county;
        this.building = building;
    }
}
