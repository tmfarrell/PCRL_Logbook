using System;
using System.IO; 
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;
using System.Data.SQLite; 
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace PCRLLogbook
{
    public partial class ConfigureForm : Form
    {
        Config config = new Config();
        string config_path;  

        public ConfigureForm(string configPath)
        {
            InitializeComponent();

            config_path = configPath; 

            // get config json
            string json = ""; 
            try {
                using (StreamReader sr = new StreamReader(config_path)) {
                    json = sr.ReadToEnd();
                } 
            }
            catch (Exception e) {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            config = JsonConvert.DeserializeObject<Config>(json); 

            // fill components with associated configuration values
            datadirText.Text = config.data_dir;
            reportdirText.Text = config.report_dir;
            daystoreportText.Text = Convert.ToString(config.days);
            feedinghrText.Text = Convert.ToString(config.feeding_hour_threshold);

            feeder0cal.Text = Convert.ToString(config.feeder_caloric_density[0]);
            feeder1cal.Text = Convert.ToString(config.feeder_caloric_density[1]);
            feeder2cal.Text = Convert.ToString(config.feeder_caloric_density[2]);

            feeder0dispense.Text = Convert.ToString(config.dispense_amount[0]);
            feeder1dispense.Text = Convert.ToString(config.dispense_amount[1]);
            feeder2dispense.Text = Convert.ToString(config.dispense_amount[2]);

            string supp_feed_temp = "";
            foreach (KeyValuePair<string, double> pair in config.supp_feed_template)
            {
                supp_feed_temp = supp_feed_temp + "(" + pair.Key +
                    ", " + pair.Value + ")\n";
            }
            suppfeedRichText.Text = supp_feed_temp; 
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            // read configuration vals from components 
            config.data_dir = datadirText.Text;
            config.report_dir = reportdirText.Text;
            config.days = Convert.ToInt16(daystoreportText.Text);
            config.feeding_hour_threshold = Convert.ToInt16(feedinghrText.Text);

            config.feeder_caloric_density[0] = Convert.ToDouble(feeder0cal.Text);
            config.feeder_caloric_density[1] = Convert.ToDouble(feeder1cal.Text);
            config.feeder_caloric_density[2] = Convert.ToDouble(feeder2cal.Text);

            config.dispense_amount[0] = Convert.ToDouble(feeder0dispense.Text);
            config.dispense_amount[1] = Convert.ToDouble(feeder1dispense.Text);
            config.dispense_amount[2] = Convert.ToDouble(feeder2dispense.Text);

            string[] supp_feed_temp = suppfeedRichText.Text.Split('\n');
            for (int i = 0; i < supp_feed_temp.Length - 1; i++)
            {
                string[] dum = supp_feed_temp[i].Replace('(',' ').Replace(')',' ').Split(',');
                config.supp_feed_template[dum[0].Trim()] = Convert.ToDouble(dum[1]);
            }

            // convert to json and write to config file 
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            using (StreamWriter outputFile = new StreamWriter(config_path))
            {
                    outputFile.WriteLine(json);
            }

            this.Close(); 
        }

        private void addsuppfeedButton_Click(object sender, EventArgs e)
        {   
            // open DB connection and declare command 
            SQLiteConnection m_dbConnection = 
                new SQLiteConnection("Data Source=PCRL_phizer_study.db;Version=3;");
            m_dbConnection.Open();

            string sql;
            SQLiteCommand command;

            //insert
            // need to review the schema and make sure it is consistent with the definition in the database 
            sql = "INSERT INTO caloric_densities VALUES ('" + addsuppfeedCategory.Text +
                "','" + addsuppfeedName.Text + "'," + 
                Convert.ToDouble(addsuppfeedEcarb.Text) + "," + 
                Convert.ToDouble(addsuppfeedEprotein.Text) + "," + 
                Convert.ToDouble(addsuppfeedEfat.Text) + "," +
                Convert.ToDouble(addsuppfeedFiber.Text) + "," +
                Convert.ToDouble(addsuppfeedWater.Text) + ")";  
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();

            MessageBox.Show("New supplemental food sucessfully added to the database."); 
        }

    }
}
