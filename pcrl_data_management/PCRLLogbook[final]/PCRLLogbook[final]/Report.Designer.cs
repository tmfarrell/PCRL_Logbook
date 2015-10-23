namespace PCRLLogbook
{
    partial class Report
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
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
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
            this.reportHeader.Text = "Report for [MID] on [Date]: ";
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
            this.pelletsFillLabel.Location = new System.Drawing.Point(114, 37);
            this.pelletsFillLabel.Name = "pelletsFillLabel";
            this.pelletsFillLabel.Size = new System.Drawing.Size(43, 13);
            this.pelletsFillLabel.TabIndex = 2;
            this.pelletsFillLabel.Text = "[x.xx] g ";
            // 
            // cognitiveTestFillLabel
            // 
            this.cognitiveTestFillLabel.AutoSize = true;
            this.cognitiveTestFillLabel.Location = new System.Drawing.Point(114, 61);
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
            this.activityFillLabel.Location = new System.Drawing.Point(114, 84);
            this.activityFillLabel.Name = "activityFillLabel";
            this.activityFillLabel.Size = new System.Drawing.Size(31, 13);
            this.activityFillLabel.TabIndex = 6;
            this.activityFillLabel.Text = "[x.xx]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Avg Activity: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Comments: ";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(100, 177);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 8;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 212);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.activityFillLabel);
            this.Controls.Add(this.label4);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeButton;
    }
}