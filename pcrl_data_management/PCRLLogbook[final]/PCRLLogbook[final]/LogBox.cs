using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
 

namespace PCRLLogbook
{
    public class LogBox : GroupBox
    { /*  
       * Logbox: basic UI unit for LogData struct.       
       */

        /* Positional + Dimensional Constants*/  
        const int label_ht = 16;
        const int label_width = 100;
        const int label_x = 6;          //x pos for labels
        const int label_y = 24;         //y pos for labels
        const int label_dy = 26;        //y-space bt labels

        const int check_x = 116;        //x pos for checkbox
        const int check_y = label_y - 4;//y pos for checkbox

        const int list_x = check_x + 26;//x pos for list

        const int button_ht = 24;       //button dimensions
        const int button_width = 120;

        /* Data Storage Struct */ 
        LogData data; 
        
        /* Controls */ 
        Label behavior = new Label();
        CheckBox behaviorCheck = new CheckBox();
        ComboBox behaviorList = new ComboBox();  

        Label stool = new Label();
        CheckBox stoolCheck = new CheckBox();
        ComboBox stoolList = new ComboBox();

        Label training = new Label();
        CheckBox trainingCheck = new CheckBox();
        ComboBox trainingList = new ComboBox(); 

        Label suppFeed = new Label();
        BindingSource suppFeedObjs = new BindingSource();
        DataGridView suppFeedTable = new DataGridView(); 

        Label equipment = new Label();
        CheckBox equipmentCheck = new CheckBox();
        CheckedListBox equipCheckList = new CheckedListBox(); 

        ReportForm reportForm;
        Button report = new Button();

        Label comment = new Label();
        RichTextBox commentText = new RichTextBox(); 

        
        /* Constructors */ 
        public LogBox() { }

        public LogBox(string name, string station, string labroom, Config config)
        {
            //general properties
            data = new LogData(name, station, labroom);     
            //commentForm = new Comment(name, this); 
            Name = name + "Logbox";
            Text = station + "-" + name;
            BackColor = SystemColors.Control;
            Width = 340; 
            Height = 320;
            Margin = new Padding(8);

            //checkboxes with labels and lists
            behavior.Text = "Behavior:";
            behavior.Height = label_ht;
            behavior.Width = label_width;
            behavior.Location = new Point(label_x, label_y);
            behaviorCheck.Location = new Point(check_x, check_y);
            behaviorCheck.Width = 20;
            behaviorCheck.CheckedChanged += new System.EventHandler(behaviorCheck_Checked);
            behaviorList.Location = new Point(list_x, check_y);
            behaviorList.Items.AddRange(config.behavior_list);

            stool.Text = "Stool:";
            stool.Height = label_ht;
            stool.Location = new Point(label_x, label_y + label_dy);
            stoolCheck.Location = new Point(check_x, check_y + label_dy);
            stoolCheck.Width = 20;
            stoolCheck.CheckedChanged += new System.EventHandler(stoolCheck_Checked);
            stoolList.Location = new Point(list_x, check_y + label_dy);
            stoolList.Items.AddRange(config.stool_list);

            suppFeed.Text = "Supplemental Feed:";
            suppFeed.Height = label_ht;
            suppFeed.Width = 130;
            suppFeed.Location = new Point(label_x, label_y + 2*label_dy);
            //foreach (SuppFood sfObj in supp_foods) {
              //  suppFeedObjs.Add(sfObj); 
            //}
            // init supplemental food data table
            suppFeedTable.AutoGenerateColumns = false;
            suppFeedTable.Location = new Point(list_x, check_y + 2 * label_dy);
            suppFeedTable.Size = new Size(180, 70);
            suppFeedTable.ScrollBars = ScrollBars.Both; 
            suppFeedTable.DataSource = suppFeedObjs; 
            // add columns
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.Width = 60; 
            suppFeedTable.Columns.Add(column);
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Amount";
            column.Name = "Amt (g)";
            column.Width = 60; 
            suppFeedTable.Columns.Add(column);
            
            equipment.Text = "Equipment:";
            equipment.Height = label_ht;
            equipment.Width = label_width;
            equipment.Location = new Point(label_x, label_y + 5*label_dy);
            equipmentCheck.Location = new Point(check_x, check_y + 5*label_dy);
            equipmentCheck.Width = 20; 
            equipmentCheck.CheckedChanged += new System.EventHandler(equipmentCheck_Checked);
            equipCheckList.Location = new Point(list_x, check_y + 5*label_dy);
            equipCheckList.Height = 66;
            equipCheckList.Width = 150; 
            equipCheckList.ScrollAlwaysVisible = true;
            equipCheckList.CheckOnClick = true; 
            equipCheckList.Items.AddRange(config.equipment_list);

            training.Text = "Training:";
            training.Height = label_ht;
            training.Width = label_width;
            training.Location = new Point(label_x, label_y + 7*label_dy + 16);
            trainingCheck.Width = 20;
            trainingCheck.Location = new Point(check_x, check_y + 7*label_dy + 16);
            trainingCheck.CheckedChanged += new System.EventHandler(trainingCheck_Checked);
            trainingList.Width = 190;
            trainingList.Location = new Point(list_x - 5, check_y + 7*label_y + 30);
            trainingList.Items.AddRange(config.training_list);

            comment.Text = "Comments:";
            comment.Location = new Point(label_x, label_y + 8*label_dy + 16);
            comment.Height = label_ht;
            comment.Width = label_width;
            comment.FlatStyle = FlatStyle.Standard;
            commentText.Location = new Point(check_x, label_y + 8*label_dy + 16);
            commentText.Width = 220;
            commentText.Height = 50;

            //report button
            /*report.Text = "Latest Report";
            report.Location = new Point(110, label_y + (int)(9.5 * label_dy) + 5);
            report.Height = button_ht;
            report.Width = button_width;
            report.FlatStyle = FlatStyle.Standard;
            report.Click += new System.EventHandler(report_Click);*/
           
            Controls.Add(behavior);
            Controls.Add(behaviorCheck);
            Controls.Add(stool);
            Controls.Add(stoolCheck);
            Controls.Add(suppFeed);
            Controls.Add(suppFeedTable);
            Controls.Add(equipment);
            Controls.Add(equipmentCheck);
            Controls.Add(training);
            Controls.Add(trainingCheck);
            Controls.Add(comment);
            Controls.Add(commentText); 
            //Controls.Add(report);
        }

