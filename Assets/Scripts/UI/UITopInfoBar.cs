using TMPro;
using UnityEngine;

public class UITopInfoBar : MonoBehaviour
{
    public static UITopInfoBar instance;

    public TextMeshProUGUI influenceText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI scrapText;

    private int influence;
    public int money;
    public int food;
    public int scrap;

    public int Influence
    {
        get
        {
            return influence;
        }
        set
        {
            influence = value;
            influenceText.text = influence.ToString();
        }
    }
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            moneyText.text = money.ToString();
        }
    }
    public int Food
    {
        get
        {
            return food;
        }
        set
        {
            food = value;
            foodText.text = food.ToString();
        }
    }
    public int Scrap
    {
        get
        {
            return scrap;
        }
        set
        {
            scrap = value;
            scrapText.text = scrap.ToString();
        }
    }
    private void Awake()
    {
        instance = this;
    }
}
