using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace YVR.Core
{
    public interface IYVRLayerHandle
    {
        void CreateLayerAsync(YVRLayerCreateInfo layerCreateInfo, Action<int> onLayerCreated = null);
        void DestroyLayerAsync(int layerId, Action onLayerDestroyed = null);

        void AddLayerAsync(int layerId, Action onLayerAdded = null);
        void RemoveLayerAsync(int layerId, Action onLayerAdded = null);

        void SetLayerMatrix(int layerId, EyeSide eyeSide, in Matrix4x4 matrix);
        void SwapBufferLayer(int layerId);

        int GetEyeBufferLayerId();

        int GetLayerColorHandle(int layerId);

        int GetLayerColorHandle(int layerId, EyeSide eyeSide);

        int[] GetAlLayersColorHandle();

        int GetLayersCount();

        void SetLayerPreSubmitCallback(Action onLayerPreSubmit);

        void SetLayerDepthAsync(int layerId, int depth, Action<bool> onLayerDepthSetted = null);
    }
}