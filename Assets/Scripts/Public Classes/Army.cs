using TMPro;
using UnityEngine;

public class Army
{
    public GameObject gameObject;
    public GameObject armyTimerCanvasGameObject;
    public ArmyMovement armyMovement;
    public string location;
    public string armyDestination;
    public TextMeshProUGUI armyTimerText;
    public bool isArmySelected;
    public bool startTimer;
    public bool isCountingDown;
    public string owner;
    public string name;
    public int size;

    // This property currently just changes the color32 of the army so we know if it selected or not.
    public bool IsArmySelected
    {
        get
        {
            return isArmySelected;
        }
        set
        {
            isArmySelected = value;

            if (isArmySelected == false && gameObject != null)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else if(isArmySelected == true && gameObject != null)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
            else
            {
                Debug.Log("GameObject is null in the Is Army Selected Property.");
            }
        }
    }

    public Army(GameObject newGameObject, GameObject newArmyTimerCanvasGameObject, ArmyMovement newArmyMovement, string newLocation, string newArmyDestination,
        TextMeshProUGUI newArmyTimerText, bool newIsArmySelected, bool newStartTimer, bool newIsCountingDown, string newOwner, string newName, int newSize)
    {
        gameObject = newGameObject;
        armyTimerCanvasGameObject = newArmyTimerCanvasGameObject;
        armyMovement = newArmyMovement;
        location = newLocation;
        armyDestination = newArmyDestination;
        armyTimerText = newArmyTimerText;
        IsArmySelected = newIsArmySelected;
        startTimer = newStartTimer;
        isCountingDown = newIsCountingDown;
        owner = newOwner;
        name = newName;
        size = newSize;
    }
}
