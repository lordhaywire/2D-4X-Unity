using UnityEngine;

public class HeroStacking : MonoBehaviour
{
    public static HeroStacking Instance;

    public int heroIndexNumber;

    private void Awake()
    {
        Instance = this;
    }
    public void StackHeroes()
    {
        var heroSpawnStack = WorldMapLoad.Instance.heroStacking[WorldMapLoad.Instance.heroes[heroIndexNumber].location];

        if (heroSpawnStack.Count > 1)
        {
            heroSpawnStack.Insert(0, heroSpawnStack[^1]);
            Debug.Log("Hero Spawn Stack Count: " + heroSpawnStack.Count);
            heroSpawnStack.RemoveAt(heroSpawnStack.Count - 1);
            Debug.Log("Hero Spawn Stack Count: " + heroSpawnStack.Count);
        }


        for (int i = 0; i < heroSpawnStack.Count; i++)
        {
            var hero = WorldMapLoad.Instance.heroes[int.Parse(heroSpawnStack[i].gameObject.name)];
            var county = WorldMapLoad.Instance.counties[hero.location];

            Debug.Log("Hero Spawn Stack Game Object Name: " + heroSpawnStack[i].gameObject.name);
            //Debug.Log("Position: " + i + " " + heroSpawnStack[i].gameObject.transform.position.x);

            hero.gameObject.GetComponent<HeroSortOrders>().heroRenderer.sortingOrder = 100 - i;
            hero.gameObject.GetComponent<HeroSortOrders>().heroCanvasTimerRenderer.sortingOrder = 100 - i;
            hero.gameObject.GetComponent<HeroSortOrders>().heroStackCountRenderer.sortingOrder = 100 - i;
            hero.gameObject.GetComponent<HeroSortOrders>().heroNameTextRenderer.sortingOrder = 100 - i;

            hero.heroStackCount.heroCountText.text = heroSpawnStack.Count.ToString();

            // Reduce duplicate code.
            if (i == 0)
            {
                heroSpawnStack[i].gameObject.GetComponentInChildren<HeroName>().heroNameGameObject.SetActive(true);
                if(heroSpawnStack.Count > 1)
                {
                    heroSpawnStack[i].gameObject.GetComponent<HeroStackCountText>().heroCountGameObject.SetActive(true);
                }
                else
                {
                    heroSpawnStack[i].gameObject.GetComponent<HeroStackCountText>().heroCountGameObject.SetActive(false);
                }   
                heroSpawnStack[i].gameObject.transform.position = county.heroSpawnLocation.transform.position;

                if(hero.justMoved != true)
                {
                    hero.IsSelected = true;
                }
                else
                {
                    hero.justMoved = false;
                }
                
            }
            else if (i == 1)
            {
                heroSpawnStack[i].gameObject.GetComponentInChildren<HeroName>().heroNameGameObject.SetActive(false);
                heroSpawnStack[i].gameObject.GetComponent<HeroStackCountText>().heroCountGameObject.SetActive(false);
                heroSpawnStack[i].gameObject.transform.position 
                    = new Vector2(county.heroSpawnLocation.transform.position.x + 0.2f
                    , county.heroSpawnLocation.transform.position.y);
                hero.IsSelected = false;
            }
            else
            {
                heroSpawnStack[i].gameObject.GetComponentInChildren<HeroName>().heroNameGameObject.SetActive(false);
                heroSpawnStack[i].gameObject.GetComponent<HeroStackCountText>().heroCountGameObject.SetActive(false);

                heroSpawnStack[i].gameObject.transform.position
                    = county.heroSpawnLocation.transform.position;
                hero.IsSelected = false;
            }
        }
    }
}
