using UnityEngine;

public class CountyInfo : MonoBehaviour
{
    public County county;
    public GameObject tokenSpawn;

    private readonly string prefabFolder = "Buildings"; // Reference to the folder containing the prefabs
    public Transform possibleBuildingsParent;
    public Transform currentBuildingsParent;

    void Start()
    {
        // Load all prefabs in the specified folder
        GameObject[] prefabs = Resources.LoadAll<GameObject>(prefabFolder);

        // Instantiate each prefab
        foreach (GameObject prefab in prefabs)
        {
            GameObject building = Instantiate(prefab, possibleBuildingsParent);
            BuildingInfo buildingInfo = building.GetComponent<BuildingInfo>();
            building.name = gameObject.name + " " + buildingInfo.buildingName;
            buildingInfo.county = county;
        }
    }
}
