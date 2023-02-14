using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExternsion
{
    public static bool RayIntersects(this RectTransform target, Ray ray, out Vector3 hitWorldPos)
    {
        Vector3[] corners = new Vector3[4];
        target.GetWorldCorners(corners); // botton left -> top Left -> top right -> bottom right
        Plane plane = new Plane(corners[0], corners[1], corners[2]); // Using three points to define a infinite plane

        bool hit = plane.Raycast(ray, out float enter); // whether the ray hit the infinite plane

        if (hit)
        {
            Vector3 intersectionPoint = ray.GetPoint(enter);
            Vector3 rectPlaneBottomEdge = corners[3] - corners[0];
            Vector3 rectPlaneLeftEdge = corners[1] - corners[0];
            Vector3 interactionEdge = intersectionPoint - corners[0];
            float bottomDot = Vector3.Dot(interactionEdge, rectPlaneBottomEdge);
            float leftDot = Vector3.Dot(interactionEdge, rectPlaneLeftEdge);

            bool withInRectPlane = bottomDot > 0 && // The Intersection point is on the up side of the bottom edge
                                   leftDot > 0 && // The Intersection point is on the right side of left edge
                                   bottomDot < rectPlaneBottomEdge.sqrMagnitude && // The distance of the projection of intersection point on the bottom edge is shorter than the bottom edge itself
                                   leftDot < rectPlaneLeftEdge.sqrMagnitude; // The distance of the projection of intersection point on the left edge is shorter than the left edge itself

            if (withInRectPlane)
            {
                hitWorldPos = intersectionPoint;
                return true;
            }
        }

        hitWorldPos = Vector3.one;
        return false;
    }
}
