using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumHash
{
    public class Constants
    {
        // Keys for XML data storage
        public static string SECRETFIELDKEY = "HashSecretField";
        public static string MESSAGEFIELDKEY = "HashMessageField";
        public static string ALGOKEY = "HashAlgo";
        public static string OUTPUTFIELDKEY = "HashOutputField";

        // Default Values
        public static string DEFAULTSECRETFIELD = "Secret";
        public static string DEFAULTMESSAGEFIELD = "Message";
        public static string DEFAULTALGO = "HMAC-SHA1";
        public static string DEFAULTOUTPUTFIELD = "HashedMessageBase64";
        public static int OUTPUTFIELDSIZE = 1024;

        // Algo Strings
        public static string SHA1 = "HMAC-SHA1";
        public static string SHA256 = "HMAC-SHA256";
        public static string SHA384 = "HMAC-SHA384";
        public static string SHA512 = "HMAC-SHA512";
        public static string MD5 = "HMAC-MD5";
        public static string RIPEMD160 = "HMAC-RIPEMD160";
    }
}
