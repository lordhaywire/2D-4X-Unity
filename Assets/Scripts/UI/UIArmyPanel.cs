using TMPro;
using UnityEngine;

public class UIArmyPanel : MonoBehaviour
{
    public static UIArmyPanel Instance;

    public TextMeshProUGUI armyOwnerText;
    public TextMeshProUGUI armyNameText;
    public TextMeshProUGUI armySizeText;

    private void Awake()
    {
        Instance = this;
    }
}
