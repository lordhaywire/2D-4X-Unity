using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBuildingPanelsRefresher : MonoBehaviour
{
    public static UIBuildingPanelsRefresher Instance;

    [SerializeField] private GameObject currentBuildingsPrefab;
    [SerializeField] private GameObject currentBuildingsParent;

    [SerializeField] private GameObject possibleBuildingsPrefab;
    [SerializeField] private GameObject possibleBuildingsParent;

    private readonly List<GameObject> currentBuildingClones = new();
    private readonly List<GameObject> possibleBuildingClones = new();

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        WorldMapLoad.Instance.RefreshBuildingPanels += PossibleBuildingsPanelsDestroyer;
        WorldMapLoad.Instance.RefreshBuildingPanels += PossibleBuildingPanelsRefresher;

        WorldMapLoad.Instance.RefreshBuildingPanels += CurrentBuildingPanelsDestroyer;
        WorldMapLoad.Instance.RefreshBuildingPanels += CurrentBuildingPanelsRefresher;
    }

    public void PossibleBuildingPanelsRefresher()
    {
        var possibleBuildings = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].possibleBuildings;
        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            possibleBuildingClones.Add(Instantiate(possibleBuildingsPrefab, possibleBuildingsParent.transform));
            possibleBuildingClones[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                possibleBuildings[i].name;
            possibleBuildingClones[i].name = i.ToString();
            possibleBuildings[i].gameObject = possibleBuildingClones[i];
        }
    }

    public void PossibleBuildingsPanelsDestroyer()
    {
        for (int i = 0; i < possibleBuildingClones.Count; i++)
        {
            Destroy(possibleBuildingClones[i]);
        }
        possibleBuildingClones.Clear();
    }
    public void CurrentBuildingPanelsRefresher()
    {
        var currentBuildings = WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].currentBuildings;
        for (int i = 0; i < currentBuildings.Count; i++)
        {
            currentBuildingClones.Add(Instantiate(currentBuildingsPrefab, currentBuildingsParent.transform));
            currentBuildingClones[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                currentBuildings[i].name;
            currentBuildingClones[i].name = i.ToString();
            currentBuildings[i].gameObject = currentBuildingClones[i];
            if (currentBuildings[i].isBuilt == true)
            {
                currentBuildingClones[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void CurrentBuildingPanelsDestroyer()
    {
        for (int i = 0; i < currentBuildingClones.Count; i++)
        {
            Destroy(currentBuildingClones[i]);
        }
        currentBuildingClones.Clear();
    }


    private void OnDisable()
    {
        WorldMapLoad.Instance.RefreshBuildingPanels -= PossibleBuildingsPanelsDestroyer;
        WorldMapLoad.Instance.RefreshBuildingPanels -= PossibleBuildingPanelsRefresher;

        WorldMapLoad.Instance.RefreshBuildingPanels -= CurrentBuildingPanelsDestroyer;
        WorldMapLoad.Instance.RefreshBuildingPanels -= CurrentBuildingPanelsRefresher;
    }
}
