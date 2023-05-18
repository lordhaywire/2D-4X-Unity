using UnityEngine;

public class HeroHierarchyList : MonoBehaviour
{
    public static HeroHierarchyList Instance;

    private void Awake()
    {
        Instance = this;
    }
}
