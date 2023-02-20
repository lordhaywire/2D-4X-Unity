using System.Collections.Generic;
using UnityEngine;

public class UIPopulationInfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject prefabHorizontalPopulationListText;
    [SerializeField] private GameObject parentPopulationListGroup;

    private readonly List<GameObject> populationListClones = new();

    private void OnEnable()
    {
        if (WorldMapLoad.countyPopulationDictionary[SelectCounty.currentlySelectedCounty] != null)
        {
            var county = WorldMapLoad.countyPopulationDictionary[SelectCounty.currentlySelectedCounty];
            var factionList =
                WorldMapLoad.factionHeroesDictionary[WorldMapLoad.counties[SelectCounty.currentlySelectedCounty].factionName];
            for (int i = 0; i < factionList.Count; i++)
            {
                populationListClones.Add
                    (Instantiate(prefabHorizontalPopulationListText, parentPopulationListGroup.transform));
                populationListClones[i].GetComponent<UIHorizontalPopulationListText>().nameText.text =
                    factionList[i].firstName + " " + factionList[i].lastName;
                Debug.Log("Faction List Name: " + factionList[i].firstName + " " + factionList[i].lastName);
                populationListClones[i].GetComponent<UIHorizontalPopulationListText>().ageText.text =
                    factionList[i].age.ToString();
                if (factionList[i].isMale == true)
                {
                    populationListClones[i].GetComponent<UIHorizontalPopulationListText>().sexText.text = "Male";
                }
                else
                {
                    populationListClones[i].GetComponent<UIHorizontalPopulationListText>().sexText.text = "Female";
                }
            }
            for (int i = populationListClones.Count; i < county.Count; i++)
            {
                populationListClones.Add
                    (Instantiate(prefabHorizontalPopulationListText, parentPopulationListGroup.transform));
                populationListClones[i].GetComponent<UIHorizontalPopulationListText>().nameText.text =
                    county[i].firstName + " " + county[i].lastName;
                populationListClones[i].GetComponent<UIHorizontalPopulationListText>().ageText.text =
                    county[i].age.ToString();
                if (county[i].isMale == true)
                {
                    populationListClones[i].GetComponent<UIHorizontalPopulationListText>().sexText.text = "Male";
                }
                else
                {
                    populationListClones[i].GetComponent<UIHorizontalPopulationListText>().sexText.text = "Female";
                }
            }
        }
        else
        {
            Debug.Log("The Currently Selected County is null, dipshit.");
        }
    }

    public void CloseButton()
    {
        for (int i = 0; i < populationListClones.Count; i++)
        {
            Destroy(populationListClones[i]);
        }
        populationListClones.Clear();
    }
}
