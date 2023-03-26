using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPossibleBuildingsPanel : MonoBehaviour
{
    public static UIPossibleBuildingsPanel instance;

    public GameObject buildingDescriptionPanel;
    private int possibleBuildingNumber;

    public event Action PossibleBuildingButtonPressed;

    public int PossibleBuildingNumber
    {
        get 
        { 
            return possibleBuildingNumber; 
        }
        set 
        { 
            possibleBuildingNumber = value;
            PossibleBuildingButtonPressed();
        }
    }

    [SerializeField] private GameObject possibleBuildingsPrefab;
    [SerializeField] private GameObject possibleBuildingsParent;

    private readonly List<GameObject> possibleBuildingClones = new();

    private void Awake() // This was OnEnable but that was causing it to instantiate every time it got opened.
    {
        instance = this;
        
        for (int i = 0; i < WorldMapLoad.instance.researchItemsTier1.Count; i++)
        {
            if (WorldMapLoad.instance.researchItemsTier1[i].isResearchDone && 
                WorldMapLoad.instance.researchItemsTier1[i].isBuilding == true)
            {
                possibleBuildingClones.Add(Instantiate(possibleBuildingsPrefab, possibleBuildingsParent.transform));
                possibleBuildingClones[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    WorldMapLoad.instance.researchItemsTier1[i].possibleBuildings.name;
                possibleBuildingClones[i].name = i.ToString();
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
