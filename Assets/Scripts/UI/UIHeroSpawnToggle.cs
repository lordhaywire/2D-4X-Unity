using UnityEngine;
using UnityEngine.UI;

public class UIHeroSpawnToggle : MonoBehaviour
{
    public GameObject heroPrefab;
    public Toggle heroSpawnToggle;

    public void HeroSpawn()
    {
        heroSpawnToggle.isOn = true;
        var heroToken = Instantiate(heroPrefab, 
            WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].heroSpawnGameObject.transform.position, Quaternion.identity);
        heroToken.gameObject.transform.parent = HeroHierarchyList.Instance.gameObject.transform;
        heroToken.GetComponent<HeroMovement>().tokenLocation = WorldMapLoad.Instance.currentlySelectedCounty;

        //heroToken.name = 0.ToString();
    }
}
