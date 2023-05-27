using UnityEngine;

public class HeroTokenSprites : MonoBehaviour
{
    public static HeroTokenSprites Instance;

    public Sprite heroSelectedSprite;
    public Sprite heroUnselectedSprite;

    private void Awake()
    {
        Instance = this;
    }
}

