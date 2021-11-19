using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_i_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Visible = true; // можно утсан. динамически!
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.button1.BackColor == Color.Coral)
                this.button1.BackColor = Color.White;
            else
                this.button1.BackColor = Color.Coral;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {// можно выбрать параметры
            if (this.checkBox1.Checked){ // можно посм атриб. булевского типа?
                //MessageBox.Show("checkBox1.Checked"); // запрогр переключение
                this.button1.Visible = false;
                this.label2.Text = "The button is invisible";
            } else{
                //MessageBox.Show("un checkBox1.Checked"); // запрогр переключение
                this.button1.Visible = true;
                this.label2.Text = "The button is visible";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.label1.Text = "Hi! " +
                               "\nYou are cute!";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "Я сразу смазал карту будня" +
                "\nПлеснувши краску из стакана";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //if (this.trackBar1.Value <20)
            //this.trackBar1.Value += 1;
            this.richTextBox1.Font = new Font(this.richTextBox1.Font.FontFamily, this.trackBar1.Value);
            this.label4.Text = $"{this.trackBar1.Value}";
        }
    }
}
