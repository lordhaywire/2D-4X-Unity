using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchTier1 : MonoBehaviour
{
    public static ResearchTier1 instance;
    public GameObject researchTitleAndDescriptionPanel;
    public List<ResearchItem> researchItemsTier1 = new();

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
        researchItemsTier1[0] = new ResearchItem(null, null, null, "Fisher's Shack", AllText.ResearchItemDescriptions.FISHERSSHACK, null, null, 1, true);
        researchItemsTier1[1] = new ResearchItem(null, null, null, "Forester's Shack", AllText.ResearchItemDescriptions.FORESTERSSHACK, null, null, 1, true);
        researchItemsTier1[2] = new ResearchItem(null, null, null, "Gardener's Shack", AllText.ResearchItemDescriptions.GARDENERSSHACK, null, null, 1, true);
        researchItemsTier1[3] = new ResearchItem(null, null, null, "Researcher's Shack", AllText.ResearchItemDescriptions.RESEARCHSSHACK, null, null, 1, true);
        researchItemsTier1[4] = new ResearchItem(null, null, null, "Scavenger's Shack", AllText.ResearchItemDescriptions.SCAVANGERSSHACK, null, null, 1, true);
        researchItemsTier1[5] = new ResearchItem(null, null, null, "Stone Worker's Shack", AllText.ResearchItemDescriptions.STONEWORKERSSHACK, null, null, 1, true);

        researchItemsTier1[6] = new ResearchItem(null, null, null, "Basic Tactics - Guns", AllText.ResearchItemDescriptions.BASICTACTICSGUNS, null, null, 1, false);
        researchItemsTier1[7] = new ResearchItem(null, null, null, "Basic Tactics - Melee", AllText.ResearchItemDescriptions.BASICTACTICSMELEE, null, null, 1, false);

        researchItemsTier1[8] = new ResearchItem(null, null, null, "Primative Melee Weaponsmith's Shack", AllText.ResearchItemDescriptions.PRIMATIVEMELEESMITHSHACK, null, null, 1, false);
        researchItemsTier1[9] = new ResearchItem(null, null, null, "Primative Ranged Weaponsmith's Shack", AllText.ResearchItemDescriptions.PRIMATIVERANGEDSMITHSHACK, null, null, 1, false);
        researchItemsTier1[10] = new ResearchItem(null, null, null, "Primative Gunsmith's Shack", AllText.ResearchItemDescriptions.PRIMATIVEGUNSMITHSHACK, null, null, 1, false);
        researchItemsTier1[11] = new ResearchItem(null, null, null, "Primative Ammunition Smith's Shack", AllText.ResearchItemDescriptions.PRIMATIVEAMMOSHACK, null, null, 1, false);
        researchItemsTier1[12] = new ResearchItem(null, null, null, "Primative Gun Ammunition Smith's Shack", AllText.ResearchItemDescriptions.PRIMATVEGUNAMMOSHACK, null, null, 1, false);

        researchItemsTier1[13] = new ResearchItem(null, null, null, "Elitism", AllText.ResearchItemDescriptions.ELITISM, null, null, 1, false);

        for (int i = 0; i < researchItemsTier1.Count; i++)
        {
            researchItemsTier1[i].gameObject = transform.GetChild(i).gameObject; // I don't think we need this.
            researchItemsTier1[i].gameObject.name = i.ToString();
            researchItemsTier1[i].toggle = researchItemsTier1[i].gameObject.transform.GetChild(0).GetComponent<Toggle>();
            researchItemsTier1[i].researchNameText = researchItemsTier1[i].gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        }
    }
    private void UpdateResearchItemsTier1()
    {
        for(int i = 0;i < researchItemsTier1.Count; i++)
        {
            researchItemsTier1[i].researchNameText.text = researchItemsTier1[i].researchName;
            if (researchItemsTier1[i].isDone == true)
            {
                researchItemsTier1[i].toggle.isOn = true;
            }
            else
            {
                researchItemsTier1[i].toggle.isOn = false;
            }           
        }
    }
}
