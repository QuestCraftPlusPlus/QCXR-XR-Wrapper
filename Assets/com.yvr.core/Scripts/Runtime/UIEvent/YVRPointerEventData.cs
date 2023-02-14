using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace YVR.Core
{
    /// <summary>
    /// Almost the same with UnityEngin.EventSystems.PointerEventData,
    /// expects that a new Ray fields indicats the ray used for interacting
    /// </summary>
    public class YVRPointerEventData : PointerEventData
    {
        /// <summary>
        /// Indicating the ray used for interaction
        /// </summary>
        public Ray worldSpaceRay = new Ray();

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="eventSystem"> eventSystem in PointerEventData </param>
        public YVRPointerEventData(EventSystem eventSystem) : base(eventSystem) { }
    }

}