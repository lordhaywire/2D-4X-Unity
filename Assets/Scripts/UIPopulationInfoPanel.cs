using TMPro;
using UnityEngine;

public class UIPopulationInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI populationListText;

    private void OnEnable()
    {
        // Reset the text to nothing between each open.
        populationListText.text = null;

        var county = WorldMapLoad.countyPopulationDictionary[SelectCounty.currentlySelectedCounty];
        for (int i = 0; i < county.Count; i++)
        {
            //populationListText.text = WorldMapLoad.countyPopulationDictionary[county][i].firstName;
            populationListText.text += "Name: " + county[i].firstName + " " + county[i].lastName +
             " Age: " + county[i].age + " Is Male? " + county[i].isMale + "\n";
        }
    }

}
