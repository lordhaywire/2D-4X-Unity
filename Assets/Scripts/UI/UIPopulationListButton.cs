using UnityEngine;

public class UIPopulationListButton : MonoBehaviour
{
    public static UIPopulationListButton Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PopulationListButtonClicked()
    {
        Debug.Log("Population List Button Game Object Name: " + gameObject.name);
        WorldMapLoad.Instance.populationDescriptionPanel.SetActive(true);
        WorldMapLoad.Instance.populationDescriptionPanelOpen = true;
        WorldMapLoad.Instance.currentlySelectedPopulation = int.Parse(gameObject.name);
        
    }
}
