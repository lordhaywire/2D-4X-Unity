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
    public string name;
    public string description;

    public string prerequisite1;
    public string prerequisite2;
    public int tier;

    public bool isBuilding;
    public bool isResearchDone;

    public PossibleBuilding possibleBuildings; // Do we really need this in here?

    public ResearchItem(GameObject gameObject, Toggle toggle, TextMeshProUGUI researchNameText,
        string researchName, string description, string prerequisite1, string prerequisite2, int tier,
        bool isBuilding, bool isResearchDone, PossibleBuilding possibleBuildings)
    {
        this.gameObject = gameObject;
        this.toggle = toggle;
        this.researchNameText = researchNameText;
        this.name = researchName;
        this.description = description;
        this.prerequisite1 = prerequisite1;
        this.prerequisite2 = prerequisite2;
        this.tier = tier;
        this.isBuilding = isBuilding;
        this.isResearchDone = isResearchDone;
        this.possibleBuildings = possibleBuildings;
    }
}
