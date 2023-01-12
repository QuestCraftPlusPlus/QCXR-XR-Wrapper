using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YVR.Core;

namespace YVR.Core.Demo
{
    public class RaycastTest : MonoBehaviour
    {
        public Button buttonClick;
        public Slider sliderDrag;
        public ScrollRect scrollRect;
        public bool clickMark = false;
        private string m_TargetGameObjectName = "TargetCube";

        private void Start()
        {
            buttonClick.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            clickMark = true;
        }

        public bool RaycastHitGameObject()
        {
            return YVRInputModule.Instance.raycastGameObject?.name == m_TargetGameObjectName;
        }
    }
}
