using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
 

namespace PCRLLogbook
{
    public class LogBox : GroupBox
    {
        LogData data; 
        
        Label behavior = new Label();
        CheckBox behaviorCheck = new CheckBox();
        ComboBox behaviorList = new ComboBox();  
        Label stool = new Label();
        CheckBox stoolCheck = new CheckBox();
        ComboBox stoolList = new ComboBox(); 
        Label blood = new Label();
        CheckBox bloodCheck = new CheckBox();
        ComboBox bloodList = new ComboBox(); 
        Label equipment = new Label();
        CheckBox equipmentCheck = new CheckBox();
        ComboBox equipmentList = new ComboBox(); 
        Button report = new Button(); 
        ReportForm reportForm; 
        Button comment = new Button();
        Comment commentForm;

        public LogBox() { }

        public LogBox(string name, int station, int labroom)
        {
            data = new LogData(name, station, labroom); 
            commentForm = new Comment(name, this); 
            Name = name + "Logbox";
            Text = station + "-" + name;
            BackColor = SystemColors.Control;
            Width = 248;
            Height = 152;
            Margin = new Padding(8);

            report.Text = "Latest Report";
            report.Location = new Point(148, 120);
            report.Height = 20;
            report.Width = 88;
            report.FlatStyle = FlatStyle.Standard;
            report.Click += new System.EventHandler(report_Click);

            comment.Text = "Comment";
            comment.Location = new Point(172, 96); 
            comment.Height = 20;
            comment.Width = 64;
            comment.FlatStyle = FlatStyle.Standard;
            comment.Click += new System.EventHandler(comment_Click);

            behavior.Text = "Misbehaved?";
            behavior.Height = 12;
            behavior.Width = 80;
            behavior.Location = new Point(6, 24);
            behaviorCheck.Location = new Point(105, 20);
            behaviorCheck.Width = 20;
            behaviorCheck.CheckedChanged += new System.EventHandler(behaviorCheck_Checked);
            behaviorList.Location = new Point(124, 20);
            behaviorList.Items.AddRange(new object[] {"pacing",
                                                      "not eating",
                                                      "not sleeping",
                                                      "aggressive"});

            stool.Text = "Stool abnormal?";
            stool.Height = 12;
            stool.Width = 90;
            stool.Location = new Point(6, 48);
            stoolCheck.Location = new Point(105, 44);
            stoolCheck.Width = 20;
            stoolCheck.CheckedChanged += new System.EventHandler(stoolCheck_Checked);
            stoolList.Location = new Point(124, 44);
            stoolList.Items.AddRange(new object[] {"pellets, hard",
                                                   "contiguous, dry",
                                                   "watery, no solids",
                                                   "entirely liquid"});

            blood.Text = "Blood?";
            blood.Height = 12;
            blood.Width = 90;
            blood.Location = new Point(6, 72);
            bloodCheck.Location = new Point(105, 68);
            bloodCheck.Width = 20;
            bloodCheck.CheckedChanged += new System.EventHandler(bloodCheck_Checked);
            bloodList.Location = new Point(124, 68);
            bloodList.Items.AddRange(new object[] {"contusion",
                                                   "blood visible", 
                                                   "abrasion",
                                                   "laceration"});

            equipment.Text = "Equipment malfunction?";
            equipment.Height = 20;
            equipment.Width = 140;
            equipment.Location = new Point(6, 96);
            equipmentCheck.Location = new Point(150, 92);
            equipmentCheck.Width = 20;
            equipmentCheck.CheckedChanged += new System.EventHandler(equipmentCheck_Checked);
            equipmentList.Location = new Point(6, 116);
            equipmentList.Items.AddRange(new object[] {"LIXIT",
                                                       "feeders", 
                                                       "fan",
                                                       "other"});

            Controls.Add(report); 
            Controls.Add(comment); 
            Controls.Add(behavior);
            Controls.Add(behaviorCheck);
            Controls.Add(stool);
            Controls.Add(stoolCheck);
            Controls.Add(blood);
            Controls.Add(bloodCheck);
            Controls.Add(equipment);
            Controls.Add(equipmentCheck); 
        }

        public void report_Click(object sender, EventArgs e)
        {
            reportForm = new ReportForm(data.Mid);
            reportForm.Show(); 
        } 

        public void comment_Click(object sender, EventArgs e)
        {
            Comment commentForm = new Comment(data.Mid, this);
            commentForm.Show();
        }

        public void behaviorCheck_Checked(object sender, EventArgs e)
        {
            if (!Controls.Contains(behaviorList))
                Controls.Add(behaviorList);
            else
                Controls.Remove(behaviorList); 
        }

        public void stoolCheck_Checked(object sender, EventArgs e)
        {
            if (!Controls.Contains(stoolList))
                Controls.Add(stoolList);
            else
                Controls.Remove(stoolList); 
        }

        public void bloodCheck_Checked(object sender, EventArgs e)
        {
            if (!Controls.Contains(bloodList))
                Controls.Add(bloodList);
            else
                Controls.Remove(bloodList); 
        }

        public void equipmentCheck_Checked(object sender, EventArgs e)
        {
            if (!Controls.Contains(equipmentList))
                Controls.Add(equipmentList);
            else
                Controls.Remove(equipmentList); 
        }

        public void setComment(string str)
        {
            data.recordComment(str); 
        } 

        public LogData GetData()
        {
            if (behaviorCheck.Checked && behaviorList.SelectedItem != null)
                data.behaviorChecked(behaviorList.SelectedItem.ToString());

            if (stoolCheck.Checked && stoolList.SelectedItem != null)
                data.stoolChecked(stoolList.SelectedItem.ToString());

            if (bloodCheck.Checked && bloodList.SelectedItem != null)
                data.bloodChecked(bloodList.SelectedItem.ToString());

            if (equipmentCheck.Checked && equipmentList.SelectedItem != null)
                data.equipChecked(equipmentList.SelectedItem.ToString()); 
            
            return data; 
        } 
    }
}
