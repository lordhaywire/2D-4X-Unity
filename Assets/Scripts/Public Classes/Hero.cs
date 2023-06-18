using System;
using UnityEngine;

[Serializable]
public class Hero
{
    public GameObject gameObject;
    public HeroMovement heroMovement; // We might be able to get rid of this because we are assigning the gameobject
                                      // to currentlySelectedHero;
                                      //public List<HeroStack> heroStacks;
    public TokenComponents tokenComponents;
    public bool isSpawned;
    private int orderLayer;
    public string owner;
    public string name;
    public int heroIndex; // Maybe move this to the top at some point.
    public int heroStackIndex;
    public int countyPopulationIndex;
    public string location;
    public string destination;
    [SerializeField] private bool isSelected;
    public bool justMoved;

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
                Debug.Log("GameObject is null in the Is Army Selected Property.");
            }
        }
    }

    public Hero(GameObject gameObject, HeroMovement heroMovement, TokenComponents tokenComponents, bool isSpawned, int OrderLayer, string owner,
        string name, int heroIndex, int heroStackIndex, int countyPopulationIndex, string location, bool IsSelected, bool justMoved, string destination,
        bool startTimer, bool isCountingDown)
    {
        this.gameObject = gameObject;
        this.heroMovement = heroMovement;
        this.tokenComponents = tokenComponents;

        this.isSpawned = isSpawned;
        this.OrderLayer = OrderLayer;
        this.owner = owner;
        this.name = name;
        this.heroIndex = heroIndex;
        this.heroStackIndex = heroStackIndex;
        this.countyPopulationIndex = countyPopulationIndex;
        this.location = location;
        this.destination = destination;
        this.IsSelected = IsSelected;
        this.justMoved = justMoved;

        this.startTimer = startTimer;
        this.isCountingDown = isCountingDown;
    }
}
