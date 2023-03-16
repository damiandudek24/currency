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
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();

        }

        Form1 f1;

        public Form2(Form1 _f1)
        {
            InitializeComponent();
            this.f1 = _f1;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string waluta = textBox1.Text;
            try
            {
                string url = "http://api.nbp.pl/api/exchangerates/rates/a/" + waluta + "/?format=json";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var d = response.StatusCode;
                f1.modtxt2(waluta);
                
                this.Hide();
                request.Abort();


            }
            catch(WebException d)
            {
                HttpWebResponse response = (HttpWebResponse)d.Response;
                MessageBox.Show("ERROR: " + Convert.ToString((int)response.StatusCode)+"");
            }
            
                



        }
    }
}
