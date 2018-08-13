using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsLab01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = null;

            using (var t2 = new SecondWindowsThread())
            {
                t2.SynchronizationContext.Send(
                    (_) =>
                    {
                        form2 = new Form2();
                        form2.Show();
                    }, null);

                Thread.Sleep(1000 * 10); // что-то долго делает.

                t2.SynchronizationContext.Send((_) => form2.Close(), null);
            }
        }
    }
}
