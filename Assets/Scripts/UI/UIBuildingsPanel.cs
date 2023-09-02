using UnityEngine;

public class UIBuildingsPanel : MonoBehaviour
{
    public static UIBuildingsPanel Instance { get; private set; }

    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private Transform possibleBuildingsScrollView;
    [SerializeField] private Transform currentBuildingsScrollView;

    public GameObject possibleBuildingDescriptionPanel;
    public GameObject currentBuildingDescriptionPanel;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        
        PanelRefresher();
    }

    public void PanelRefresher()
    {
        PanelClearer();
        // First run with possible buildings
        Transform possibleBuildingsParent = WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyInfo>().possibleBuildingsParent;
        CreateUIBuildings(possibleBuildingsParent, possibleBuildingsScrollView);

        // Second run with current buildings
        Transform currentBuildingsParent = WorldMapLoad.Instance.CurrentlySelectedCounty.GetComponent<CountyInfo>().currentBuildingsParent;
        CreateUIBuildings(currentBuildingsParent, currentBuildingsScrollView);
    }

    public void CreateUIBuildings(Transform parent, Transform scrollView)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject uIBuilding = Instantiate(buildingPrefab, scrollView);
            UIBuildingButton uiBuildingButton = uIBuilding.GetComponent<UIBuildingButton>();
            BuildingInfo buildingInfo = parent.GetChild(i).GetComponent<BuildingInfo>();

            uiBuildingButton.buildingNameText.text = buildingInfo.buildingName;
            uiBuildingButton.actualBuilding = parent.GetChild(i).gameObject;
            Debug.Log(uiBuildingButton.actualBuilding.name);
        }
    }

    public void PanelClearer()
    {
        // First run to destroy possible buildings
        UIBuildingDestroyer(possibleBuildingsScrollView);

        // Second run to destroy current buildings
        UIBuildingDestroyer(currentBuildingsScrollView);
    }

    public void UIBuildingDestroyer(Transform parent)
    { 
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    private void OnDisable()
    {
        PanelClearer();
    }
}
