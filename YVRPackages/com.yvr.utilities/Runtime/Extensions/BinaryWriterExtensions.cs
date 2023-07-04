using System.IO;
using System.Text;

namespace YVR.Utilities
{
    public static class BinaryWriterExtensions
    {
        public static void WriteString(this BinaryWriter writer, string value, Encoding encoding = null)
        {
            if (value == null)
            {
                writer.Write(-1);
                return;
            }

            encoding ??= Encoding.UTF8;
            byte[] data = encoding.GetBytes(value);
            writer.WriteBytes(data);
        }

        public static void WriteBytes(this BinaryWriter writer, byte[] value)
        {
            if (value == null)
            {
                writer.Write(-1);
                return;
            }

            writer.Write(value.Length);
            writer.Write(value);
        }
    }
}