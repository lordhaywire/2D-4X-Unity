using TMPro;
using UnityEngine;

public class UICountyPanel : MonoBehaviour
{
    public static GameObject heroInfoList;
    public static GameObject armyInfoList;

    public static TextMeshProUGUI countyOwnerText;
    public static TextMeshProUGUI countyNameText;
    public static TextMeshProUGUI countyPopulationText;
    private void Awake()
    {
        heroInfoList = transform.GetChild(4).GetChild(0).gameObject;
        armyInfoList = transform.GetChild(4).GetChild(1).gameObject;

        countyOwnerText = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        countyNameText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        countyPopulationText = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }
}
