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
        }
    }
}