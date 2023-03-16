using System;
using UnityEngine;

[Serializable]
public class ResearchItem
{
    public string researchName;
    public int tier;
    public ResearchItem nextResearch;
    public GameObject nextResearchGameObject;

    public bool isDone;

    // Since this is the only item that can be changed by the player, can we just have this in there?
    public ResearchItem(bool isDone)
    {
        this.isDone = isDone;
    }
}
