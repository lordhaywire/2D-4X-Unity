using UnityEngine;

public class Army
{
    public GameObject gameObject;
    public string owner;
    public string name;
    public int size;

    public Army(GameObject newGameObject, string newOwner,string newName, int newSize)
    {
        gameObject = newGameObject;
        owner = newOwner;
        name = newName;
        size = newSize;
    }
}
