namespace PCRLLogbook
{
    partial class Comment
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
            this.commentTextbox = new System.Windows.Forms.RichTextBox();
            this.commentLabel = new System.Windows.Forms.Label();
            this.recordButton = new System.Windows.Forms.Button();
            this.dateLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // commentTextbox
            // 
            this.commentTextbox.Location = new System.Drawing.Point(12, 60);
            this.commentTextbox.Name = "commentTextbox";
            this.commentTextbox.Size = new System.Drawing.Size(318, 209);
            this.commentTextbox.TabIndex = 0;
            this.commentTextbox.Text = "";
            // 
            // commentLabel
            // 
            this.commentLabel.AutoSize = true;
            this.commentLabel.Location = new System.Drawing.Point(12, 9);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(95, 13);
            this.commentLabel.TabIndex = 1;
            this.commentLabel.Text = "Comment on [MID]";
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(130, 278);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(75, 23);
            this.recordButton.TabIndex = 2;
            this.recordButton.Text = "Record";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(12, 32);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(59, 13);
            this.dateLabel.TabIndex = 3;
            this.dateLabel.Text = "[DateTime]";
            // 
            // Comment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 308);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.recordButton);
            this.Controls.Add(this.commentLabel);
            this.Controls.Add(this.commentTextbox);
            this.Name = "Comment";
            this.ShowIcon = false;
            this.Text = " NHP Logbook: Log Comment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox commentTextbox;
        private System.Windows.Forms.Label commentLabel;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.Label dateLabel;
    }
}