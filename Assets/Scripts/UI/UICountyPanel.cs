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
    }
}
