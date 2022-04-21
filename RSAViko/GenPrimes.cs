using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Security.Cryptography;

namespace RSAViko
{
    public partial class GenPrimes : Form
    {
        public GenPrimes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int bitSize = (int)comboBox1.SelectedItem;



        }

        protected void GenPrime()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();

            //int arrlen = ()
        }
    }
}
