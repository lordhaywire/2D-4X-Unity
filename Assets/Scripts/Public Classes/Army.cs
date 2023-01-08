using TMPro;
using UnityEngine;

public class Army
{
    public GameObject gameObject;
    public GameObject armyTimerCanvasGameObject;
    public Transform currentLocation;
    public TextMeshProUGUI armyTimerText;
    public bool isArmySelected;
    public bool startTimer;
    public string owner;
    public string name;
    public int size;

    public Army(GameObject newGameObject, GameObject newArmyTimerCanvasGameObject, Transform newCurrentLocation, 
        TextMeshProUGUI newArmyTimerText, bool newIsArmySelected, bool newStartTimer, string newOwner,string newName, int newSize)
    {
        gameObject = newGameObject;
        armyTimerCanvasGameObject = newArmyTimerCanvasGameObject;
        currentLocation = newCurrentLocation;
        armyTimerText = newArmyTimerText;
        isArmySelected = newIsArmySelected;
        startTimer = newStartTimer;
        owner = newOwner;
        name = newName;
        size = newSize;
    }
}
