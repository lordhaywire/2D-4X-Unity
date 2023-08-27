using UnityEngine;

public class UIBuildingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject buildingPrefab;

    public void PanelRefresher(GameObject buildingsParent, GameObject buildingsScrollView)
    {
        GameObject county = WorldMapLoad.Instance.CurrentlySelectedCounty;

        for (int i = 0; i < buildingsParent.transform.childCount; i++)
        {
            GameObject uIGameObject = Instantiate(buildingPrefab, buildingsScrollView.transform);
        }
        
    }
}
