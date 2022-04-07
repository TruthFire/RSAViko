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
    public partial class Form1 : Form
    {
        int N, fn;
        int[] publicKey = new int[2];
        string msg = "Hello world";
        public Form1()
        {
            InitializeComponent();
            

        }

        protected int gcd(int a, int b)
        {
            if (b == 0)
                return a;
            else
                return gcd(a, a % b);
        }

        private bool IsTheNumberSimple(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
