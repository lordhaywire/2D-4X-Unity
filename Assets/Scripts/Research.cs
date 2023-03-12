using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Research : MonoBehaviour
{
    public static Research instance;
    
    public GameObject researchItemPrefab;
    public GameObject tier1Research;
    public GameObject tier2Research;
    public GameObject tier3Research;

    public List<ResearchItem> researchItems = new();

    private void Awake()
    {
        instance = this;
        GenerateResearchPanel(); 
    }

    private void GenerateResearchPanel()
    {
        for (int i = 0; i < researchItems.Count; i++)
        {
            switch (researchItems[i].tier)
            {
                case 1:
                    var tier1item = Instantiate(researchItemPrefab, tier1Research.transform);
                    tier1item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = researchItems[i].researchName;
                    break;
                case 2:

                    var tier2item = Instantiate(researchItemPrefab, tier2Research.transform);
                    tier2item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = researchItems[i].researchName;
                    break;
                case 3:
                    var tier3item = Instantiate(researchItemPrefab, tier3Research.transform);
                    tier3item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = researchItems[i].researchName;
                    break;
            }
        }
    }
}
