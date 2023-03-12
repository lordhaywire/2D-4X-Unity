using System;

[Serializable]
public class ResearchItem
{
    public string researchName;
    public bool isDone;
    public int tier;

    // Since this is the only item that can be changed by the player, can we just have this in there?
    public ResearchItem(bool isDone)
    {
        this.isDone = isDone;
    }
}
