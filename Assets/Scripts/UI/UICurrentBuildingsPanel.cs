using UnityEngine;

public class UICurrentBuildingsPanel : MonoBehaviour
{
    public static UICurrentBuildingsPanel instance;

    public GameObject currentBuildingsGroupGameObject;

    private void Awake()
    {
        instance = this;
    }
}
