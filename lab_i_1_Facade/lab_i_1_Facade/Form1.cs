using System;
using System.Windows.Forms;

namespace lab_i_1_Facade
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label1.Text = Facade.Operation1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.label1.Text = Facade.Operation2();
        }
    }
}
