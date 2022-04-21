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
using System.IO;

namespace RSAViko
{
    public partial class Form1 : Form
    {
        // p, q, n, z, d = 0, e, i;
        int p, q;
        BigInteger n, e, d, phi;



        public Form1()
        {
            InitializeComponent();
            

        }

        public void count_e_d()
        {
            Random r = new Random();
            List<BigInteger> e_Candidates = new();
            n = p * q;
            phi = (p - 1) * (q - 1);
            int amount = 0;
            for (e = p; e < phi; e++)
            {
                if (gcd(e, phi) == 1)
                {
                    amount++;
                    e_Candidates.Add(e);
                }
                if(amount == 20)
                {
                    break;
                }
            }

            e = e_Candidates[r.Next(0,e_Candidates.Count)];
            BigInteger x;
            List<BigInteger> x_Candidates = new();
            for(int i = 0; i < 10; i++)
            {
                x = 1 + (i * phi);

                if(x%e == 0)
                {
                    x_Candidates.Add(x);
                }
            }

            d = x_Candidates[r.Next(0, x_Candidates.Count)] / e;

        }

        public string Encrypt(string msg)
        {
            List<Byte> bytes = new List<Byte>();
            string str = "";
            foreach(char c in msg)
            {
                str += (char)BigInteger.ModPow((int)c, e, n);
            }

            byte[] arr = System.Text.Encoding.ASCII.GetBytes(str);

            
            return Convert.ToBase64String(arr, 0, arr.Length);
        }

        public string Decrypt(string msg)
        {
            return "";
        }

        

        protected BigInteger gcd(BigInteger a, BigInteger b)
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

        string GetPrime(int line)
        {
            using (var sr = new StreamReader("primes.txt"))
            {
                for (int i = 1; i < line; i++)
                    sr.ReadLine();
                return sr.ReadLine();
            }
        }


        


        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
