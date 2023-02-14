
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Android;


namespace YVR.Core.XR
{
    [InitializeOnLoad]
    public class YVRGradleGeneration: IPostGenerateGradleAndroidProject

    {
        public const string TOOL_NAME = "YVR/Tools/";
        private const string prefName= "VR_ONLY";
        private const string menuItemVROnly = TOOL_NAME + "VR mode";

        public static bool vrOnly = false;

        static YVRGradleGeneration()
        {
            EditorApplication.delayCall += OnDelayCall;
        }

        [MenuItem(menuItemVROnly)]
        static void ToggleVROnly()
        {
            vrOnly = !vrOnly;
            Menu.SetChecked(menuItemVROnly, vrOnly);

            int newValue = (vrOnly) ? 1 : 0;
            PlayerPrefs.SetInt(prefName, newValue);
            PlayerPrefs.Save();

            UnityEngine.Debug.Log("select vr only: " + vrOnly);
        }

        static void OnDelayCall()
        {
            var defaultValue = vrOnly ? 0 : 1;
            vrOnly = PlayerPrefs.GetInt(prefName, defaultValue) != 0;
            Menu.SetChecked(menuItemVROnly, vrOnly);

            UnityEngine.Debug.Log("OnDelayCall: " + vrOnly);
        }

        public void PatchAndroidManifest(string path)
        {
            string manifestFolder = Path.Combine(path, "src/main");
            string file = manifestFolder + "/AndroidManifest.xml";

            PatchAndroidManifest(vrOnly,file);
        }

        #region build callback
        int IOrderedCallback.callbackOrder => 9999;

        void IPostGenerateGradleAndroidProject.OnPostGenerateGradleAndroidProject(string path)
        {
            PatchAndroidManifest(path);
        }

        #endregion build callback


        #region XML

        public static void PatchAndroidManifest(bool vrOnly, string sourceFile, string destinationFile = null, bool skipExistingAttributes = true)
        {
            if (destinationFile == null)
            {
                destinationFile = sourceFile;
            }

            bool modifyIfFound = !skipExistingAttributes;

            try
            {
                // Load android manfiest file
                XmlDocument doc = new XmlDocument();
                doc.Load(sourceFile);

                XmlElement element = (XmlElement)doc.SelectSingleNode("/manifest");
                if (element == null)
                {
                    UnityEngine.Debug.LogError("Could not find manifest tag in android manifest.");
                    return;
                }

                // Get android namespace URI from the manifest
                string androidNamepsaceURI = element.GetAttribute("xmlns:android");
                if (string.IsNullOrEmpty(androidNamepsaceURI))
                {
                    UnityEngine.Debug.LogError("Could not find Android Namespace in manifest.");
                    return;
                }

                // make sure the VR Mode tag is set in the manifest
                if (vrOnly)
                {
                    AddOrRemoveTag(doc,
                        androidNamepsaceURI,
                        "/manifest/application",
                        "meta-data",
                        "com.yvr.application.mode",
                        modifyIfFound,
                       new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>( "value", "vr_only" ) });
                }

                doc.Save(destinationFile);
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }


        }

        public static void AddOrRemoveTag(XmlDocument doc, string namespaceURI, string path, string elementName, string name, bool modifyIfFound, List<KeyValuePair<string, string>> attrs)
        {
            var nodes = doc.SelectNodes($"{path}/{elementName}");
            XmlElement element = GetXmlElement(nodes, name, "name", namespaceURI);

            if (element == null)
            {
                var parent = doc.SelectSingleNode(path);
                element = doc.CreateElement(elementName);
                element.SetAttribute("name", namespaceURI, name);
                parent.AppendChild(element);
            }

            var toAddOrModifyAttrs = attrs.Where(keyValue => !string.IsNullOrEmpty(keyValue.Value) && (string.IsNullOrEmpty(element.GetAttribute(keyValue.Key, namespaceURI)) || modifyIfFound));
            toAddOrModifyAttrs.ToList().ForEach(attr => { element.SetAttribute(attr.Key, namespaceURI, attr.Value); });

            var toRemoveAttrs = attrs.Where(keyValue => string.IsNullOrEmpty(keyValue.Value) && !string.IsNullOrEmpty(element.GetAttribute(keyValue.Key, namespaceURI)));
            toRemoveAttrs.ToList().ForEach(attr => { element.RemoveAttribute(attr.Key, namespaceURI); });

        }

        private static XmlElement GetXmlElement(XmlNodeList nodes, string name, string localName, string namespaceURI)
        {
            return nodes.Cast<XmlElement>().FirstOrDefault(node => string.IsNullOrEmpty(name) || name == node.GetAttribute(localName, namespaceURI));
        }

        #endregion XML
    }

}
