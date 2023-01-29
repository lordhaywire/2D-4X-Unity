using TMPro;
using UnityEngine;

public class Army
{
    public GameObject gameObject;
    public GameObject armyTimerCanvasGameObject;
    public string currentLocation;
    public string armyDestination;
    public TextMeshProUGUI armyTimerText;
    public bool isArmySelected;
    public bool startTimer;
    public bool isCountingDown;
    public string owner;
    public string name;
    public int size;

    public Army(GameObject newGameObject, GameObject newArmyTimerCanvasGameObject, string newCurrentLocation, string newArmyDestination,
        TextMeshProUGUI newArmyTimerText, bool newIsArmySelected, bool newStartTimer, bool newIsCountingDown, string newOwner,string newName, int newSize)
    {
        gameObject = newGameObject;
        armyTimerCanvasGameObject = newArmyTimerCanvasGameObject;
        currentLocation = newCurrentLocation;
        armyDestination = newArmyDestination;
        armyTimerText = newArmyTimerText;
        isArmySelected = newIsArmySelected;
        startTimer = newStartTimer;
        isCountingDown = newIsCountingDown;
        owner = newOwner;
        name = newName;
        size = newSize;
    }
}
