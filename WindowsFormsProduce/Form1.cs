using RabbitMQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsProduce
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var p = new RabbitMQ1();
            p.Send();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var p = new RabbitMQ3();
            p.Send();
        }
    }
}
