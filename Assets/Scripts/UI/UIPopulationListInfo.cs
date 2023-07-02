using TMPro;
using UnityEngine;

public class UIPopulationListInfo : MonoBehaviour
{
    public static UIPopulationListInfo Instance;

    public CountyPopulation countyPopulation;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ageText;
    public TextMeshProUGUI sexText;
    public TextMeshProUGUI currentActivityText;
    public TextMeshProUGUI nextActivityText;
    

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {

        // Fill in the Game Object's text fields from the list.
        
        nameText.text =
            countyList[i].firstName + " " + countyList[i].lastName;
        populationListClones[i].GetComponent<UIHorizontalPopulationListText>().ageText.text =
            countyList[i].age.ToString();
        if (countyList[i].isMale == true)
        {
            populationListClones[i].GetComponent<UIHorizontalPopulationListText>().sexText.text = "Male";
        }
        else
        {
            populationListClones[i].GetComponent<UIHorizontalPopulationListText>().sexText.text = "Female";
        }
        // This makes it so the text box looks right.
        if (countyList[i].currentBuilding == null)
        {
            populationListClones[i].GetComponent<UIHorizontalPopulationListText>().currentActivityText.text =
                                $"{countyList[i].currentActivity}";
        }
        else
        {
            populationListClones[i].GetComponent<UIHorizontalPopulationListText>().currentActivityText.text =
            $"{countyList[i].currentActivity} \n {countyList[i].currentBuilding.name}";
        }
        // This makes it so the text box looks right.
        if (countyList[i].nextBuilding == null)
        {
            populationListClones[i].GetComponent<UIHorizontalPopulationListText>().nextActivityText.text =
            $"{countyList[i].nextActivity}";
        }
        else
        {
            populationListClones[i].GetComponent<UIHorizontalPopulationListText>().nextActivityText.text =
            $"{countyList[i].nextActivity} \n {countyList[i].nextBuilding.name}";
        }
        
    }

    public void PopulationListButtonClicked()
    {
        WorldMapLoad.Instance.populationDescriptionPanel.SetActive(true);
        WorldMapLoad.Instance.populationDescriptionPanelOpen = true;
        WorldMapLoad.Instance.currentlySelectedCountyPopulation = countyPopulation;
    }
}
