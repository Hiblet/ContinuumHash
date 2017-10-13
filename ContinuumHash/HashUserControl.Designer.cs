namespace ContinuumHash
{
    partial class HashUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelHashHeader = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxSecretField = new System.Windows.Forms.ComboBox();
            this.labelHashSecret = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboBoxMessageField = new System.Windows.Forms.ComboBox();
            this.labelHashMessage = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.comboBoxAlgo = new System.Windows.Forms.ComboBox();
            this.labelHashAlgo = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelHashOutputField = new System.Windows.Forms.Label();
            this.textBoxOutputField = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelHashHeader);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 50);
            this.panel1.TabIndex = 0;
            // 
            // labelHashHeader
            // 
            this.labelHashHeader.AutoSize = true;
            this.labelHashHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHashHeader.Location = new System.Drawing.Point(10, 14);
            this.labelHashHeader.Name = "labelHashHeader";
            this.labelHashHeader.Size = new System.Drawing.Size(54, 24);
            this.labelHashHeader.TabIndex = 0;
            this.labelHashHeader.Text = "Hash";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comboBoxSecretField);
            this.panel2.Controls.Add(this.labelHashSecret);
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(300, 50);
            this.panel2.TabIndex = 1;
            // 
            // comboBoxSecretField
            // 
            this.comboBoxSecretField.FormattingEnabled = true;
            this.comboBoxSecretField.Location = new System.Drawing.Point(105, 15);
            this.comboBoxSecretField.Name = "comboBoxSecretField";
            this.comboBoxSecretField.Size = new System.Drawing.Size(180, 21);
            this.comboBoxSecretField.TabIndex = 1;
            // 
            // labelHashSecret
            // 
            this.labelHashSecret.AutoSize = true;
            this.labelHashSecret.Location = new System.Drawing.Point(12, 18);
            this.labelHashSecret.Name = "labelHashSecret";
            this.labelHashSecret.Size = new System.Drawing.Size(66, 13);
            this.labelHashSecret.TabIndex = 0;
            this.labelHashSecret.Text = "Secret Field:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.comboBoxMessageField);
            this.panel3.Controls.Add(this.labelHashMessage);
            this.panel3.Location = new System.Drawing.Point(0, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(300, 50);
            this.panel3.TabIndex = 2;
            // 
            // comboBoxMessageField
            // 
            this.comboBoxMessageField.FormattingEnabled = true;
            this.comboBoxMessageField.Location = new System.Drawing.Point(105, 15);
            this.comboBoxMessageField.Name = "comboBoxMessageField";
            this.comboBoxMessageField.Size = new System.Drawing.Size(180, 21);
            this.comboBoxMessageField.TabIndex = 1;
            // 
            // labelHashMessage
            // 
            this.labelHashMessage.AutoSize = true;
            this.labelHashMessage.Location = new System.Drawing.Point(12, 18);
            this.labelHashMessage.Name = "labelHashMessage";
            this.labelHashMessage.Size = new System.Drawing.Size(78, 13);
            this.labelHashMessage.TabIndex = 0;
            this.labelHashMessage.Text = "Message Field:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.comboBoxAlgo);
            this.panel4.Controls.Add(this.labelHashAlgo);
            this.panel4.Location = new System.Drawing.Point(0, 150);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(300, 50);
            this.panel4.TabIndex = 3;
            // 
            // comboBoxAlgo
            // 
            this.comboBoxAlgo.FormattingEnabled = true;
            this.comboBoxAlgo.Location = new System.Drawing.Point(105, 15);
            this.comboBoxAlgo.Name = "comboBoxAlgo";
            this.comboBoxAlgo.Size = new System.Drawing.Size(180, 21);
            this.comboBoxAlgo.TabIndex = 1;
            // 
            // labelHashAlgo
            // 
            this.labelHashAlgo.AutoSize = true;
            this.labelHashAlgo.Location = new System.Drawing.Point(12, 18);
            this.labelHashAlgo.Name = "labelHashAlgo";
            this.labelHashAlgo.Size = new System.Drawing.Size(56, 13);
            this.labelHashAlgo.TabIndex = 0;
            this.labelHashAlgo.Text = "Algorithm: ";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.textBoxOutputField);
            this.panel5.Controls.Add(this.labelHashOutputField);
            this.panel5.Location = new System.Drawing.Point(0, 200);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(300, 50);
            this.panel5.TabIndex = 4;
            // 
            // labelHashOutputField
            // 
            this.labelHashOutputField.AutoSize = true;
            this.labelHashOutputField.Location = new System.Drawing.Point(12, 18);
            this.labelHashOutputField.Name = "labelHashOutputField";
            this.labelHashOutputField.Size = new System.Drawing.Size(67, 13);
            this.labelHashOutputField.TabIndex = 1;
            this.labelHashOutputField.Text = "Output Field:";
            // 
            // textBoxOutputField
            // 
            this.textBoxOutputField.Location = new System.Drawing.Point(105, 15);
            this.textBoxOutputField.Name = "textBoxOutputField";
            this.textBoxOutputField.Size = new System.Drawing.Size(180, 20);
            this.textBoxOutputField.TabIndex = 2;
            // 
            // HashUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "HashUserControl";
            this.Size = new System.Drawing.Size(300, 250);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelHashHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboBoxSecretField;
        private System.Windows.Forms.Label labelHashSecret;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBoxMessageField;
        private System.Windows.Forms.Label labelHashMessage;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox comboBoxAlgo;
        private System.Windows.Forms.Label labelHashAlgo;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelHashOutputField;
        private System.Windows.Forms.TextBox textBoxOutputField;
    }
}
