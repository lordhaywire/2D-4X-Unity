using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResearchItem : MonoBehaviour
{
    public Toggle toggle;
    public TextMeshProUGUI researchName;


    public void UIResearchItemButton()
    {
        UIResearchPanel.Instance.researchDescriptionPanel.SetActive(true);

        ResearchItem researchItem = WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].researchItems[int.Parse(name)];

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
