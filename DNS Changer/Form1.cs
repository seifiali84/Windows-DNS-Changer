using System.Net.NetworkInformation;
using System.Management;
namespace DNS_Changer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string path = "data/data.csv";
        List<NetworkInterface> Networks = new List<NetworkInterface>();
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            comboBox2.Items.Clear();
            foreach (var item in GetDNSNames())
            {
                comboBox2.Items.Add(item);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private List<string> GetDNSNames()
        {
            List<string> names = new List<string>();
            if (File.Exists(path))
            {
                
                var data = File.ReadAllLines(path);

                foreach (var item in data)
                {
                    names.Add(item.Split(',')[0]);
                }
            }
            else
            {
                MessageBox.Show("path not exist");
            }
            return names;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Networks = NorexDNSLib.GetAllNetworkInterfaces();
            foreach (var item in Networks)
            {
                comboBox1.Items.Add(item.Name);
            }
            comboBox1.SelectedItem = NorexDNSLib.GetActiveEthernetOrWifiNetworkInterface().Name;
            comboBox2.Items.Clear();
            foreach (var item in GetDNSNames())
            {
                comboBox2.Items.Add(item);
            }
            label3.Text = "";
            if (NorexDNSLib.GetActiveDnsServers() != null)
            {
                foreach (var item in NorexDNSLib.GetActiveDnsServers())
                {
                    label3.Text += item + "\n";
                }
            }
            else
            {
                label3.Text = "Clear";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}