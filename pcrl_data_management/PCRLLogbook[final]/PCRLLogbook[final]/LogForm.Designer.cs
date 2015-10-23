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
            this.labmemberLabel = new System.Windows.Forms.Label();
            this.labmemberFillLabel = new System.Windows.Forms.Label();
            this.checkinFillLabel = new System.Windows.Forms.Label();
            this.checkinLabel = new System.Windows.Forms.Label();
            this.saveCheckoutButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // leftLayoutPanel
            // 
            this.leftLayoutPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.leftLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.leftLayoutPanel.Location = new System.Drawing.Point(20, 90);
            this.leftLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.leftLayoutPanel.Name = "leftLayoutPanel";
            this.leftLayoutPanel.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.leftLayoutPanel.Size = new System.Drawing.Size(1500, 320);
            this.leftLayoutPanel.TabIndex = 0;
            // 
            // rightLayoutPanel
            // 
            this.rightLayoutPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rightLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.rightLayoutPanel.Location = new System.Drawing.Point(20, 440);
            this.rightLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.rightLayoutPanel.Name = "rightLayoutPanel";
            this.rightLayoutPanel.Size = new System.Drawing.Size(1500, 320);
            this.rightLayoutPanel.TabIndex = 1;
            // 
            // labmemberLabel
            // 
            this.labmemberLabel.AutoSize = true;
            this.labmemberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labmemberLabel.Location = new System.Drawing.Point(25, 21);
            this.labmemberLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labmemberLabel.Name = "labmemberLabel";
            this.labmemberLabel.Size = new System.Drawing.Size(87, 16);
            this.labmemberLabel.TabIndex = 4;
            this.labmemberLabel.Text = "Lab Member:";
            // 
            // labmemberFillLabel
            // 
            this.labmemberFillLabel.AutoSize = true;
            this.labmemberFillLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labmemberFillLabel.Location = new System.Drawing.Point(140, 21);
            this.labmemberFillLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labmemberFillLabel.Name = "labmemberFillLabel";
            this.labmemberFillLabel.Size = new System.Drawing.Size(92, 16);
            this.labmemberFillLabel.TabIndex = 5;
            this.labmemberFillLabel.Text = "[Lab Member]";
            // 
            // checkinFillLabel
            // 
            this.checkinFillLabel.AutoSize = true;
            this.checkinFillLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkinFillLabel.Location = new System.Drawing.Point(140, 48);
            this.checkinFillLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkinFillLabel.Name = "checkinFillLabel";
            this.checkinFillLabel.Size = new System.Drawing.Size(157, 16);
            this.checkinFillLabel.TabIndex = 7;
            this.checkinFillLabel.Text = "[Labroom # at DateTime]";
            // 
            // checkinLabel
            // 
            this.checkinLabel.AutoSize = true;
            this.checkinLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkinLabel.Location = new System.Drawing.Point(25, 48);
            this.checkinLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkinLabel.Name = "checkinLabel";
            this.checkinLabel.Size = new System.Drawing.Size(81, 16);
            this.checkinLabel.TabIndex = 6;
            this.checkinLabel.Text = "Checked in: ";
            // 
            // saveCheckoutButton
            // 
            this.saveCheckoutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveCheckoutButton.Location = new System.Drawing.Point(1261, 26);
            this.saveCheckoutButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveCheckoutButton.Name = "saveCheckoutButton";
            this.saveCheckoutButton.Size = new System.Drawing.Size(183, 38);
            this.saveCheckoutButton.TabIndex = 9;
            this.saveCheckoutButton.Text = "Save and Checkout";
            this.saveCheckoutButton.UseVisualStyleBackColor = true;
            this.saveCheckoutButton.Click += new System.EventHandler(this.saveCheckoutButton_Click);
            // 
            // backButton
            // 
            this.backButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.Location = new System.Drawing.Point(1037, 26);
            this.backButton.Margin = new System.Windows.Forms.Padding(4);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(103, 38);
            this.backButton.TabIndex = 10;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(1148, 26);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(108, 38);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1544, 815);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.saveCheckoutButton);
            this.Controls.Add(this.checkinFillLabel);
            this.Controls.Add(this.checkinLabel);
            this.Controls.Add(this.labmemberFillLabel);
            this.Controls.Add(this.labmemberLabel);
            this.Controls.Add(this.rightLayoutPanel);
            this.Controls.Add(this.leftLayoutPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(995, 853);
            this.Name = "LogForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCRL Logbook: Log";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel leftLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel rightLayoutPanel;
        private System.Windows.Forms.Label labmemberLabel;
        private System.Windows.Forms.Label labmemberFillLabel;
        private System.Windows.Forms.Label checkinFillLabel;
        private System.Windows.Forms.Label checkinLabel;
        private System.Windows.Forms.Button saveCheckoutButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button saveButton;
    }
}