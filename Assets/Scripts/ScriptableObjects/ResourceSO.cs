using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Create New Resource", order = 1)]
public class ResourceSO : ScriptableObject
{
    public string resourceName;
    public string descriptions;
}
