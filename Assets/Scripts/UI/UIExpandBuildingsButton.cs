using UnityEngine;

public class UIExpandBuildingsButton : MonoBehaviour
{
    // This should probably be called UIExpandBuildingsPanelButton or some shit.
    
    public static UIExpandBuildingsButton instance;
    public GameObject expandBuildingButtonGameObject;

    private void Awake()
    {
        instance = this;
    }
}
