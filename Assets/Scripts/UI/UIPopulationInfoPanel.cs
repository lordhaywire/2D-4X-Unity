using System.Collections.Generic;
using UnityEngine;

public class UIPopulationInfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject prefabHorizontalPopulationListText;
    [SerializeField] private GameObject parentPopulationListGroup;
    [SerializeField] private GameObject populationInfoPanel;

    private readonly List<GameObject> populationListClones = new();

    private void OnEnable()
    {
        TimeKeeper.instance.PauseandUnpause();

        if (WorldMapLoad.instance.countyPopulationDictionary[SelectCounty.currentlySelectedCounty] != null)
        {
            var countyList = WorldMapLoad.instance.countyPopulationDictionary[SelectCounty.currentlySelectedCounty];
            var factionList =
                WorldMapLoad.instance.factionHeroesDictionary[WorldMapLoad.instance.counties[SelectCounty.currentlySelectedCounty].faction.name];
            // This is for the leaders of each factionNameAndColors.
            for (int i = 0; i < factionList.Count; i++)
            {
                populationListClones.Add
                    (Instantiate(prefabHorizontalPopulationListText, parentPopulationListGroup.transform));
                populationListClones[i].GetComponent<UIHorizontalPopulationListText>().nameText.text =
                    factionList[i].firstName + " " + factionList[i].lastName;
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
                populationListClones[i].GetComponent<UIHorizontalPopulationListText>().activityText.text =
                    factionList[i].activity;
            }
            // This is for the normal population in the county.
            for (int i = populationListClones.Count; i < countyList.Count; i++)
            {
                populationListClones.Add
                    (Instantiate(prefabHorizontalPopulationListText, parentPopulationListGroup.transform));
                populationListClones[i].GetComponent<UIHorizontalPopulationListText>().nameText.text =
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
                populationListClones[i].GetComponent<UIHorizontalPopulationListText>().activityText.text =
                    countyList[i].activity;
            }
        }
        else
        {
            Debug.Log("The Currently Selected County is null, dipshit.");
        }
    }

    private void OnDisable()
    {
        TimeKeeper.instance.PauseandUnpause();
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
