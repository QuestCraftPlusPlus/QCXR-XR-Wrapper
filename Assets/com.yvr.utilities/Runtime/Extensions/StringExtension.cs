using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace YVR.Utilities
{
    public static class StringExtension
    {
        private static Dictionary<string, string> s_Source2MD5Dict = new Dictionary<string, string>();

        /// <summary>
        /// Encode target string into md5 in 32 upper characters format
        /// </summary>
        /// <param name="source"> Target string </param>
        /// <returns> MD5 in 32 upper characters format </returns>
        public static string EncodeTo32UpperMD5(this string source)
        {
            if (s_Source2MD5Dict.ContainsKey(source))
                return s_Source2MD5Dict[source];

            StringBuilder builder = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));

                data.IndexForEach(i => builder.Append(data[i[0]].ToString("X2")));
            }

            string result = builder.ToString();
            s_Source2MD5Dict.Add(source, result);
            return result;
        }

        /// <summary>
        /// Verify whether <param name="source"></param>'s md5 value is equal to <param name="targetMD5Hash"></param>
        /// </summary>
        /// <param name="source"> Target string to be verified. </param>
        /// <param name="targetMD5Hash"> MD5 value as target MD5. </param>
        /// <returns></returns>
        public static bool VerifyMD5Cache(this string source, in string targetMD5Hash)
        {
            string md5 = source.EncodeTo32UpperMD5();
            return StringComparer.Ordinal.Compare(md5, targetMD5Hash) == 0;
        }
    }
}