using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSAViko
{
    public partial class Enter_keys : Form
    {
        Form1 activeForm;
        public Enter_keys(Form1 f1)
        {
            activeForm = f1;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            activeForm.e = Convert.ToInt32(textBox1.Text);
            activeForm.d = Convert.ToInt32(textBox2.Text);
            MessageBox.Show("Sekmingai.");
            this.Close();
        }
    }
}
