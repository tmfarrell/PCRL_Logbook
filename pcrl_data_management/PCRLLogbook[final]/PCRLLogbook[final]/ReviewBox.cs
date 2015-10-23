using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing; 
using System.Linq;
using System.Text;

namespace PCRLLogbook
{
    public class ReviewBox : GroupBox
    {
        string labroom;
        LogData monkData; 
        Button change = new Button();
        RichTextBox dataDisplay = new RichTextBox();
        
        public ReviewBox() { }

        public ReviewBox(LogData monkdata)
        {
            Width = 160;
            Height = 210;
            monkData = monkdata;
            labroom = monkdata.Labroom; 
            Name = monkdata.Mid + "ReviewBox"; 
            Text = monkdata.Station + "-" + monkdata.Mid;

            dataDisplay.Width = 150;
            dataDisplay.Height = 162;
            dataDisplay.Text = monkdata.toString();
            dataDisplay.Location = new Point(6, 16);
            dataDisplay.Font = new Font("Microsoft San Serif", 8);

            change.Width = 80;
            change.Text = "Change";
            change.Location = new Point(40, 184);
            change.Click += new System.EventHandler(change_Click);

            Controls.Add(dataDisplay);
            Controls.Add(change); 
        }

        public void change_Click(object sender, EventArgs e)
        {
            MessageBox.Show("dataDisplay: " + dataDisplay.Text + 
              "monkData: " + monkData.toString());

            monkData.updateFromString(dataDisplay.Text);

            MessageBox.Show(monkData.toString());
        }

        public LogData getData()
        {
            return monkData; 
        }
    }
}
