using System.Net.NetworkInformation;
using System.Management;
namespace DNS_Changer
{
    public partial class Form1 : Form
    {
        NetworkInterface SelectedNET;
        string[] SelectedDNS;
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
            if(SelectedDNS == null)
            {
                MessageBox.Show("Select a dns or add a new one.");
            }
            else if(SelectedNET == null)
            {
                MessageBox.Show("Network Interface not found(404)");
            }
            else
            {
                string[] DNS = { SelectedDNS[1], SelectedDNS[2] };
                NorexDNSLib.SetDnsServers(SelectedNET.Name , DNS);
                MessageBox.Show("DNS is Set.");
                label3.Text = DNS[0] + "\n" + DNS[1];
            }
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
                MessageBox.Show("data path not exist");
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
            SelectedNET = NorexDNSLib.GetActiveEthernetOrWifiNetworkInterface();
            comboBox1.SelectedItem = SelectedNET;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedNET = NorexDNSLib.ReadNetworkInterfaceByName(comboBox1.Text);
        }
        private string[] ReadDNSByName(string DNSName)
        {
            string[] data = File.ReadAllLines(path);
            var q = from i in data where i.Split(',')[0] == DNSName select i;
            foreach (var item in q)
            {
                return item.Split(',');
            }
            return null;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedDNS = ReadDNSByName(comboBox2.Text);
        }
    }
}