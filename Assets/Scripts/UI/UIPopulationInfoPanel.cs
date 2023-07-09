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

        if (WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.CurrentlySelectedCounty.name] != null)
        {
            var countyList = WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.CurrentlySelectedCounty.name];

            for (int i = 0; i < countyList.Count; i++)
            {
                GameObject population = Instantiate(prefabHorizontalPopulationListText, parentPopulationListGroup.transform);
                //Debug.Log("UI Population Info Panel: " + countyList[i].firstName + " " + countyList[i].lastName);
                population.GetComponent<UIPopulationListInfo>().countyPopulation = countyList[i];
                populationListClones.Add(population); // We need to kill all the kids instead of making a pointless list.


                // Rename all the Game Objects in the list to the index for later clickablity.
                // We can probably ditch this shit too.
                populationListClones[i].name = i.ToString();
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
