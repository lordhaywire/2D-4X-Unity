using System;
using UnityEngine;

public class Banker : MonoBehaviour
{
    public static Banker Instance;


    public void RemoveCostOfHeroButton()
    {
        if (WorldMapLoad.Instance.costOfHero > WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].Influence)
        {
            UIPopulationDescriptionPanel.Instance.notEnoughResourcesPanel.SetActive(true);
        }
        else
        {
            WorldMapLoad.Instance.factions[WorldMapLoad.Instance.playerFactionID].Influence -= WorldMapLoad.Instance.costOfHero;
            WorldMapLoad.Instance.countyPopulationDictionary[WorldMapLoad.Instance.currentlySelectedCounty][WorldMapLoad.Instance.currentlySelectedPopulation].isHero
                = true;
            UIHeroScrollView.Instance.DestoryPanel();
            UIHeroScrollView.Instance.RefreshPanel();
        }
    }
}
