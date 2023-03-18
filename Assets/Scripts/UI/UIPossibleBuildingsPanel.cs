using UnityEngine;

public class UIPossibleBuildingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject possibleBuildingsPrefab;
    [SerializeField] private GameObject possibleBuildingsParent;

    private GameObject possibleBuildingClone;

    private void OnEnable()
    {
        for (int i = 0; i < WorldMapLoad.instance.researchItemsTier1.Count; i++)
        {
            if (WorldMapLoad.instance.researchItemsTier1[i].isDone)
            {
                possibleBuildingClone = Instantiate(possibleBuildingsPrefab);
            }
            possibleBuildingClone.transform = possibleBuildingsParent.transform;
            
        }
            
    }
}
