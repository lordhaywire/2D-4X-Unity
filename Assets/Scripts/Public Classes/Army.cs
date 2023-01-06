using UnityEngine;

public class Army
{
    public GameObject gameObject;
    public Transform currentLocation;
    public bool isArmySelected;
    public string owner;
    public string name;
    public int size;

    public Army(GameObject newGameObject, Transform newCurrentLocation, bool newIsArmySelected, string newOwner,string newName, int newSize)
    {
        gameObject = newGameObject;
        currentLocation = newCurrentLocation;
        isArmySelected = newIsArmySelected;
        owner = newOwner;
        name = newName;
        size = newSize;
    }
}
