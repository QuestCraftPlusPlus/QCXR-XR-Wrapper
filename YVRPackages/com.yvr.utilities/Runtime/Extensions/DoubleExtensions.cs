namespace YVR.Utilities
{
    public static class DoubleExtensions
    {
        public static double NanoSec2MilliSec(this double value) { return value / 1000000; }

        public static string NanoSec2MilliSec(this double value, int precision)
        {
            return value.NanoSec2MilliSec().ToString($"F{precision}");
        }
    }
}