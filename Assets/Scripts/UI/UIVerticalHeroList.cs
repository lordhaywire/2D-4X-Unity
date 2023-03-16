using TMPro;
using UnityEngine;

public class UIVerticalHeroList : MonoBehaviour
{
    //private GameObject leaderButtonGameObject; // This will be changed to an array when we are instantiating the buttons.
    public static TextMeshProUGUI leaderButtonText; // This will be changed to an array when we are instantiating the buttons.

    private void Awake()
    {
        //leaderButtonGameObject = transform.GetChild(0).uIResearchItemPanelGameObject;
        leaderButtonText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
