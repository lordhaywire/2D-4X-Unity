using UnityEngine;

public class UICollapseBuildingsButton : MonoBehaviour
{
    public static UICollapseBuildingsButton instance;

    public GameObject collapseBuildingButton;

    private void Start()
    {
        instance = this;
    }
}
