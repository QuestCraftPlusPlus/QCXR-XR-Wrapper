using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace HurricaneVR.Editor
{
    public class HVRSetupWindow : EditorWindow
    {
        private const string URLHurricaneReview = "https://assetstore.unity.com/packages/tools/physics/hurricane-vr-physics-interaction-toolkit-177300#reviews";
        private const string URLKofi = "https://ko-fi.com/cloudwalkingames";
        private const string URLPatreon = "https://www.patreon.com/user?u=46531723&fan_landing=true";

        private const string URLBasicGrabbableWithPose = "https://www.youtube.com/watch?v=HZQ6QdMmZ34";
        private const string URLVRIKIntegrationSetup = "https://cloudwalker2020.github.io/HurricaneVR-Docs/manual/integrations/finalik.html";
        private const string LatestNotes = "HVRLatestReleaseNotes";

        private const string URLManual = "https://cloudwalker2020.github.io/HurricaneVR-Docs/manual/intro.html";
        private const string URLHandGrabber = "https://cloudwalker2020.github.io/HurricaneVR-Docs/manual/hands.html";
        private const string URLSockets = "https://cloudwalker2020.github.io/HurricaneVR-Docs/manual/sockets.html";
        private const string URLSetup = "https://cloudwalker2020.github.io/HurricaneVR-Docs/manual/setup.html";
        private const string URLCustomHand = "https://cloudwalker2020.github.io/HurricaneVR-Docs/manual/hand_setup.html";
        private const string URLDoor = "https://cloudwalker2020.github.io/HurricaneVR-Docs/manual/components/door.html";
        private const string URLDiscord = "https://discord.com/invite/7QUXEcuwKY";

        private const string DEFINESteamVR = "HVR_STEAMVR";
        private const string DEFINEOculus = "HVR_OCULUS";
        //private const string DEFINEPun = "HVR_PUN";

        private static HVRSetupWindow _window;
        private VisualElement _root;

        private List<VisualElement> _panels = new List<VisualElement>();

        [MenuItem("Tools/HurricaneVR/Setup")]
        public static void ShowWindow()
        {
            _window = GetWindow<HVRSetupWindow>(true);
            _window.titleContent = new GUIContent("Hurricane v" + HVREditorManager.Version);
            _window.minSize = new Vector2(800f, 600f);
        }

#if CLOUDWALKER


        [Shortcut("Refresh Window", KeyCode.F9)]
        public static void Refresh()
        {
            if (_window)
            {
                _window.Close();
            }
            ShowWindow();
        }

#endif

        public void OnEnable()
        {
            _root = rootVisualElement;
            var visualTree = UnityEngine.Resources.Load<VisualTreeAsset>("HVRSetupWindow");
            _root.Add(visualTree.CloneTree());

            var projectSetupPanel = _root.Q<VisualElement>("ProjectSetupPanel");
            var notesPanel = _root.Q<VisualElement>("ReleaseNotesPanel");
            var aboutPanel = _root.Q<VisualElement>("AboutPanel");

            _panels.Add(projectSetupPanel);
            _panels.Add(notesPanel);
            _panels.Add(aboutPanel);

            _root.Q<Button>("BtnProjectSetup").clickable.clicked += () =>
            {
                UpdatePanel(projectSetupPanel);
            };


            _root.Q<Button>("BtnAbout").clickable.clicked += () =>
            {
                UpdatePanel(aboutPanel);
            };

            _root.Q<Button>("BtnReleaseNotes").clickable.clicked += () =>
            {
                UpdatePanel(notesPanel);
            };


            _root.Q<Button>("BtnSetupLayers").clickable.clicked += SetupLayers;
            _root.Q<Button>("BtnSetupPhysics").clickable.clicked += SetupPhysics;
            _root.Q<Button>("BtnSetupMatrix").clickable.clicked += SetupMatrix;

            var releaseNotes = _root.Q<TextElement>("TxtReleaseNotes");
            var releaseText = Resources.Load<TextAsset>(LatestNotes);
            if (releaseText)
            {
                releaseNotes.text = releaseText.text;
            }

            SetupUrl("BtnTutBasicGrabbable", URLBasicGrabbableWithPose);
            SetupUrl("BtnVRIKSetup", URLVRIKIntegrationSetup);

            SetupUrl("BtnReview", URLHurricaneReview);
            SetupUrl("BtnKofi", URLKofi);
            SetupUrl("btnDocs", URLManual);
            SetupUrl("btnSockets", URLSockets);
            SetupUrl("btnHandGrabber", URLHandGrabber);
            SetupUrl("btnDiscord", URLDiscord);
            SetupUrl("btnSetup", URLSetup);
            SetupUrl("btnCustomHand", URLCustomHand);
            SetupUrl("btnDoorSetup", URLDoor);
            //SetupUrl("BtnPatreon", URLPatreon);

            UpdatePanel(notesPanel);

            SetupDefineButton("BtnEnableSteamVR", "BtnDisableSteamVR", DEFINESteamVR);
            SetupDefineButton("BtnEnableOculus", "BtnDisableOculus", DEFINEOculus);
            //SetupDefineButton("BtnEnablePUN", "BtnDisablePUN", DEFINEPun);

            _root.Q<Button>("BtnExtractSteamVR").clickable.clicked += () =>
            {
                AssetDatabase.ImportPackage(Application.dataPath + "/HurricaneVR/Framework/Integrations/SteamVRIntegration.unitypackage", true);
            };

            _root.Q<Button>("BtnExtractOculus").clickable.clicked += () =>
            {
                AssetDatabase.ImportPackage(Application.dataPath + "/HurricaneVR/Framework/Integrations/OculusIntegration.unitypackage", true);
            };

            //_root.Q<Button>("BtnExtractPUN").clickable.clicked += () =>
            //{
            //    AssetDatabase.ImportPackage(Application.dataPath + "/HurricaneVR/Framework/Integrations/PUN2Integration.unitypackage", true);
            //};
        }

        private void SetupUrl(string buttonName, string url)
        {
            _root.Q<Button>(buttonName).clickable.clicked += () =>
            {
                Application.OpenURL(url);
            };
        }

        private void UpdatePanel(VisualElement panel)
        {
            foreach (var p in _panels)
            {
                p.style.display = DisplayStyle.None;
            }

            panel.style.display = DisplayStyle.Flex;
        }

        private void SetupPhysics()
        {
            UnityEngine.Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/DynamicsManager.asset");
            if (asset.Length == 0)
            {
                Debug.Log($"Couldn't load DynamicsManager.asset");
                return;
            }

            try
            {
                var so = new SerializedObject(asset[0]);

                var prop = so.FindProperty("m_DefaultSolverIterations");
                prop.intValue = 6;

                prop = so.FindProperty("m_DefaultSolverVelocityIterations");
                prop.intValue = 2;

                prop = so.FindProperty("m_DefaultMaxAngularSpeed");
                prop.floatValue = 30f;

                so.ApplyModifiedProperties();
                so.Update();

                EditorUtility.SetDirty(asset[0]);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                Debug.Log($"Dynamics Manager Updated");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void SetupMatrix()
        {
            UnityEngine.Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/DynamicsManager.asset");
            if (asset.Length == 0)
            {
                Debug.Log($"Couldn't load DynamicsManager.asset");
                return;
            }

            try
            {
                var so = new SerializedObject(asset[0]);

                SetLayer(HVRLayers.Player, HVRLayers.Grabbable);
                SetLayer(HVRLayers.Player, HVRLayers.Hand);
                SetLayer(HVRLayers.Player, HVRLayers.Player);

                SetLayer(HVRLayers.Grabbable, HVRLayers.Hand, false);
                SetLayer(HVRLayers.Grabbable, HVRLayers.Grabbable, false);
                SetLayer(HVRLayers.Hand, HVRLayers.Hand, false);

                so.ApplyModifiedProperties();
                so.Update();

                EditorUtility.SetDirty(asset[0]);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                Debug.Log($"Dynamics Manager Updated (Collision Matrix)");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void IgnoreAllBut(HVRLayers one, HVRLayers but)
        {
            var layerOne = LayerMask.NameToLayer(one.ToString());
            var layerTwo = LayerMask.NameToLayer(but.ToString());
            if (layerOne < 0)
            {
                Debug.LogWarning($"{one} layer does not exist.");
            }

            if (layerTwo < 0)
            {
                Debug.LogWarning($"{but} layer does not exist.");
            }

            if (layerOne < 0 || layerTwo < 0)
                return;

            for (int i = 0; i < 32; i++)
            {
                if (i == layerTwo)
                {
                    Physics.IgnoreLayerCollision(layerOne, i, false);
                }
                else
                {
                    Physics.IgnoreLayerCollision(layerOne, i, true);
                }
            }


        }

        private void SetLayer(HVRLayers one, HVRLayers two, bool ignore = true)
        {
            var layerOne = LayerMask.NameToLayer(one.ToString());
            var layerTwo = LayerMask.NameToLayer(two.ToString());
            if (layerOne < 0)
            {
                Debug.LogWarning($"{one} layer does not exist.");
            }

            if (layerTwo < 0)
            {
                Debug.LogWarning($"{two} layer does not exist.");
            }

            if (layerOne < 0 || layerTwo < 0)
                return;

            Physics.IgnoreLayerCollision(layerOne, layerTwo, ignore);
        }

        private void SetLayer(HVRLayers one, int layerTwo, bool ignore = true)
        {
            var layerOne = LayerMask.NameToLayer(one.ToString());
            
            if (layerOne < 0)
            {
                Debug.LogWarning($"{one} layer does not exist.");
            }

            if (layerOne < 0 || layerTwo < 0)
                return;

            Physics.IgnoreLayerCollision(layerOne, layerTwo, ignore);
        }

        private void SetupLayers()
        {
            var log = new StringBuilder();

            UnityEngine.Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");

            if ((asset != null) && (asset.Length > 0))
            {
                try
                {
                    var tagManager = asset[0];
                    SerializedObject serializedObject = new SerializedObject(asset[0]);
                    SerializedProperty layers = serializedObject.FindProperty("layers");

                    //TryShiftOldPlayerLayer(layers);

                    CleanUpOldDynamicPoseLayers(layers);

                    TryUpdateLayer(layers, HVRConstants.PlayerLayer, HVRLayers.Player.ToString(), log);
                    log.AppendLine("-----------------");
                    TryUpdateLayer(layers, HVRConstants.GrabbableLayer, HVRLayers.Grabbable.ToString(), log);
                    log.AppendLine("-----------------");
                    TryUpdateLayer(layers, HVRConstants.HandLayer, HVRLayers.Hand.ToString(), log);
                    log.AppendLine("-----------------");
                    TryUpdateDynamicPoseLayer(layers, log);

                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();

                    EditorUtility.SetDirty(tagManager);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    EditorUtility.DisplayDialog("Layer Report", log.ToString(), "Ok");

                    Debug.Log($"TagManager saved.");
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            else
            {
                Debug.LogWarning($"Unable to load ProjectSettings/TagManager.asset");
            }
        }

        //private static void TryShiftOldPlayerLayer(SerializedProperty layers)
        //{
        //    var playerOldValue = layers.GetArrayElementAtIndex(HVRConstants.OldPlayerLayer).stringValue;
        //    var playerNewValue = layers.GetArrayElementAtIndex(HVRConstants.PlayerLayer).stringValue;

        //    if (playerOldValue == HVRLayers.Player.ToString() && string.IsNullOrWhiteSpace(playerNewValue))
        //    {
        //        layers.GetArrayElementAtIndex(HVRConstants.PlayerLayer).stringValue = HVRLayers.Player.ToString();
        //        layers.GetArrayElementAtIndex(HVRConstants.OldPlayerLayer).stringValue = null;
        //    }
        //}

        private static void TryUpdateDynamicPoseLayer(SerializedProperty layers, StringBuilder log)
        {
            var dynamicPoseSet = false;

            for (int i = 8; i < layers.arraySize; ++i)
            {
                var layer = layers.GetArrayElementAtIndex(i).stringValue;

                if (layer == HVRLayers.DynamicPose.ToString())
                {
                    dynamicPoseSet = true;
                    break;
                }
            }

            if (!dynamicPoseSet)
            {
                for (int i = 9; i < layers.arraySize; ++i)
                {
                    if (string.IsNullOrWhiteSpace(layers.GetArrayElementAtIndex(i).stringValue))
                    {
                        layers.GetArrayElementAtIndex(i).stringValue = HVRLayers.DynamicPose.ToString();
                        log.AppendLine($"{HVRLayers.DynamicPose} assigned to layer {i}");
                        dynamicPoseSet = true;
                        break;
                    }
                }
            }

            if (!dynamicPoseSet)
            {
                log.AppendLine($"{HVRLayers.DynamicPose} layer could not be set. Make space for an additional layer and try again.");
            }
        }

        private static void CleanUpOldDynamicPoseLayers(SerializedProperty layers)
        {
            for (int i = 8; i < layers.arraySize; ++i)
            {
                var layer = layers.GetArrayElementAtIndex(i).stringValue;

                if (layer == "LeftTarget" || layer == "RightTarget")
                {
                    layers.GetArrayElementAtIndex(i).stringValue = null;
                }
            }
        }

        private void TryUpdateLayer(SerializedProperty layers, int layer, string layerName, StringBuilder log)
        {

            var anySpace = false;

            for (var i = 8; i < layers.arraySize; ++i)
            {
                var layerN = layers.GetArrayElementAtIndex(i).stringValue;

                if (string.IsNullOrWhiteSpace(layerN))
                {
                    anySpace = true;
                }
                
                if (i == layer)
                    continue;

                if (layerN == layerName)
                {
                    log.AppendLine($"{layerName} was found at position {i}. Intended position is {layer}. Remove it and try again.");
                    return;
                }
            }

            if (!anySpace)
            {
                log.AppendLine($"No layer space available for required layer '{layerName}'. Make space and try again.");
                return;
            }

            var property = layers.GetArrayElementAtIndex(layer);
            if (string.IsNullOrWhiteSpace(property.stringValue))
            {
                log.AppendLine($"{layerName} assigned to layer {layer}");
                property.stringValue = layerName;
            }
            else if (property.stringValue != layerName)
            {
                log.AppendLine($"{property.stringValue} was found at position {layer}. Intended layer is {layerName}. Remove and try again, or manually place {layerName} in another slot and update any {layerName} objects accordingly.");
            }
            else
            {
                log.AppendLine($"{layerName} already exists at slot {layer}");
            }
        }

        private void SetupDefineButton(string buttonNameEnable, string buttonNameDisable, string define)
        {
            _root.Q<Button>(buttonNameEnable).clickable.clicked += () =>
            {
                SetupDefine(define, true);
            };

            _root.Q<Button>(buttonNameDisable).clickable.clicked += () =>
            {
                SetupDefine(define, false);
            };
        }

        public void SetupDefine(string define, bool enable)
        {
            if (enable)
            {
                string dir;
                switch (define)
                {
                    case DEFINEOculus:
                        dir = $"Oculus";
                        break;
                    case DEFINESteamVR:
                        dir = $"SteamVR";
                        break;
                    //case DEFINEPun:
                    //    dir = "PUN";
                    //    break;
                    default:
                        return;
                }

                var path = Path.Combine(HVRSettings.Instance.GetRootFrameworkDirectory(), "Integrations");
                path = Path.Combine(path, dir);
                if (!Directory.Exists(path))
                {
                    EditorUtility.DisplayDialog("Error!", $"{path} does not exist. Extract the integration package before enabling the scripting define symbol", "Ok!");
                    return;
                }

            }

            var definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup).Trim();
            var allDefines = definesString.Split(';').ToList();

            if (enable)
            {
                allDefines.Add(define);
                allDefines = allDefines.Distinct().ToList();
            }
            else
            {
                var index = allDefines.IndexOf(define);
                if (index >= 0)
                {
                    allDefines.RemoveAt(index);
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", allDefines.ToArray()));
        }
    }


}