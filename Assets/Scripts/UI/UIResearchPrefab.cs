using UnityEngine;

public class UIResearchPrefab : MonoBehaviour
{
    public void OpenResearchItemPanelButton()
    {
        UIResearchItemPanel.instance.uIResearchItemPanelGameObject.SetActive(true);
    }
}
