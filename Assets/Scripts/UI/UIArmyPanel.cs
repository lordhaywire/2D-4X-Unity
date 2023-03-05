using TMPro;
using UnityEngine;

public class UIArmyPanel : MonoBehaviour
{
    public static TextMeshProUGUI armyOwnerText;
    public static TextMeshProUGUI armyNameText;
    public static TextMeshProUGUI armySizeText;

    private void Awake()
    {
        armyOwnerText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        armyNameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        armySizeText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }
}
