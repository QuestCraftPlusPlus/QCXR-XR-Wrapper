using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Base class of yvrrequest class
    /// </summary>
    public class YVRRequest
    {
        /// <summary>
        /// Request ID
        /// </summary>
        public int requestID { protected set; get; }

        private YVRMessage.Callback callback;

        internal YVRRequest(int requestID) { Debug.Log("this request ID is : " + requestID); this.requestID = requestID; }

        /// <summary>
        /// Add callback function to this request
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public YVRRequest OnComplete(YVRMessage.Callback callback)
        {
            this.callback = callback;
            YVRCallback.AddRequest(this);

            return this;
        }

        internal virtual void HandleMessage(YVRMessage msg)
        {
            if (this.callback != null)
            {
                callback(msg);
            }
            else
            {
                Debug.LogError("[YVRPlatform] Request with no handler. This should never happen.");
            }
        }
    }

    /// <summary>
    /// Generics class of yvrrequest class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class YVRRequest<T> : YVRRequest
    {
        private YVRMessage<T>.Callback callback = null;

        internal YVRRequest(int requestID) : base(requestID) { }
        /// <summary>
        /// Add callback function to this request
        /// </summary>
        /// <param name="cb"></param>
        /// <returns></returns>
        public YVRRequest<T> OnComplete(YVRMessage<T>.Callback cb)
        {
            if (this.callback != null)
            {
                Debug.LogWarning("[YVRPlatform] There is already a request handler, but you still attemp to add a new request handler.");
            }

            this.callback = cb;
            YVRCallback.AddRequest(this);

            return this;
        }

        internal override void HandleMessage(YVRMessage msg)
        {
            if (!(msg is YVRMessage<T>))
            {
                Debug.LogErrorFormat("[YVRPlatform] Unable to handle this type of message : {0}", msg.GetType());
                return;
            }

            if (this.callback != null)
            {
                callback((YVRMessage<T>)msg);
            }
            else
            {
                Debug.LogError("[YVRPlatform] Request with no handler. This should never happen.");
            }
        }
    }
}