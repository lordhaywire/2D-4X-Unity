using System.Collections.Generic;
using UnityEngine;

public class UIPopulationInfoPanel : MonoBehaviour
{
    public static UIPopulationInfoPanel Instance;

    [SerializeField] private GameObject prefabHorizontalPopulationListText;
    [SerializeField] private GameObject parentPopulationListGroup;

    private readonly List<GameObject> populationListClones = new();

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        TimeKeeper.Instance.PauseTime();

        WorldMapLoad.Instance.populationInfoPanelOpen = true;

        if (WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.currentlySelectedCounty] != null)
        {
            var countyList = WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.currentlySelectedCounty];

            for (int i = 0; i < countyList.Count; i++)
            {
                //int listIndex = i + factionList.Count;
                //Debug.Log("List Index: " + listIndex);
                //Debug.Log("County List Length: " + countyList.Count);
                populationListClones.Add
                    (Instantiate(prefabHorizontalPopulationListText, parentPopulationListGroup.transform));

                // Rename all the Game Objects in the list to the index for later clickablity.
                populationListClones[i].name = i.ToString();

                // Fill in the Game Object's text fields from the list.
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
        }
        else
        {
            Debug.Log("The Currently Selected County is null, dipshit.");
        }
    }
    private void OnDisable()
    {
        TimeKeeper.Instance.UnpauseTime();

        DestroyPopulation();
    }

    private void DestroyPopulation()
    {
        for (int i = 0; i < populationListClones.Count; i++)
        {
            Destroy(populationListClones[i]);
        }
        populationListClones.Clear();
    }
}
