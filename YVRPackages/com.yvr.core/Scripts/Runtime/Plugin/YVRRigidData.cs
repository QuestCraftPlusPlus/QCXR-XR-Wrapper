using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public enum DeviceNode
    {
        /// <summary>
        ///
        /// </summary>
        None = -1,
        /// <summary>
        /// Node representing the left hand.
        /// </summary>
        ControllerLeft,
        /// <summary>
        /// Node representing the right hand.
        /// </summary>
        ControllerRight,
        /// <summary>
        /// Node representing a point between the left and right eyes.
        /// </summary>
        EyeCenter,
        /// <summary>
        ///  Node representing the left eye.
        /// </summary>
        EyeLeft,
        /// <summary>
        ///  Node representing the right eye.
        /// </summary>
        EyeRight,
        /// <summary>
        /// The Count of DeviceNode
        /// </summary>
        Count
    }
    [ExcludeFromDocs]
    public enum Step
    {
        Render = -1,
        Physics = 0,
    }

    [ExcludeFromDocs]
    [StructLayout(LayoutKind.Sequential)]
    public struct PoseData
    {
        public Quaternion orientation;
        public Vector3 position;
    }

    [ExcludeFromDocs]
    public enum YVREventType
    {
        /// <summary>
        /// No event
        /// </summary>
        None = 0,
        /// <summary>
        /// The application's frames are visible to the user.
        /// </summary>
        VisibilityGained = 2,
        /// <summary>
        /// The application's frames are no longer visible to the user.
        /// </summary>
        VisibilityLost = 3,
        /// <summary>
        /// The current activity is in the foreground and has input focus.
        /// </summary>
        FocusGained = 4,
        /// <summary>
        /// The current activity is in the background (but possibly still visible) and has lost input focus.
        /// </summary>
        FocusLost = 5,
        /// <summary>
        /// The display refresh rate has changed
        /// </summary>
        RefreshRate = 6,
        /// <summary>
        /// recenter occurred
        /// </summary>
        Recenter = 20,
        /// <summary>
        /// Frame will be submitted soon
        /// </summary>
        PreSubmit = 100,
        /// <summary>
        /// Frame has just been submitted
        /// </summary>
        PostSubmit = 101,
        /// <summary>
        /// The input device has changed
        /// </summary>
        InputDeviceChange = 200
    }
}