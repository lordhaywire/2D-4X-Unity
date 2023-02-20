using TMPro;
using UnityEngine;

public class UIVerticalArmyList : MonoBehaviour
{ 
    public static TextMeshProUGUI armyButtonText; // This will be changed to an array when we are instantiating the buttons.

    private void Awake()
    {
        armyButtonText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
