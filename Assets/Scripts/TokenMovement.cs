using UnityEngine;

public class TokenMovement : MonoBehaviour
{
    private TokenInfo tokenInfo;
    [SerializeField] private GameObject stackCounterGameObject;
    public bool returnHome;
    private bool move;

    public bool Move
    {
        get { return move; }
        set
        {
            move = value;
            if (move == true)
            {
                stackCounterGameObject.SetActive(false);
                tokenInfo.countyPopulation.location.GetComponent<CountyHeroStacking>().spawnedTokenList.Remove(gameObject);
                WorldMapLoad.Instance.countyHeroes[tokenInfo.countyPopulation.location.name]
                .Remove(gameObject.GetComponent<TokenInfo>().countyPopulation);
                WorldMapLoad.Instance.countyPopulationDictionary[tokenInfo.countyPopulation.location.name]
                .Remove(gameObject.GetComponent<TokenInfo>().countyPopulation);

            }
            else
            {
                if (tokenInfo.countyPopulation.destination.GetComponent<CountyHeroStacking>().spawnedTokenList.Count() > 1)
                {
                    stackCounterGameObject.SetActive(true);
                }

            }
        }
    }


    private void Awake()
    {
        tokenInfo = GetComponent<TokenInfo>();
    }
    private void FixedUpdate()
    {
        if (Move == true)
        {
            MoveToken();
        }
    }
    private void MoveToken()
    {
        float step = Globals.Instance.tokenSpeed * Time.fixedDeltaTime;
        Transform destination = tokenInfo.countyPopulation.destination.GetComponent<CountyInfo>().tokenSpawn.transform;
        WorldMapLoad.Instance.heroesAndArmiesVerticalGroup.SetActive(false);

        transform.position = Vector2.MoveTowards(transform.position, destination.position, step);
        if (transform.position == destination.position)
        {
            tokenInfo.countyPopulation.location = tokenInfo.countyPopulation.destination;
            tokenInfo.countyPopulation.destination.GetComponent<CountyHeroStacking>().spawnedTokenList.Insert(0,gameObject);
            WorldMapLoad.Instance.countyPopulationDictionary[tokenInfo.countyPopulation.destination.name]
                .Add(gameObject.GetComponent<TokenInfo>().countyPopulation);
            WorldMapLoad.Instance.countyHeroes[tokenInfo.countyPopulation.destination.name]
                .Add(gameObject.GetComponent<TokenInfo>().countyPopulation);
            Move = false;

            tokenInfo.countyPopulation.destination = null;
            //Debug.Log(gameObject.name + " has reached " + destination.name);
        }
    }
}
