namespace YVR.Utilities
{
    public class LogPrefixDummyController : LoggerControllerBase
    {
        public static LogPrefixDummyController defaultController { get; private set; } = new LogPrefixDummyController();

        public override bool IsLogValid(object context, string log, LogPriority priority, string prefix = null)
        {
            return false;
        }
    }
}