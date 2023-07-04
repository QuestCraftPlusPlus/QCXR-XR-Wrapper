using UnityEngine;
using System;

namespace YVR.Core
{
    public struct XRSize
    {
        public float width;
        public float height;

        public XRSize(float width, float height)
        {
            this.width = width;
            this.height = height;
        }
    }

    public struct XRPose
    {
        /// <summary>
        /// The orientation
        /// </summary>
        public Quaternion orientation;

        /// <summary>
        /// The position
        /// </summary>
        public Vector3 position;

        public XRPose(Vector3 position, Quaternion orientation)
        {
            this.position = position;
            this.orientation = orientation;
        }

        public static XRPose identity => new XRPose() {position = Vector3.zero, orientation = Quaternion.identity};

        public static XRPose operator *(XRPose lhs, XRPose rhs)
        {
            var ret = new XRPose();
            ret.position = lhs.position + lhs.orientation * rhs.position;
            ret.orientation = lhs.orientation * rhs.orientation;
            return ret;
        }

        public XRPose Inverse()
        {
            var ret = new XRPose();
            ret.orientation = Quaternion.Inverse(orientation);
            ret.position = ret.orientation * -position;
            return ret;
        }
    }

    public interface IYVRLayerHandle
    {
        void CreateLayerAsync(YVRLayerCreateInfo layerCreateInfo, Action<int> onLayerCreated = null);
        void DestroyLayerAsync(int layerId, bool destroyImmediate, Action onLayerDestroyed = null);

        void AddLayerAsync(int layerId, Action onLayerAdded = null);
        void RemoveLayerAsync(int layerId, Action onLayerAdded = null);

        void SwapBufferLayer(int layerId);

        void SetLayerPose(in int layerId, in XRPose pose);
        void SetLayerSize(in int layerId, in XRSize size);
        void SetLayerCylinderParam(in int layerId, in float radius, in float centralAngle, in float aspectRatio);

        int GetEyeBufferLayerId();

        int GetLayerColorHandle(int layerId, int index);
        int GetLayerColorHandle(int layerId);

        int[] GetAlLayersColorHandle();

        int GetLayersCount();

        void SetLayerPreSubmitCallback(Action onLayerPreSubmit);

        void SetLayerDepthAsync(int layerId, int depth, Action<bool> onLayerDepthSet = null);
    }
}