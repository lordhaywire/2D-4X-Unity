using TMPro;
using UnityEngine;

public class UIProvincePanel : MonoBehaviour
{
    public static TextMeshProUGUI countyOwnerText;
    public static TextMeshProUGUI countyNameText;
    public static TextMeshProUGUI countyPopulationText;


    private void Awake()
    {
        countyOwnerText = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        countyNameText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        countyPopulationText = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }
}
