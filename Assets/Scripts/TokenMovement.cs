using UnityEngine;

public class TokenMovement : MonoBehaviour
{
    private TokenInfo tokenInfo;
    public bool move;
    public bool returnHome;

    private void Awake()
    {
        tokenInfo = GetComponent<TokenInfo>();
    }
    private void FixedUpdate()
    {
        if (move == true)
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
        Transform destination = tokenInfo.countyPopulation.destination.transform;
        
        transform.position = Vector2.MoveTowards(transform.position, destination.position, step);
        if(transform.position == destination.position)
        {
            move = false;
            tokenInfo.countyPopulation.location = tokenInfo.countyPopulation.destination;
            tokenInfo.countyPopulation.destination = null;
            Debug.Log("Token has reached its destination.");
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
            Debug.Log("Token has returned home.");
        }

    }
}
