using System.Collections.Generic;
using UnityEngine;

public class CountyListCreator : MonoBehaviour
{
    public static CountyListCreator instance;

    public List<CountyList> countiesList = new();
    private void Awake()
    {
        instance = this;
        for (int i = 0; i < transform.childCount; i++)
        {
            countiesList.Add(new CountyList(transform.GetChild(i).name, transform.GetChild(i).gameObject));
        }
    }
}
