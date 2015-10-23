namespace PCRLLogbook
{
    partial class ReviewForm
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
            this.labmemberFillLabel = new System.Windows.Forms.Label();
            this.labmemberLabel = new System.Windows.Forms.Label();
            this.dateFillLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.labroom1Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.labroom1Label = new System.Windows.Forms.Label();
            this.labroom2Label = new System.Windows.Forms.Label();
            this.labroom2Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // labmemberFillLabel
            // 
            this.labmemberFillLabel.AutoSize = true;
            this.labmemberFillLabel.Location = new System.Drawing.Point(90, 12);
            this.labmemberFillLabel.Name = "labmemberFillLabel";
            this.labmemberFillLabel.Size = new System.Drawing.Size(55, 13);
            this.labmemberFillLabel.TabIndex = 3;
            this.labmemberFillLabel.Text = "[First Last]";
            // 
            // labmemberLabel
            // 
            this.labmemberLabel.AutoSize = true;
            this.labmemberLabel.Location = new System.Drawing.Point(12, 12);
            this.labmemberLabel.Name = "labmemberLabel";
            this.labmemberLabel.Size = new System.Drawing.Size(72, 13);
            this.labmemberLabel.TabIndex = 2;
            this.labmemberLabel.Text = "Lab Member: ";
            // 
            // dateFillLabel
            // 
            this.dateFillLabel.AutoSize = true;
            this.dateFillLabel.Location = new System.Drawing.Point(90, 34);
            this.dateFillLabel.Name = "dateFillLabel";
            this.dateFillLabel.Size = new System.Drawing.Size(36, 13);
            this.dateFillLabel.TabIndex = 5;
            this.dateFillLabel.Text = "[Date]";
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(12, 34);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(36, 13);
            this.dateLabel.TabIndex = 4;
            this.dateLabel.Text = "Date: ";
            // 
            // labroom1Panel
            // 
            this.labroom1Panel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labroom1Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labroom1Panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.labroom1Panel.Location = new System.Drawing.Point(12, 79);
            this.labroom1Panel.Name = "labroom1Panel";
            this.labroom1Panel.Size = new System.Drawing.Size(777, 228);
            this.labroom1Panel.TabIndex = 6;
            // 
            // labroom1Label
            // 
            this.labroom1Label.AutoSize = true;
            this.labroom1Label.Location = new System.Drawing.Point(12, 63);
            this.labroom1Label.Name = "labroom1Label";
            this.labroom1Label.Size = new System.Drawing.Size(63, 13);
            this.labroom1Label.TabIndex = 7;
            this.labroom1Label.Text = "Labroom 1: ";
            // 
            // labroom2Label
            // 
            this.labroom2Label.AutoSize = true;
            this.labroom2Label.Location = new System.Drawing.Point(12, 317);
            this.labroom2Label.Name = "labroom2Label";
            this.labroom2Label.Size = new System.Drawing.Size(63, 13);
            this.labroom2Label.TabIndex = 9;
            this.labroom2Label.Text = "Labroom 2: ";
            // 
            // labroom2Panel
            // 
            this.labroom2Panel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labroom2Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labroom2Panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.labroom2Panel.Location = new System.Drawing.Point(12, 333);
            this.labroom2Panel.Name = "labroom2Panel";
            this.labroom2Panel.Size = new System.Drawing.Size(777, 228);
            this.labroom2Panel.TabIndex = 8;
            // 
            // ReviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 580);
            this.Controls.Add(this.labroom2Label);
            this.Controls.Add(this.labroom2Panel);
            this.Controls.Add(this.labroom1Label);
            this.Controls.Add(this.labroom1Panel);
            this.Controls.Add(this.dateFillLabel);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.labmemberFillLabel);
            this.Controls.Add(this.labmemberLabel);
            this.Name = "ReviewForm";
            this.ShowIcon = false;
            this.Text = "NHP Logbook: Review";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labmemberFillLabel;
        private System.Windows.Forms.Label labmemberLabel;
        private System.Windows.Forms.Label dateFillLabel;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.FlowLayoutPanel labroom1Panel;
        private System.Windows.Forms.Label labroom1Label;
        private System.Windows.Forms.Label labroom2Label;
        private System.Windows.Forms.FlowLayoutPanel labroom2Panel;
    }
}