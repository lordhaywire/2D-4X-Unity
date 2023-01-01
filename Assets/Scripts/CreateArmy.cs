using UnityEngine;

public class CreateArmy : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private GameObject provinceGameObject;
    [SerializeField] private GameObject armyListGameObject;

    private int numberOfUnits;

    public void CreateUnitButton()
    {
        if(numberOfUnits < 1)
        {
            // Instantiate and assign to Army list.
            WorldMapLoad.armies[0].gameObject = Instantiate(unitPrefab, provinceGameObject.transform.position, Quaternion.identity); // Spawn the token, put it in a location and stop is rotation

            // Move GameObject to army list in Inspector.
            WorldMapLoad.armies[0].gameObject.transform.parent = armyListGameObject.transform;
            
            numberOfUnits++;
        }
        else
        {
            Debug.Log("You already have 1 unit!");
        }
    }
}
