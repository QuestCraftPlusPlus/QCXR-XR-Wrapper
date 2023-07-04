using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace YVR.Core
{
    public partial class YVRInputModule : PointerInputModule
    {
        /// <summary>
        ///  The base class of the input data provider
        /// </summary>
        public abstract class YVRInputDataBaseProvider
        {
            /// <summary>
            /// The origin point of the ray is rayTransform.position.
            /// The direction of the ray is rayTransform.forward.
            /// </summary>
            public abstract Transform rayTransform { get; }
            /// <summary>
            /// The direction of the ray.
            /// </summary>
            public virtual Vector3 rayDirection => rayTransform.forward;
            /// <summary>
            /// The origin point of the ray.
            /// </summary>
            public virtual Vector3 rayOrigin => rayTransform.position;
            /// <summary>
            /// The data of scroll
            /// </summary>
            public abstract Vector2 scrollData { get; }
            /// <summary>
            /// Pressed or not
            /// </summary>
            public abstract bool pressed { get; }
            /// <summary>
            /// Released or not
            /// </summary>
            public abstract bool released { get; }
            /// <summary>
            /// Whether to press for a long time
            /// </summary>
            public abstract bool longPressed { get; }
            /// <summary>
            ///  Whether thumbStick up
            /// </summary>
            public abstract bool thumbStickUp { get; }
            /// <summary>
            ///  Whether thumbStick down
            /// </summary>
            public abstract bool thumbStickDown { get; }

            /// <summary>
            /// Result detection of each frame of ray detection
            /// </summary>
            /// <param name="go">the object the raycast hit</param>
            public virtual void onRaycastHit(GameObject go) { }
        }
    }
}
