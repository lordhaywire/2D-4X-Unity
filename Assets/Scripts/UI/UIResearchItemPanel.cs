using UnityEngine;

public class UIResearchItemPanel : MonoBehaviour
{
    public static UIResearchItemPanel Instance;

    public GameObject uIResearchItemPanelGameObject;
    private void Awake()
    {
        Instance = this;
    }
}
