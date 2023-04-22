using UnityEngine;

public class UIResearchItem : MonoBehaviour
{
    public void UIResearchItemButton()
    {
        ResearchTier1.instance.researchTitleAndDescriptionPanel.SetActive(true);

        var researchItem = WorldMapLoad.instance.factions[WorldMapLoad.instance.playerFactionID].researchItems[int.Parse(name)];
        UIResearchTitleDescriptionPanel.instance.title.text =
            researchItem.name;
        UIResearchTitleDescriptionPanel.instance.description.text =
            researchItem.description;
        if(researchItem.prerequisite1 == null && researchItem.prerequisite1 == null)
        {
            UIResearchTitleDescriptionPanel.instance.prerequisite.text = "None";
        }
        else
        {
            Debug.Log("We don't have any research that currently has prerequisites so something is fucked.");
        }
    }
}
