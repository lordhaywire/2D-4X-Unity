using TMPro;
using UnityEngine;

public class UIResearchTitleDescriptionPanel : MonoBehaviour
{
    public static UIResearchTitleDescriptionPanel instance;

    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI prerequisite;

    private void Awake()
    {
        instance = this;
    }
}
