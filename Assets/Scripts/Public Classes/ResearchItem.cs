using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ResearchItem
{
    public string name;
    public string description;

    public string prerequisite1;
    public string prerequisite2;
    public int tier;

    public bool isBuilding;
    public bool isResearchDone;

    public ResearchItem( 
        string researchName, string description, string prerequisite1, string prerequisite2, int tier,
        bool isBuilding, bool isResearchDone) //GameObject gameObject,
    {
        this.name = researchName;
        this.description = description;
        this.prerequisite1 = prerequisite1;
        this.prerequisite2 = prerequisite2;
        this.tier = tier;
        this.isBuilding = isBuilding;
        this.isResearchDone = isResearchDone;
    }
}
