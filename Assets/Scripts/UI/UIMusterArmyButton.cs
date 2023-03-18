using UnityEngine;
using UnityEngine.UI;

public class UIMusterArmyButton : MonoBehaviour
{
    public static UIMusterArmyButton instance;
    public GameObject musterArmyButtonGameObject;

    private void Awake()
    {
        instance = this;
    }
}
