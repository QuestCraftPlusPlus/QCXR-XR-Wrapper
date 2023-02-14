using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HurricaneVR.Framework.Core.UI
{
    public class HVRInputModule : PointerInputModule
    {
        public static HVRInputModule Instance { get; private set; }

        public float MaxDistance = 3f;

        [Tooltip("Button used to toggle presses.")]
        public HVRButtons PressButton;

        [Tooltip("Canvases for UI pointer interaction.")]
        public List<Canvas> UICanvases = new List<Canvas>();

        public List<HVRUIPointer> Pointers { get; private set; }

        [Tooltip("Angle the pointer has to move before a drag starts, too low and click events on a scroll rect will not execute")]
        public float AngleDragThreshold = 1f;

        protected override void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("Multiple HVRInputModule components found. Deleting component from " + gameObject.name);
                Destroy(this);
            }

            Pointers = FindObjectsOfType<HVRUIPointer>().ToList();

            foreach (var pointer in Pointers)
            {
                pointer.PointerEventData = new PointerEventData(eventSystem);
            }
        }

        public void AddPointer(HVRUIPointer pointer)
        {
            if (Pointers == null)
            {
                Pointers = new List<HVRUIPointer>();
            }

            if (!Pointers.Contains(pointer))
            {
                Pointers.Add(pointer);
                pointer.PointerEventData = new PointerEventData(eventSystem);
            }
        }

        public void AddCanvas(Canvas canvas)
        {
            if (UICanvases.Contains(canvas))
                return;
            UICanvases.Add(canvas);
        }

        public void RemoveCanvas(Canvas canvas)
        {
            if (!UICanvases.Contains(canvas))
                return;
            UICanvases.Remove(canvas);
        }

        public override void Process()
        {
            for (var j = 0; j < Pointers.Count; j++)
            {
                var pointer = Pointers[j];
                if (!pointer || !pointer.isActiveAndEnabled)
                    continue;

                pointer.CurrentUIElement = null;

                for (var i = 0; i < UICanvases.Count; i++)
                {
                    var canvas = UICanvases[i];
                    if (!canvas)
                        continue;
                    canvas.worldCamera = pointer.Camera;
                }


                pointer.Process();


                SendUpdateEventToSelectedObject();

                eventSystem.RaycastAll(pointer.PointerEventData, m_RaycastResultCache);
                pointer.PointerEventData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);

                if (pointer.PointerEventData.pointerCurrentRaycast.distance > MaxDistance)
                {
                    continue;
                }

                m_RaycastResultCache.Clear();
                pointer.CurrentUIElement = pointer.PointerEventData.pointerCurrentRaycast.gameObject;

                var screenPosition = (Vector2)pointer.Camera.WorldToScreenPoint(pointer.PointerEventData.pointerCurrentRaycast.worldPosition);
                var delta = screenPosition - pointer.PointerEventData.position;
                pointer.PointerEventData.position = screenPosition;
                pointer.PointerEventData.delta = delta;

                var buttonState = HVRController.GetButtonState(pointer.HandSide, PressButton);

                HandlePointerExitAndEnter(pointer.PointerEventData, pointer.CurrentUIElement);

                if (buttonState.JustActivated)
                {
                    ProcessPress(pointer);
                }
                else if (buttonState.Active)
                {
                    ProcessDrag(pointer.PointerEventData);
                }
                else if (buttonState.JustDeactivated)
                {
                    ProcessRelease(pointer);
                }


                ProcessMove(pointer.PointerEventData);
                ProcessScroll(pointer.PointerEventData);
            }

            for (var i = 0; i < UICanvases.Count; i++)
            {
                var canvas = UICanvases[i];
                if (!canvas)
                    continue;
                canvas.worldCamera = null;
            }
        }

        private static void ProcessScroll(PointerEventData eventData)
        {
            if (!Mathf.Approximately(eventData.scrollDelta.sqrMagnitude, 0.0f))
            {
                var scrollHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.pointerCurrentRaycast.gameObject);
                ExecuteEvents.ExecuteHierarchy(scrollHandler, eventData, ExecuteEvents.scrollHandler);
            }
        }

        protected bool SendUpdateEventToSelectedObject()
        {
            if (eventSystem.currentSelectedGameObject == null)
                return false;

            var data = GetBaseEventData();
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.updateSelectedHandler);
            return data.used;
        }

        protected override void ProcessDrag(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            if (!eventData.dragging)
            {
                var startDrag = ShouldStartDrag(eventData);

                if (startDrag)
                {
                    ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.beginDragHandler);
                    eventData.dragging = true;
                }
                else
                {
                    return;
                }
            }

            if (eventData.pointerPress != eventData.pointerDrag)
            {
                ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);
                eventData.eligibleForClick = false;
                eventData.pointerPress = null;
                eventData.rawPointerPress = null;
            }
            ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.dragHandler);
        }

        private bool ShouldStartDrag(PointerEventData eventData)
        {
            if (!eventData.useDragThreshold)
                return true;

            var cameraPos = eventData.pressEventCamera.transform.position;
            var pressDir = (eventData.pointerPressRaycast.worldPosition - cameraPos).normalized;
            var currentDir = (eventData.pointerCurrentRaycast.worldPosition - cameraPos).normalized;
            return Vector3.Dot(pressDir, currentDir) < Mathf.Cos(Mathf.Deg2Rad * AngleDragThreshold);
        }

        private void ProcessPress(HVRUIPointer pointer)
        {
            var eventData = pointer.PointerEventData;
            eventData.eligibleForClick = true;
            eventData.delta = Vector2.zero;
            eventData.dragging = false;
            eventData.useDragThreshold = true;
            eventData.pressPosition = eventData.position;
            eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;

            DeselectIfSelectionChanged(pointer.CurrentUIElement, eventData);

            var pressed = ExecuteEvents.ExecuteHierarchy(pointer.CurrentUIElement, eventData, ExecuteEvents.pointerDownHandler);
            if (pressed == null)
            {
                pressed = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointer.CurrentUIElement);
            }

            var time = Time.unscaledTime;

            if (pressed == eventData.lastPress)
            {
                var diffTime = time - eventData.clickTime;
                if (diffTime < 0.3f)
                    ++eventData.clickCount;
                else
                    eventData.clickCount = 1;

                eventData.clickTime = time;
            }
            else
            {
                eventData.clickCount = 1;
            }

            eventData.pointerPress = pressed;
            eventData.rawPointerPress = pointer.CurrentUIElement;
            eventData.clickTime = Time.unscaledTime;
            eventData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(pointer.CurrentUIElement);
            if (eventData.pointerDrag != null)
                ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.initializePotentialDrag);
        }

        private void ProcessRelease(HVRUIPointer pointer)
        {
            var eventData = pointer.PointerEventData;

            ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);

            var handler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointer.CurrentUIElement);
            if (eventData.pointerPress == handler && eventData.eligibleForClick)
            {
                ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerClickHandler);
            }
            else if (eventData.pointerDrag != null && eventData.dragging)
            {
                ExecuteEvents.ExecuteHierarchy(pointer.CurrentUIElement, eventData, ExecuteEvents.dropHandler);
            }

            if (eventData.pointerDrag != null && eventData.dragging)
            {
                ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.endDragHandler);
            }

            eventData.eligibleForClick = false;
            eventData.dragging = false;
            eventData.pointerDrag = null;
            eventData.pressPosition = Vector2.zero;
            eventData.pointerPress = null;
            eventData.rawPointerPress = null;

            if (pointer.CurrentUIElement != eventData.pointerEnter)
            {
                HandlePointerExitAndEnter(eventData, null);
                HandlePointerExitAndEnter(eventData, pointer.CurrentUIElement);
            }
        }

    }
}
