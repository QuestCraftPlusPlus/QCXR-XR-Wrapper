using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace YVR.Core
{
    /// <summary>
    /// Provides access to the boundary system.
    /// </summary>
    public class YVRBoundary
    {
        /// <summary>
        /// Specifies a tracked node that can be queried through the boundary system.
        /// </summary>
        public enum BoundaryNode
        {
            /// <summary>
            /// Tracks the left controller node
            /// </summary>
            ControllerLeft = DeviceNode.ControllerLeft,
            /// <summary>
            /// Tracks the right controller node
            /// </summary>
            ControllerRight = DeviceNode.ControllerRight,
            /// <summary>
            /// Tracks the hand node
            /// </summary>
            Head = DeviceNode.EyeCenter
        }

        /// <summary>
        /// The test results of boundary system queries.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct BoundaryTestResult
        {
            /// <summary>
            /// Whether the quired test would violate the boundary
            /// </summary>
            [MarshalAs(UnmanagedType.U1)]
            public bool IsTriggering;
            /// <summary>
            /// The Distance between the queried test object and the closest boundary point
            /// </summary>
            public float ClosestDistance;
            /// <summary>
            /// The closest point to the queried test object
            /// </summary>
            public Vector3 ClosestPoint;
            /// <summary>
            /// The normal of the closest point to the queried test object
            /// </summary>
            public Vector3 ClosestPointNormal;
        }

        /// <summary>
        ///  Returns true if the boundary system is currently configured with valid boundary data.
        /// </summary>
        /// <returns>Is the boundary data valid</returns>
        public bool GetConfigured() { return YVRPlugin.Instance.GetBoundaryConfigured(); }

        /// <summary>
        /// Returns the results of testing a tracked node
        /// </summary>
        /// <returns>The test results of boundary system queries.</returns>
        public BoundaryTestResult TestNode(BoundaryNode node)
        {
            BoundaryTestResult testResult = default;
            YVRPlugin.Instance.TestBoundaryNode(node, ref testResult);
            return testResult;
        }

        /// <summary>
        /// Returns the results of testing a 3d point
        /// </summary>
        /// <returns>The test results of boundary system queries.</returns>
        public BoundaryTestResult TestPoint(Vector3 point)
        {
            BoundaryTestResult testResult = default;
            YVRPlugin.Instance.TestBoundaryPoint(point, ref testResult);
            return testResult;
        }

        /// <summary>
        /// Returns a vector that indicates the spatial dimensions
        /// </summary>
        public Vector3 GetDimensions() { return YVRPlugin.Instance.GetBoundaryDimensions(); }

        /// <summary>
        /// Returns true if the boundary system is currently visible.
        /// </summary>
        public bool GetVisible() { return YVRPlugin.Instance.GetBoundaryVisible(); }

        /// <summary>
        /// Requests the boundary system visibility to be set to the specified value.
        /// </summary>
        public void SetVisible(bool visible)
        {
            YVRPlugin.Instance.SetBoundaryVisible(visible);
        }

        /// <summary>
        /// Returns an array of 3d points (in clockwise order) that define the specified boundary type.
        /// The points must add the offset of the camera root node when moving the root node.
        /// </summary>
        public Vector3[] GetGeometry() { return YVRPlugin.Instance.GetBoundaryGeometry(); }

    }
}
