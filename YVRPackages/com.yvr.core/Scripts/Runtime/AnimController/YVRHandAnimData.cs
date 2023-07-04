using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core
{
    public partial class YVRHandAnimController : MonoBehaviour
    {
        private const string ANIM_PARAM_NAME_THUMBSTICKTOUCH = "ThumbstickTouch";
        private const string ANIM_PARAM_NAME_DEF_BLEND_BUTTON = "DefaultBlendButton";
        private const string ANIM_PARAM_NAME_JOY_X = "JoyX";
        private const string ANIM_PARAM_NAME_JOY_Y = "JoyY";
        private const string ANIM_PARAM_NAME_ONETOUCH = "OneTouch";
        private const string ANIM_PARAM_NAME_TWOTOUCH = "TwoTouch";
        private const string ANIM_PARAM_NAME_ONEBUTTON = "OneButton";
        private const string ANIM_PARAM_NAME_TWOBUTTON = "TwoButton";
        private const string ANIM_PARAM_NAME_ISTOUCHINDEXTRIGGER = "IsTouchIndexTrigger";
        private const string ANIM_PARAM_NAME_INDEXTRIGGER = "IndexTrigger";
        private const string ANIM_PARAM_NAME_HANDTRIGGER = "HandTrigger";
        private const string ANIM_PARAM_NAME_TOUCHSTICKBLENDBUTTON = "TouchStickBlendButton";
        private const float INPUT_RATE_CHANGE = 10f;

        private int anim_param_Id_ThumbstickTouch = -1;
        private int anim_param_Id_DefaultBlendButton = -1;
        private int anim_param_Id_TouchStickBlendButton = -1;
        private int anim_param_Id_Joy_X = -1;
        private int anim_param_Id_Joy_Y = -1;
        private int anim_param_Id_OneTouch = -1;
        private int anim_param_Id_TwoTouch = -1;
        private int anim_param_Id_OneButton = -1;
        private int anim_param_Id_TwoButton = -1;
        private int anim_param_Id_IsTouchIndexTrigger = -1;
        private int anim_param_Id_IndexTrigger = -1;
        private int anim_param_Id_HandTrigger = -1;

        [ExcludeFromDocs]
        public struct YVRHandControllerAnimData
        {
            public float thumbstickTouchBlend;
            public Vector2 joy;
            public float oneTouchBlend;
            public float twoTouchBlend;
            public float onePressBlend;
            public float twoPressBlend;
            public float indexTriggerBlend;
            public float handTriggerBlend;
            public float defaultBlendButton;
            public float touchStickBlendButton;
            public bool isIndexTriggerTouch;
        }
    }
}