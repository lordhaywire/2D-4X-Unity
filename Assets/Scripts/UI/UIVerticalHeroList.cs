using TMPro;
using UnityEngine;

public class UIVerticalHeroList : MonoBehaviour
{
    public static UIVerticalHeroList Instance;

    //private GameObject leaderButtonGameObject; 
    public TextMeshProUGUI leaderButtonText;

    private void Awake()
    {
        Instance = this;
        //leaderButtonGameObject = transform.gameObject;
        leaderButtonText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
