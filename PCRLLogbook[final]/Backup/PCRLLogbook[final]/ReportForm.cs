using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PCRLLogbook
{
    public partial class ReportForm : Form
    {
        public ReportForm(string mid)
        {
            InitializeComponent();

            reportHeader.Text = "Report for " + mid + " on [Date]:";

            //fill in data fields from dataset
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close(); 
        }
    }
}
