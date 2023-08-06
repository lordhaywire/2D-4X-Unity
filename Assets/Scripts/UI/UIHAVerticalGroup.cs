using UnityEngine;

public class UIHAVerticalGroup : MonoBehaviour
{
    private void OnEnable()
    {
        if(WorldMapLoad.Instance.countyHeroes[ WorldMapLoad.Instance.CurrentlySelectedCounty.name].Count > 0)
        {
            WorldMapLoad.Instance.heroScrollView.SetActive(true);
        }
        else
        {
            WorldMapLoad.Instance.heroScrollView.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
