using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PackageVersionObject : ScriptableObject
{
    public List<PackageInfo> packages;
}
