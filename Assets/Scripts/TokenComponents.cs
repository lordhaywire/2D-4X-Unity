using TMPro;
using UnityEngine;

public class TokenComponents : MonoBehaviour
{
    public static TokenComponents Instance;

    public GameObject counterGameObject;
    public SpriteRenderer spriteRenderer;
    public TextMeshProUGUI counterText;
    public Canvas timerCanvas;
    public Canvas counterCanvas;
    public Canvas nameCanvas;

    private void Awake()
    {
        Instance = this;
    }
}
