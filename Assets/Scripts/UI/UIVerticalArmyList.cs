using TMPro;
using UnityEngine;

public class UIVerticalArmyList : MonoBehaviour
{ 
    public static TextMeshProUGUI armyButtonText;

    private void Awake()
    {
        armyButtonText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
