using UnityEngine;

public class UIExpandBuildingsPanel : MonoBehaviour
{
    public static UIExpandBuildingsPanel instance;
    public GameObject expandBuildingButtonGameObject;

    private void Awake()
    {
        instance = this;
    }
}
