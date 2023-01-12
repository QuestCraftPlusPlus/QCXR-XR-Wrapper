using System;

public static class ByteArrayExtension
{
    /// <summary>
    /// sbyte[] convert to byte[]
    /// </summary>
    /// <param name="origin"> sbyte[] </param>
    /// <returns></returns>
    public static byte[] ToByteArray(this sbyte[] origin)
    {
        byte[] result = new byte[origin.Length];
        Buffer.BlockCopy(origin, 0, result, 0, origin.Length);
        return result;
    }
}