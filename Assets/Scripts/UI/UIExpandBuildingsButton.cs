using UnityEngine;

public class UIExpandBuildingsButton : MonoBehaviour
{
    // This should probably be called UIExpandBuildingsPanelButton or some shit.
    
    public static UIExpandBuildingsButton Instance;
    public GameObject expandBuildingButtonGameObject;

    private void Awake()
    {
        Instance = this;
    }
}
