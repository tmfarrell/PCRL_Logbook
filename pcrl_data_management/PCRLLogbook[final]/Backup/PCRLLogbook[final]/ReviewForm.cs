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
    public partial class ReviewForm : Form
    {
        LabroomForm Labroom = new LabroomForm(); 

        public ReviewForm()
        {
            InitializeComponent();
        }

        public ReviewForm(LabroomForm labroomForm)
        {
            InitializeComponent();

            Labroom = labroomForm; 
            labmemberFillLabel.Text = Labroom.labmemberName;
            dateFillLabel.Text = DateTime.Now.ToLongDateString(); 

            foreach (LogData d in Labroom.Data)
            {
                if (d.Labroom == 1) 
                    labroom1Panel.Controls.Add(new ReviewBox(d));
                else if (d.Labroom == 2)
                    labroom2Panel.Controls.Add(new ReviewBox(d)); 
            } 

        } 
    }
}
