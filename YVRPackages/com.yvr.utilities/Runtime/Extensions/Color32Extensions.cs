using System.Linq;
using UnityEngine;

public static class Color32Extensions
{
    public static bool IsEqualTo(this Color32[] thisArray, Color32[] otherArray)
    {
        bool isEqual = thisArray.Count() == otherArray.Count();

        for (int i = 0; i != thisArray.Count() && isEqual; ++i)
            isEqual &= thisArray[i].IsEqual(otherArray[i]);

        return isEqual;
    }

    public static bool IsEqual(this Color32 thisColor, Color32 targetColor)
    {
        return thisColor.r == targetColor.r && thisColor.g == targetColor.g && thisColor.b == targetColor.b && thisColor.a == targetColor.a;
    }
}
