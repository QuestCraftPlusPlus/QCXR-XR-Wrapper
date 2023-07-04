using UnityEngine;

namespace YVR.Core
{
    public class HandInteractEffectController : MonoBehaviour
    {
        public GameObject pointer;
        public HandType handType;
        public YVRHand yvrHand;
        public SkinnedMeshRenderer handSkinnedMeshRenderer;
        private MaterialPropertyBlock m_HandMaterialPropertyBlock;
        private MaterialPropertyBlock m_PointerMaterialPropertyBlock;
        private MeshRenderer m_PointerMeshRenderer;
        private float m_CompressLimit = 0.48f;
        private float m_ScaleLimit = 0.2f;
        private float m_IndexFingerPinchStrength;
        private float m_IndexFingerPinchStrengthLimit = 0.9f;
        private float m_SoftMinValue = 0.319f;
        private float m_SoftMaxValue = 1.54f;
        private string m_SoftMin = "_SoftMin";
        private string m_SoftMax = "_SoftMax";
        private string m_ForceState = "_ForceState";
        private float m_ForceStateMax = 1;
        private float m_PinchFactor;
        private float m_PointerZOffset = 0.03f;
        private HandJointLocations m_HandJointLocations;

        private void Start()
        {
            m_PointerMaterialPropertyBlock = new MaterialPropertyBlock();
            m_HandMaterialPropertyBlock = new MaterialPropertyBlock();
            m_PointerMeshRenderer = pointer.GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            m_HandJointLocations = handType == HandType.HandLeft
                ? YVRHandManager.instance.leftHandData
                : YVRHandManager.instance.rightHandData;
            m_IndexFingerPinchStrength = YVRHandManager.instance.GetFingerPinchStrength(handType, XRHandFingerID.Index);
            m_PinchFactor = (1 - Mathf.Clamp(m_IndexFingerPinchStrength - m_CompressLimit, 0, 1 - m_CompressLimit) /
                (1 - m_CompressLimit));
            UpdatePointerPose();
            UpdateHandEffect();
        }

        private void UpdatePointerPose()
        {
            if (pointer == null) return;

            if ((((HandStatus)m_HandJointLocations.aimState.status & HandStatus.InputStateValid) != 0) &&
                m_HandJointLocations.isActive == 1)
            {
                pointer.SetActive(true);
                Vector3 pointerPosition =
                    (2 * yvrHand.handJoints[(int)HandJoint.JointThumbTip].position +
                     yvrHand.handJoints[(int)HandJoint.JointIndexTip].position) / 3;
                pointer.transform.position = pointerPosition + pointer.transform.up * m_PointerZOffset;
                if (m_IndexFingerPinchStrength >= m_CompressLimit)
                {
                    float pointerScale = m_ScaleLimit + m_PinchFactor;
                    pointer.transform.localScale = new Vector3(pointerScale, 1, pointerScale);
                    float softMin = m_IndexFingerPinchStrength >= m_IndexFingerPinchStrengthLimit
                        ? 0
                        : m_SoftMinValue * m_PinchFactor;
                    float softMax = m_IndexFingerPinchStrength >= m_IndexFingerPinchStrengthLimit
                        ? 0
                        : m_SoftMaxValue * m_PinchFactor;
                    m_PointerMaterialPropertyBlock.SetFloat(m_SoftMin, softMin);
                    m_PointerMaterialPropertyBlock.SetFloat(m_SoftMax, softMax);
                    m_PointerMeshRenderer.SetPropertyBlock(m_PointerMaterialPropertyBlock);
                }
                else
                {
                    float pointerScale = Mathf.Clamp(pointer.transform.localScale.x + Time.deltaTime, 0, 1);
                    pointer.transform.localScale = new Vector3(pointerScale, 1, pointerScale);
                    m_PointerMaterialPropertyBlock.SetFloat(m_SoftMin, m_SoftMinValue);
                    m_PointerMaterialPropertyBlock.SetFloat(m_SoftMax, m_SoftMaxValue);
                    m_PointerMeshRenderer.SetPropertyBlock(m_PointerMaterialPropertyBlock);
                }
            }
            else
            {
                pointer.SetActive(false);
            }
        }

        private void UpdateHandEffect()
        {
            if (handSkinnedMeshRenderer == null) return;

            if (m_IndexFingerPinchStrength >= m_CompressLimit)
            {
                m_HandMaterialPropertyBlock.SetFloat(m_ForceState, (1 - m_PinchFactor) * m_ForceStateMax);
            }
            else
            {
                float forceValue = Mathf.Clamp(m_HandMaterialPropertyBlock.GetFloat(m_ForceState) - Time.deltaTime * 2,
                    0, m_ForceStateMax);
                m_HandMaterialPropertyBlock.SetFloat(m_ForceState, forceValue);
            }

            handSkinnedMeshRenderer.SetPropertyBlock(m_HandMaterialPropertyBlock);
        }
    }
}