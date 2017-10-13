using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace ContinuumHash
{
    public class XmlInputConfiguration
    {
        public string SecretField { get; private set; }
        public string MessageField { get; private set; }
        public string Algo { get; private set; }
        public string OutputField { get; private set; }



        // Note that the constructor is private.  Instances are created through the LoadFromConfigration method.
        private XmlInputConfiguration(string secretField, string messageField, string algo, string outputField)
        {
            SecretField = secretField;
            MessageField = messageField;
            Algo = algo;
            OutputField = outputField;
        }

        public static XmlInputConfiguration LoadFromConfiguration(XmlElement eConfig)
        {
            string secretField = Constants.DEFAULTSECRETFIELD;
            XmlElement xe = eConfig.SelectSingleNode(Constants.SECRETFIELDKEY) as XmlElement;
            if (xe != null)
            {
                if (!string.IsNullOrEmpty(xe.InnerText))
                    secretField = xe.InnerText;
            }

            string messageField = Constants.DEFAULTMESSAGEFIELD;
            xe = eConfig.SelectSingleNode(Constants.MESSAGEFIELDKEY) as XmlElement;
            if (xe != null)
            {
                if (!string.IsNullOrEmpty(xe.InnerText))
                    messageField = xe.InnerText;
            }

            string algo = Constants.DEFAULTALGO;
            xe = eConfig.SelectSingleNode(Constants.ALGOKEY) as XmlElement;
            if (xe != null)
            {
                if (!string.IsNullOrEmpty(xe.InnerText))
                    algo = xe.InnerText;
            }

            string outputField = Constants.DEFAULTOUTPUTFIELD;
            xe = eConfig.SelectSingleNode(Constants.OUTPUTFIELDKEY) as XmlElement;
            if (xe != null)
            {
                if (!string.IsNullOrEmpty(xe.InnerText))
                    outputField = xe.InnerText;
            }


            return new XmlInputConfiguration(secretField, messageField, algo, outputField);
        }
    }
}
