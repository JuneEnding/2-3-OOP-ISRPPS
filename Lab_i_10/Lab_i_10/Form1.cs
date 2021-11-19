using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // add
//using System.IO.S; // add

namespace Lab_i_10
{
    public partial class Form1 : Form
    {
        private string fileopen;

        public Form1()
        {
            InitializeComponent();
            //this.richTextBox1.Text = "Example with richTextBox1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.colorDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    this.label2.BackColor = this.colorDialog1.Color;
                    break;
            }// end switch
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.fontDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    this.label2.Font = this.fontDialog1.Font;
                    break;
            }// end switch
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    //MessageBox.Show(this.openFileDialog1.FileName);
                    using (Stream fileopen = this.openFileDialog1.OpenFile())
                        this.richTextBox1.LoadFile(fileopen, RichTextBoxStreamType.RichText);
                    break;
            }// end switch
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.saveFileDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    MessageBox.Show(this.openFileDialog1.FileName);
                    try
                    {
                        using (Stream fileopen = this.saveFileDialog1.OpenFile()) ;
                        {
                            this.richTextBox1.SaveFile(fileopen, RichTextBoxStreamType.PlainText);
                        }
                    }
                    catch (IOException exc)
                    {
                        MessageBox.Show(exc.Message, "Error");
                        return;
                    }
                    break;
                default:
                    break;
            }// end switch
            //this.saveFileDialog1.Filter = ".exe";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.folderBrowserDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    MessageBox.Show(this.folderBrowserDialog1.RootFolder.ToString());
                    MessageBox.Show(this.folderBrowserDialog1.SelectedPath.ToString());
                    break;
            }
        }
        //private void ritchTextBox1_TextCahged(object sender, EventArgs e )
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e) {
            Clipboard.SetText(this.richTextBox1.Text);
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e) {
            this.richTextBox1.Text = Clipboard.GetText();
        }
    } // end Form
}
