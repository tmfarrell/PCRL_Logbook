namespace PCRLLogbook
{
    partial class LogForm
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
            this.leftLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.rightLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.leftLabel = new System.Windows.Forms.Label();
            this.rightLabel = new System.Windows.Forms.Label();
            this.labmemberLabel = new System.Windows.Forms.Label();
            this.labmemberFillLabel = new System.Windows.Forms.Label();
            this.checkinFillLabel = new System.Windows.Forms.Label();
            this.checkinLabel = new System.Windows.Forms.Label();
            this.saveCheckoutButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.monkeyTableAdapter = new PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.MonkeyTableAdapter();
            this.observationsTableAdapter = new PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.ObservationsTableAdapter();
            this.pcrlLogbookDBDataSet = new PCRLLogbook.PCRLLogbookDBDataSet1();
            ((System.ComponentModel.ISupportInitialize)(this.pcrlLogbookDBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // leftLayoutPanel
            // 
            this.leftLayoutPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.leftLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.leftLayoutPanel.Location = new System.Drawing.Point(15, 70);
            this.leftLayoutPanel.Name = "leftLayoutPanel";
            this.leftLayoutPanel.Padding = new System.Windows.Forms.Padding(2);
            this.leftLayoutPanel.Size = new System.Drawing.Size(1200, 420);
            this.leftLayoutPanel.TabIndex = 0;
            // 
            // rightLayoutPanel
            // 
            this.rightLayoutPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rightLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.rightLayoutPanel.Location = new System.Drawing.Point(15, 512);
            this.rightLayoutPanel.Name = "rightLayoutPanel";
            this.rightLayoutPanel.Size = new System.Drawing.Size(1200, 420);
            this.rightLayoutPanel.TabIndex = 1;
            // 
            // leftLabel
            // 
            this.leftLabel.AutoSize = true;
            this.leftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftLabel.Location = new System.Drawing.Point(19, 57);
            this.leftLabel.Name = "leftLabel";
            this.leftLabel.Size = new System.Drawing.Size(49, 13);
            this.leftLabel.TabIndex = 2;
            this.leftLabel.Text = "Left Side";
            // 
            // rightLabel
            // 
            this.rightLabel.AutoSize = true;
            this.rightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightLabel.Location = new System.Drawing.Point(19, 499);
            this.rightLabel.Name = "rightLabel";
            this.rightLabel.Size = new System.Drawing.Size(56, 13);
            this.rightLabel.TabIndex = 3;
            this.rightLabel.Text = "Right Side";
            // 
            // labmemberLabel
            // 
            this.labmemberLabel.AutoSize = true;
            this.labmemberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labmemberLabel.Location = new System.Drawing.Point(19, 9);
            this.labmemberLabel.Name = "labmemberLabel";
            this.labmemberLabel.Size = new System.Drawing.Size(69, 13);
            this.labmemberLabel.TabIndex = 4;
            this.labmemberLabel.Text = "Lab Member:";
            // 
            // labmemberFillLabel
            // 
            this.labmemberFillLabel.AutoSize = true;
            this.labmemberFillLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labmemberFillLabel.Location = new System.Drawing.Point(105, 9);
            this.labmemberFillLabel.Name = "labmemberFillLabel";
            this.labmemberFillLabel.Size = new System.Drawing.Size(72, 13);
            this.labmemberFillLabel.TabIndex = 5;
            this.labmemberFillLabel.Text = "[Lab Member]";
            // 
            // checkinFillLabel
            // 
            this.checkinFillLabel.AutoSize = true;
            this.checkinFillLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkinFillLabel.Location = new System.Drawing.Point(105, 31);
            this.checkinFillLabel.Name = "checkinFillLabel";
            this.checkinFillLabel.Size = new System.Drawing.Size(125, 13);
            this.checkinFillLabel.TabIndex = 7;
            this.checkinFillLabel.Text = "[Labroom # at DateTime]";
            // 
            // checkinLabel
            // 
            this.checkinLabel.AutoSize = true;
            this.checkinLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkinLabel.Location = new System.Drawing.Point(19, 31);
            this.checkinLabel.Name = "checkinLabel";
            this.checkinLabel.Size = new System.Drawing.Size(67, 13);
            this.checkinLabel.TabIndex = 6;
            this.checkinLabel.Text = "Checked in: ";
            // 
            // saveCheckoutButton
            // 
            this.saveCheckoutButton.Location = new System.Drawing.Point(1084, 26);
            this.saveCheckoutButton.Name = "saveCheckoutButton";
            this.saveCheckoutButton.Size = new System.Drawing.Size(131, 23);
            this.saveCheckoutButton.TabIndex = 9;
            this.saveCheckoutButton.Text = "Save and Checkout";
            this.saveCheckoutButton.UseVisualStyleBackColor = true;
            this.saveCheckoutButton.Click += new System.EventHandler(this.saveCheckoutButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(926, 26);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(71, 23);
            this.backButton.TabIndex = 10;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1003, 26);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // monkeyTableAdapter
            // 
            this.monkeyTableAdapter.ClearBeforeFill = true;
            // 
            // observationsTableAdapter
            // 
            this.observationsTableAdapter.ClearBeforeFill = true;
            // 
            // pcrlLogbookDBDataSet
            // 
            this.pcrlLogbookDBDataSet.DataSetName = "PCRLLogbookDBDataSet1";
            this.pcrlLogbookDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1234, 882);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.saveCheckoutButton);
            this.Controls.Add(this.checkinFillLabel);
            this.Controls.Add(this.checkinLabel);
            this.Controls.Add(this.labmemberFillLabel);
            this.Controls.Add(this.labmemberLabel);
            this.Controls.Add(this.rightLabel);
            this.Controls.Add(this.leftLabel);
            this.Controls.Add(this.rightLayoutPanel);
            this.Controls.Add(this.leftLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(750, 700);
            this.Name = "LogForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NHP Logbook: Log";
            ((System.ComponentModel.ISupportInitialize)(this.pcrlLogbookDBDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel leftLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel rightLayoutPanel;
        private System.Windows.Forms.Label leftLabel;
        private System.Windows.Forms.Label rightLabel;
        private System.Windows.Forms.Label labmemberLabel;
        private System.Windows.Forms.Label labmemberFillLabel;
        private System.Windows.Forms.Label checkinFillLabel;
        private System.Windows.Forms.Label checkinLabel;
        private System.Windows.Forms.Button saveCheckoutButton;
        private System.Windows.Forms.Button backButton;
        private PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.MonkeyTableAdapter monkeyTableAdapter;
        private PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.ObservationsTableAdapter observationsTableAdapter;
        private PCRLLogbookDBDataSet1 pcrlLogbookDBDataSet;
        private System.Windows.Forms.Button saveButton;
    }
}