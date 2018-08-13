using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsLab01
{
    public partial class Form2 : Form
    {
        private Int32 number;
        public Form2()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.number += 1;
            this.label1.Text = this.number.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label2.Text += " Hi!";
        }
    }
}
