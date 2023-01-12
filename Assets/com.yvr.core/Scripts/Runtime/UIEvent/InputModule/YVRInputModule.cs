using System;
using System.Collections;
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
    public partial class YVRInputModule : PointerInputModule
    {
        /// <summary> Minimum drag threshhold in degree </summary>
        public float dragAngleThreshold = 1;

        /// <summary> Whether use HMD as ray transform when both controller is disconnected </summary>
        public bool enableHMDRayAgent = false;

        /// <summary>
        /// Disable YVRInputModule when losing focus
        /// </summary>
        [SerializeField]
        protected bool disableModuleOnFocusLost = false;

        /// <summary> Transform that decide ray's origin and direction </summary>
        public Transform rayTransform { get => inputDataProvider?.rayTransform; }


        /// <summary> Callback when raycast hit sth  </summary>
        public event Action<GameObject, Vector3, Vector3> onRaycastHit = null;

        /// <summary> Callback when module input changed </summary>
        public event Action<YVRInputDataBaseProvider> onInputDataProviderChanged = null;

        /// <summary> Customized input data provider </summary>
        public YVRInputDataBaseProvider customProvider = null;

        private static YVRInputModule _instance;

        /// <summary>
        /// Get Instance of YVRInputModule
        /// </summary>
        public static YVRInputModule Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<YVRInputModule>();

                return _instance;
            }
        }

        /// <summary>
        /// The raycast result of this frame
        /// </summary>
        public RaycastResult raycastResult { get; private set; }

        /// <summary>
        /// The raycasting gameobject of this frame
        /// </summary>
        public GameObject raycastGameObject => raycastResult.gameObject;

        /// <summary>
        /// Currect using input data provider source
        /// </summary>
        public DataProviderSource inputDataProviderSource { get; private set; }

        private YVRPointerEventData _eventData = null;

        /// <summary>
        ///  The event data set from inputmodule to event system
        /// </summary>
        public YVRPointerEventData eventData { get { return _eventData ?? (_eventData = new YVRPointerEventData(eventSystem)); } }

        private MouseState simulatedMouseState = new MouseState();
        private YVRInputDataBaseProvider _inputDataProvider = null;

        private YVRInputDataBaseProvider _rControllerProvider = null;
        private YVRInputDataBaseProvider rControllerProvider => _rControllerProvider != null ? _rControllerProvider : _rControllerProvider = new YVRInputDataRControllerProvider();

        private YVRInputDataBaseProvider _lControllerProvider = null;
        private YVRInputDataBaseProvider lControllerProvider => _lControllerProvider != null ? _lControllerProvider : _lControllerProvider = new YVRInputDataLControllerProvider();

        private YVRInputDataBaseProvider _dataDummyProvider = null;
        private YVRInputDataBaseProvider dataDummyProvider => _dataDummyProvider != null ? _dataDummyProvider : _dataDummyProvider = new YVRInputDataDummyProvider();

        private YVRInputDataBaseProvider _dataHMDProvider = null;
        private YVRInputDataBaseProvider dataHMDProvider => _dataHMDProvider != null ? _dataHMDProvider : _dataHMDProvider = new YVRInputDataHMDProvider();

        /// <summary>
        /// Current using input data provider
        /// </summary>
        public YVRInputDataBaseProvider inputDataProvider
        {
            get => _inputDataProvider;
            private set
            {
                if (_inputDataProvider == value) return;
                _inputDataProvider = value;
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

        private void OnFocusGained()
        {
            this.enabled = true;
        }

        private void OnFocusLost()
        {
            this.enabled = false;
        }


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
                case DataProviderSource.RightController: // Check right controller first to ensure use right controller while both controllers connected
                    inputDataProvider = rControllerProvider;
                    break;
                case DataProviderSource.LeftController:
                    inputDataProvider = lControllerProvider;
                    break;
                case DataProviderSource.Custom:
                    inputDataProvider = customProvider;
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
                if (enableHMDRayAgent && !YVRInput.IsControllerConnected(ControllerType.LeftTouch) && !YVRInput.IsControllerConnected(ControllerType.RightTouch))
                    SetInputDataProvider(DataProviderSource.HMD);
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
            RaycastResult temp = GetFirstRaycast(inputDataProvider.rayOrigin, inputDataProvider.rayDirection, inputDataProvider.scrollData, m_RaycastResultCache);
            eventData.pointerCurrentRaycast = temp;

            if (temp.module || (!temp.module && raycastResult.module)) // Trigger event while raycast sth or the first not raycast sth
            {
                onRaycastHit?.SafeInvoke(temp.gameObject, temp.worldPosition, temp.worldNormal);
                inputDataProvider.onRaycastHit(temp.gameObject);
            }

            raycastResult = temp;

            YVRGraphicRaycaster yvrGraphicRaycaster = raycastResult.module as YVRGraphicRaycaster;
            if (yvrGraphicRaycaster != null)
                eventData.position = yvrGraphicRaycaster.GetRaycastResultScreenPosition(raycastResult);

            YVRPhysicsRaycaster yvrPhysicalRaycaster = raycastResult.module as YVRPhysicsRaycaster;
            if (yvrPhysicalRaycaster != null)
                eventData.position = yvrPhysicalRaycaster.GetRaycastResultScreenPosition(raycastResult);

            simulatedMouseState.SetButtonState(PointerEventData.InputButton.Left, GetSimulatedButtonState(), eventData);

            return simulatedMouseState;
        }

        /// <summary>
        /// Use event system to get first raycast result
        /// </summary>
        /// <param name="origin"> Ray origin </param>
        /// <param name="direction"> Ray direction </param>
        /// <param name="scrollDelta"> ScrollDelta, default to be zero</param>
        /// <param name="raycastResults"> List to contain all raycast results, default to be null </param>
        /// <returns></returns>
        public RaycastResult GetFirstRaycast(Vector3 origin, Vector3 direction, Vector2 scrollDelta = default, List<RaycastResult> raycastResults = default)
        {
            if (raycastResults == null) raycastResults = new List<RaycastResult>();

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
            bool pressed = mouseState.AnyPressesThisFrame();
            bool released = mouseState.AnyReleasesThisFrame();

            PointerInputModule.MouseButtonEventData buttonData = mouseState.GetButtonState(PointerEventData.InputButton.Left).eventData;
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
        /// <param name="data"> Target simulated mouse data </param>
        protected virtual void ProcessMousePress(MouseButtonEventData data)
        {
            PointerEventData eventData = data.buttonData;
            GameObject currentRaycastingGO = eventData.pointerCurrentRaycast.gameObject;

            if (data.PressedThisFrame()) // Click Down
            {
                eventData.eligibleForClick = true;
                eventData.pressPosition = eventData.position;
                eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;

                DeselectIfSelectionChanged(currentRaycastingGO, eventData); // Deselect the current selected GameObject if the currently pointed-at GameObject is different.

                // Find gameObject that can receive press event
                // first find the gameObjects which inherits IPointerDownHandler
                // if not found, find the gameObjects which inherits IPointerClickHandler
                GameObject currentClickDownGO = ExecuteEvents.ExecuteHierarchy(currentRaycastingGO, eventData, ExecuteEvents.pointerDownHandler) ??
                                        ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentRaycastingGO);

                // Calculate click count
                float currentTime = Time.unscaledTime;
                if (currentClickDownGO == eventData.lastPress)
                {
                    float diffTime = currentTime - eventData.clickTime;
                    eventData.clickCount = diffTime < 0.3f ? eventData.clickCount + 1 : 1;
                }

                // rawPointerPress is the gameobject that is raycasted currently
                // pointerPress GO is the gameObject that actually received press event, which may be the same with rawPointerPress or one of the ancestors of the rawPointerPress
                eventData.pointerPress = currentClickDownGO;
                eventData.rawPointerPress = currentRaycastingGO;
                eventData.clickTime = currentTime;

                // When click down, handle the initial drag related events
                eventData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(currentRaycastingGO);
                if (eventData.pointerDrag) // Execute IInitializePotentialDrag Event if GameObject inherits from IDragHandler
                    ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.initializePotentialDrag);
            }

            if (data.ReleasedThisFrame()) // Click Up
            {
                // Execute IPointerUpHandler event one the press gameObject or its ancestor
                ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);

                // Find GameObject that inherits IPointerClickHandler
                GameObject currentClickUpGO = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentRaycastingGO);
                if (eventData.pointerPress == currentClickUpGO && eventData.eligibleForClick)
                    ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerClickHandler);
                else if (eventData.pointerDrag != null)
                    ExecuteEvents.ExecuteHierarchy(currentRaycastingGO, eventData, ExecuteEvents.dropHandler);

                eventData.eligibleForClick = false;
                eventData.pointerPress = null;
                eventData.rawPointerPress = null;

                // When click up, handle the end drag related events
                if (eventData.pointerDrag != null && eventData.dragging)
                    ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.endDragHandler);

                eventData.dragging = false;
                eventData.pointerDrag = null;

                // redo pointer enter / exit to refresh state
                // so that if we moused over somethign that ignored it before
                // due to having pressed on something else
                // it now gets it.
                if (currentRaycastingGO != eventData.pointerEnter)
                {
                    HandlePointerExitAndEnter(eventData, null);
                    HandlePointerExitAndEnter(eventData, currentRaycastingGO);
                }
            }
        }

        /// <summary>
        /// Process the simulated mouse drag state
        /// </summary>
        /// <param name="data"> Target simulated mouse data </param>
        protected virtual void ProcessDrag(MouseButtonEventData data)
        {
            PointerEventData eventData = data.buttonData;

            if (eventData.pointerDrag != null && !eventData.dragging && ShouldStartDrag(eventData))
            {
                ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.beginDragHandler);
                eventData.dragging = true;
            }

            if (eventData.dragging && eventData.pointerDrag != null)
            {
                // When the GO that received CLickDown message is not the same GO that received drag message
                if (eventData.pointerPress != eventData.pointerDrag)
                {
                    ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);
                    eventData.eligibleForClick = false;
                    eventData.pointerPress = null;
                    eventData.rawPointerPress = null;
                }

                ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.dragHandler);
            }
        }


        /// <summary>
        /// Process the simulated mouse scroll state
        /// </summary>
        /// <param name="data"> Target simulated mouse data </param>
        protected virtual void ProcessScroll(MouseButtonEventData data)
        {
            PointerEventData eventData = data.buttonData;
            if (!Mathf.Approximately(eventData.scrollDelta.sqrMagnitude, 0.0f))
            {
                GameObject scrollHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.pointerCurrentRaycast.gameObject);
                ExecuteEvents.ExecuteHierarchy(scrollHandler, eventData, ExecuteEvents.scrollHandler);
            }
        }

        /// <summary>
        /// Process the simulated mouse move state
        /// </summary>
        /// <param name="data"> Target simulated mouse data </param>
        protected override void ProcessMove(PointerEventData pointerEvent)
        {
            HandlePointerExitAndEnter(pointerEvent, pointerEvent.pointerCurrentRaycast.gameObject);
        }

        // Whether the drag distance matches the dragThreshold
        private bool ShouldStartDrag(PointerEventData pointerEventData)
        {
            if (!pointerEventData.useDragThreshold) return true; // Some UI Element may modify this parameter, like Scroller

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
            _instance = null;
        }
    }
}