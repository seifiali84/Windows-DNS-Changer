using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DNS_Changer
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        string path = "data/data.csv";
        private void AddDataToCsvFile(string DNSName, string ns1, string ns2)
        {
            string[] data = { DNSName + "," + ns1 + "," + ns2 };
            File.AppendAllLines(path , data);
        }
        // cancel button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("please enter a name for dns");
            }
            else if (!NorexDNSLib.IsValidIpAddress(textBox2.Text))
            {
                MessageBox.Show("please enter a valid ip for ns1");
            }
            else if (!NorexDNSLib.IsValidIpAddress(textBox1.Text))
            {
                MessageBox.Show("please enter a valid ip for ns2");
            }
            else
            {
                // add dns to data.csv file
                AddDataToCsvFile(textBox3.Text, textBox2.Text, textBox1.Text);
            }
        }


        private void textBox2_Leave(object sender, EventArgs e)
        {
            // validating
            if (!NorexDNSLib.IsValidIpAddress(textBox2.Text))
                label5.Visible = true;
            else
                label5.Visible = false;
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            // validating
            if (!NorexDNSLib.IsValidIpAddress(textBox1.Text))
                label3.Visible = true;
            else
                label3.Visible = false;
        }
    }
}
