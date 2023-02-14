namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate properties of message error 
    /// </summary>
    public class YVRError
    {
        /// <summary>
        /// The error code of message error
        /// </summary>
        public readonly ErrorCode errorCode;

        /// <summary>
        /// The error message of message error
        /// </summary>
        public readonly string errorMsg;

        internal YVRError(int eCode, string eMsg)
        {
            this.errorCode = (ErrorCode)eCode;
            this.errorMsg = eMsg;
        }

        /// <summary>
        /// Enum of error code
        /// </summary>
        public enum ErrorCode
        {
            UNKNOW_ERROR = -9999,
            SERVICE_RUN_ERROR = -5,
            NO_USER_INFO = -4,
            NEED_OFFLINE_ENTITLE = -3,
            NO_REQUEST_METHOD = -2,
            NO_LOGGED_USER = -1,

            SUCCESS = 0,

            NETWORK_ERROR = 404,
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("errorCode:【{0}】,\n\r", errorCode));
            str.Append(string.Format("errorMsg:【{0}】,\n\r", errorMsg ?? "null"));
            return str.ToString();
        }
    }
}