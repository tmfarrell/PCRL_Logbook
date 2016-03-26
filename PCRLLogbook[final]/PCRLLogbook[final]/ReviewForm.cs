using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace PCRLLogbook
{
    public partial class ReviewForm : Form
    {
        LabroomForm Labroom = new LabroomForm();
        Dictionary<string, ReviewBox[]> Data = new Dictionary<string, ReviewBox[]>(); 

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


            foreach (string labroom in Labroom.Data.Keys)
            {
                // add each new ReviewBox to Data field
                Data[labroom] = new ReviewBox[8];
                int i = 0;
                try {
                    foreach (LogData log in Labroom.Data[labroom])
                        Data[labroom][i++] = new ReviewBox(log);
                }
                catch (KeyNotFoundException e) { 
                    //skip if no data input for that labroom
                }

                if (labroom == "W715") {
                    foreach (ReviewBox rB in Data[labroom]) 
                        W715Panel.Controls.Add(rB);
                } 
                else if (labroom == "W716") {
                    foreach (ReviewBox rB in Data[labroom]) 
                        W716Panel.Controls.Add(rB);
                }
                else if (labroom == "W718") {
                    foreach (ReviewBox rB in Data[labroom]) 
                        W718Panel.Controls.Add(rB);
                }   
            } 

        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            //get changes
            foreach (string labroom in Data.Keys) {
                for (int i = 0; i < 8; i++)
                {
                    try {
                        Labroom.Data[labroom][i] = Data[labroom][i].getData();
                        //MessageBox.Show("Got data from reviewbox and stored in labroom data struct..."); 

                    }
                    catch (KeyNotFoundException exp) {
                        //skip if no data input for that labroom
                    }
                } 
            }
            MessageBox.Show("Changes recorded.");  
            Close(); 
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show( 
                "Are you sure you want to go back?\n" + 
                "This will result in unsaved changes.\n",
                "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Labroom.Show();
                Close();
            } 
        } 
    }
}
