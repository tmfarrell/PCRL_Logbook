namespace PCRLLogbook
{
    partial class CreateAccountForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label labMemberLabel;
            System.Windows.Forms.Label firstLabel;
            System.Windows.Forms.Label lastLabel;
            System.Windows.Forms.Label passwordLabel;
            System.Windows.Forms.Label cellNumberLabel;
            System.Windows.Forms.Label confirmPwLabel;
            this.labMemberTextBox = new System.Windows.Forms.TextBox();
            this.firstTextBox = new System.Windows.Forms.TextBox();
            this.lastTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.cellNumberTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.confirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.backButton = new System.Windows.Forms.Button();
            this.labMemberTableAdapter = new PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.LabMemberTableAdapter();
            this.pcrlLogbookDBDataSet = new PCRLLogbook.PCRLLogbookDBDataSet1();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            labMemberLabel = new System.Windows.Forms.Label();
            firstLabel = new System.Windows.Forms.Label();
            lastLabel = new System.Windows.Forms.Label();
            passwordLabel = new System.Windows.Forms.Label();
            cellNumberLabel = new System.Windows.Forms.Label();
            confirmPwLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcrlLogbookDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labMemberLabel
            // 
            labMemberLabel.AutoSize = true;
            labMemberLabel.Location = new System.Drawing.Point(18, 25);
            labMemberLabel.Name = "labMemberLabel";
            labMemberLabel.Size = new System.Drawing.Size(58, 13);
            labMemberLabel.TabIndex = 1;
            labMemberLabel.Text = "Username:";
            // 
            // firstLabel
            // 
            firstLabel.AutoSize = true;
            firstLabel.Location = new System.Drawing.Point(18, 55);
            firstLabel.Name = "firstLabel";
            firstLabel.Size = new System.Drawing.Size(29, 13);
            firstLabel.TabIndex = 3;
            firstLabel.Text = "First:";
            // 
            // lastLabel
            // 
            lastLabel.AutoSize = true;
            lastLabel.Location = new System.Drawing.Point(18, 87);
            lastLabel.Name = "lastLabel";
            lastLabel.Size = new System.Drawing.Size(30, 13);
            lastLabel.TabIndex = 5;
            lastLabel.Text = "Last:";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new System.Drawing.Point(18, 122);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new System.Drawing.Size(56, 13);
            passwordLabel.TabIndex = 7;
            passwordLabel.Text = "Password:";
            // 
            // cellNumberLabel
            // 
            cellNumberLabel.AutoSize = true;
            cellNumberLabel.Location = new System.Drawing.Point(18, 195);
            cellNumberLabel.Name = "cellNumberLabel";
            cellNumberLabel.Size = new System.Drawing.Size(67, 13);
            cellNumberLabel.TabIndex = 9;
            cellNumberLabel.Text = "Cell Number:";
            // 
            // confirmPwLabel
            // 
            confirmPwLabel.AutoSize = true;
            confirmPwLabel.Location = new System.Drawing.Point(18, 160);
            confirmPwLabel.Name = "confirmPwLabel";
            confirmPwLabel.Size = new System.Drawing.Size(94, 13);
            confirmPwLabel.TabIndex = 12;
            confirmPwLabel.Text = "Confirm Password:";
            // 
            // labMemberTextBox
            // 
            this.labMemberTextBox.Location = new System.Drawing.Point(120, 22);
            this.labMemberTextBox.Name = "labMemberTextBox";
            this.labMemberTextBox.Size = new System.Drawing.Size(130, 20);
            this.labMemberTextBox.TabIndex = 2;
            // 
            // firstTextBox
            // 
            this.firstTextBox.Location = new System.Drawing.Point(120, 55);
            this.firstTextBox.Name = "firstTextBox";
            this.firstTextBox.Size = new System.Drawing.Size(130, 20);
            this.firstTextBox.TabIndex = 4;
            // 
            // lastTextBox
            // 
            this.lastTextBox.Location = new System.Drawing.Point(120, 87);
            this.lastTextBox.Name = "lastTextBox";
            this.lastTextBox.Size = new System.Drawing.Size(130, 20);
            this.lastTextBox.TabIndex = 6;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(120, 122);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(130, 20);
            this.passwordTextBox.TabIndex = 8;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // cellNumberTextBox
            // 
            this.cellNumberTextBox.Location = new System.Drawing.Point(120, 192);
            this.cellNumberTextBox.Name = "cellNumberTextBox";
            this.cellNumberTextBox.Size = new System.Drawing.Size(130, 20);
            this.cellNumberTextBox.TabIndex = 12;
            this.cellNumberTextBox.Text = "(xxx)xxx-xxxx";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(111, 233);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(127, 23);
            this.saveButton.TabIndex = 14;
            this.saveButton.Text = "Save and Login";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // confirmPasswordTextBox
            // 
            this.confirmPasswordTextBox.Location = new System.Drawing.Point(120, 157);
            this.confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            this.confirmPasswordTextBox.Size = new System.Drawing.Size(130, 20);
            this.confirmPasswordTextBox.TabIndex = 10;
            this.confirmPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(21, 233);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 16;
            this.backButton.Text = "Exit";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // labMemberTableAdapter
            // 
            this.labMemberTableAdapter.ClearBeforeFill = true;
            // 
            // pcrlLogbookDBDataSet
            // 
            this.pcrlLogbookDBDataSet.DataSetName = "PCRLLogbookDBDataSet1";
            this.pcrlLogbookDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = this.pcrlLogbookDBDataSet;
            this.bindingSource.Position = 0;
            // 
            // CreateAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 277);
            this.Controls.Add(this.backButton);
            this.Controls.Add(confirmPwLabel);
            this.Controls.Add(this.confirmPasswordTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(labMemberLabel);
            this.Controls.Add(this.labMemberTextBox);
            this.Controls.Add(firstLabel);
            this.Controls.Add(this.firstTextBox);
            this.Controls.Add(lastLabel);
            this.Controls.Add(this.lastTextBox);
            this.Controls.Add(passwordLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(cellNumberLabel);
            this.Controls.Add(this.cellNumberTextBox);
            this.Name = "CreateAccountForm";
            this.ShowIcon = false;
            this.Text = " PCRL Logbook: Create Account";
            ((System.ComponentModel.ISupportInitialize)(this.pcrlLogbookDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox labMemberTextBox;
        private System.Windows.Forms.TextBox firstTextBox;
        private System.Windows.Forms.TextBox lastTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox cellNumberTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox confirmPasswordTextBox;
        private System.Windows.Forms.Button backButton;
        private PCRLLogbook.PCRLLogbookDBDataSet1TableAdapters.LabMemberTableAdapter labMemberTableAdapter;
        private PCRLLogbookDBDataSet1 pcrlLogbookDBDataSet;
        private System.Windows.Forms.BindingSource bindingSource;

    }
}