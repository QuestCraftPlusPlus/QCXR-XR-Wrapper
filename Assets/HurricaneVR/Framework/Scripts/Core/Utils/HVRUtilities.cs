using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HurricaneVR.Framework.Core.Utils
{
    public static class HVRUtilities
    {
        public static Vector3 FindNearestPointOnLine(Vector3 origin, Vector3 end, Vector3 point)
        {
            //Get heading
            var heading = (end - origin);
            float magnitudeMax = heading.magnitude;
            heading.Normalize();

            //Do projection from the point but clamp it
            var lhs = point - origin;
            float dotP = Vector3.Dot(lhs, heading);
            dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
            return origin + heading * dotP;
        }

        public static Vector3 OrthogonalVector(this Vector3 v)
        {
            //////https://math.stackexchange.com/questions/137362/how-to-find-perpendicular-vector-to-another-vector
            v.Normalize();
            var x = v.x;
            var y = v.y;
            var z = v.z;
            var v1 = new Vector3(0f, z, -y);
            var v2 = new Vector3(-z, 0f, x);
            var v3 = new Vector3(-y, x, 0f);
            var largest = v1;
            if (v2.magnitude > largest.magnitude)
                largest = v2;
            if (v3.magnitude > largest.magnitude)
                largest = v3;
            return largest;
        }

        public static Vector3 AngularVelocity(this Quaternion current, Quaternion previous)
        {
            var deltaRotation = current * Quaternion.Inverse(previous);
            if (deltaRotation.w < 0)
            {
                deltaRotation.x = -deltaRotation.x;
                deltaRotation.y = -deltaRotation.y;
                deltaRotation.z = -deltaRotation.z;
                deltaRotation.w = -deltaRotation.w;
            }

            deltaRotation.ToAngleAxis(out var angle, out var axis);
            angle *= Mathf.Deg2Rad;
            return axis * (angle / Time.fixedDeltaTime);
        }

        public static List<Collider> GetColliders(this GameObject go, bool includedTriggers = false)
        {
            return go.GetComponentsInChildren<Collider>().Where(e => !e.isTrigger || includedTriggers).ToList();
        }

        public static IEnumerable<Collider> GetColliders(this Rigidbody rigidbody, bool includeTriggers = false)
        {
            return GetColliders(rigidbody, rigidbody.transform, includeTriggers);
        }


        private static IEnumerable<Collider> GetColliders(this Rigidbody rigidbody, Transform transform, bool includeTriggers = false)
        {
            var rb = transform.GetComponent<Rigidbody>();
            if (rb && rb != rigidbody)
                yield break;

            foreach (var c in transform.GetComponents<Collider>())
            {
                if (!c.enabled) continue;
                if (!c.isTrigger || (c.isTrigger && includeTriggers))
                    yield return c;
            }

            foreach (Transform child in transform)
            {
                foreach (var c in GetColliders(rigidbody, child))
                {
                    yield return c;
                }
            }
        }

        public static Bounds GetColliderBounds(this Rigidbody rb)
        {
            var bounds = new Bounds();
            var first = true;
            foreach (var collider in rb.GetColliders())
            {
                if (first)
                {
                    first = false;
                    bounds = collider.bounds;
                }
                else
                {
                    bounds.Encapsulate(collider.bounds);
                }
            }

            return bounds;
        }

        public static Bounds GetColliderBounds(this Transform transform)
        {
            var colliders = transform.GetComponents<Collider>();
            return colliders.GetColliderBounds();
        }

        public static Bounds GetColliderBounds(this GameObject go)
        {
            var colliders = go.GetComponents<Collider>();
            return colliders.GetColliderBounds();
        }

        public static Bounds GetColliderBounds(this List<Collider> colliders)
        {
            var bounds = new Bounds();
            for (var i = 0; i < colliders.Count; i++)
            {
                var collider = colliders[i];
                if (i == 0)
                {
                    bounds = collider.bounds;
                }
                else
                {
                    bounds.Encapsulate(collider.bounds);
                }
            }

            return bounds;
        }

        public static Bounds GetColliderBounds(this Collider[] colliders)
        {
            var bounds = new Bounds();
            var first = true;
            for (var i = 0; i < colliders.Length; i++)
            {
                var collider = colliders[i];

                if (!collider.enabled) continue;

                if (first)
                {
                    first = false;
                    bounds = collider.bounds;
                }
                else
                {
                    bounds.Encapsulate(collider.bounds);
                }
            }

            return bounds;
        }

        public static void DrawBounds(this Bounds bounds)
        {
            Vector3 v3Center = bounds.center;
            Vector3 v3Extents = bounds.extents;

            var v3FrontTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z); // Front top left corner
            var v3FrontTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z); // Front top right corner
            var v3FrontBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z); // Front bottom left corner
            var v3FrontBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z); // Front bottom right corner
            var v3BackTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z); // Back top left corner
            var v3BackTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z); // Back top right corner
            var v3BackBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z); // Back bottom left corner
            var v3BackBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z); // Back bottom right corner


            var color = Color.magenta;
            Debug.DrawLine(v3FrontTopLeft, v3FrontTopRight, color);
            Debug.DrawLine(v3FrontTopRight, v3FrontBottomRight, color);
            Debug.DrawLine(v3FrontBottomRight, v3FrontBottomLeft, color);
            Debug.DrawLine(v3FrontBottomLeft, v3FrontTopLeft, color);

            Debug.DrawLine(v3BackTopLeft, v3BackTopRight, color);
            Debug.DrawLine(v3BackTopRight, v3BackBottomRight, color);
            Debug.DrawLine(v3BackBottomRight, v3BackBottomLeft, color);
            Debug.DrawLine(v3BackBottomLeft, v3BackTopLeft, color);

            Debug.DrawLine(v3FrontTopLeft, v3BackTopLeft, color);
            Debug.DrawLine(v3FrontTopRight, v3BackTopRight, color);
            Debug.DrawLine(v3FrontBottomRight, v3BackBottomRight, color);
            Debug.DrawLine(v3FrontBottomLeft, v3BackBottomLeft, color);
        }

        public static string LogFormat(this Vector3 v)
        {
            return $"{v.x:f3},{v.y:f3},{v.z:f3}";
        }

        public static string LogFormatF0(this Vector3 v)
        {
            return $"{v.x:f0},{v.y:f0},{v.z:f0}";
        }

        public static float Remap(this float num, float lowVal, float highVal, float min, float max)
        {
            return min + (num - lowVal) * (max - min) / (highVal - lowVal);
        }

        public static T GetRandom<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
                return default;

            return array[Random.Range(0, array.Length)];
        }

        public static HVRAxis GetHVRAxis(this Vector3 v)
        {
            if (v == Vector3.right) return HVRAxis.X;
            if (v == -Vector3.right) return HVRAxis.NegX;
            if (v == Vector3.up) return HVRAxis.Y;
            if (v == -Vector3.up) return HVRAxis.NegY;
            if (v == Vector3.forward) return HVRAxis.Z;
            return HVRAxis.NegZ;
        }

        public static Vector3 GetVector(this HVRAxis axis)
        {
            if (axis == HVRAxis.X)
            {
                return Vector3.right;
            }

            if (axis == HVRAxis.Y)
            {
                return Vector3.up;
            }

            if (axis == HVRAxis.Z)
            {
                return Vector3.forward;
            }


            if (axis == HVRAxis.NegX)
            {
                return -Vector3.right;
            }

            if (axis == HVRAxis.NegY)
            {
                return -Vector3.up;
            }

            return -Vector3.forward;
        }

        public static T EnsureComponent<T>(this GameObject obj) where T : UnityEngine.Component
        {
            if (!obj.TryGetComponent(out T component))
            {
                component = obj.AddComponent<T>();
            }

            return component;
        }

        public static T EnsureComponent<T>(this Transform t) where T : UnityEngine.Component
        {
            return t.gameObject.EnsureComponent<T>();
        }

        public static Vector3 QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            var oneMinusT = 1f - t;
            return
                oneMinusT * oneMinusT * p0 +
                2f * oneMinusT * t * p1 +
                t * t * p2;
        }

        public static Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float u = 1f - t;
            float t2 = t * t;
            float u2 = u * u;
            float u3 = u2 * u;
            float t3 = t2 * t;

            Vector3 result =
                (u3) * p0 +
                (3f * u2 * t) * p1 +
                (3f * u * t2) * p2 +
                (t3) * p3;

            return result;
        }

        public static Vector3 GetBottom(this CapsuleCollider capsule)
        {
            return capsule.transform.TransformPoint(capsule.center) + Vector3.down * capsule.height * .5f;
        }

        public static Vector3 GetBottom(this CharacterController capsule)
        {
            return capsule.transform.TransformPoint(capsule.center) + Vector3.down * capsule.height * .5f;
        }

        public static void SetKinematic(this Rigidbody rb)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rb.isKinematic = true;
        }

        public static Vector3 ComputeClosestVector(Vector3 v, Vector3 v1, Vector3 v2, bool fixDirection = true)
        {
            var vNorm = v.normalized;

            var v1Dot = Vector3.Dot(vNorm, v1.normalized);
            var v2Dot = Vector3.Dot(vNorm, v2.normalized);

            if (Mathf.Abs(v1Dot) > Mathf.Abs(v2Dot))
            {
                if (v1Dot < 0 && fixDirection)
                {
                    return -v1;
                }

                return v1;
            }

            if (v2Dot < 0 && fixDirection)
            {
                return -v2;
            }

            return v2;
        }

        /// <summary>
        /// return the position and rotation of target relative to relativeTo
        /// </summary>
        public static void GetRelativesTo(this Transform target, Transform relativeTo, out Vector3 pos, out Quaternion rot)
        {
            pos = relativeTo.InverseTransformPoint(target.position);
            rot = Quaternion.Inverse(relativeTo.rotation) * target.rotation;
        }

        /// <summary>
        /// return the position and rotation of target relative to relativeTo
        /// </summary>
        public static void GetRelativesTo(this Component target, Transform relativeTo, out Vector3 pos, out Quaternion rot)
        {
            pos = Vector3.zero;
            rot = Quaternion.identity;
            if (!relativeTo || !target) return;

            pos = relativeTo.transform.InverseTransformPoint(target.transform.position);
            rot = Quaternion.Inverse(relativeTo.transform.rotation) * target.transform.rotation;
        }

        /// <summary>
        /// return the position and rotation of target relative to relativeTo
        /// </summary>
        public static void GetRelativesTo(this Component target, Component relativeTo, out Vector3 pos, out Quaternion rot)
        {
            pos = Vector3.zero;
            rot = Quaternion.identity;
            if (!relativeTo || !target) return;

            pos = relativeTo.transform.InverseTransformPoint(target.transform.position);
            rot = Quaternion.Inverse(relativeTo.transform.rotation) * target.transform.rotation;
        }

        /// <summary>
        /// returns the rotation of rotation relative to 'relativeTo'
        /// </summary>
        public static Quaternion GetRelativeRotation(this Component relativeTo, Quaternion rotation) => Quaternion.Inverse(relativeTo.transform.rotation) * rotation;

        /// <summary>
        /// Is any component NaN
        /// </summary>
        public static bool IsNaN(this Vector3 v)
        {
            return float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);
        }

        /// <summary>
        /// Is any component infinity
        /// </summary>
        public static bool IsInfinity(this Vector3 v)
        {
            return float.IsInfinity(v.x) || float.IsInfinity(v.y) || float.IsInfinity(v.z);
        }

        /// <summary>
        /// Is any component nan or infinity
        /// </summary>
        public static bool IsInvalid(this Vector3 v)
        {
            return float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z) || float.IsInfinity(v.x) || float.IsInfinity(v.y) || float.IsInfinity(v.z);
        }
        
        public static List<string> SplitClean(this string s, char separator)
        {
            return s.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim()).Where(e => !e.IsNullOrWhiteSpace()).ToList();
        }
        
        public static bool IsNullOrWhiteSpace(this string s) => string.IsNullOrWhiteSpace(s);
    }
}