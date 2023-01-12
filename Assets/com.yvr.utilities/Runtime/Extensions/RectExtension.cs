using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectExtension
{
    public static Rect ToPixelRect(this Rect normalizedRect, int pixelWidth, int pixelHeight)
    {
        return new Rect(normalizedRect.x * pixelWidth, normalizedRect.y * pixelHeight,
                normalizedRect.width * pixelWidth, normalizedRect.height * pixelHeight);
    }
}
