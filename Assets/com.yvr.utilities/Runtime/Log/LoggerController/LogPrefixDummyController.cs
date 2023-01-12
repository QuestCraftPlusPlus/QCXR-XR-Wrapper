using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YVR.Utilities
{
    public class LogPrefixDummyController : LoggerControllerBase
    {

        public static LogPrefixDummyController defaultController { get; private set; } = new LogPrefixDummyController();

        public override bool IsLogValid(in object context, in string log, in LogPriority priority, in string prefix = null)
        {
            return false;
        }
    }
}