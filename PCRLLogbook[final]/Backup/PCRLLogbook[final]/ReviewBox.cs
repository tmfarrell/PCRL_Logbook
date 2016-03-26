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
        LogData MonkData;
        RichTextBox DataDisplay = new RichTextBox();
        Button Change = new Button(); 

        public ReviewBox() { }

        public ReviewBox(LogData monkdata)
        {
            MonkData = monkdata;
            Name = monkdata.Mid + "ReviewBox"; 
            Text = monkdata.Station + "-" + monkdata.Mid;

            DataDisplay.ReadOnly = true; 
            DataDisplay.Text = monkdata.toString();
            DataDisplay.Location = new Point(6, 16);

            Change.Text = "Make Changes";
            Change.Width = 80;
            Change.Location = new Point(25, 25); 

            Controls.Add(DataDisplay);
            Controls.Add(Change); 
        } 


    }
}
