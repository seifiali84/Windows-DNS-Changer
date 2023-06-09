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
        List<NetworkInterface> Networks = new List<NetworkInterface>();
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
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


        private void Form1_Load(object sender, EventArgs e)
        {
            Networks = NorexDNSLib.GetAllNetworkInterfaces();
            foreach (var item in Networks)
            {
                comboBox1.Items.Add(item.Name);
            }
            comboBox1.SelectedItem = NorexDNSLib.GetActiveEthernetOrWifiNetworkInterface().Name;
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
    }
}