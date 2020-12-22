namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    partial class AnalyticsOptionsPageCustom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalyticsOptionsPageCustom));
            this.lblSupportKey = new System.Windows.Forms.Label();
            this.lblAnalyticsInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSupportKey
            // 
            this.lblSupportKey.AutoSize = true;
            this.lblSupportKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupportKey.Location = new System.Drawing.Point(16, 12);
            this.lblSupportKey.Name = "lblSupportKey";
            this.lblSupportKey.Size = new System.Drawing.Size(96, 16);
            this.lblSupportKey.TabIndex = 17;
            this.lblSupportKey.Text = "Support Key:";
            // 
            // lblAnalyticsInfo
            // 
            this.lblAnalyticsInfo.AutoSize = true;
            this.lblAnalyticsInfo.Location = new System.Drawing.Point(16, 51);
            this.lblAnalyticsInfo.Name = "lblAnalyticsInfo";
            this.lblAnalyticsInfo.Size = new System.Drawing.Size(354, 195);
            this.lblAnalyticsInfo.TabIndex = 20;
            this.lblAnalyticsInfo.Text = resources.GetString("lblAnalyticsInfo.Text");
            // 
            // AnalyticsOptionsPageCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.lblAnalyticsInfo);
            this.Controls.Add(this.lblSupportKey);
            this.Name = "AnalyticsOptionsPageCustom";
            this.Size = new System.Drawing.Size(390, 351);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblSupportKey;
        private System.Windows.Forms.Label lblAnalyticsInfo;
    }
}
