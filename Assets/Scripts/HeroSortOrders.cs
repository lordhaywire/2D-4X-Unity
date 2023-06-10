using UnityEngine;

public class HeroSortOrders : MonoBehaviour
{
    public static HeroSortOrders Instance;

    public SpriteRenderer heroRenderer;
    public Canvas heroCanvasTimerRenderer;
    public Canvas heroStackCountRenderer;
    public Canvas heroNameTextRenderer;
    private void Awake()
    {
        Instance = this;
    }
}
