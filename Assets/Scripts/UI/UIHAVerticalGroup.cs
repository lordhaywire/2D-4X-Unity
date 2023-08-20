using UnityEngine;

public class UIHAVerticalGroup : MonoBehaviour
{
    private void OnEnable()
    {
        if (WorldMapLoad.Instance.countyHeroes[WorldMapLoad.Instance.CurrentlySelectedCounty.name].Count > 0
            && UIPlayerUI.Instance.countyInfoPanel.activeSelf == true || UIPlayerUI.Instance.heroInfoPanel.activeSelf == true)
        {
            UIPlayerUI.Instance.heroScrollView.SetActive(true);
        }
        else
        {
            UIPlayerUI.Instance.heroScrollView.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
