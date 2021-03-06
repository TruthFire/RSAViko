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
        public BigInteger n, e, d, phi;



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
            for (e = 2; e < phi; e++)
            {
                if (BigInteger.GreatestCommonDivisor(phi,e) == 1)
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
            getDec_Key();

            MessageBox.Show($"Viesasis raktas: {e}.\nPrivatus raktas: {d}");

        }

        public string Encrypt(string msg)
        {
            List<Byte> bytes = new List<Byte>();
            string str = "";
            foreach(char c in msg)
            {
                str += (char)BigInteger.ModPow((int)c, e, n);
            }

           // MessageBox.Show(System.Text.Encoding.GetEncoding(str).ToString());
           byte[] bytes2 = System.Text.Encoding.UTF8.GetBytes(str);
            str= System.Convert.ToBase64String(bytes2);




            return str;
        }

        public string Decrypt(string msg)
        {
            byte[] b64Bytes = System.Convert.FromBase64String(msg);
            string rawString = System.Text.Encoding.UTF8.GetString(b64Bytes);
            string rez = "";
            foreach(char c in rawString)
            {
                rez += (char)BigInteger.ModPow((int)c, d, n);
            }
            return rez;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = Decrypt(textBox4.Text);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Enter_keys ek = new(this);
            ek.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(textBox4.Text))
                {
                    throw new ArgumentNullException("Please encrypt your text first");
                }
                string creationTime = DateTime.Now.ToString("dd_mm_yyyy_HH_mm_ss");
                string fileName = "EncryptedText_" + creationTime + ".txt";

                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file
                File.WriteAllText(fileName, textBox4.Text);

                if (MessageBox.Show("Do you want to save public and private keys in separate file?",
                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    fileName = "Keys_" + creationTime + ".txt";
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    string Keys = String.Format("Public: {0}\nPrivate: {1}.txt", e, d);
                    File.WriteAllText(fileName, Keys);

                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Show openfiledialog
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "";
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    // Read all content of file to textbox
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBox4.Text = File.ReadAllText(openFileDialog.SafeFileName);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        BigInteger Euclid(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (b < a)
            {
                var t = a;
                a = b;
                b = t;
            }

            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            BigInteger gcd = Euclid(b % a, a, out x, out y);

            BigInteger newY = x;
            BigInteger newX = y - (b / a) * x;

            x = newX;
            y = newY;
            return gcd;
        }

        void getDec_Key()
        {
            BigInteger x, y;
            Euclid(e,phi,out x, out y);
           // MessageBox.Show($"{x}, {y}");
            if(x < 0)
            {
                d = phi + x;
            }
            else
            {
                d = phi;
            }
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
            p = Convert.ToInt32(textBox1.Text);
            q = Convert.ToInt32(textBox2.Text);
            count_e_d();

            textBox4.Text = Encrypt(textBox3.Text);

        }
    }
}
