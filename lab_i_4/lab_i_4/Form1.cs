using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab_i_4
{
    public partial class Form1 : Form
    {
        public Form1(){
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e){
            MessageBox.Show("Hi!");// Просто показали
            MessageBox.Show("Hi!!!", "Test 2");
            // последние параметры - атрибуты классов типа enum - перечисление
            MessageBox.Show("HI!!!!!", "Test 3", MessageBoxButtons.OKCancel);
            MessageBox.Show("Blue question?", "Test 4", 
                             MessageBoxButtons.AbortRetryIgnore, 
                             MessageBoxIcon.Question);
            MessageBox.Show("Yellow exclamation!", "Test 5", 
                             MessageBoxButtons.YesNoCancel, 
                             MessageBoxIcon.Exclamation, 
                             MessageBoxDefaultButton.Button2);
        }

        private void button2_Click(object sender, EventArgs e){
            // В качестве значения переменной класса тепа enum должна выступать одна из констант, определенных в данном 
            // перечислении. Несмотря на то, что каждая константа сопоставляется с определенным 
            // числом, мы не можем присвоить ей числовое значение
            MessageBox.Show("You clicked this button.");
            DialogResult result = MessageBox.Show("The button thinks you wanted to embarrass it!" +
                                                   "\nIs that so ? ", 
                                                   "Question from the button", 
                                                   MessageBoxButtons.YesNoCancel, 
                                                   MessageBoxIcon.Question, 
                                                   MessageBoxDefaultButton.Button1);
            switch (result){
                case DialogResult.Yes:
                    MessageBox.Show("The button is confused.");
                    this.button2.BackColor = Color.Red; 
                    break;
                case DialogResult.No:
                    MessageBox.Show("The button forgives you.");
                    this.button2.BackColor = Color.LightGray; 
                    break;
                case DialogResult.Cancel:
                    MessageBox.Show("The button is disappointed in you and doesn't want to see you again.");
                    Application.Exit();
                    break;
                default:
                    break;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
