using TMPro;
using UnityEngine;

public class UIArmyPanel : MonoBehaviour
{
    public static UIArmyPanel instance;
    public TextMeshProUGUI armyOwnerText;
    public TextMeshProUGUI armyNameText;
    public TextMeshProUGUI armySizeText;

    private void Awake()
    {
        instance = this;
        /*
        armyOwnerText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        armyNameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        armySizeText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        */
    }
}
