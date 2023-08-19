using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResearchPanel : MonoBehaviour
{
    public static UIResearchPanel Instance;

    public GameObject researchDescriptionPanel;
    public GameObject researchItemsParent;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        TimeKeeper.Instance.PauseTime();

        List<ResearchItem> researchItems = WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].researchItems;

        for (int i = 0; i < researchItems.Count; i++)
        {
            researchItemsParent.transform.GetChild(i).gameObject.SetActive(true);

            TextMeshProUGUI researchName = researchItemsParent.transform.GetChild(i).GetComponent<UIResearchItem>().researchName;
            Toggle toggle = researchItemsParent.transform.GetChild(i).GetComponent<UIResearchItem>().toggle;
            researchItemsParent.transform.GetChild(i).gameObject.name = i.ToString();

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

    private void OnDisable()
    {
        TimeKeeper.Instance.UnpauseTime();  
    }


}
