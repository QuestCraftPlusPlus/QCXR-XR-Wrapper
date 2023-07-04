using System;

namespace YVR.Core
{
    public class YVRLayerEditorHandle : IYVRLayerHandle
    {
        public void CreateLayerAsync(YVRLayerCreateInfo layerCreateInfo, Action<int> onLayerCreated = null) { }
        public void DestroyLayerAsync(int layerId, bool destroyImmediate, Action onLayerDestroyed = null) { }
        public void AddLayerAsync(int layerId, Action onLayerAdded = null) { }
        public void RemoveLayerAsync(int layerId, Action onLayerAdded = null) { }

        public void SwapBufferLayer(int layerId) { }
        public int GetEyeBufferLayerId() { return 0; }

        public int GetLayerColorHandle(int layerId, int index) { return 0; }
        public int GetLayerColorHandle(int layerId) { return 0; }

        public void SetLayerPose(in int layerId, in XRPose pose) { }
        public void SetLayerSize(in int layerId, in XRSize size) { }
        public void SetLayerCylinderParam(in int layerId, in float radius, in float centralAngle, in float aspectRatio) { }

        public int[] GetAlLayersColorHandle() { return null; }

        public int GetLayersCount() { return 0; }

        public void SetLayerPreSubmitCallback(Action onLayerPreSubmit) { }

        public void SetLayerDepthAsync(int layerId, int depth, Action<bool> onLayerDepthSet = null) { }
    }
}