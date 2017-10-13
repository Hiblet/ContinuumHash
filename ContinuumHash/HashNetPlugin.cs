using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using AlteryxGuiToolkit.Plugins;
using AlteryxRecordInfoNet;
using System.Security.Cryptography; // HMACSHA1

namespace ContinuumHash
{
    public class HashNetPlugin :
        AlteryxRecordInfoNet.INetPlugin,
        AlteryxRecordInfoNet.IIncomingConnectionInterface
    {

        private int _toolID; // Integer identifier provided by Alteryx, this tools tool number.
        private EngineInterface _engineInterface; // Reference provided by Alteryx so that we can talk to the engine.
        private XmlElement _xmlProperties; // Xml configuration for this custom tool

        private AlteryxRecordInfoNet.PluginOutputConnectionHelper _outputHelper;

        private AlteryxRecordInfoNet.RecordInfo _recordInfoIn;
        private AlteryxRecordInfoNet.RecordInfo _recordInfoOut;

        private string _secretField = Constants.DEFAULTSECRETFIELD;
        private string _messageField = Constants.DEFAULTMESSAGEFIELD;
        private string _algo = Constants.DEFAULTALGO;
        private string _outputField = Constants.DEFAULTOUTPUTFIELD;



        public void PI_Init(int nToolID, EngineInterface engineInterface, XmlElement pXmlProperties)
        {
            _toolID = nToolID;
            _engineInterface = engineInterface;
            _xmlProperties = pXmlProperties;

            // Use the information in the pXmlProperties parameter to get the user control info

            XmlElement configElement = XmlHelpers.GetFirstChildByName(_xmlProperties, "Configuration", true);
            if (configElement != null)
            {
                XmlElement xe = XmlHelpers.GetFirstChildByName(configElement, Constants.SECRETFIELDKEY, false);
                if (xe != null) _secretField = xe.InnerText;

                xe = XmlHelpers.GetFirstChildByName(configElement, Constants.MESSAGEFIELDKEY, false);
                if (xe != null) _messageField = xe.InnerText;

                xe = XmlHelpers.GetFirstChildByName(configElement, Constants.ALGOKEY, false);
                if (xe != null) _algo = xe.InnerText;

                xe = XmlHelpers.GetFirstChildByName(configElement, Constants.DEFAULTOUTPUTFIELD, false);
                if (xe != null)
                {
                    if (!string.IsNullOrWhiteSpace(xe.InnerText))
                        _outputField = xe.InnerText;
                }
            }


            _outputHelper = new AlteryxRecordInfoNet.PluginOutputConnectionHelper(_toolID, _engineInterface);
        }

        public IIncomingConnectionInterface PI_AddIncomingConnection(string pIncomingConnectionType, string pIncomingConnectionName)
        {
            return this;
        }

        public bool PI_AddOutgoingConnection(string pOutgoingConnectionName, OutgoingConnection outgoingConnection)
        {
            _outputHelper.AddOutgoingConnection(outgoingConnection);
            return true;
        }

        public bool PI_PushAllRecords(long nRecordLimit)
        {
            return true;
        }

        public void PI_Close(bool bHasErrors)
        {
            _outputHelper.Close();
        }

        public bool ShowDebugMessages()
        {
            return true;
        }

        public XmlElement II_GetPresortXml(XmlElement pXmlProperties)
        {
            return null;
        }

        public bool II_Init(RecordInfo recordInfo)
        {
            _recordInfoIn = recordInfo;

            return true;
        }


        public void II_Close()
        { }

        public void II_UpdateProgress(double dPercent)
        {
            // Since our progress is directly proportional to he progress of the
            // upstream tool, we can simply output it's percentage as our own.
            if (_engineInterface.OutputToolProgress(_toolID, dPercent) != 0)
            {
                // If this returns anything but 0, then the user has canceled the operation.
                throw new AlteryxRecordInfoNet.UserCanceledException();
            }

            // Have the PluginOutputConnectionHelper ask the downstream tools to update their progress.
            _outputHelper.UpdateProgress(dPercent);
        }



        public bool II_PushRecord(RecordData recordDataIn)
        {
            if (_recordInfoOut == null)
            {
                // Prep the output once
                populateRecordInfoOut();
                _outputHelper.Init(_recordInfoOut, "Output", null, _xmlProperties);
            }

            string secret = getFieldBaseStringData(_secretField, recordDataIn);
            string message = getFieldBaseStringData(_messageField, recordDataIn);


            AlteryxRecordInfoNet.Record recordOut = _recordInfoOut.CreateRecord();
            recordOut.Reset();

            // Transfer existing data
            copyRecordData(recordDataIn, recordOut);

            string sHash = performHash(secret, message);

            // Output Field is last
            AlteryxRecordInfoNet.FieldBase fbOut = _recordInfoOut[(int)_recordInfoIn.NumFields()];
            fbOut.SetFromString(recordOut, sHash);

            _outputHelper.PushRecord(recordOut.GetRecord());

            return true;
        }



        ///////////////////////////////////////////////////////////////////////
        // Helpers
        //

        private string getFieldBaseStringData(string fieldName, RecordData recordDataIn)
        {
            FieldBase fb = null;
            try
            {
                fb = _recordInfoIn.GetFieldByName(fieldName, true);
            }
            catch
            {
                throw new Exception($"The field [{fieldName}] was not found.");
            }

            return fb.GetAsString(recordDataIn);
        }

        private string performHash(string secret, string message)
        {
            HMAC hmac = null;
            byte[] secretKey = System.Text.Encoding.ASCII.GetBytes(secret);

            if (string.Equals(_algo, Constants.SHA1, StringComparison.OrdinalIgnoreCase))
                hmac = new HMACSHA1 { Key = secretKey };
            else if (string.Equals(_algo, Constants.SHA256, StringComparison.OrdinalIgnoreCase))
                hmac = new HMACSHA256 { Key = secretKey };
            else if (string.Equals(_algo, Constants.SHA384, StringComparison.OrdinalIgnoreCase))
                hmac = new HMACSHA384 { Key = secretKey };
            else if (string.Equals(_algo, Constants.SHA512, StringComparison.OrdinalIgnoreCase))
                hmac = new HMACSHA512 { Key = secretKey };
            else if (string.Equals(_algo, Constants.MD5, StringComparison.OrdinalIgnoreCase))
                hmac = new HMACMD5 { Key = secretKey };
            else if (string.Equals(_algo, Constants.RIPEMD160, StringComparison.OrdinalIgnoreCase))
                hmac = new HMACRIPEMD160 { Key = secretKey };

            if (hmac == null) throw new Exception($"Hash algo [{_algo}] not recognised");

            return performHashGeneric(message, hmac);
        }

        /*
    private string performHash(string secret, string message)
    {
        if (string.Equals(_algo, Constants.SHA1, StringComparison.OrdinalIgnoreCase)) return performHashSha1(secret, message);
        if (string.Equals(_algo, Constants.SHA256, StringComparison.OrdinalIgnoreCase)) return performHashSha256(secret, message);
        if (string.Equals(_algo, Constants.SHA384, StringComparison.OrdinalIgnoreCase)) return performHashSha384(secret, message);
        if (string.Equals(_algo, Constants.SHA512, StringComparison.OrdinalIgnoreCase)) return performHashSha512(secret, message);
        if (string.Equals(_algo, Constants.MD5, StringComparison.OrdinalIgnoreCase)) return performHashMd5(secret, message);
        if (string.Equals(_algo, Constants.RIPEMD160, StringComparison.OrdinalIgnoreCase)) return performHashRipe(secret, message);

        throw new Exception($"Hash algo [{_algo}] not recognised");
    }
    */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <remarks>
        /// Secret is expected to be an Ascii string signing key.
        /// For OAUTH, this will be the consumer secret, and the accessToken secret,
        /// Uri escaped, and then concatenated with an ampersand.  (See OAuth.cs in ExchangeCCG project).
        /// Message is expected to an Ascii string to be hashed.
        /// For OAUTH, this will be the httpMethod capitalised, then ampersand, then
        /// the Url (URI Escaped), then ampersand, and then the OAuth parameters and Payload sorted together.
        /// All of this is expected to happen outside the Hash function.
        /// </remarks>
        /// 

        private string performHashGeneric(string message, HMAC hmac)
        {
            byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes(message);
            byte[] hashBytes = hmac.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }

        /*
        private string performHashSha1(string secret, string message)
        {
            var hmac = new HMACSHA1 { Key = System.Text.Encoding.ASCII.GetBytes(secret) };
            byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes(message);
            byte[] hashBytes = hmac.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);
        }

        private string performHashSha256(string secret, string message)
        {
            var hmac = new HMACSHA256 { Key = System.Text.Encoding.ASCII.GetBytes(secret) };

            //return $"WOULD HASH MESSAGE [{message}] WITH SECRET [{secret}] USING ALGO [{_algo}]";
        }

        private string performHashSha384(string secret, string message)
        {
            return $"WOULD HASH MESSAGE [{message}] WITH SECRET [{secret}] USING ALGO [{_algo}]";
        }

        private string performHashSha512(string secret, string message)
        {
            return $"WOULD HASH MESSAGE [{message}] WITH SECRET [{secret}] USING ALGO [{_algo}]";
        }

        private string performHashMd5(string secret, string message)
        {
            return $"WOULD HASH MESSAGE [{message}] WITH SECRET [{secret}] USING ALGO [{_algo}]";
        }

        private string performHashRipe(string secret, string message)
        {
            return $"WOULD HASH MESSAGE [{message}] WITH SECRET [{secret}] USING ALGO [{_algo}]";
        }
        */

        /// <summary>
        /// Return a list of the inbound field names from record info in.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> getFieldNames()
        {
            List<string> fieldNames = new List<string>();
            uint countFields = _recordInfoIn.NumFields();
            for (int i = 0; i < countFields; ++i)
            {
                FieldBase fb = _recordInfoIn[i];
                if (fb != null)
                {
                    string fieldName = fb.GetFieldName();
                }
            }

            return fieldNames;
        }

        private void populateRecordInfoOut()
        {
            _recordInfoOut = new AlteryxRecordInfoNet.RecordInfo();

            // Copy the fieldbase structure of the incoming record
            uint countFields = _recordInfoIn.NumFields();
            for (int i = 0; i < countFields; ++i)
            {
                FieldBase fbIn = _recordInfoIn[i];
                _recordInfoOut.AddField(fbIn.GetFieldName(), fbIn.FieldType, (int)fbIn.Size, fbIn.Scale, fbIn.GetSource(), fbIn.GetDescription());
            }

            // Add the output column at the end
            _recordInfoOut.AddField(_outputField, FieldType.E_FT_String, Constants.OUTPUTFIELDSIZE, 0, "", "");
        }

        private void copyRecordData(RecordData recordDataIn, Record recordOut)
        {
            uint countFields = _recordInfoIn.NumFields();
            for (int i = 0; i < countFields; ++i)
            {
                FieldBase fbIn = _recordInfoIn[i];
                FieldBase fbOut = _recordInfoOut[i];

                // Point a fieldbase reference to the record out item.
                fbOut.SetFromString(recordOut, fbIn.GetAsString(recordDataIn));
            }
        }

    }
}
