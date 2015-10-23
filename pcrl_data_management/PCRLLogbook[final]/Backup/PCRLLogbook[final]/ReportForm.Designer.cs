namespace PCRLLogbook
{
    partial class ReportForm
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
            this.reportHeader = new System.Windows.Forms.Label();
            this.pelletsLabel = new System.Windows.Forms.Label();
            this.pelletsFillLabel = new System.Windows.Forms.Label();
            this.cognitiveTestFillLabel = new System.Windows.Forms.Label();
            this.congitiveTestLabel = new System.Windows.Forms.Label();
            this.activityFillLabel = new System.Windows.Forms.Label();
            this.activityLabel = new System.Windows.Forms.Label();
            this.latestObsLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.latestFillBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // reportHeader
            // 
            this.reportHeader.AutoSize = true;
            this.reportHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportHeader.Location = new System.Drawing.Point(12, 13);
            this.reportHeader.Name = "reportHeader";
            this.reportHeader.Size = new System.Drawing.Size(136, 13);
            this.reportHeader.TabIndex = 0;
            this.reportHeader.Text = "Report for [MID] for [Date]: ";
            // 
            // pelletsLabel
            // 
            this.pelletsLabel.AutoSize = true;
            this.pelletsLabel.Location = new System.Drawing.Point(12, 37);
            this.pelletsLabel.Name = "pelletsLabel";
            this.pelletsLabel.Size = new System.Drawing.Size(97, 13);
            this.pelletsLabel.TabIndex = 1;
            this.pelletsLabel.Text = "Pellets Consumed: ";
            // 
            // pelletsFillLabel
            // 
            this.pelletsFillLabel.AutoSize = true;
            this.pelletsFillLabel.Location = new System.Drawing.Point(119, 37);
            this.pelletsFillLabel.Name = "pelletsFillLabel";
            this.pelletsFillLabel.Size = new System.Drawing.Size(43, 13);
            this.pelletsFillLabel.TabIndex = 2;
            this.pelletsFillLabel.Text = "[x.xx] g ";
            // 
            // cognitiveTestFillLabel
            // 
            this.cognitiveTestFillLabel.AutoSize = true;
            this.cognitiveTestFillLabel.Location = new System.Drawing.Point(119, 61);
            this.cognitiveTestFillLabel.Name = "cognitiveTestFillLabel";
            this.cognitiveTestFillLabel.Size = new System.Drawing.Size(42, 13);
            this.cognitiveTestFillLabel.TabIndex = 4;
            this.cognitiveTestFillLabel.Text = "[x.xx] s ";
            // 
            // congitiveTestLabel
            // 
            this.congitiveTestLabel.AutoSize = true;
            this.congitiveTestLabel.Location = new System.Drawing.Point(12, 61);
            this.congitiveTestLabel.Name = "congitiveTestLabel";
            this.congitiveTestLabel.Size = new System.Drawing.Size(103, 13);
            this.congitiveTestLabel.TabIndex = 3;
            this.congitiveTestLabel.Text = "Cognitive Test Avg: ";
            // 
            // activityFillLabel
            // 
            this.activityFillLabel.AutoSize = true;
            this.activityFillLabel.Location = new System.Drawing.Point(119, 84);
            this.activityFillLabel.Name = "activityFillLabel";
            this.activityFillLabel.Size = new System.Drawing.Size(31, 13);
            this.activityFillLabel.TabIndex = 6;
            this.activityFillLabel.Text = "[x.xx]";
            // 
            // activityLabel
            // 
            this.activityLabel.AutoSize = true;
            this.activityLabel.Location = new System.Drawing.Point(13, 84);
            this.activityLabel.Name = "activityLabel";
            this.activityLabel.Size = new System.Drawing.Size(69, 13);
            this.activityLabel.TabIndex = 5;
            this.activityLabel.Text = "Avg Activity: ";
            // 
            // latestObsLabel
            // 
            this.latestObsLabel.AutoSize = true;
            this.latestObsLabel.Location = new System.Drawing.Point(12, 107);
            this.latestObsLabel.Name = "latestObsLabel";
            this.latestObsLabel.Size = new System.Drawing.Size(104, 13);
            this.latestObsLabel.TabIndex = 7;
            this.latestObsLabel.Text = "Latest Observations:";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(101, 210);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 8;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // latestFillBox
            // 
            this.latestFillBox.Location = new System.Drawing.Point(122, 107);
            this.latestFillBox.Name = "latestFillBox";
            this.latestFillBox.ReadOnly = true;
            this.latestFillBox.Size = new System.Drawing.Size(155, 97);
            this.latestFillBox.TabIndex = 9;
            this.latestFillBox.Text = "Observations";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 242);
            this.Controls.Add(this.latestFillBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.latestObsLabel);
            this.Controls.Add(this.activityFillLabel);
            this.Controls.Add(this.activityLabel);
            this.Controls.Add(this.cognitiveTestFillLabel);
            this.Controls.Add(this.congitiveTestLabel);
            this.Controls.Add(this.pelletsFillLabel);
            this.Controls.Add(this.pelletsLabel);
            this.Controls.Add(this.reportHeader);
            this.Name = "Report";
            this.ShowIcon = false;
            this.Text = " PCRL Logbook: Report";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label reportHeader;
        private System.Windows.Forms.Label pelletsLabel;
        private System.Windows.Forms.Label pelletsFillLabel;
        private System.Windows.Forms.Label cognitiveTestFillLabel;
        private System.Windows.Forms.Label congitiveTestLabel;
        private System.Windows.Forms.Label activityFillLabel;
        private System.Windows.Forms.Label activityLabel;
        private System.Windows.Forms.Label latestObsLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.RichTextBox latestFillBox;
    }
}