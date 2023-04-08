using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBuildingsPanel : MonoBehaviour
{
    public static UIBuildingsPanel instance;

    public GameObject buildingDescriptionPanel;
    public GameObject possibleBuildingsGroupGameObject;

    private int possibleBuildingNumber;
    private int currentBuildingNumber;

    public event Action PossibleBuildingButtonPressed;
    public event Action CurrentBuildingButtonPressed;

    public int CurrentBuildingNumber
    {
        get
        {
            return currentBuildingNumber;
        }
        set
        {
            currentBuildingNumber = value;
            CurrentBuildingButtonPressed?.Invoke();
        }
    }

    public int PossibleBuildingNumber
    {
        get
        {
            return possibleBuildingNumber;
        }
        set
        {
            possibleBuildingNumber = value;
            PossibleBuildingButtonPressed?.Invoke();
        }
    }

    [SerializeField] private GameObject possibleBuildingsPrefab;
    [SerializeField] private GameObject possibleBuildingsParent;

    private readonly List<GameObject> possibleBuildingClones = new();
    private void OnEnable() // This needs to be triggered by an event or when another county is selected.
    {
        instance = this;
        UICountyPanel.instance.buildingsPanelExpanded = true;
        var possibleBuildings = WorldMapLoad.instance.counties[WorldMapLoad.instance.currentlySelectedCounty].possibleBuildings;
        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildingClones.Add(Instantiate(possibleBuildingsPrefab, possibleBuildingsParent.transform));
            possibleBuildingClones[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                possibleBuildings[i].name;
            possibleBuildingClones[i].name = i.ToString();
            possibleBuildings[i].gameObject = possibleBuildingClones[i];
        }
    }
    public void CollapseButton()
    {
        UICountyPanel.instance.buildingsPanelExpanded = false;
        for (int i = 0; i < possibleBuildingClones.Count; i++)
        {
            Destroy(possibleBuildingClones[i]);
        }
        possibleBuildingClones.Clear();
    }

}
