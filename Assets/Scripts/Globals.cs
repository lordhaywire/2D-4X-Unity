using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals Instance { get; private set; }

    public bool startPaused;
    public float tokenSpeed;
    private void Awake()
    {
        Instance = this;
    }
}
