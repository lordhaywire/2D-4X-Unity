using UnityEngine;

public class CreateUnit : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private GameObject provinceGameObject;
    private int numberOfUnits;

    public void CreateUnitButton()
    {
        if(numberOfUnits < 1)
        {
            Instantiate(unitPrefab, provinceGameObject.transform.position, Quaternion.identity); // Spawn the token, put it in a location and stop is rotation
            numberOfUnits++;
        }
        else
        {
            Debug.Log("You already have 1 unit!");
        }
    }
}
