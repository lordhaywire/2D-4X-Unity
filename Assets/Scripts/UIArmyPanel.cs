using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIArmyPanel : MonoBehaviour
{
    public static TextMeshProUGUI armyOwnerText;
    public static TextMeshProUGUI armyNameText;
    public static TextMeshProUGUI armySizeText;


    private void Awake()
    {
        armyOwnerText = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        armyNameText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        armySizeText = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }
}
