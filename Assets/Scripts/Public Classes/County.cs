using UnityEngine;
public class County
{
    public int countyID; // This isn't used yet, and if it never is then we need to get rid of it.
    public bool isCapital;
    public GameObject countyCenterGameObject;
    public SpriteRenderer spriteRenderer;
    public SelectCounty selectCounty; // We may be able to remove this.
    public FactionNameAndColor faction;
    public string province;
    public int population;

    public County(int newCountyID, bool newIsCapital, GameObject newCountyCenterGameObject, SpriteRenderer newSpriteRendered,
        SelectCounty newSelectCounty, FactionNameAndColor newFaction, string newProvince, int newPopulation)
    {
        countyID = newCountyID;
        isCapital = newIsCapital;
        countyCenterGameObject = newCountyCenterGameObject;
        spriteRenderer = newSpriteRendered;
        selectCounty = newSelectCounty;
        faction = newFaction;
        province = newProvince;
        population = newPopulation;
    }
}
