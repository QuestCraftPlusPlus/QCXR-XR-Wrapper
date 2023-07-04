using System.Linq;
using UnityEngine;

namespace YVR.Utilities
{
    public static class ColorArrayExtensions
    {
        public static bool IsEqualTo(this Color[] thisArray, Color[] otherArray)
        {
            bool isEqual = thisArray.Count() == otherArray.Count();

            for (int i = 0; i != thisArray.Count() && isEqual; ++i)
                isEqual &= thisArray[i] == otherArray[i];

            return isEqual;
        }
    }
}
