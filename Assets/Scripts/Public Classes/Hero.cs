using TMPro;
using UnityEngine;

public class Hero
{
    public GameObject gameObject;
    public HeroMovement heroMovement;

    public string owner;
    public string name;
    public int heroIndex;
    public int countyPopulationIndex;
    public string location;
    public string destination;
    public bool isSelected;

    public GameObject timerCanvasGameObject;
    public TextMeshProUGUI timerText;
    public bool startTimer;
    public bool isCountingDown;

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
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (isSelected == true && gameObject != null)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                //Debug.Log("GameObject is null in the Is Army Selected Property.");
            }
        }
    }

    public Hero(GameObject gameObject, HeroMovement heroMovement, string owner,
        string name, int heroIndex, int countyPopulationIndex, string location, bool IsSelected, string destination, 
        GameObject timerCanvasGameObject, TextMeshProUGUI timerText, bool startTimer, bool isCountingDown)
    {
        this.gameObject = gameObject;
        this.timerCanvasGameObject = timerCanvasGameObject;
        this.heroMovement = heroMovement;

        this.owner = owner;
        this.name = name;
        this.heroIndex = heroIndex;
        this.countyPopulationIndex = countyPopulationIndex;
        this.location = location;
        this.destination = destination;
        this.IsSelected = IsSelected;

        this.timerText = timerText;
        this.startTimer = startTimer;
        this.isCountingDown = isCountingDown;
    }
}
