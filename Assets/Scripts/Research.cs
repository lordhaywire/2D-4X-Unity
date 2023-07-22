using UnityEngine;

public class Research : MonoBehaviour
{
    public static Research Instance;

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
