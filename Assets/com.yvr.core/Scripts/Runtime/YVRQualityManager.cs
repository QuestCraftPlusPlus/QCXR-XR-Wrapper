using System;
using System.Linq;
using System.Collections;
using UnityEngine;

namespace YVR.Core
{
    /// <summary>
    /// Manager for rendering quality
    /// </summary>
    [Serializable]
    public partial class YVRQualityManager
    {
        [Tooltip("K1: VSync every frame.K2: VSync two frame.")]
        [SerializeField] private VSyncCount _vSyncCount = VSyncCount.k1;

        [SerializeField]
        private FixedFoveatedRenderingLevel _fixedFoveatedRenderingLevel = FixedFoveatedRenderingLevel.Off;

        [SerializeField] private bool m_EnableASW = false;

        /// <summary>
        /// The event which will be triggered when vSync count changed
        /// </summary>
        public event Action<VSyncCount> onVSyncCountChanged = null;

        /// <summary>
        /// The event which will be triggered when ffr level changed
        /// </summary>
        public event Action<FixedFoveatedRenderingLevel> onFFRLevelChanged = null;
        
        /// <summary>
        /// The event which will be triggered when asw enable state changed
        /// </summary>
        public event Action<bool> onASWEnableChanged = null;


        /// <summary>
        /// Set or get current vSync count
        /// </summary>
        public VSyncCount vSyncCount
        {
            get { return _vSyncCount; }
            set
            {
                if (_vSyncCount == value) return;

                _vSyncCount = value;
                ApplyVSyncCount(value);
            }
        }

        /// <summary>
        /// Set or get current fixed foveated rendering level
        /// </summary>
        public FixedFoveatedRenderingLevel fixedFoveatedRenderingLevel
        {
            get { return _fixedFoveatedRenderingLevel; }
            set
            {
                if (_fixedFoveatedRenderingLevel == value) return;

                _fixedFoveatedRenderingLevel = value;
                ApplyFFR(value);
            }
        }
        
        /// <summary>
        /// Set or get current asw enable state
        /// </summary>
        public bool aswEnable
        {
            get { return m_EnableASW; }
            set
            {
                if (m_EnableASW == value) return;

                m_EnableASW = value;
                ApplyASW(value);
            }
        }


        /// <summary>
        /// Gets the recommended MSAA level for optimal quality/performance the current device.
        /// </summary>
        public int recommendAntiAlisingLevel => 4;

        /// <summary>
        /// Whether to use recommend MSAA level, if true, the value of @YVR.Core.YVRQualityManager.antiAliasing will be ignored
        /// </summary>
        public bool useRecommendedMSAALevel = true;

        /// <summary>
        /// Using the data from unity Inspector panel to initialize vSync count and ffr level
        /// </summary>
        public void Initialize() { YVRManager.instance.StartCoroutine(Wait2FramesBeforeInitializing()); }

        private void ApplyVSyncCount(VSyncCount value)
        {
            YVRPlugin.Instance.SetVSyncCount(value);
            QualitySettings.vSyncCount = (int) value;
            onVSyncCountChanged?.SafeInvoke(value);
        }

        private void ApplyFFR(FixedFoveatedRenderingLevel value)
        {
            YVRPlugin.Instance.SetFoveation((int) value);
            onFFRLevelChanged?.SafeInvoke(value);
        }

        private void ApplyASW(bool enable)
        {
            YVRPlugin.Instance.SetASWEnable(enable);
            onASWEnableChanged?.SafeInvoke(enable);
        }

        // The native color buffer may be allocate after several frames
        // Thus waiting a second to ensure the eye buffer has been created
        private IEnumerator Wait2FramesBeforeInitializing()
        {
            ApplyVSyncCount(vSyncCount);
            yield return new WaitForSecondsRealtime(1.0f);
            ApplyFFR(fixedFoveatedRenderingLevel);
            ApplyASW(m_EnableASW);
        }
    }
}