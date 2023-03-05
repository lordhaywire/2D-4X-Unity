using TMPro;
using UnityEngine;

public class UICountyPanel : MonoBehaviour
{
    public static UICountyPanel instance;

    public GameObject heroInfoList;
    public GameObject armyInfoList;

    public TextMeshProUGUI countyOwnerText;
    public TextMeshProUGUI countyNameText;
    public TextMeshProUGUI countyPopulationText;
    private void Awake()
    {
        instance = this;
        /*
        heroInfoList = transform.GetChild(4).GetChild(0).gameObject;
        armyInfoList = transform.GetChild(4).GetChild(1).gameObject;

        countyOwnerText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        countyNameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        countyPopulationText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        */
    }
}
