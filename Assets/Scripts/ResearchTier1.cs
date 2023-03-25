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
        for (int i = 0; i < WorldMapLoad.instance.researchItemsTier1.Count; i++)
        {
            WorldMapLoad.instance.researchItemsTier1[i].gameObject = transform.GetChild(i).gameObject; // I don't think we need this.
            WorldMapLoad.instance.researchItemsTier1[i].gameObject.name = i.ToString();
            WorldMapLoad.instance.researchItemsTier1[i].toggle = WorldMapLoad.instance.researchItemsTier1[i].gameObject.transform.GetChild(0).GetComponent<Toggle>();
            WorldMapLoad.instance.researchItemsTier1[i].researchNameText = WorldMapLoad.instance.researchItemsTier1[i].gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
    }
    private void UpdateResearchItemsTier1()
    {
        for(int i = 0;i < WorldMapLoad.instance.researchItemsTier1.Count; i++)
        {
            WorldMapLoad.instance.researchItemsTier1[i].researchNameText.text = WorldMapLoad.instance.researchItemsTier1[i].name;
            if (WorldMapLoad.instance.researchItemsTier1[i].isResearchDone == true)
            {
                WorldMapLoad.instance.researchItemsTier1[i].toggle.isOn = true;
            }
            else
            {
                WorldMapLoad.instance.researchItemsTier1[i].toggle.isOn = false;
            }           
        }
    }
}
