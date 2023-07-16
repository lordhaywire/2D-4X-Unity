using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals Instance { get; private set; }

    public float tokenSpeed;
    private void Awake()
    {
        Instance = this;
    }
}
