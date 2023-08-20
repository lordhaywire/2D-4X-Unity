using UnityEngine;

public class UIPlayerUI : MonoBehaviour
{
    public static UIPlayerUI Instance { get; private set; }

    public GameObject populationListPanel;

    public GameObject countyInfoPanel;
    public GameObject heroInfoPanel;
    public GameObject armyInfoPanel; // This exists but is isn't in use.  It is also outdated.
    public GameObject heroScrollView;
    public GameObject armyScrollView;

    private void Awake()
    {
        Instance = this;
    }

    public void CloseInfoPanelButton()
    {
        countyInfoPanel.SetActive(false);
        heroInfoPanel.SetActive(false);

        heroScrollView.SetActive(false);
        armyScrollView.SetActive(false);
    }
}
