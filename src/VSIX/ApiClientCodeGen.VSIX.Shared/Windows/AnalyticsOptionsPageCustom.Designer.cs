namespace Rapicgen.Windows
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
            this.btnSupportKey = new System.Windows.Forms.Button();
            this.chkDisableTelemetry = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblSupportKey
            // 
            this.lblSupportKey.AutoSize = true;
            this.lblSupportKey.Location = new System.Drawing.Point(16, 12);
            this.lblSupportKey.Name = "lblSupportKey";
            this.lblSupportKey.Size = new System.Drawing.Size(68, 13);
            this.lblSupportKey.TabIndex = 17;
            this.lblSupportKey.Text = "Support Key:";
            // 
            // lblAnalyticsInfo
            // 
            this.lblAnalyticsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAnalyticsInfo.AutoSize = true;
            this.lblAnalyticsInfo.Location = new System.Drawing.Point(16, 68);
            this.lblAnalyticsInfo.Name = "lblAnalyticsInfo";
            this.lblAnalyticsInfo.Size = new System.Drawing.Size(351, 221);
            this.lblAnalyticsInfo.TabIndex = 20;
            this.lblAnalyticsInfo.Text = resources.GetString("lblAnalyticsInfo.Text");
            // 
            // btnSupportKey
            // 
            this.btnSupportKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSupportKey.Location = new System.Drawing.Point(295, 7);
            this.btnSupportKey.Name = "btnSupportKey";
            this.btnSupportKey.Size = new System.Drawing.Size(75, 23);
            this.btnSupportKey.TabIndex = 21;
            this.btnSupportKey.Text = "Copy";
            this.btnSupportKey.UseVisualStyleBackColor = true;
            this.btnSupportKey.Click += new System.EventHandler(this.btnSupportKey_Click);
            // 
            // chkDisableTelemetry
            // 
            this.chkDisableTelemetry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDisableTelemetry.AutoSize = true;
            this.chkDisableTelemetry.Location = new System.Drawing.Point(19, 38);
            this.chkDisableTelemetry.Name = "chkDisableTelemetry";
            this.chkDisableTelemetry.Size = new System.Drawing.Size(204, 17);
            this.chkDisableTelemetry.TabIndex = 23;
            this.chkDisableTelemetry.Text = "Disable Error and Telemetry collection";
            this.chkDisableTelemetry.UseVisualStyleBackColor = true;
            this.chkDisableTelemetry.CheckedChanged += new System.EventHandler(this.chkDisableTelemetry_CheckedChanged);
            // 
            // AnalyticsOptionsPageCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.chkDisableTelemetry);
            this.Controls.Add(this.btnSupportKey);
            this.Controls.Add(this.lblAnalyticsInfo);
            this.Controls.Add(this.lblSupportKey);
            this.Name = "AnalyticsOptionsPageCustom";
            this.Size = new System.Drawing.Size(390, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblSupportKey;
        private System.Windows.Forms.Label lblAnalyticsInfo;
        private System.Windows.Forms.Button btnSupportKey;
        private System.Windows.Forms.CheckBox chkDisableTelemetry;
    }
}
