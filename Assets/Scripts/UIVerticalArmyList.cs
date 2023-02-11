using TMPro;
using UnityEngine;

public class UIVerticalArmyList : MonoBehaviour
{
    //private GameObject leaderButtonGameObject; // This will be changed to an array when we are instantiating the buttons.
    public static TextMeshProUGUI armyButtonText; // This will be changed to an array when we are instantiating the buttons.

    private void Awake()
    {
        //leaderButtonGameObject = transform.GetChild(0).gameObject;
        armyButtonText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
