using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;


namespace Zad3
{
    public partial class Form1 : Form
    {
        public string wal;
        private List<double> lista = new List<double>();
        public Form1()
        {
            
            InitializeComponent();
            //new JObject();
        }
        public Form1(string str)
        {

            InitializeComponent();
            wal = str;
            //new JObject();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Form2 f = new Form2(this);
            f.Show();
            

        }
        public void modtxt2(string val)
        {
            textBox2.Text += "\r\n" + val;
            wal = val;
            MessageBox.Show(wal);
            timer1.Stop();
            timer1.Start();


        }

        public void liczarka(string waluta)
        {
            try
            {
                Uri url = new Uri("http://api.nbp.pl/api/exchangerates/rates/a/" + waluta + "/?format=json");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
                dynamic data = JObject.Parse(content);
                var cur = data.currency;
                var dat = data.rates[0].effectiveDate;
                double mid = data.rates[0].mid;
                using (StreamWriter sw = new StreamWriter(waluta + ".txt", true))
                {
                    sw.WriteLine(cur + "|" + dat + "|" + Convert.ToString(mid));
                }
                request.Abort();
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            liczarka(wal);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            string plik = textBox1.Text;
            try
            {
                using (StreamReader sr = new StreamReader(plik+".txt"))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] bits = line.Split('|');
                        double x = double.Parse(bits[2]);
                        lista.Add(x);
                       
                    }
                    sr.Close();
                    
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ee.Message);
            }

            textBox3.Text = Convert.ToString(lista.Average());
            lista.Clear();
        }
    }

    

}
