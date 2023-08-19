using UnityEngine;

public class UIResearch : MonoBehaviour
{
    public static UIResearch Instance;

    public GameObject researchTitleAndDescriptionPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {    
        TimeKeeper.Instance.PauseTime();
    }

    private void OnDisable()
    {
        TimeKeeper.Instance.UnpauseTime();  
    }
}
