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
    public string description;

    public string prerequisite1;
    public string prerequisite2;
    public int tier;

    public bool isResearchDone;
    public bool isBuilding;
    public bool isBuilt;

    public int influanceCost;
    public int daysToBuild;
    public int currentEmployees;
    public int maxEmployees;


    public ResearchItem(GameObject gameObject, Toggle toggle, TextMeshProUGUI researchNameText,
        string researchName, string description, string prerequisite1, string prerequisite2, int tier,
        bool isResearchDone, bool isBuilding, bool isBuilt, int influanceCost, int daysToBuild, int currentEmployees, int maxEmployees)
    {
        this.gameObject = gameObject;
        this.toggle = toggle;
        this.researchNameText = researchNameText;
        this.researchName = researchName;
        this.description = description;
        this.prerequisite1 = prerequisite1;
        this.prerequisite2 = prerequisite2;
        this.tier = tier;
        this.isResearchDone = isResearchDone;
        this.isBuilding = isBuilding;
        this.isBuilt = isBuilt;
        this.influanceCost = influanceCost;
        this.daysToBuild = daysToBuild;
        this.currentEmployees = currentEmployees;
        this.maxEmployees = maxEmployees;
    }
}
