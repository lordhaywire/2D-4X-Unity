using TMPro;
using UnityEngine;

public class Hero
{
    public GameObject gameObject;
    public HeroMovement heroMovement; // We might be able to get rid of this because we are assigning the gameobject
                                      // to currentlySelectedHero;
    public string owner;
    public string name;
    public int heroIndex;
    public int countyPopulationIndex;
    public string location;
    public string destination;
    public bool isSelected;

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
        bool startTimer, bool isCountingDown)
    {
        this.gameObject = gameObject;
        this.heroMovement = heroMovement;

        this.owner = owner;
        this.name = name;
        this.heroIndex = heroIndex;
        this.countyPopulationIndex = countyPopulationIndex;
        this.location = location;
        this.destination = destination;
        this.IsSelected = IsSelected;

        this.startTimer = startTimer;
        this.isCountingDown = isCountingDown;
    }
}
