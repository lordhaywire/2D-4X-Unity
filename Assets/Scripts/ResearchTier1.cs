using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchTier1 : MonoBehaviour
{
    public static ResearchTier1 instance;
    public GameObject researchTitleAndDescriptionPanel;

    private void Awake()
    {
        instance = this;
        FirstRunResearch();
    }

    private void OnEnable()
    {
        TimeKeeper.instance.PauseandUnpause();
        UpdateResearchItemsTier1();
    }

    private void OnDisable()
    {
        TimeKeeper.instance.PauseandUnpause();
    }

    private void FirstRunResearch()
    {
        // This goes through the research list and sets their Game Objects to the correct gameOjects and toggles.
        var researchItems = WorldMapLoad.instance.factions[WorldMapLoad.instance.playerFactionID].researchItems;
        for (int i = 0; i < researchItems.Count; i++)
        {
            researchItems[i].gameObject = transform.GetChild(i).gameObject; // I don't think we need this.
            researchItems[i].gameObject.name = i.ToString();
            researchItems[i].toggle = researchItems[i].gameObject.transform.GetChild(0).GetComponent<Toggle>();
            researchItems[i].researchNameText = researchItems[i].gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
    }
    private void UpdateResearchItemsTier1()
    {
        var researchItems = WorldMapLoad.instance.factions[WorldMapLoad.instance.playerFactionID].researchItems;
        for (int i = 0;i < researchItems.Count; i++)
        {
            researchItems[i].researchNameText.text = researchItems[i].name;
            if (researchItems[i].isResearchDone == true)
            {
                researchItems[i].toggle.isOn = true;
            }
            else
            {
                researchItems[i].toggle.isOn = false;
            }           
        }
    }
}
