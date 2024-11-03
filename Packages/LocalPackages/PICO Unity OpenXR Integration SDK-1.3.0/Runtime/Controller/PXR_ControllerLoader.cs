/*******************************************************************************
Copyright © 2015-2022 PICO Technology Co., Ltd.All rights reserved.  

NOTICE：All information contained herein is, and remains the property of 
PICO Technology Co., Ltd. The intellectual and technical concepts 
contained herein are proprietary to PICO Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
PICO Technology Co., Ltd. 
*******************************************************************************/

using System.Collections;
using System.IO;
using LitJson;
using Unity.XR.OpenXR.Features.PICOSupport;
using UnityEngine;
using UnityEngine.XR;

namespace Unity.XR.PXR
{
    public class PXR_ControllerLoader : MonoBehaviour
    {
        [SerializeField]
        private bool Lefthand;

        public GameObject neo3L;
        public GameObject neo3R;
        public GameObject PICO_4L;
        public GameObject PICO_4R;
        public GameObject G3;
        public GameObject PICO_4U_L;
        public GameObject PICO_4U_R;
        
        private Texture2D modelTexture2D;

        private int controllerType = -1;

        private JsonData curControllerData = null;
        private int systemOrLocal = 0;
        private bool loadModelSuccess = false;
        private string modelName = "";
        private string texFormat = "";
        private string prePath = "";
        private bool isconnect = false;

        private bool leftControllerState = false;
        private bool rightControllerState = false;

        private enum ControllerSimulationType
        {
            None,
            Neo3,
            PICO4,
            G3,
            PICO4U
        }
#if UNITY_EDITOR
        [SerializeField]
        private ControllerSimulationType controllerSimulation = ControllerSimulationType.None;
#endif

        private InputDevice currentController;
        void Awake()
        {
           
#if UNITY_EDITOR
            switch (controllerSimulation)
            {
                case ControllerSimulationType.Neo3:
                    {
                        Instantiate(Lefthand ? neo3L : neo3R, transform, false);
                        break;
                    }
                case ControllerSimulationType.PICO4:
                    {
                        Instantiate(Lefthand ? PICO_4L : PICO_4R, transform, false);
                        break;
                    }
                case ControllerSimulationType.G3:
                    {
                        Instantiate(G3, transform, false);
                        break;
                    }
                case ControllerSimulationType.PICO4U:
                    {
                        Instantiate(Lefthand ? PICO_4U_L : PICO_4U_R, transform, false);
                        break;
                    }
            }
#endif
        }

        void Start()
        {
            controllerType = OpenXRExtensions.GetControllerType();
            InputDevices.GetDeviceAtXRNode(Lefthand
                ? XRNode.LeftHand
                : XRNode.RightHand).TryGetFeatureValue(CommonUsages.isTracked, out isconnect);
            if (isconnect)
            {
                RefreshController();
            }
    
            InvokeRepeating("CheckConnect", 0, 0.5f);
        }

        void CheckConnect()
        {
            InputDevices.GetDeviceAtXRNode(Lefthand
                ? XRNode.LeftHand
                : XRNode.RightHand).TryGetFeatureValue(CommonUsages.isTracked, out isconnect);
            if (isconnect)
            {
                RefreshController();
            }
            else
            {
                DestroyLocalController();
            }
        }

        private void RefreshController()
        {
            if (!loadModelSuccess)
            {
                LoadControllerFromPrefab(Lefthand);
            }
           
            if (loadModelSuccess)
            {
                var currentController = transform.Find(modelName);
                currentController.gameObject.SetActive(true);
            }
            
        }
        
        private void DestroyLocalController()
        {
            if (loadModelSuccess)
            {
                var currentController = transform.Find(modelName);
                currentController.gameObject.SetActive(false);
            }
        }

        private void LoadControllerFromPrefab(bool hand)
        {
            GameObject go;
            switch (controllerType)
            {
                case 5:
                    go= Instantiate(hand ? neo3L : neo3R, transform, false);
                    go.name=modelName = (hand ? neo3L : neo3R).name;
                    loadModelSuccess = true;
                    break;
                case 6:
                    go= Instantiate(hand  ? PICO_4L : PICO_4R, transform, false);
                    go.name=modelName = (hand ? PICO_4L : PICO_4R).name;
                    loadModelSuccess = true;
                    break;
                case 7:
                    go=Instantiate(G3, transform, false);
                    go.name=modelName = G3.name;
                    loadModelSuccess = true;
                    break;
                case 8:
                    go=Instantiate(hand ? PICO_4U_L : PICO_4U_R, transform, false);
                    go.name=modelName = (hand ? PICO_4U_L : PICO_4U_R).name;
                    loadModelSuccess = true;
                    break;
                default:
                    loadModelSuccess = false;
                    break;
            }
        }
        
       
        
    }
}

