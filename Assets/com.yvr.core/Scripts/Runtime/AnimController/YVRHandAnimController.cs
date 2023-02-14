using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using YVR.Core;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public partial class YVRHandAnimController : MonoBehaviour
    {
        [SerializeField]
        private ControllerType controllerType = ControllerType.None;
        [SerializeField]
        private Animator handAnimator = null;
        [SerializeField]
        private Animator controllerAnimator = null;

        private YVRHandControllerAnimData animData;

        private float delayTimer = 0;

        private void Start()
        {
            animData = new YVRHandControllerAnimData();
            anim_param_Id_Joy_X = Animator.StringToHash(ANIM_PARAM_NAME_JOY_X);
            anim_param_Id_Joy_Y = Animator.StringToHash(ANIM_PARAM_NAME_JOY_Y);
            anim_param_Id_OneButton = Animator.StringToHash(ANIM_PARAM_NAME_ONEBUTTON);
            anim_param_Id_TwoButton = Animator.StringToHash(ANIM_PARAM_NAME_TWOBUTTON);
            anim_param_Id_IndexTrigger = Animator.StringToHash(ANIM_PARAM_NAME_INDEXTRIGGER);
            anim_param_Id_IsTouchIndexTrigger = Animator.StringToHash(ANIM_PARAM_NAME_ISTOUCHINDEXTRIGGER);
            anim_param_Id_HandTrigger = Animator.StringToHash(ANIM_PARAM_NAME_HANDTRIGGER);
            anim_param_Id_ThumbstickTouch = Animator.StringToHash(ANIM_PARAM_NAME_THUMBSTICKTOUCH);
            anim_param_Id_OneTouch = Animator.StringToHash(ANIM_PARAM_NAME_ONETOUCH);
            anim_param_Id_TwoTouch = Animator.StringToHash(ANIM_PARAM_NAME_TWOTOUCH);
            anim_param_Id_DefaultBlendButton = Animator.StringToHash(ANIM_PARAM_NAME_DEF_BLEND_BUTTON);
            anim_param_Id_TouchStickBlendButton = Animator.StringToHash(ANIM_PARAM_NAME_TOUCHSTICKBLENDBUTTON);

            YVRManager.instance.eventsManager.onUpdate += UpdateAnimation;
        }

        private void UpdateAnimation()
        {
            UpdateAnimData();
            UpdateAnimStates();
        }

        private void UpdateAnimData()
        {
            UpdateAnimParamValue(YVRInput.Get(YVRInput.VirtualTouch.One, controllerType), ref animData.oneTouchBlend);
            UpdateAnimParamValue(YVRInput.Get(YVRInput.VirtualTouch.Two, controllerType), ref animData.twoTouchBlend);

            animData.oneTouchBlend = YVRInput.Get(YVRInput.VirtualButton.One, controllerType) ? 1 : animData.oneTouchBlend;
            animData.twoTouchBlend = YVRInput.Get(YVRInput.VirtualButton.Two, controllerType) ? 1 : animData.twoTouchBlend;
            float targetButtonOneValue = YVRInput.Get(YVRInput.VirtualButton.One, controllerType) ? 2 : animData.oneTouchBlend;
            float targetButtonTwoValue = YVRInput.Get(YVRInput.VirtualButton.Two, controllerType) ? 2 : animData.twoTouchBlend;

            // Target value for button one/two is dependent on the button state
            // If button is touched, the target value should be 1, if button is down, the target value should be two, otherwise to be 0
            UpdateAnimParamValue(targetButtonOneValue != 0, ref animData.onePressBlend, 0, targetButtonOneValue);
            UpdateAnimParamValue(targetButtonTwoValue != 0, ref animData.twoPressBlend, 0, targetButtonTwoValue);

            // The "thumbstick touch" and "one/two button" animation is mutually exclusive
            // Thus only update thumbstick touch animation while one/two button is not used
            bool usingOneTwoButton = (animData.onePressBlend + animData.twoPressBlend) > 0.001f;
            bool isTouchTumbstick = YVRInput.Get(YVRInput.VirtualTouch.Thumbstick, controllerType);
            UpdateAnimParamValue(isTouchTumbstick && !usingOneTwoButton, ref animData.thumbstickTouchBlend);

            UpdateAnimParamValue(!isTouchTumbstick, ref animData.defaultBlendButton);

            // delay of 0.1 second predicts that the "one" or "two" button will be pressed for better transition
            delayTimer = YVRInput.GetUp(YVRInput.VirtualTouch.Thumbstick, controllerType) ? Time.time + 0.1f : delayTimer;
            if (delayTimer > Time.time)
            {
                animData.defaultBlendButton = 0;
                animData.thumbstickTouchBlend = usingOneTwoButton ? animData.thumbstickTouchBlend : 1;
                animData.oneTouchBlend = animData.oneTouchBlend > 0.01f ? 1 : animData.oneTouchBlend;
                animData.twoTouchBlend = animData.twoTouchBlend > 0.01f ? 1 : animData.twoTouchBlend;
            }

            // DefaultBlendButton and touchStickBlendButton blend between one/two button animation tree and thumbstick touch animation tree
            animData.touchStickBlendButton = usingOneTwoButton ? 1 - animData.defaultBlendButton : 0;

            animData.joy = YVRInput.Get(YVRInput.VirtualAxis2D.Thumbstick, controllerType);
            animData.indexTriggerBlend = YVRInput.Get(YVRInput.VirtualAxis1D.IndexTrigger, controllerType);
            animData.isIndexTriggerTouch = YVRInput.Get(YVRInput.VirtualTouch.IndexTrigger, controllerType) || animData.indexTriggerBlend >= 0.01f;

            UpdateAnimParamValue(YVRInput.Get(YVRInput.VirtualButton.HandTrigger, controllerType), ref animData.handTriggerBlend);
        }

        private void UpdateAnimStates()
        {
            //thumb
            handAnimator.SetFloat(anim_param_Id_ThumbstickTouch, animData.thumbstickTouchBlend);
            handAnimator.SetFloat(anim_param_Id_DefaultBlendButton, animData.defaultBlendButton);
            handAnimator.SetFloat(anim_param_Id_TouchStickBlendButton, animData.touchStickBlendButton);
            handAnimator.SetFloat(anim_param_Id_OneTouch, animData.oneTouchBlend);
            handAnimator.SetFloat(anim_param_Id_TwoTouch, animData.twoTouchBlend);
            handAnimator.SetFloat(anim_param_Id_OneButton, animData.onePressBlend);
            handAnimator.SetFloat(anim_param_Id_TwoButton, animData.twoPressBlend);
            handAnimator.SetFloat(anim_param_Id_Joy_X, animData.joy.x);
            handAnimator.SetFloat(anim_param_Id_Joy_Y, animData.joy.y);

            controllerAnimator.SetFloat(anim_param_Id_ThumbstickTouch, animData.thumbstickTouchBlend);
            controllerAnimator.SetFloat(anim_param_Id_OneTouch, animData.oneTouchBlend);
            controllerAnimator.SetFloat(anim_param_Id_TwoTouch, animData.twoTouchBlend);
            controllerAnimator.SetFloat(anim_param_Id_OneButton, animData.onePressBlend);
            controllerAnimator.SetFloat(anim_param_Id_TwoButton, animData.twoPressBlend);
            controllerAnimator.SetFloat(anim_param_Id_Joy_X, animData.joy.x);
            controllerAnimator.SetFloat(anim_param_Id_Joy_Y, animData.joy.y);
            //index
            handAnimator.SetBool(anim_param_Id_IsTouchIndexTrigger, animData.isIndexTriggerTouch);
            handAnimator.SetFloat(anim_param_Id_IndexTrigger, animData.indexTriggerBlend);
            controllerAnimator.SetFloat(anim_param_Id_IndexTrigger, animData.indexTriggerBlend);
            //middle
            handAnimator.SetFloat(anim_param_Id_HandTrigger, animData.handTriggerBlend);
            controllerAnimator.SetFloat(anim_param_Id_HandTrigger, animData.handTriggerBlend);
        }

        private void UpdateAnimParamValue(bool symbol, ref float value, float min = 0, float max = 1)
        {
            float rateDelta = Time.deltaTime * INPUT_RATE_CHANGE;
            float sign = symbol ? 1.0f : -1.0f;
            value = Mathf.Clamp(value + rateDelta * sign, min, max);
        }

        private void OnDestroy() { YVRManager.instance.eventsManager.onUpdate -= UpdateAnimation; }
    }
}