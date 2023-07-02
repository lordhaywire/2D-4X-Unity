using System.Collections;
using TMPro;
using UnityEngine;

public class TokenInfo : MonoBehaviour
{
    public static TokenInfo Instance;

    public Hero hero;
    public SpawnedTokenList spawnedTokenList;

    public TextMeshProUGUI nameText;
    public GameObject nameGameObject;

    public GameObject counterGameObject;
    public SpriteRenderer spriteRenderer;
    public Canvas timerCanvas;
    public Canvas counterCanvas;
    public Canvas nameCanvas;

    public TextMeshProUGUI counterText;

    private int orderInLayer;
    public int OrderInLayer
    {
        get { return orderInLayer; }

        set
        {
            orderInLayer = value;
            spriteRenderer.sortingOrder = value;
            timerCanvas.sortingOrder = value;
            counterCanvas.sortingOrder = value;
            nameCanvas.sortingOrder = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        nameGameObject.SetActive(true);

        StartCoroutine(AfterWaitForOneFrame());
    }

    IEnumerator AfterWaitForOneFrame()
    {
        yield return null;

        nameText.text = $"{hero.countyPopulation.firstName} {hero.countyPopulation.lastName}";
    }
}
