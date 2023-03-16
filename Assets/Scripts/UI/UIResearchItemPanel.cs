using UnityEngine;

public class UIResearchItemPanel : MonoBehaviour
{
    public static UIResearchItemPanel instance;

    public GameObject uIResearchItemPanelGameObject;
    private void Awake()
    {
        instance = this;
    }
}
