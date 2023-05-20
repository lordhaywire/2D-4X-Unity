using UnityEngine;
using UnityEngine.UI;

public class UIHeroSpawnToggle : MonoBehaviour
{
    public GameObject heroPrefab;
    public Toggle heroSpawnToggle;

    public void HeroSpawn()
    {
        heroSpawnToggle.isOn = true;

        var heroListIndex = gameObject.transform.parent.name;
        Debug.Log("Hero Index List: " + heroListIndex);
        var spawnedHeroToken = Instantiate(heroPrefab,
            WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].heroSpawnLocation.transform.position, 
            Quaternion.identity);
        //WorldMapLoad.Instance.heroes[int.Parse(heroListIndex)]

        //





        
        //spawnedHeroToken.gameObject.transform.parent = HeroHierarchyList.Instance.gameObject.transform;
        //
        //spawnedHeroToken.GetComponent<HeroMovement>().tokenLocation = WorldMapLoad.Instance.currentlySelectedCounty;

        //spawnedHeroToken.name = 0.ToString();
    }
}