        /* Methods */ 
        //shows last report
        public void report_Click(object sender, EventArgs e)
        {
            reportForm = new ReportForm(data.Mid);
            reportForm.Show(); 
        }

        //checkbox methods 
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

        public void equipmentCheck_Checked(object sender, EventArgs e)
        {
            if (!Controls.Contains(equipCheckList))
                Controls.Add(equipCheckList);
            else
                Controls.Remove(equipCheckList); 
        }

        public void trainingCheck_Checked(object sender, EventArgs e)
        {
            if (!Controls.Contains(trainingList))
                Controls.Add(trainingList);
            else
                Controls.Remove(trainingList); 
        }

        public void setSupppFeed()
        {
            int num_foods = suppFeedTable.Rows.Count; 
            SuppFood[] foods = new SuppFood[num_foods];

            string s; 
            for (int row = 0; row < num_foods; row++)
            {
                for (int col = 0; col < suppFeedTable.Rows[row].Cells.Count; col++)
                {
                    try
                    {
                        s = suppFeedTable.Rows[row].Cells[col].Value.ToString();
                        if (col == 0 && !s.Equals(""))
                        {
                            foods[row] = new SuppFood();
                            foods[row].Name = s;
                        }
                        if (col == 1 && !s.Equals(""))
                            foods[row].Amount = Convert.ToDouble(s);
                    }
                    catch { }
                } 
            }
            data.recordSuppFed(foods); 
        }

        //returns data struct
        public LogData GetData()
        {
            if (behaviorCheck.Checked && behaviorList.SelectedItem != null)
                data.behaviorChecked(behaviorList.SelectedItem.ToString());

            if (stoolCheck.Checked && stoolList.SelectedItem != null)
                data.stoolChecked(stoolList.SelectedItem.ToString());

            if (trainingCheck.Checked && trainingList.SelectedItem != null)
                data.trainingChecked(trainingList.SelectedItem.ToString());

            if (equipmentCheck.Checked && equipCheckList.CheckedItems != null)
            {
                string[] equip = new string[11];  
                int i = 0;
                foreach (Object item in equipCheckList.CheckedItems)
                {
                    equip[i++] = item.ToString();
                } 
                data.equipChecked(equip);
            }

            setSupppFeed(); 
            data.Comments = commentText.Text.ToString(); 
            
            return data; 
        } 
    }
}
