using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AlteryxGuiToolkit.Document;
using System.Xml;


namespace ContinuumHash
{
    public partial class HashUserControl : UserControl, AlteryxGuiToolkit.Plugins.IPluginConfiguration
    {
        public HashUserControl()
        {
            InitializeComponent();
        }

        public Control GetConfigurationControl(
            AlteryxGuiToolkit.Document.Properties docProperties,
            XmlElement eConfig,
            XmlElement[] eIncomingMetaInfo,
            int nToolId,
            string strToolName)
        {
            XmlInputConfiguration xmlConfig = XmlInputConfiguration.LoadFromConfiguration(eConfig);

            if (xmlConfig == null)
                return this;

            // Populate any tool controls from the config

            ///////////////////////////////////////////////////////////////////
            // Secret Field
            //

            string secretField = xmlConfig.SecretField;
            comboBoxSecretField.Items.Clear();

            if (eIncomingMetaInfo == null || eIncomingMetaInfo[0] == null)
            {
                // No incoming connection;  Just add the field 
                comboBoxSecretField.Items.Add(secretField);
                comboBoxSecretField.SelectedIndex = 0;
            }
            else
            {
                // We have an incoming connection

                var xmlElementMetaInfo = eIncomingMetaInfo[0];
                var xmlElementRecordInfo = xmlElementMetaInfo.FirstChild;
                foreach (XmlElement elementChild in xmlElementRecordInfo)
                {
                    string fieldName = elementChild.GetAttribute("name");
                    string fieldType = elementChild.GetAttribute("type");

                    if (isStringType(fieldType)) comboBoxSecretField.Items.Add(fieldName);
                }

                // If the secretField matches a possible field in the combo box, make it the selected field.
                // If the selectedField does not match, do not select anything and blank the selectedField.
                if (!string.IsNullOrWhiteSpace(secretField))
                {
                    int selectedIndex = comboBoxSecretField.FindStringExact(secretField);
                    if (selectedIndex >= 0)
                        comboBoxSecretField.SelectedIndex = selectedIndex; // Found; Select this item                    
                }

            } // end of "if (eIncomingMetaInfo == null || eIncomingMetaInfo[0] == null)"



            ///////////////////////////////////////////////////////////////////
            // Message Field
            //

            string messageField = xmlConfig.MessageField;
            comboBoxMessageField.Items.Clear();

            if (eIncomingMetaInfo == null || eIncomingMetaInfo[0] == null)
            {
                // No incoming connection;  Just add the field and select it
                comboBoxMessageField.Items.Add(messageField);
                comboBoxMessageField.SelectedIndex = 0;
            }
            else
            {
                // We have an incoming connection

                var xmlElementMetaInfo = eIncomingMetaInfo[0];
                var xmlElementRecordInfo = xmlElementMetaInfo.FirstChild;
                foreach (XmlElement elementChild in xmlElementRecordInfo)
                {
                    string fieldName = elementChild.GetAttribute("name");
                    string fieldType = elementChild.GetAttribute("type");

                    if (isStringType(fieldType)) comboBoxMessageField.Items.Add(fieldName);
                }

                // If the messageField matches a possible field in the combo box, make it the selected field.
                // If the field does not match, do not select anything and blank the field.
                if (!string.IsNullOrWhiteSpace(messageField))
                {
                    int selectedIndex = comboBoxMessageField.FindStringExact(messageField);
                    if (selectedIndex >= 0)
                        comboBoxMessageField.SelectedIndex = selectedIndex; // Found; Select this item                    
                }

            } // end of "if (eIncomingMetaInfo == null || eIncomingMetaInfo[0] == null)"



            ///////////////////////////////////////////////////////////////////
            // Algo
            //

            string algo = xmlConfig.Algo;
            comboBoxAlgo.Items.Clear();
            comboBoxAlgo.Items.Add(Constants.SHA1);
            comboBoxAlgo.Items.Add(Constants.SHA256);
            comboBoxAlgo.Items.Add(Constants.SHA384);
            comboBoxAlgo.Items.Add(Constants.SHA512);
            comboBoxAlgo.Items.Add(Constants.MD5);
            comboBoxAlgo.Items.Add(Constants.RIPEMD160);

            if (!string.IsNullOrWhiteSpace(algo))
            {
                int selectedIndex = comboBoxAlgo.FindStringExact(algo);
                if (selectedIndex >= 0)
                    comboBoxAlgo.SelectedIndex = selectedIndex; // Found; Select this item                    
            }



            ///////////////////////////////////////////////////////////////////
            // Output Field
            //

            string outputField = xmlConfig.OutputField;
            textBoxOutputField.Text = outputField;

            return this;
        }

        public void SaveResultsToXml(XmlElement eConfig, out string strDefaultAnnotation)
        {
            XmlElement xe = XmlHelpers.GetOrCreateChildNode(eConfig, Constants.SECRETFIELDKEY);
            var selectedItem = comboBoxSecretField.SelectedItem;
            string secretField = selectedItem == null ? Constants.DEFAULTSECRETFIELD : selectedItem.ToString();
            xe.InnerText = secretField;

            xe = XmlHelpers.GetOrCreateChildNode(eConfig, Constants.MESSAGEFIELDKEY);
            selectedItem = comboBoxMessageField.SelectedItem;
            string messageField = selectedItem == null ? Constants.DEFAULTMESSAGEFIELD : selectedItem.ToString();
            xe.InnerText = messageField;

            xe = XmlHelpers.GetOrCreateChildNode(eConfig, Constants.ALGOKEY);
            selectedItem = comboBoxAlgo.SelectedItem;
            string algo = selectedItem == null ? Constants.DEFAULTALGO : selectedItem.ToString();
            xe.InnerText = algo;

            xe = XmlHelpers.GetOrCreateChildNode(eConfig, Constants.OUTPUTFIELDKEY);
            string outputField = textBoxOutputField.Text;
            xe.InnerText = string.IsNullOrWhiteSpace(outputField) ? Constants.DEFAULTOUTPUTFIELD : outputField;

            // Set the default annotation to be the name of the tool.
            strDefaultAnnotation = "Hash";
        }


        // Helper
        private static bool isStringType(string fieldType)
        {
            return string.Equals(fieldType, "string", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(fieldType, "v_string", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(fieldType, "wstring", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(fieldType, "v_wstring", StringComparison.OrdinalIgnoreCase);
        }

    }
}
