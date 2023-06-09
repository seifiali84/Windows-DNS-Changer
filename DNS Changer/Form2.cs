using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            maskedTextBox1.Mask = "###.###.###.###" ;
            maskedTextBox1.ValidatingType = typeof(System.Net.IPAddress);
            maskedTextBox2.Mask = "###.###.###.###";
            maskedTextBox2.ValidatingType = typeof(System.Net.IPAddress);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }
    }
}
