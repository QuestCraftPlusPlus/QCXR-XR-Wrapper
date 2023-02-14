using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    public static Vector2 GetMax(this Vector2 a, Vector2 b)
    {
        float absA = a.sqrMagnitude;
        float absB = b.sqrMagnitude;
        if (absA >= absB) return a;
        else return b;
    }

    public static Vector2 Scale(this Vector2 origin, float widthScale, float heightScale)
    {
        Vector2 result = origin;
        result.x *= widthScale;
        result.y *= heightScale;
        return result;
    }

    public static Vector2 Scale(this Vector2 origin, float scale)
    {
        Vector2 result = origin;
        result.x *= scale;
        result.y *= scale;
        return result;
    }

    public static Vector4 ToVector4(this Rect origin)
    {
        return new Vector4(origin.x, origin.y, origin.width, origin.height);
    }

}
