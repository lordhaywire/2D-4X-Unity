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
                if (tokenInfo.countyPopulation.destination.GetComponent<CountyHeroStacking>().spawnedTokenList.Count > 1)
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

        if (returnHome == true)
        {
            MoveTokenHome();
        }
    }
    private void MoveToken()
    {
        float step = Globals.Instance.tokenSpeed * Time.fixedDeltaTime;
        Transform destination = tokenInfo.countyPopulation.destination.GetComponent<CountyInfo>().tokenSpawn.transform;


        transform.position = Vector2.MoveTowards(transform.position, destination.position, step);
        if (transform.position == destination.position)
        {
            Move = false;
            tokenInfo.countyPopulation.location = tokenInfo.countyPopulation.destination;
            tokenInfo.countyPopulation.destination.GetComponent<CountyHeroStacking>().spawnedTokenList.Add(gameObject);
            WorldMapLoad.Instance.countyPopulationDictionary[tokenInfo.countyPopulation.destination.name]
                .Add(gameObject.GetComponent<TokenInfo>().countyPopulation);
            WorldMapLoad.Instance.countyHeroes[tokenInfo.countyPopulation.destination.name]
                .Add(gameObject.GetComponent<TokenInfo>().countyPopulation);
            tokenInfo.countyPopulation.destination = null;
            Debug.Log(gameObject.name + " has reached " + destination.name);
        }
    }

    private void MoveTokenHome()
    {
        float step = Globals.Instance.tokenSpeed * Time.fixedDeltaTime;
        Transform homeLocation = tokenInfo.countyPopulation.location.GetComponent<CountyInfo>().tokenSpawn.transform;

        transform.position = Vector2.MoveTowards(transform.position, homeLocation.position, step);
        if (transform.position == homeLocation.position)
        {
            returnHome = false;
            tokenInfo.countyPopulation.destination = null;
            tokenInfo.countyPopulation.location.GetComponent<CountyHeroStacking>().spawnedTokenList.Add(gameObject);
            WorldMapLoad.Instance.countyPopulationDictionary[tokenInfo.countyPopulation.destination.name]
                .Add(gameObject.GetComponent<TokenInfo>().countyPopulation);
            WorldMapLoad.Instance.countyHeroes[tokenInfo.countyPopulation.destination.name]
                .Add(gameObject.GetComponent<TokenInfo>().countyPopulation);
            Debug.Log(gameObject.name + " has returned home.");
        }

    }
}
