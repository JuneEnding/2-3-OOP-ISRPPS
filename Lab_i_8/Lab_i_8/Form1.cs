using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_i_8
{
    public partial class Form1 : Form
    {
        User user = new User();
        char @operator ;
        public Form1() {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) { // +
            //MessageBox.Show(" " + Convert.ToInt32(this.textBox1.Text));
            //this.textBox2.Text = user.getResult().ToString();
            //user.Compute('+', Convert.ToInt32(this.textBox1.Text));
            @operator  = '+';
        }
        private void button6_Click(object sender, EventArgs e) {
            //user.Compute('-', Convert.ToInt32(this.textBox1.Text));
            //this.textBox2.Text = user.getResult().ToString();
            @operator  = '-';
        }
        private void button7_Click(object sender, EventArgs e) {
            //user.Compute('*', Convert.ToInt32(this.textBox1.Text));
            //this.textBox2.Text = user.getResult().ToString();
            @operator  = '*';
        } 
        private void button8_Click(object sender, EventArgs e) {
            //user.Compute('/', Convert.ToInt32(this.textBox1.Text));
            //this.textBox2.Text = user.getResult().ToString();
            @operator  = '/';
        }
        private void button2_Click(object sender, EventArgs e) { // = 
            //Convert.ToInt32(this.textBox2.Text);
            //user.Compute('+', Convert.ToInt32(this.textBox2.Text));
            user.Compute( @operator , Convert.ToInt32(this.textBox1.Text));
            this.textBox2.Text = user.getResult().ToString();
        }
        private void button3_Click(object sender, EventArgs e) { // Undo
            user.Undo(1);// листаем на единицу
            this.textBox2.Text = user.getResult().ToString();
        }
        private void Redo_Click(object sender, EventArgs e) {
            user.Redo(1);
            this.textBox2.Text = user.getResult().ToString();
        }

        private void buttonC_Click(object sender, EventArgs e){
            user.Compute('-', Convert.ToInt32(this.textBox2.Text));
            textBox2.Text = "0";
            //textBox1.Text = "0";
        }
    }
}
