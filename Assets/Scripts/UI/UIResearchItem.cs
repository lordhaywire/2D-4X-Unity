using UnityEngine;

public class UIResearchItem : MonoBehaviour
{
    public void UIResearchItemButton()
    {
        ResearchTier1.instance.researchTitleAndDescriptionPanel.SetActive(true);
    }
}
