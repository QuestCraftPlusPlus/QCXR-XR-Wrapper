using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// The manager of requests and messages
    /// </summary>
    public class YVRCallback
    {
        #region Request
        private static Dictionary<int, YVRRequest> dicRequestIDsToRequests = new Dictionary<int, YVRRequest>();

        internal static void AddRequest(YVRRequest request)
        {
            if (request.requestID == 0)
            {
                Debug.LogErrorFormat("[YVRPlatform] This request is invalid. Request failed. Request ID: " + request.requestID);
                return;
            }

            dicRequestIDsToRequests[request.requestID] = request;
        }
        #endregion

        #region Notification
        private class NotificationCallback
        {
            private YVRMessage.Callback callback;

            public NotificationCallback() { }

            public NotificationCallback(YVRMessage.Callback cb)
            {
                this.callback = cb;
            }

            public virtual void HandleMessage(YVRMessage msg)
            {
                if (this.callback != null)
                    this.callback(msg);
            }
        }

        private class NotificationCallback<T> : NotificationCallback
        {
            private YVRMessage<T>.Callback callback;

            public NotificationCallback(YVRMessage<T>.Callback cb)
            {
                this.callback = cb;
            }

            public override void HandleMessage(YVRMessage msg)
            {
                if (this.callback != null)
                {
                    if (msg is YVRMessage<T>)
                    {
                        this.callback((YVRMessage<T>)msg);
                    }
                    else
                    {
                        Debug.LogError("[YVRPlatform] Unable to handle message: " + msg.GetType());
                    }
                }
            }
        }

        private static Dictionary<string, NotificationCallback> dicNotificationCallbacks = new Dictionary<string, NotificationCallback>();

        internal static void SetNotificationCallback(string type, YVRMessage.Callback callback)
        {
            if (callback == null)
            {
                throw new Exception("[YVRPlatform] Cannot provide a null notification callback.");
            }

            dicNotificationCallbacks[type] = new NotificationCallback(callback);
        }

        internal static void SetNotificationCallback<T>(string type, YVRMessage<T>.Callback callback)
        {
            if (callback == null)
            {
                throw new Exception("[YVRPlatform] Cannot provide a null notification callback.");
            }

            dicNotificationCallbacks[type] = new NotificationCallback<T>(callback);
        }
        #endregion

        /// <summary>
        /// This function pops all messages at one time.
        /// </summary>
        internal static void RunCallbacks()
        {
            while (true)
            {
                YVRMessage msg = YVRMessage.PopMessage();

                if (msg == null)
                {
                    break;
                }

                HandleMessage(msg);
            }
        }

        /// <summary>
        /// This function pops messages no more than limit at one time.
        /// </summary>
        /// <param name="limit"></param>
        internal static void RunCallbacks(uint limit)
        {
            for (int i = 0; i < limit; i++)
            {
                YVRMessage msg = YVRMessage.PopMessage();

                if (msg == null)
                {
                    break;
                }

                HandleMessage(msg);
            }
        }

        internal static void HandleMessage(YVRMessage msg)
        {
            YVRRequest req;
            NotificationCallback callbackHolder;
            if (msg.requestID != 0 && dicRequestIDsToRequests.TryGetValue(msg.requestID, out req))
            {
                req.HandleMessage(msg);

                dicRequestIDsToRequests.Remove(msg.requestID);
            }
            else if (dicNotificationCallbacks.TryGetValue(msg.type, out callbackHolder))
            {
                callbackHolder.HandleMessage(msg);
            }
            else
            {
                Debug.LogErrorFormat("[YVRPlatform] This request ID is not recorded. Request ID: " + msg.requestID);
            }
        }

        internal static void OnApplicationQuit()
        {
            dicRequestIDsToRequests.Clear();
        }
    }
}