namespace Rapicgen.Windows
{
    partial class GeneralOptionsPageCustom
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
            this.lblJavaPath = new System.Windows.Forms.Label();
            this.tbJavaPath = new System.Windows.Forms.TextBox();
            this.btnJavaPath = new System.Windows.Forms.Button();
            this.btnNSwagPath = new System.Windows.Forms.Button();
            this.tbNSwagPath = new System.Windows.Forms.TextBox();
            this.lblNSwagPath = new System.Windows.Forms.Label();
            this.btnOpenApi = new System.Windows.Forms.Button();
            this.tbOpenApiPath = new System.Windows.Forms.TextBox();
            this.lblOpenApiPath = new System.Windows.Forms.Label();
            this.btnSwagger = new System.Windows.Forms.Button();
            this.tbSwaggerPath = new System.Windows.Forms.TextBox();
            this.lblSwaggerPath = new System.Windows.Forms.Label();
            this.cbInstallMissingPackages = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblJavaPath
            // 
            this.lblJavaPath.AutoSize = true;
            this.lblJavaPath.Location = new System.Drawing.Point(13, 16);
            this.lblJavaPath.Name = "lblJavaPath";
            this.lblJavaPath.Size = new System.Drawing.Size(295, 13);
            this.lblJavaPath.TabIndex = 0;
            this.lblJavaPath.Text = "Path to java.exe. Leave empty to get path from JAVA_HOME";
            // 
            // tbJavaPath
            // 
            this.tbJavaPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbJavaPath.Location = new System.Drawing.Point(16, 32);
            this.tbJavaPath.Name = "tbJavaPath";
            this.tbJavaPath.Size = new System.Drawing.Size(325, 20);
            this.tbJavaPath.TabIndex = 1;
            this.tbJavaPath.TextChanged += new System.EventHandler(this.TbJavaPath_TextChanged);
            // 
            // btnJavaPath
            // 
            this.btnJavaPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJavaPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJavaPath.Location = new System.Drawing.Point(347, 32);
            this.btnJavaPath.Name = "btnJavaPath";
            this.btnJavaPath.Size = new System.Drawing.Size(30, 20);
            this.btnJavaPath.TabIndex = 2;
            this.btnJavaPath.Text = "...";
            this.btnJavaPath.UseVisualStyleBackColor = true;
            this.btnJavaPath.Click += new System.EventHandler(this.BtnJavaPath_Click);
            // 
            // 
            // btnNSwagPath
            // 
            this.btnNSwagPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNSwagPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNSwagPath.Location = new System.Drawing.Point(347, 83);
            this.btnNSwagPath.Name = "btnNSwagPath";
            this.btnNSwagPath.Size = new System.Drawing.Size(30, 20);
            this.btnNSwagPath.TabIndex = 5;
            this.btnNSwagPath.Text = "...";
            this.btnNSwagPath.UseVisualStyleBackColor = true;
            this.btnNSwagPath.Click += new System.EventHandler(this.BtnNSwagPath_Click);
            // 
            // tbNSwagPath
            // 
            this.tbNSwagPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNSwagPath.Location = new System.Drawing.Point(16, 83);
            this.tbNSwagPath.Name = "tbNSwagPath";
            this.tbNSwagPath.Size = new System.Drawing.Size(325, 20);
            this.tbNSwagPath.TabIndex = 4;
            this.tbNSwagPath.TextChanged += new System.EventHandler(this.TbNSwagPath_TextChanged);
            // 
            // lblNSwagPath
            // 
            this.lblNSwagPath.AutoSize = true;
            this.lblNSwagPath.Location = new System.Drawing.Point(13, 67);
            this.lblNSwagPath.Name = "lblNSwagPath";
            this.lblNSwagPath.Size = new System.Drawing.Size(264, 13);
            this.lblNSwagPath.TabIndex = 3;
            this.lblNSwagPath.Text = "Full path to NSwag.exe (Installs as .NET tool if not found)";
            // 
            // btnOpenApi
            // 
            this.btnOpenApi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenApi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenApi.Location = new System.Drawing.Point(347, 195);
            this.btnOpenApi.Name = "btnOpenApi";
            this.btnOpenApi.Size = new System.Drawing.Size(30, 20);
            this.btnOpenApi.TabIndex = 11;
            this.btnOpenApi.Text = "...";
            this.btnOpenApi.UseVisualStyleBackColor = true;
            this.btnOpenApi.Click += new System.EventHandler(this.BtnOpenApi_Click);
            // 
            // tbOpenApiPath
            // 
            this.tbOpenApiPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOpenApiPath.Location = new System.Drawing.Point(16, 195);
            this.tbOpenApiPath.Name = "tbOpenApiPath";
            this.tbOpenApiPath.Size = new System.Drawing.Size(325, 20);
            this.tbOpenApiPath.TabIndex = 10;
            this.tbOpenApiPath.TextChanged += new System.EventHandler(this.TbOpenApiPath_TextChanged);
            // 
            // lblOpenApiPath
            // 
            this.lblOpenApiPath.AutoSize = true;
            this.lblOpenApiPath.Location = new System.Drawing.Point(13, 179);
            this.lblOpenApiPath.Name = "lblOpenApiPath";
            this.lblOpenApiPath.Size = new System.Drawing.Size(366, 13);
            this.lblOpenApiPath.TabIndex = 9;
            this.lblOpenApiPath.Text = "Full path OpenAPI Generator JAR file. Leave empty to download on-demand";
            // 
            // btnSwagger
            // 
            this.btnSwagger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwagger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwagger.Location = new System.Drawing.Point(347, 138);
            this.btnSwagger.Name = "btnSwagger";
            this.btnSwagger.Size = new System.Drawing.Size(30, 20);
            this.btnSwagger.TabIndex = 8;
            this.btnSwagger.Text = "...";
            this.btnSwagger.UseVisualStyleBackColor = true;
            this.btnSwagger.Click += new System.EventHandler(this.BtnSwagger_Click);
            // 
            // tbSwaggerPath
            // 
            this.tbSwaggerPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSwaggerPath.Location = new System.Drawing.Point(16, 138);
            this.tbSwaggerPath.Name = "tbSwaggerPath";
            this.tbSwaggerPath.Size = new System.Drawing.Size(325, 20);
            this.tbSwaggerPath.TabIndex = 7;
            this.tbSwaggerPath.TextChanged += new System.EventHandler(this.TbSwaggerPath_TextChanged);
            // 
            // lblSwaggerPath
            // 
            this.lblSwaggerPath.AutoSize = true;
            this.lblSwaggerPath.Location = new System.Drawing.Point(13, 122);
            this.lblSwaggerPath.Name = "lblSwaggerPath";
            this.lblSwaggerPath.Size = new System.Drawing.Size(373, 13);
            this.lblSwaggerPath.TabIndex = 6;
            this.lblSwaggerPath.Text = "Full path to Swagger Codegen JAR file. Leave empty to download on-demand";
            // 
            // cbInstallMissingPackages
            // 
            this.cbInstallMissingPackages.AutoSize = true;
            this.cbInstallMissingPackages.Location = new System.Drawing.Point(16, 230);
            this.cbInstallMissingPackages.Name = "cbInstallMissingPackages";
            this.cbInstallMissingPackages.Size = new System.Drawing.Size(238, 17);
            this.cbInstallMissingPackages.TabIndex = 13;
            this.cbInstallMissingPackages.Text = "Automatically install missing NuGet packages";
            this.cbInstallMissingPackages.UseVisualStyleBackColor = true;
            this.cbInstallMissingPackages.CheckedChanged += new System.EventHandler(this.InstallMissingPackages_CheckedChanged);
            // 
            // GeneralOptionsPageCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.cbInstallMissingPackages);
            this.Controls.Add(this.btnOpenApi);
            this.Controls.Add(this.tbOpenApiPath);
            this.Controls.Add(this.lblOpenApiPath);
            this.Controls.Add(this.btnSwagger);
            this.Controls.Add(this.tbSwaggerPath);
            this.Controls.Add(this.lblSwaggerPath);
            this.Controls.Add(this.btnNSwagPath);
            this.Controls.Add(this.tbNSwagPath);
            this.Controls.Add(this.lblNSwagPath);
            this.Controls.Add(this.btnJavaPath);
            this.Controls.Add(this.tbJavaPath);
            this.Controls.Add(this.lblJavaPath);
            this.Name = "GeneralOptionsPageCustom";
            this.Size = new System.Drawing.Size(390, 265);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblJavaPath;
        private System.Windows.Forms.TextBox tbJavaPath;
        private System.Windows.Forms.Button btnJavaPath;
        private System.Windows.Forms.Button btnNSwagPath;
        private System.Windows.Forms.TextBox tbNSwagPath;
        private System.Windows.Forms.Label lblNSwagPath;
        private System.Windows.Forms.Button btnOpenApi;
        private System.Windows.Forms.TextBox tbOpenApiPath;
        private System.Windows.Forms.Label lblOpenApiPath;
        private System.Windows.Forms.Button btnSwagger;
        private System.Windows.Forms.TextBox tbSwaggerPath;
        private System.Windows.Forms.Label lblSwaggerPath;
        private System.Windows.Forms.CheckBox cbInstallMissingPackages;
    }
}
