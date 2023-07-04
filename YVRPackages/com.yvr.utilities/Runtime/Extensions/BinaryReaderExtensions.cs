using System.IO;
using System.Text;

namespace YVR.Utilities
{
    public static class BinaryReaderExtensions
    {
        public static string GetString(this BinaryReader reader, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            int length = reader.ReadInt32();
            if (length < 0) return null;

            return encoding.GetString(reader.ReadBytes(length));
        }

        public static byte[] GetBytes(this BinaryReader reader)
        {
            int length = reader.ReadInt32();
            if (length < 0)
            {
                return null;
            }

            return reader.ReadBytes(length);
        }
    }
    
}