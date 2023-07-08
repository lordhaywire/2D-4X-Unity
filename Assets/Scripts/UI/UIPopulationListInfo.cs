using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIPopulationListInfo : MonoBehaviour
{
    public static UIPopulationListInfo Instance;// { get; private set; }

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
        StartCoroutine(UpdateText());  
    }

    IEnumerator UpdateText()
    {
        yield return null;

        // Fill in the Game Object's text fields from the list.
        nameText.text = countyPopulation.firstName + " " + countyPopulation.lastName;

        ageText.text = countyPopulation.age.ToString();

        if (countyPopulation.isMale == true)
        {
            sexText.text = "Male";
        }
        else
        {
            sexText.text = "Female";
        }
        // This makes it so the text box looks right. What the fuck is this?
        if (countyPopulation.currentBuilding == null)
        {
            currentActivityText.text = $"{countyPopulation.currentActivity}";
        }
        else
        {
            currentActivityText.text =
            $"{countyPopulation.currentActivity} \n {countyPopulation.currentBuilding.name}";
        }
        // This makes it so the text box looks right. What the fuck is this?
        if (countyPopulation.nextBuilding == null)
        {
            nextActivityText.text = $"{countyPopulation.nextActivity}";
        }
        else
        {
            nextActivityText.text =
            $"{countyPopulation.nextActivity} \n {countyPopulation.nextBuilding.name}";
        }

    }

    public void PopulationListButtonClicked()
    {
        WorldMapLoad.Instance.populationDescriptionPanel.SetActive(true);
        WorldMapLoad.Instance.populationDescriptionPanelOpen = true;
        WorldMapLoad.Instance.currentlySelectedCountyPopulation = countyPopulation;
    }


}
