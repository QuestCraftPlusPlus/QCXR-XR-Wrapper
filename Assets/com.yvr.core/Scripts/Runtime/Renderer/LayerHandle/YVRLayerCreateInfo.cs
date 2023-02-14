using System.Runtime.InteropServices;

namespace YVR.Core
{
    public struct YVRLayerCreateInfo
    {
        public int depth;
        public int width;
        public int height;
        public int swapChainBufferCount;
        public int textureType;
        public int layerFlags;

        [MarshalAs(UnmanagedType.U1)]
        public bool separateSwapChain;

        [MarshalAs(UnmanagedType.U1)]
        public bool autoShowAfterCreated;

        public YVRLayerCreateInfo(int depth, int width, int height, int swapChainBufferCount)
            : this(depth, width, height, swapChainBufferCount, 0, 2, false, true) { }

        public YVRLayerCreateInfo(int depth, int width, int height, int swapChainBufferCount, int textureType, int layerFlags, bool separateSwapChain, bool autoShowAfterCreated)
        {
            this.depth = depth;
            this.width = width;
            this.height = height;
            this.swapChainBufferCount = swapChainBufferCount;
            this.textureType = textureType;
            this.layerFlags = layerFlags;
            this.separateSwapChain = separateSwapChain;
            this.autoShowAfterCreated = autoShowAfterCreated;
        }
    }
}