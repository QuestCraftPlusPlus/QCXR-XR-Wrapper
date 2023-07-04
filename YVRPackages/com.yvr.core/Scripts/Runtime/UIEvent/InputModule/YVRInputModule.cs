using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core
{
    /// <summary>
    /// Override PointerInputModule to allow UI to interact with custom ray
    /// </summary>
    /// <seealso cref="YVR.Core.YVRGraphicRaycaster" />
    /// <seealso cref="YVR.Core.YVRPhysicsRaycaster" />
    public partial class YVRInputModule
    {
        /// <summary> Minimum drag threshold in degree </summary>
        public float dragAngleThreshold = 1;

        /// <summary> Whether use HMD as ray transform when both controller is disconnected </summary>
        public bool enableHMDRayAgent = false;

        /// <summary>
        /// Disable YVRInputModule when losing focus
        /// </summary>
        [SerializeField] protected bool disableModuleOnFocusLost = false;

        /// <summary> Transform that decide ray's origin and direction </summary>
        public Transform rayTransform => inputDataProvider?.rayTransform;


        /// <summary> Callback when raycast hit sth  </summary>
        public event Action<GameObject, Vector3, Vector3> onRaycastHit = null;

        /// <summary> Callback when module input changed </summary>
        public event Action<YVRInputDataBaseProvider> onInputDataProviderChanged = null;

        /// <summary> Customized input data provider </summary>
        public YVRInputDataBaseProvider customProvider = null;

        private static YVRInputModule s_Instance;

        /// <summary>
        /// Get Instance of YVRInputModule
        /// </summary>
        public static YVRInputModule Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = FindObjectOfType<YVRInputModule>();

                return s_Instance;
            }
        }

        /// <summary>
        /// The raycast result of this frame
        /// </summary>
        public RaycastResult raycastResult { get; private set; }

        /// <summary>
        /// The ray casting game object of this frame
        /// </summary>
        public GameObject raycastGameObject => raycastResult.gameObject;

        /// <summary>
        /// current using input data provider source
        /// </summary>
        public DataProviderSource inputDataProviderSource { get; private set; }

        private YVRPointerEventData m_EventData = null;

        /// <summary>
        ///  The event data set from input module to event system
        /// </summary>
        public YVRPointerEventData eventData => m_EventData ??= new YVRPointerEventData(eventSystem);

        private MouseState m_SimulatedMouseState = new MouseState();
        private YVRInputDataBaseProvider m_InputDataProvider = null;

        private YVRInputDataBaseProvider m_RControllerProvider = null;

        private YVRInputDataBaseProvider rControllerProvider =>
            m_RControllerProvider ??= new YVRInputDataRControllerProvider();

        private YVRInputDataBaseProvider m_LControllerProvider = null;

        private YVRInputDataBaseProvider lControllerProvider =>
            m_LControllerProvider ??= new YVRInputDataLControllerProvider();

        private YVRInputDataBaseProvider m_DataDummyProvider = null;
        private YVRInputDataBaseProvider dataDummyProvider => m_DataDummyProvider ??= new YVRInputDataDummyProvider();

        private YVRInputDataBaseProvider m_DataHMDProvider = null;
        private YVRInputDataBaseProvider dataHMDProvider => m_DataHMDProvider ??= new YVRInputDataHMDProvider();

        private YVRInputDataLHandTrackingProvider m_LHandProvider = null;

        private YVRInputDataLHandTrackingProvider lHandProvider =>
            m_LHandProvider ??= new YVRInputDataLHandTrackingProvider();

        private YVRInputDataRHandTrackingProvider m_RHandProvider = null;

        private YVRInputDataRHandTrackingProvider rHandProvider =>
            m_RHandProvider ??= new YVRInputDataRHandTrackingProvider();

        /// <summary>
        /// Current using input data provider
        /// </summary>
        public YVRInputDataBaseProvider inputDataProvider
        {
            get => m_InputDataProvider;
            private set
            {
                if (m_InputDataProvider == value) return;
                m_InputDataProvider = value;
                onInputDataProviderChanged?.Invoke(value);
            }
        }

        [ExcludeFromDocs]
        protected override void Start()
        {
            base.Start();
            if (disableModuleOnFocusLost)
            {
                YVRManager.instance.eventsManager.onFocusGained += OnFocusGained;
                YVRManager.instance.eventsManager.onFocusLost += OnFocusLost;
            }
        }

        private void OnFocusGained() { this.enabled = true; }

        private void OnFocusLost() { this.enabled = false; }


        /// <summary>
        /// Set the currently used data provider
        /// </summary>
        /// <param name="source"></param>
        public void SetInputDataProvider(DataProviderSource source)
        {
            inputDataProviderSource = source;
            switch (source)
            {
                case DataProviderSource.HMD:
                    inputDataProvider = enableHMDRayAgent ? dataHMDProvider : dataDummyProvider;
                    break;
                case DataProviderSource.RightController
                    : // Check right controller first to ensure use right controller while both controllers connected
                    inputDataProvider = rControllerProvider;
                    break;
                case DataProviderSource.LeftController:
                    inputDataProvider = lControllerProvider;
                    break;
                case DataProviderSource.Custom:
                    inputDataProvider = customProvider;
                    break;
                case DataProviderSource.LeftHand:
                    inputDataProvider = lHandProvider;
                    break;
                case DataProviderSource.RightHand:
                    inputDataProvider = rHandProvider;
                    break;
                case DataProviderSource.None:
                default:
                    inputDataProvider = dataDummyProvider;
                    break;
            }
        }

        /// <summary>
        /// Override PointerInputModule.Process to handle custom input events
        /// </summary>
        public override void Process()
        {
            if (inputDataProvider == null)
            {
                if (enableHMDRayAgent && !YVRInput.IsControllerConnected(ControllerType.LeftTouch) &&
                    !YVRInput.IsControllerConnected(ControllerType.RightTouch))
                    SetInputDataProvider(DataProviderSource.HMD);
                else if (YVRManager.instance.handManager.leftHandData.isActive == 1)
                    SetInputDataProvider(DataProviderSource.LeftHand);
                else if (YVRManager.instance.handManager.rightHandData.isActive == 1)
                    SetInputDataProvider(DataProviderSource.RightHand);
                else if (YVRInput.IsControllerConnected(ControllerType.LeftTouch))
                    SetInputDataProvider(DataProviderSource.LeftController);
                else if (YVRInput.IsControllerConnected(ControllerType.RightTouch))
                    SetInputDataProvider(DataProviderSource.RightController);
                else
                    SetInputDataProvider(DataProviderSource.None);
            }

            if (!rayTransform) return;

            ProcessMouseState(GetSimulatedMouseState());
        }

        /// <summary>
        /// Get the simulated mouse state which override the default mouse state required by PointerInputModule
        /// </summary>
        /// <returns> Simulated mouse state through custom ray </returns>
        protected virtual MouseState GetSimulatedMouseState()
        {
            RaycastResult temp = GetFirstRaycast(inputDataProvider.rayOrigin, inputDataProvider.rayDirection,
                                                 inputDataProvider.scrollData, m_RaycastResultCache);
            eventData.pointerCurrentRaycast = temp;

            // Trigger event while raycast sth or the first not raycast sth
            if (temp.module || (!temp.module && raycastResult.module))
            {
                onRaycastHit?.SafeInvoke(temp.gameObject, temp.worldPosition, temp.worldNormal);
                inputDataProvider.onRaycastHit(temp.gameObject);
            }

            raycastResult = temp;

            var yvrGraphicRaycaster = raycastResult.module as YVRGraphicRaycaster;
            if (yvrGraphicRaycaster != null)
                eventData.position = yvrGraphicRaycaster.GetRaycastResultScreenPosition(raycastResult);

            var yvrPhysicalRaycaster = raycastResult.module as YVRPhysicsRaycaster;
            if (yvrPhysicalRaycaster != null)
                eventData.position = yvrPhysicalRaycaster.GetRaycastResultScreenPosition(raycastResult);

            m_SimulatedMouseState.SetButtonState(PointerEventData.InputButton.Left, GetSimulatedButtonState(),
                                                 eventData);

            return m_SimulatedMouseState;
        }

        /// <summary>
        /// Use event system to get first raycast result
        /// </summary>
        /// <param name="origin"> Ray origin </param>
        /// <param name="direction"> Ray direction </param>
        /// <param name="scrollDelta"> ScrollDelta, default to be zero</param>
        /// <param name="raycastResults"> List to contain all raycast results, default to be null </param>
        /// <returns></returns>
        public RaycastResult GetFirstRaycast(Vector3 origin, Vector3 direction, Vector2 scrollDelta = default,
                                             List<RaycastResult> raycastResults = default)
        {
            raycastResults ??= new List<RaycastResult>();

            eventData.Reset();
            eventData.worldSpaceRay.origin = origin;
            eventData.worldSpaceRay.direction = direction;
            eventData.scrollDelta = scrollDelta;
            eventData.useDragThreshold = true;
            eventSystem.RaycastAll(eventData, raycastResults); // Get All raycast result and fill m_RaycastResultCache
            return FindFirstRaycast(raycastResults);
        }

        /// <summary>
        /// Process the simulated mouse state
        /// </summary>
        /// <param name="mouseState"> Simulated mouse state </param>
        protected virtual void ProcessMouseState(MouseState mouseState)
        {
            mouseState.AnyPressesThisFrame();
            mouseState.AnyReleasesThisFrame();

            MouseButtonEventData buttonData = mouseState.GetButtonState(PointerEventData.InputButton.Left).eventData;
            ProcessMousePress(buttonData);
            ProcessMove(buttonData.buttonData);
            ProcessDrag(buttonData);
            ProcessScroll(buttonData);
        }

        /// <summary>
        /// Get the simulate button state (press / released or not) in this frame
        /// </summary>
        /// <returns> Simulated button state </returns>
        protected virtual PointerEventData.FramePressState GetSimulatedButtonState()
        {
            bool pressed = inputDataProvider.pressed;
            bool released = inputDataProvider.released;

            if (pressed && released)
                return PointerEventData.FramePressState.PressedAndReleased;
            if (pressed)
                return PointerEventData.FramePressState.Pressed;
            if (released)
                return PointerEventData.FramePressState.Released;

            return PointerEventData.FramePressState.NotChanged;
        }

        /// <summary>
        /// Process the simulated mouse button press state
        /// </summary>
        /// <param name="mouseData"> Target simulated mouse data </param>
        protected virtual void ProcessMousePress(MouseButtonEventData mouseData)
        {
            PointerEventData data = mouseData.buttonData;
            GameObject go = data.pointerCurrentRaycast.gameObject;

            if (mouseData.PressedThisFrame()) // Click Down
            {
                data.eligibleForClick = true;
                data.pressPosition = data.position;
                data.pointerPressRaycast = data.pointerCurrentRaycast;

                // Deselect the current selected GameObject if the currently pointed-at GameObject is different.
                DeselectIfSelectionChanged(go, data);

                // Find gameObject that can receive press event
                // first find the gameObjects which inherits IPointerDownHandler
                // if not found, find the gameObjects which inherits IPointerClickHandler
                GameObject currentClickDownGO
                    = ExecuteEvents.ExecuteHierarchy(go, data, ExecuteEvents.pointerDownHandler) ??
                      ExecuteEvents.GetEventHandler<IPointerClickHandler>(go);

                // Calculate click count
                float currentTime = Time.unscaledTime;
                if (currentClickDownGO == data.lastPress)
                {
                    float diffTime = currentTime - data.clickTime;
                    data.clickCount = diffTime < 0.3f ? data.clickCount + 1 : 1;
                }

                // rawPointerPress is the game object that is raycasted currently
                // pointerPress GO is the gameObject that actually received press event, which may be the same with rawPointerPress or one of the ancestors of the rawPointerPress
                data.pointerPress = currentClickDownGO;
                data.rawPointerPress = go;
                data.clickTime = currentTime;

                // When click down, handle the initial drag related events
                data.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(go);

                // Execute IInitializePotentialDrag Event if GameObject inherits from IDragHandler
                if (data.pointerDrag)
                    ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.initializePotentialDrag);
            }

            if (mouseData.ReleasedThisFrame()) // Click Up
            {
                // Execute IPointerUpHandler event one the press gameObject or its ancestor
                ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

                // Find GameObject that inherits IPointerClickHandler
                GameObject currentClickUpGO = ExecuteEvents.GetEventHandler<IPointerClickHandler>(go);
                if (data.pointerPress == currentClickUpGO && data.eligibleForClick)
                    ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
                else if (data.pointerDrag != null)
                    ExecuteEvents.ExecuteHierarchy(go, data, ExecuteEvents.dropHandler);

                data.eligibleForClick = false;
                data.pointerPress = null;
                data.rawPointerPress = null;

                // When click up, handle the end drag related events
                if (data.pointerDrag != null && data.dragging)
                    ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.endDragHandler);

                data.dragging = false;
                data.pointerDrag = null;

                // redo pointer enter / exit to refresh state
                // so that if we moused over something that ignored it before
                // due to having pressed on something else
                // it now gets it.
                if (go == data.pointerEnter) return;

                HandlePointerExitAndEnter(data, null);
                HandlePointerExitAndEnter(data, go);
            }
        }

        /// <summary>
        /// Process the simulated mouse drag state
        /// </summary>
        /// <param name="mouseData"> Target simulated mouse data </param>
        protected virtual void ProcessDrag(MouseButtonEventData mouseData)
        {
            PointerEventData data = mouseData.buttonData;

            if (data.pointerDrag != null && !data.dragging && ShouldStartDrag(data))
            {
                ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.beginDragHandler);
                data.dragging = true;
            }

            if (!data.dragging || data.pointerDrag == null) return;

            // When the GO that received CLickDown message is not the same GO that received drag message
            if (data.pointerPress != data.pointerDrag)
            {
                ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);
                data.eligibleForClick = false;
                data.pointerPress = null;
                data.rawPointerPress = null;
            }

            ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.dragHandler);
        }


        /// <summary>
        /// Process the simulated mouse scroll state
        /// </summary>
        /// <param name="mouseData"> Target simulated mouse data </param>
        protected virtual void ProcessScroll(MouseButtonEventData mouseData)
        {
            PointerEventData data = mouseData.buttonData;
            if (Mathf.Approximately(data.scrollDelta.sqrMagnitude, 0.0f)) return;

            GameObject scrollHandler
                = ExecuteEvents.GetEventHandler<IScrollHandler>(data.pointerCurrentRaycast.gameObject);
            ExecuteEvents.ExecuteHierarchy(scrollHandler, data, ExecuteEvents.scrollHandler);
        }

        /// <summary>
        /// Process the simulated mouse move state
        /// </summary>
        /// <param name="pointerEvent"> Target simulated mouse data </param>
        protected override void ProcessMove(PointerEventData pointerEvent)
        {
            HandlePointerExitAndEnter(pointerEvent, pointerEvent.pointerCurrentRaycast.gameObject);
        }

        // Whether the drag distance matches the dragThreshold
        private bool ShouldStartDrag(PointerEventData pointerEventData)
        {
            if (!pointerEventData.useDragThreshold)
                return true; // Some UI Element may modify this parameter, like Scroller

            Vector3 cameraPos = pointerEventData.pressEventCamera.transform.position;
            Vector3 pressDirection = (pointerEventData.pointerPressRaycast.worldPosition - cameraPos).normalized;
            Vector3 currentDirection = (pointerEventData.pointerCurrentRaycast.worldPosition - cameraPos).normalized;
            return Vector3.Dot(pressDirection, currentDirection) < Mathf.Cos(Mathf.Deg2Rad * dragAngleThreshold);
        }

        [ExcludeFromDocs]
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (YVRManager.instance)
            {
                YVRManager.instance.eventsManager.onFocusGained -= OnFocusGained;
                YVRManager.instance.eventsManager.onFocusLost -= OnFocusLost;
            }

            s_Instance = null;
        }
    }
}