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

namespace WindowsFormsConsumer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new Task(() =>
            {
                var p = new RabbitMQ1();
                p.Receive();
            }).Start();

            //new Task(() =>
            //{
            //    var p = new Class1();
            //    p.Get();
            //}).Start();

            //new Task(() =>
            //{
            //    var p = new Class1();
            //    p.Get();
            //}).Start();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var p = new RabbitMQ3();
            p.Receive();
        }
    }
}
