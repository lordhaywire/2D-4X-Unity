using System.Collections.Generic;
using UnityEngine;

public class Hero
{
    public GameObject gameObject;
    public HeroMovement heroMovement; // We might be able to get rid of this because we are assigning the gameobject
                                      // to currentlySelectedHero;
                                      //public List<HeroStack> heroStacks;
    public HeroStackCount heroStackCount;
    public bool isSpawned;
    private int orderLayer;
    public string owner;
    public string name;
    public int heroIndex; // Maybe move this to the top at some point.
    public int countyPopulationIndex;
    public string location;
    public string destination;
    private bool isSelected;

    public bool startTimer;
    public bool isCountingDown;

    // I don't think we need to store this info.
    public int OrderLayer
    {
        get
        {
            return orderLayer;
        }
        set
        {
            orderLayer = value;
            if (gameObject != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = orderLayer;
            }

        }
    }
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
                gameObject.GetComponent<SpriteRenderer>().sprite = HeroTokenSprites.Instance.heroUnselectedSprite;
            }
            else if (isSelected == true && gameObject != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = HeroTokenSprites.Instance.heroSelectedSprite;
            }
            else
            {
                //Debug.Log("GameObject is null in the Is Army Selected Property.");
            }
        }
    }

    public Hero(GameObject gameObject, HeroMovement heroMovement, HeroStackCount heroStacking, bool isSpawned, int OrderLayer, string owner,
        string name, int heroIndex, int countyPopulationIndex, string location, bool IsSelected, string destination,
        bool startTimer, bool isCountingDown)
    {
        this.gameObject = gameObject;
        this.heroMovement = heroMovement;
        this.heroStackCount = heroStacking;

        //heroStacks = new();// This initializes the list. It is not in the constructor. 

        this.isSpawned = isSpawned;
        this.OrderLayer = OrderLayer;
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
