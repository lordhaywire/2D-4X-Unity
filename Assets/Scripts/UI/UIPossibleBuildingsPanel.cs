using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPossibleBuildingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject possibleBuildingsPrefab;
    [SerializeField] private GameObject possibleBuildingsParent;

    private readonly List<GameObject> possibleBuildingClones = new();

    private void OnEnable()
    {
        for (int i = 0; i < WorldMapLoad.instance.researchItemsTier1.Count; i++)
        {
            if (WorldMapLoad.instance.researchItemsTier1[i].isResearchDone && 
                WorldMapLoad.instance.researchItemsTier1[i].isBuilding == true)
            {
                possibleBuildingClones.Add(Instantiate(possibleBuildingsPrefab, possibleBuildingsParent.transform));
                possibleBuildingClones[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    WorldMapLoad.instance.researchItemsTier1[i].researchName;
            }
            
        }
            
    }

    public void CloseButton()
    {
        for (int i = 0; i < possibleBuildingClones.Count; i++)
        {
            Destroy(possibleBuildingClones[i]);
        }
        possibleBuildingClones.Clear();
    }
}
