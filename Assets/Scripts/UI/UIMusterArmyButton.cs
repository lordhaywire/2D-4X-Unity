using UnityEngine;
using UnityEngine.UI;

public class UIMusterArmyButton : MonoBehaviour
{
    public static UIMusterArmyButton Instance;
    public GameObject musterArmyButtonGameObject;

    private void Awake()
    {
        Instance = this;
    }
}
