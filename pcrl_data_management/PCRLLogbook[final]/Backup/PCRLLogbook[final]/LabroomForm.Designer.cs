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
            this.labroomButton3 = new System.Windows.Forms.Button();
            this.labroomButton2 = new System.Windows.Forms.Button();
            this.labroomButton1 = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.loginLogTableAdapter = new PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.LoginLogTableAdapter();
            this.labroomLogTableAdapter = new PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.LabroomLogTableAdapter();
            this.pcrlLogbookDBDataSet = new PCRLLogbook.PCRLLogbookDBDataSet1();
            this.reviewSaveButton = new System.Windows.Forms.Button();
            this.observationsTableAdapter = new PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.ObservationsTableAdapter();
            this.labroomCheckinBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcrlLogbookDBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // labmemberLabel
            // 
            this.labmemberLabel.AutoSize = true;
            this.labmemberLabel.Location = new System.Drawing.Point(12, 18);
            this.labmemberLabel.Name = "labmemberLabel";
            this.labmemberLabel.Size = new System.Drawing.Size(72, 13);
            this.labmemberLabel.TabIndex = 0;
            this.labmemberLabel.Text = "Lab Member: ";
            // 
            // labmemberFillLabel
            // 
            this.labmemberFillLabel.AutoSize = true;
            this.labmemberFillLabel.Location = new System.Drawing.Point(90, 18);
            this.labmemberFillLabel.Name = "labmemberFillLabel";
            this.labmemberFillLabel.Size = new System.Drawing.Size(55, 13);
            this.labmemberFillLabel.TabIndex = 1;
            this.labmemberFillLabel.Text = "[First Last]";
            // 
            // loginTimeFillLabel
            // 
            this.loginTimeFillLabel.AutoSize = true;
            this.loginTimeFillLabel.Location = new System.Drawing.Point(90, 45);
            this.loginTimeFillLabel.Name = "loginTimeFillLabel";
            this.loginTimeFillLabel.Size = new System.Drawing.Size(59, 13);
            this.loginTimeFillLabel.TabIndex = 3;
            this.loginTimeFillLabel.Text = "[DateTime]";
            // 
            // loginTimeLabel
            // 
            this.loginTimeLabel.AutoSize = true;
            this.loginTimeLabel.Location = new System.Drawing.Point(12, 45);
            this.loginTimeLabel.Name = "loginTimeLabel";
            this.loginTimeLabel.Size = new System.Drawing.Size(60, 13);
            this.loginTimeLabel.TabIndex = 2;
            this.loginTimeLabel.Text = "Logged in: ";
            // 
            // labroomCheckinBox
            // 
            this.labroomCheckinBox.Controls.Add(this.labroomButton3);
            this.labroomCheckinBox.Controls.Add(this.labroomButton2);
            this.labroomCheckinBox.Controls.Add(this.labroomButton1);
            this.labroomCheckinBox.Location = new System.Drawing.Point(15, 86);
            this.labroomCheckinBox.Name = "labroomCheckinBox";
            this.labroomCheckinBox.Size = new System.Drawing.Size(270, 70);
            this.labroomCheckinBox.TabIndex = 4;
            this.labroomCheckinBox.TabStop = false;
            this.labroomCheckinBox.Text = "Check into Labroom";
            // 
            // labroomButton3
            // 
            this.labroomButton3.Location = new System.Drawing.Point(177, 31);
            this.labroomButton3.Name = "labroomButton3";
            this.labroomButton3.Size = new System.Drawing.Size(75, 23);
            this.labroomButton3.TabIndex = 2;
            this.labroomButton3.Text = "Labroom 3";
            this.labroomButton3.UseVisualStyleBackColor = true;
            this.labroomButton3.Click += new System.EventHandler(this.labroomButton3_Click);
            // 
            // labroomButton2
            // 
            this.labroomButton2.Location = new System.Drawing.Point(96, 31);
            this.labroomButton2.Name = "labroomButton2";
            this.labroomButton2.Size = new System.Drawing.Size(75, 23);
            this.labroomButton2.TabIndex = 1;
            this.labroomButton2.Text = "Labroom 2";
            this.labroomButton2.UseVisualStyleBackColor = true;
            this.labroomButton2.Click += new System.EventHandler(this.labroomButton2_Click);
            // 
            // labroomButton1
            // 
            this.labroomButton1.Location = new System.Drawing.Point(15, 31);
            this.labroomButton1.Name = "labroomButton1";
            this.labroomButton1.Size = new System.Drawing.Size(75, 23);
            this.labroomButton1.TabIndex = 0;
            this.labroomButton1.Text = "Labroom 1";
            this.labroomButton1.UseVisualStyleBackColor = true;
            this.labroomButton1.Click += new System.EventHandler(this.labroomButton1_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(158, 176);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(109, 23);
            this.logoutButton.TabIndex = 5;
            this.logoutButton.Text = "Save and Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // loginLogTableAdapter
            // 
            this.loginLogTableAdapter.ClearBeforeFill = true;
            // 
            // labroomLogTableAdapter
            // 
            this.labroomLogTableAdapter.ClearBeforeFill = true;
            // 
            // pcrlLogbookDBDataSet
            // 
            this.pcrlLogbookDBDataSet.DataSetName = "PCRLLogbookDBDataSet1";
            this.pcrlLogbookDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reviewSaveButton
            // 
            this.reviewSaveButton.Location = new System.Drawing.Point(30, 176);
            this.reviewSaveButton.Name = "reviewSaveButton";
            this.reviewSaveButton.Size = new System.Drawing.Size(104, 23);
            this.reviewSaveButton.TabIndex = 6;
            this.reviewSaveButton.Text = "Review and Save";
            this.reviewSaveButton.UseVisualStyleBackColor = true;
            this.reviewSaveButton.Click += new System.EventHandler(this.reviewSaveButton_Click);
            // 
            // observationsTableAdapter
            // 
            this.observationsTableAdapter.ClearBeforeFill = true;
            // 
            // LabroomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 214);
            this.Controls.Add(this.reviewSaveButton);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.labroomCheckinBox);
            this.Controls.Add(this.loginTimeFillLabel);
            this.Controls.Add(this.loginTimeLabel);
            this.Controls.Add(this.labmemberFillLabel);
            this.Controls.Add(this.labmemberLabel);
            this.Name = "LabroomForm";
            this.ShowIcon = false;
            this.Text = "NHP Logbook: Labroom Check-In";
            this.labroomCheckinBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcrlLogbookDBDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labmemberLabel;
        private System.Windows.Forms.Label labmemberFillLabel;
        private System.Windows.Forms.Label loginTimeFillLabel;
        private System.Windows.Forms.Label loginTimeLabel;
        private System.Windows.Forms.GroupBox labroomCheckinBox;
        private System.Windows.Forms.Button labroomButton3;
        private System.Windows.Forms.Button labroomButton2;
        private System.Windows.Forms.Button labroomButton1;
        private System.Windows.Forms.Button logoutButton;
        private PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.LoginLogTableAdapter loginLogTableAdapter;
        private PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.LabroomLogTableAdapter labroomLogTableAdapter;
        private PCRLLogbookDBDataSet1 pcrlLogbookDBDataSet;
        private System.Windows.Forms.Button reviewSaveButton;
        private PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.ObservationsTableAdapter observationsTableAdapter;
    }
}