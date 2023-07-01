using System.Collections;
using TMPro;
using UnityEngine;

public class HeroName : MonoBehaviour
{

    public GameObject heroNameGameObject;
    [SerializeField] private TextMeshProUGUI heroNameText;

    private void OnEnable()
    {

        heroNameGameObject.SetActive(true);

        StartCoroutine(AfterWaitForOneFrame());
    }

    IEnumerator AfterWaitForOneFrame()
    {
        yield return null;

        heroNameText.text = WorldMapLoad.Instance.heroes[int.Parse(WorldMapLoad.Instance.CurrentlySelectedHero.name)].name;
    }
}
