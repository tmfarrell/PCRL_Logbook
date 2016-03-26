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
    public partial class Comment : Form
    {
        public string comment;
        public LogBox Logbox; 
        
        public Comment(string mid, LogBox logbox)
        {
            InitializeComponent();

            Logbox = logbox; 
            commentLabel.Text = "Comment on " + mid; 
            dateLabel.Text = DateTime.Now.ToLongDateString(); 
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            comment = commentTextbox.Text;
            Logbox.setComment(comment);
            Close(); 
        }
    }
}
