using System;
using System.Linq;
using System.Text;
using System.Drawing; 
using System.Windows.Forms;
using System.Collections.Generic;

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
            Width = 175;
            Height = 225;
            monkData = monkdata;
            labroom = monkdata.Labroom; 
            Name = monkdata.Mid + "ReviewBox"; 
            Text = monkdata.Station + "-" + monkdata.Mid;

            dataDisplay.Width = 150;
            dataDisplay.Height = 180;
            dataDisplay.Text = monkdata.toString();
            dataDisplay.Location = new Point(6, 16);
            dataDisplay.Font = new Font("Microsoft San Serif", 8);

            /*change.Width = 80;
            change.Text = "Change";
            change.Location = new Point(40, 184);
            change.Click += new System.EventHandler(change_Click);
            Controls.Add(change);*/

            Controls.Add(dataDisplay); 
        }

        public LogData getData()
        {
            monkData.updateFromString(dataDisplay.Text);
            return monkData; 
        }
    }
}
