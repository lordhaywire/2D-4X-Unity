using TMPro;
using UnityEngine;

public class SpawnedArmy
{
    public GameObject gameObject;
    public GameObject timerCanvasGameObject;
    public ArmyMovement armyMovement;
    public string location;
    public string destination;
    public TextMeshProUGUI timerText;
    public bool isSelected;
    public bool startTimer;
    public bool isCountingDown;
    public string owner;
    public string name;
    public int size;

    // This property currently just changes the color32 of the army so we know if it selected or not.
    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;

            if (isSelected == false && gameObject != null)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else if(isSelected == true && gameObject != null)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
            else
            {
                Debug.Log("GameObject is null in the Is Army Selected Property.");
            }
        }
    }

    public SpawnedArmy(GameObject gameObject, GameObject timerCanvasGameObject, ArmyMovement armyMovement, string location, 
        string destination, TextMeshProUGUI timerText, bool IsSelected, bool startTimer, bool isCountingDown, string owner, 
        string name, int size)
    {
        this.gameObject = gameObject;
        this.timerCanvasGameObject = timerCanvasGameObject;
        this.armyMovement = armyMovement;
        this.location = location;
        this.destination = destination;
        this.timerText = timerText;
        this.IsSelected = IsSelected;
        this.startTimer = startTimer;
        this.isCountingDown = isCountingDown;
        this.owner = owner;
        this.name = name;
        this.size = size;
    }
}
