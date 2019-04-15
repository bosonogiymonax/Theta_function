using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Тета_функция
{
    public partial class Form1 : Form
    {
        const int k_max = 500;
        const double buf_max = 80;
        const double pi = Math.PI;
        public Form1()
        {
            InitializeComponent();
        }

        public double Theta(double t, double sig, int num) //num - номер способа
        {
            double sum, buf, xx, buf2, q;
            int k;
            if (num == 1)
            {
                sum = 1;
                k = 0;
                do
                {
                    k++;
                    buf = (k * k) / (2 * sig * sig);
                    sum += 2 * Math.Exp(-buf) * Math.Cos(2 * k * t);
                } while ((buf < buf_max) && (k < k_max));
                q = Math.Exp(-1 / (2 * sig * sig)); //считаем и выводим q в текстбокс
                textBox1.Text = q.ToString();
                return sum;
            }
            else
            {
                k = (int)Math.Round(Math.Abs(t) / pi);
                xx = Math.Abs(t) - k * pi;
                sum = Math.Exp(-(sig * xx * xx));
                k = 0;
                do
                {
                    k++;
                    buf = sig * Math.Pow(xx + pi * k, 2);
                    buf2 = (sig * sig * Math.Pow(xx - 2 * pi * k, 2)) / 2;
                    //buf2 = sig * Math.Pow(xx - pi * k, 2); 
                    sum += Math.Exp(-((0.5 * sig * sig) * Math.Pow(t - 2 * pi * k, 2)));
                    //sum += Math.Exp(-buf) + Math.Exp(-buf2);
                } while (((buf < buf_max) || (buf2 < buf_max)) && (k < k_max));

                q = Math.Exp(-1 / (2 * sig * sig)); //считаем и выводим q в текстбокс
                textBox1.Text = q.ToString();
                return Math.Sqrt(2 * pi) * sig * sum;
                //return Math.Sqrt(pi * sig) * sum;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(double i = -4; i<=4; i = i+0.01)
            {
                chart1.Series[0].Points.AddXY(i + 0.01, Theta(i, 7, 1));
                chart1.Series[1].Points.AddXY(i + 0.01, Theta(i, 7, 2));
            }
        }
    }
}
