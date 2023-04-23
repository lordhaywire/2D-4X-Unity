using UnityEngine;

public class UIResearchItem : MonoBehaviour
{
    public void UIResearchItemButton()
    {
        Research.Instance.researchTitleAndDescriptionPanel.SetActive(true);

        var researchItem = WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].researchItems[int.Parse(name)];
        UIResearchTitleDescriptionPanel.Instance.title.text =
            researchItem.name;
        UIResearchTitleDescriptionPanel.Instance.description.text =
            researchItem.description;
        if(researchItem.prerequisite1 == null && researchItem.prerequisite1 == null)
        {
            UIResearchTitleDescriptionPanel.Instance.prerequisite.text = "None";
        }
        else
        {
            Debug.Log("We don't have any research that currently has prerequisites so something is fucked.");
        }
    }
}
