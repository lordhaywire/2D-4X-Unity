using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchTier1 : MonoBehaviour
{
    /*
    [SerializeField] private GameObject researchItem0;
    [SerializeField] private GameObject researchItem1;
    [SerializeField] private GameObject researchItem2;
    [SerializeField] private GameObject researchItem3;
    [SerializeField] private GameObject researchItem4;
    [SerializeField] private GameObject researchItem5;
    [SerializeField] private GameObject researchItem6;
    [SerializeField] private GameObject researchItem7;
    [SerializeField] private GameObject researchItem8;
    [SerializeField] private GameObject researchItem9;
    [SerializeField] private GameObject researchItem10;
    [SerializeField] private GameObject researchItem11;
    [SerializeField] private GameObject researchItem12;
    [SerializeField] private GameObject researchItem13;
    */
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
        researchItemsTier1[0] = new ResearchItem(null, null, null, "Fisher's Shack", 1, true);
        researchItemsTier1[1] = new ResearchItem(null, null, null, "Forester's Shack", 1, true);
        researchItemsTier1[2] = new ResearchItem(null, null, null, "Gardener's Shack", 1, true);
        researchItemsTier1[3] = new ResearchItem(null, null, null, "Researcher's Shack", 1, true);
        researchItemsTier1[4] = new ResearchItem(null, null, null, "Scavenger's Shack", 1, true);
        researchItemsTier1[5] = new ResearchItem(null, null, null, "Stone Worker's Shack", 1, true);

        researchItemsTier1[6] = new ResearchItem(null, null, null, "Basic Tactics - Guns", 1, false);
        researchItemsTier1[7] = new ResearchItem(null, null, null, "Basic Tactics - Melee", 1, false);

        researchItemsTier1[8] = new ResearchItem(null, null, null, "Primative Melee Weaponsmith's Shack", 1, false);
        researchItemsTier1[9] = new ResearchItem(null, null, null, "Primative Ranged Weaponsmith's Shack", 1, false);
        researchItemsTier1[10] = new ResearchItem(null, null, null, "Primative Gunsmith's Shack", 1, false);
        researchItemsTier1[11] = new ResearchItem(null, null, null, "Primative Ammunition Smith's Shack", 1, false);
        researchItemsTier1[12] = new ResearchItem(null, null, null, "Primative Gun Ammunition Smith's Shack", 1, false);

        researchItemsTier1[13] = new ResearchItem(null, null, null, "Elitism", 1, false);

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
