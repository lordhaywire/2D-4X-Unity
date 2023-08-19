using UnityEngine;

public class UIPlayerUI : MonoBehaviour
{
    public static UIPlayerUI Instance { get; private set; }

    public GameObject populationListPanel;

    private void Awake()
    {
        Instance = this;
    }
}
