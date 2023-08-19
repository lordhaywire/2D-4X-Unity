using TMPro;
using UnityEngine;

public class UITopInfoBar : MonoBehaviour
{
    public static UITopInfoBar Instance;

    public TextMeshProUGUI influenceText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI scrapText;

    private int influence;
    private int money;
    private int food;
    private int scrap;

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
        Instance = this;
        WorldMapLoad.Instance.factions[0].InfluenceChanged += UpdateInfluenceText;
    }

    private void UpdateInfluenceText()
    {
        influenceText.text = WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].influence.ToString();
    }
}
