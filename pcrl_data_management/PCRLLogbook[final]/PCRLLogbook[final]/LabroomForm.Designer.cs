namespace PCRLLogbook
{
    partial class LabroomForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labmemberLabel = new System.Windows.Forms.Label();
            this.labmemberFillLabel = new System.Windows.Forms.Label();
            this.loginTimeFillLabel = new System.Windows.Forms.Label();
            this.loginTimeLabel = new System.Windows.Forms.Label();
            this.labroomCheckinBox = new System.Windows.Forms.GroupBox();
            this.W718Button = new System.Windows.Forms.Button();
            this.W716Button = new System.Windows.Forms.Button();
            this.W715Button = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.reviewSaveButton = new System.Windows.Forms.Button();
            this.configButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.configPath = new System.Windows.Forms.TextBox();
            this.configPathLabel = new System.Windows.Forms.Label();
            this.labroomCheckinBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // labmemberLabel
            // 
            this.labmemberLabel.AutoSize = true;
            this.labmemberLabel.Location = new System.Drawing.Point(13, 9);
            this.labmemberLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labmemberLabel.Name = "labmemberLabel";
            this.labmemberLabel.Size = new System.Drawing.Size(90, 16);
            this.labmemberLabel.TabIndex = 0;
            this.labmemberLabel.Text = "Lab Member: ";
            // 
            // labmemberFillLabel
            // 
            this.labmemberFillLabel.AutoSize = true;
            this.labmemberFillLabel.Location = new System.Drawing.Point(99, 9);
            this.labmemberFillLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labmemberFillLabel.Name = "labmemberFillLabel";
            this.labmemberFillLabel.Size = new System.Drawing.Size(69, 16);
            this.labmemberFillLabel.TabIndex = 1;
            this.labmemberFillLabel.Text = "[First Last]";
            // 
            // loginTimeFillLabel
            // 
            this.loginTimeFillLabel.AutoSize = true;
            this.loginTimeFillLabel.Location = new System.Drawing.Point(99, 42);
            this.loginTimeFillLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.loginTimeFillLabel.Name = "loginTimeFillLabel";
            this.loginTimeFillLabel.Size = new System.Drawing.Size(76, 16);
            this.loginTimeFillLabel.TabIndex = 3;
            this.loginTimeFillLabel.Text = "[DateTime]";
            // 
            // loginTimeLabel
            // 
            this.loginTimeLabel.AutoSize = true;
            this.loginTimeLabel.Location = new System.Drawing.Point(13, 42);
            this.loginTimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.loginTimeLabel.Name = "loginTimeLabel";
            this.loginTimeLabel.Size = new System.Drawing.Size(74, 16);
            this.loginTimeLabel.TabIndex = 2;
            this.loginTimeLabel.Text = "Logged in: ";
            // 
            // labroomCheckinBox
            // 
            this.labroomCheckinBox.Controls.Add(this.W718Button);
            this.labroomCheckinBox.Controls.Add(this.W716Button);
            this.labroomCheckinBox.Controls.Add(this.W715Button);
            this.labroomCheckinBox.Location = new System.Drawing.Point(19, 143);
            this.labroomCheckinBox.Margin = new System.Windows.Forms.Padding(4);
            this.labroomCheckinBox.Name = "labroomCheckinBox";
            this.labroomCheckinBox.Padding = new System.Windows.Forms.Padding(4);
            this.labroomCheckinBox.Size = new System.Drawing.Size(639, 93);
            this.labroomCheckinBox.TabIndex = 4;
            this.labroomCheckinBox.TabStop = false;
            this.labroomCheckinBox.Text = "Check into Labroom";
            // 
            // W718Button
            // 
            this.W718Button.Location = new System.Drawing.Point(453, 23);
            this.W718Button.Margin = new System.Windows.Forms.Padding(4);
            this.W718Button.Name = "W718Button";
            this.W718Button.Size = new System.Drawing.Size(178, 50);
            this.W718Button.TabIndex = 2;
            this.W718Button.Text = "W718";
            this.W718Button.UseVisualStyleBackColor = true;
            this.W718Button.Click += new System.EventHandler(this.labroomButton3_Click);
            // 
            // W716Button
            // 
            this.W716Button.Location = new System.Drawing.Point(247, 23);
            this.W716Button.Margin = new System.Windows.Forms.Padding(4);
            this.W716Button.Name = "W716Button";
            this.W716Button.Size = new System.Drawing.Size(178, 50);
            this.W716Button.TabIndex = 1;
            this.W716Button.Text = "W716";
            this.W716Button.UseVisualStyleBackColor = true;
            this.W716Button.Click += new System.EventHandler(this.labroomButton2_Click);
            // 
            // W715Button
            // 
            this.W715Button.Location = new System.Drawing.Point(8, 23);
            this.W715Button.Margin = new System.Windows.Forms.Padding(4);
            this.W715Button.Name = "W715Button";
            this.W715Button.Size = new System.Drawing.Size(200, 50);
            this.W715Button.TabIndex = 0;
            this.W715Button.Text = "W715";
            this.W715Button.UseVisualStyleBackColor = true;
            this.W715Button.Click += new System.EventHandler(this.labroomButton1_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(382, 244);
            this.logoutButton.Margin = new System.Windows.Forms.Padding(4);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(171, 51);
            this.logoutButton.TabIndex = 5;
            this.logoutButton.Text = "Save and Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // reviewSaveButton
            // 
            this.reviewSaveButton.Location = new System.Drawing.Point(123, 244);
            this.reviewSaveButton.Margin = new System.Windows.Forms.Padding(4);
            this.reviewSaveButton.Name = "reviewSaveButton";
            this.reviewSaveButton.Size = new System.Drawing.Size(164, 51);
            this.reviewSaveButton.TabIndex = 6;
            this.reviewSaveButton.Text = "Review and Save";
            this.reviewSaveButton.UseVisualStyleBackColor = true;
            this.reviewSaveButton.Click += new System.EventHandler(this.reviewSaveButton_Click);
            // 
            // configButton
            // 
            this.configButton.Location = new System.Drawing.Point(316, 57);
            this.configButton.Margin = new System.Windows.Forms.Padding(4);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(237, 28);
            this.configButton.TabIndex = 8;
            this.configButton.Text = "Configure";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(316, 93);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(237, 28);
            this.button1.TabIndex = 9;
            this.button1.Text = "Generate Reports";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // configPath
            // 
            this.configPath.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.configPath.Location = new System.Drawing.Point(316, 28);
            this.configPath.Name = "configPath";
            this.configPath.Size = new System.Drawing.Size(352, 22);
            this.configPath.TabIndex = 10;
            this.configPath.Text = "e.g. C:\\Downloads\\pcrl_data_management\\config.json";
            // 
            // configPathLabel
            // 
            this.configPathLabel.AutoSize = true;
            this.configPathLabel.Location = new System.Drawing.Point(313, 9);
            this.configPathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.configPathLabel.Name = "configPathLabel";
            this.configPathLabel.Size = new System.Drawing.Size(147, 16);
            this.configPathLabel.TabIndex = 11;
            this.configPathLabel.Text = "Configuration File Path: ";
            // 
            // LabroomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 318);
            this.Controls.Add(this.configPathLabel);
            this.Controls.Add(this.configPath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.configButton);
            this.Controls.Add(this.reviewSaveButton);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.labroomCheckinBox);
            this.Controls.Add(this.loginTimeFillLabel);
            this.Controls.Add(this.loginTimeLabel);
            this.Controls.Add(this.labmemberFillLabel);
            this.Controls.Add(this.labmemberLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LabroomForm";
            this.ShowIcon = false;
            this.Text = "PCRL Logbook: Labroom Check-In";
            this.labroomCheckinBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labmemberLabel;
        private System.Windows.Forms.Label labmemberFillLabel;
        private System.Windows.Forms.Label loginTimeFillLabel;
        private System.Windows.Forms.Label loginTimeLabel;
        private System.Windows.Forms.GroupBox labroomCheckinBox;
        private System.Windows.Forms.Button W718Button;
        private System.Windows.Forms.Button W716Button;
        private System.Windows.Forms.Button W715Button;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Button reviewSaveButton;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox configPath;
        private System.Windows.Forms.Label configPathLabel;
    }
}