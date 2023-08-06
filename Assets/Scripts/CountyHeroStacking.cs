using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class CountyHeroStacking : MonoBehaviour
{

    public ListWithNotify<GameObject> spawnedTokenList = new(); // This is not a normal list.

    private void Awake()
    {
        spawnedTokenList.notifyListeners += ListChanged;
    }
    void ListChanged()
    {
        //Debug.Log("Something got added to the list.");
        StackTokens();
    }


    public class ListWithNotify<T> where T : class
    {

        readonly List<T> list = new();
        public Action notifyListeners;

        // This makes this function like a normal list.
        public T this[int i]
        {
            get
            {
                if(list.Count != 0)
                {
                    return list[i];
                }
                else
                {
                    Debug.Log("Default: " + default(T));
                    return null;
                }
            }
            set { list[i] = value; }
        }

        // These make the make the normal list actions work correctly. If we need to do other things to the list we need to add them here.
        public int Count() // This is nor a method so when using it, it will need ().
        {
            return list.Count();
        }
        public void Add(T item)
        {
            list.Add(item);
            notifyListeners?.Invoke();
        }

        public void Insert(int i, T item)
        {
            list.Insert(i, item);
            notifyListeners?.Invoke();
        }

        public void Remove(T item)
        {
            list.Remove(item);
            notifyListeners?.Invoke();
        }

        public void RemoveAt(int i)
        {
            list.RemoveAt(i);
            notifyListeners?.Invoke();
        }

    }

    public void StackTokens()
    {
        if (spawnedTokenList.Count() > 1)
        {
            spawnedTokenList[0].GetComponent<TokenInfo>().counterGameObject.SetActive(true);

            for (int i = 0; i < spawnedTokenList.Count(); i++)
            {
                TokenInfo tokenInfo = spawnedTokenList[i].GetComponent<TokenInfo>();
                GameObject tokenLocation = tokenInfo.countyPopulation.location;

                // Change each token's order to be lower then the one on "top" of it.
                tokenInfo.OrderInLayer = 100 - i;
                tokenInfo.counterText.text = spawnedTokenList.Count().ToString();

                if (i == 0)
                {
                    spawnedTokenList[i].GetComponentInChildren<TokenInfo>().nameGameObject.SetActive(true);

                    spawnedTokenList[i].transform.position = tokenLocation.GetComponent<CountyInfo>().tokenSpawn.transform.position;

                    if (WorldMapLoad.Instance.CurrentlySelectedToken.GetComponent<TokenMovement>().Move == false)
                    {
                        WorldMapLoad.Instance.CurrentlySelectedToken = spawnedTokenList[i];
                    }

                }
                else
                {
                    spawnedTokenList[i].GetComponentInChildren<TokenInfo>().nameGameObject.SetActive(false);
                    spawnedTokenList[i].GetComponent<TokenInfo>().counterGameObject.SetActive(false);
                    spawnedTokenList[i].transform.position
                        = new Vector2(tokenLocation.GetComponent<CountyInfo>().tokenSpawn.transform.position.x + (i * 0.1f)
                        , tokenLocation.GetComponent<CountyInfo>().tokenSpawn.transform.position.y);
                }
            }
        }
    }
}
