using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A7_kbryla
{
    public partial class Form1 : Form
    {
        int countries = 0;
        Boolean found = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            // TODO: This line of code loads data into the 'deaths_by_CountryDataSet.Country' table. You can move, or remove it, as needed.
            this.countryTableAdapter.Fill(this.deaths_by_CountryDataSet.Country);

            List<String> nameRegions = new List<String>();
               
            foreach (DataRowView row in countryBindingSource.List)
            {
                found = false;
                foreach (String name in nameRegions)
                {
                    if ((String)row["Region"] == name)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    nameRegions.Add(((String)row["Region"]));
                }
            }

            for (int i = 0; i < nameRegions.Count(); i++)
            {
                countries = 0;
                foreach (DataRowView row in countryBindingSource.List)
                {
                    if(nameRegions[i] == (String)row["Region"])
                    {
                        countries++;
                    }
                }

                listRegions.Items.Add(String.Format("{0,-25}{1,4}", nameRegions[i], countries));
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int count = 0;
            int total = 0;
            int unintentional = 0;
            int intentional = 0;
            String country;
            String region = txtSearch.Text.ToString();

            String lineOne = String.Format("\r\n \r\n {0, -30} {1, -18} {2, -18}", "Country", "Unintentional", "Intentional");
            String lineTwo = String.Format("\r\n{0, -30} {1, -18} {2, -18}", "=======", "=============", "===========");
            String outputLine = "";
            foreach (DataRowView row in countryBindingSource.List)
            {
                String currentRegion = (String)row["Region"];


                if (currentRegion.StartsWith(region))
                {
                    country = (String)row["Country"];
                    unintentional = (int)row["Unintentional injuries"];
                    intentional = (int)row["Intentional injuries"];
                    total += unintentional + intentional;
                    outputLine += String.Format("{ 0, -30} { 1, -18} { 2, -18}\r\n", country.ToString(),
                        unintentional.ToString(), intentional.ToString());
                    txtSearchResults.Text ="Results for: " + region + lineOne + lineTwo + outputLine + 
                        "=================================================================\r\n" + "Total Deaths from injuries: " + total;
                    count++;
                }
            }
            if(count == 0)
            {
                txtSearchResults.Text = "Error, please choose a valid region.";
            }
        }
    }
}
