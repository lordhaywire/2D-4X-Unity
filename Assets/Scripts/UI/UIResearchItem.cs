using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResearchItem : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private TextMeshProUGUI researchName;

    private void OnEnable()
    {
        var researchItems = WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].researchItems;
        for (int i = 0; i < researchItems.Count; i++)
        {
            researchName.text = researchItems[i].name;
            if (researchItems[i].isResearchDone == true)
            {
                toggle.isOn = true;
            }
            else
            {
                toggle.isOn = false;
            }
        }
    }
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
