using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ResearchItem
{
    public GameObject gameObject; // I am don't think we actually need this.
    public Toggle toggle;
    public TextMeshProUGUI researchNameText;
    public string researchName;
    public int tier;

    public bool isDone;

    public ResearchItem(GameObject gameObject, Toggle toggle, TextMeshProUGUI researchNameText,
        string researchName, int tier, bool isDone)
    {
        this.toggle = toggle;
        this.researchNameText = researchNameText;
        this.gameObject = gameObject;
        this.researchName = researchName;
        this.tier = tier;
        this.isDone = isDone;
    }
}
