using UnityEngine;

public class UICollapseBuildingsButton : MonoBehaviour
{
    public static UICollapseBuildingsButton Instance;

    public GameObject collapseBuildingButton;

    private void Start()
    {
        Instance = this;
    }
}
