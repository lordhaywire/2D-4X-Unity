using TMPro;
using UnityEngine;

public class UIRecruitHeroConfirm : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI areYouSureHeroText;
    private CountyPopulation countyPopulation;
    private void OnEnable()
    {
        countyPopulation = WorldMapLoad.Instance.currentlySelectedCountyPopulation;

        areYouSureHeroText.text = $"{AllText.UIText.AREYOUSUREHERO} {countyPopulation.firstName} {countyPopulation.lastName}";
    }

    public void ConfirmHeroRecruitment()
    {
        Banker.Instance.RemoveCostOfHero();

        WorldMapLoad.Instance.currentlySelectedCountyPopulation.isHero = true;

        UIHeroScrollViewRefresher.Instance.RefreshPanel();
        WorldMapLoad.Instance.heroesAndArmiesVerticalGroup.SetActive(true);
    }
}
