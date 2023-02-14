using UnityEngine;
using UnityEngine.UI;

namespace HurricaneVR.Framework.Core.Utils
{
    /// <summary>
    /// Debug Tool for viewing logs in VR
    /// </summary>
    public class DebugVR : MonoBehaviour
    {
        public static DebugVR Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<DebugVR>();

                    if (_instance == null)
                    {
                        _instance = new GameObject("DebugVR").AddComponent<DebugVR>();
                    }
                }
                return _instance;
            }
        }

        private static DebugVR _instance;

        public Transform DebugPanel;

        float MaxTextEntries = 15;
        string LastDebugMsg;
        int lastDebugMsgCount;

        void Awake()
        {

            if (_instance == null)
                _instance = this;
            else
                Destroy(this);
        }

        /// <summary>
        /// Logs Debug messages
        /// </summary>
        /// <param name="msg"></param>
        public void Log(string msg)
        {
            Debug.Log(msg, gameObject);
            VRDebugLog(msg, Color.white);
        }

        /// <summary>
        /// Logs debug messages with input color
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="textColor"></param>
        public void Log(string msg, Color textColor)
        {
            Debug.Log(msg, gameObject);
            VRDebugLog(msg, textColor);
        }

        /// <summary>
        /// Creates text objects at runtime
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="textColor"></param>
        private void VRDebugLog(string msg, Color textColor)
        {
            // Add to Holder if available
            if (DebugPanel != null)
            {
                if (msg == LastDebugMsg)
                {
                    GameObject lastChild = DebugPanel.GetChild(DebugPanel.childCount - 1).gameObject;
                    Text lastChildLine = lastChild.GetComponent<Text>();
                    lastDebugMsgCount++;

                    lastChildLine.text = $"({lastDebugMsgCount}) {msg}";
                }
                else
                {
                    GameObject Obj = new GameObject();
                    Obj.transform.parent = DebugPanel;
                    Obj.transform.localPosition = Vector3.zero;
                    Obj.transform.localScale = Vector3.one;
                    Obj.transform.name = "DebugVR Text";

                    Text tempText = Obj.AddComponent<Text>();
                    tempText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                    tempText.color = textColor;
                    tempText.fontSize = 2;
                    tempText.horizontalOverflow = HorizontalWrapMode.Wrap;
                    tempText.verticalOverflow = VerticalWrapMode.Overflow;
                    tempText.text = msg;

                    RectTransform rect = Obj.GetComponent<RectTransform>();
                    rect.localScale = Vector3.one;
                    rect.localRotation = Quaternion.identity;

                    lastDebugMsgCount = 1;
                }

                LastDebugMsg = msg;
                DeleteOldEntries();
            }
        }

        /// <summary>
        /// Starts deleting old enteries when reached max entires
        /// </summary>
        public void DeleteOldEntries()
        {
            for (int i = DebugPanel.childCount; i > MaxTextEntries; i--)
            {
                Destroy(DebugPanel.GetChild(0).gameObject);
            }
        }

    }
}