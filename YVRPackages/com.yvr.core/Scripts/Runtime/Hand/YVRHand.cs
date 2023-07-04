using System.Collections.Generic;
using UnityEngine;

namespace YVR.Core
{
    public class YVRHand : MonoBehaviour
    {
        public HandType handType;

        public List<Transform> handJoints = new List<Transform>(new Transform[(int)HandJoint.JointMax]);
        private HandJointLocations m_HandJointLocations = new HandJointLocations();
        public Transform pointerPose;
        private SkinnedMeshRenderer m_SkinnedMeshRenderer;
        private GameObject m_PointerPoseGO;

        private void Awake()
        {
            m_SkinnedMeshRenderer = this.transform.GetComponentInChildren<SkinnedMeshRenderer>();
        }

        public void UpdateHandTransform()
        {
            m_HandJointLocations = handType == HandType.HandLeft
                ? YVRHandManager.instance.leftHandData
                : YVRHandManager.instance.rightHandData;
            UpdateHandJoints();
            UpdatePointerPose();
        }

        private void UpdateHandJoints()
        {
            if (m_HandJointLocations.isActive != 0)
            {
                if (!m_SkinnedMeshRenderer.gameObject.activeSelf)
                    m_SkinnedMeshRenderer.gameObject.SetActive(true);
                for (int i = 0; i < handJoints.Count; ++i)
                {
                    if (handJoints[i] == null) continue;

                    if (i == (int)HandJoint.JointWrist)
                    {
#if UNITY_2021_3_OR_NEWER
                        handJoints[i].SetLocalPositionAndRotation(m_HandJointLocations.jointLocations[i].pose.position,
                            m_HandJointLocations.jointLocations[i].pose.orientation);
#else
                        handJoints[i].localPosition = m_HandJointLocations.jointLocations[i].pose.position;
                        handJoints[i].localRotation = m_HandJointLocations.jointLocations[i].pose.orientation;
#endif

                        if (handJoints[i].localScale != Vector3.one * m_HandJointLocations.handScale)
                        {
                            handJoints[i].localScale = Vector3.one * m_HandJointLocations.handScale;
                        }
                    }
                    else
                    {
                        handJoints[i].rotation = m_HandJointLocations.jointLocations[i].pose.orientation;
                    }
                }
            }
            else
            {
                if (m_SkinnedMeshRenderer != null && m_SkinnedMeshRenderer.gameObject.activeSelf)
                    m_SkinnedMeshRenderer.gameObject.SetActive(false);
            }
        }

        private void UpdatePointerPose()
        {
            if (m_HandJointLocations.isActive == 0 || (m_HandJointLocations.aimState.status & HandStatus.InputStateValid) == 0) return;

            pointerPose.localPosition = m_HandJointLocations.aimState.aimPose.position.FromFlippedZVector3f();
            pointerPose.rotation = m_HandJointLocations.aimState.aimPose.orientation.FromFlippedZQuatf();
        }
    }
}