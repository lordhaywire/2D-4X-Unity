using TMPro;
using UnityEngine;

public class UIProvincePanel : MonoBehaviour
{
    //public GameObject provincePanel;
    
    public static TextMeshProUGUI provinceOwnerText;
    public static TextMeshProUGUI provinceNameText;
    public static TextMeshProUGUI provincePopulationText;

    /*private void Awake()
    {
        provincePanel = this.gameObject;
    }*/
    private void Start()
    {
        provinceOwnerText = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        provinceNameText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        provincePopulationText = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }
}
