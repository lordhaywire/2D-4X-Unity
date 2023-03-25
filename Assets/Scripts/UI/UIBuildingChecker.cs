using UnityEngine;

public class UIBuildingChecker : MonoBehaviour
{
    private int unemployed;
    private void OnEnable()
    {
        UIBuildingDescriptionPanel.instance.CurrentEmployeesChanged += CheckEnoughPopulation;
    }

    private void CheckEnoughPopulation()
    {
        unemployed = 0;
        for (int i = 0; i < WorldMapLoad.instance.countyPopulationDictionary[WorldMapLoad.instance.currentlySelectedCounty].Count; i++)
        {
            if (WorldMapLoad.instance.countyPopulationDictionary[WorldMapLoad.instance.currentlySelectedCounty][i].activity
                != "Scavenging")
            {
                unemployed++;
            }
        }
        if(unemployed <= WorldMapLoad.instance.possibleBuildings[UIPossibleBuildingsPanel.instance.PossibleBuildingNumber].currentEmployees)
        {
            Debug.Log("There are enough unemployed people.");
        }
    }

    private void OnDisable()
    {
        UIBuildingDescriptionPanel.instance.CurrentEmployeesChanged -= CheckEnoughPopulation;
    }
}
