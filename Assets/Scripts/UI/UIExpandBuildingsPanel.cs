using UnityEngine;

public class UIExpandBuildingsPanel : MonoBehaviour
{
    // This should probably be called UIExpandBuildingsPanelButton or some shit.
    
    public static UIExpandBuildingsPanel instance;
    public GameObject expandBuildingButtonGameObject;

    private void Awake()
    {
        instance = this;
    }
}
