using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab_i_3
{
    delegate void DelegateTool(MouseEventArgs e);
    //private 
    public partial class Form1 : Form {
        Bitmap bitmap = null;
        Graphics gr = null;
        Pen pen = new Pen(Color.White, 3.5f); // Ручка!! Посмотри атрибуты (ручка отлич от кости!)
        Pen pBrush = new Pen(new SolidBrush(Color.Black), 15.5f);

        private DelegateTool _operation; // Делегат
        
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            gr = Graphics.FromImage(bitmap);
            this._operation = tBrush;
        }
        
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.textBox1.Text = "X = " + e.X + " Y = " + e.Y;
            this.label2.Text = "X = " + e.X + " Y = " + e.Y;
            Graphics gr = this.pictureBox1.CreateGraphics(); // графика 
            if (e.Button != MouseButtons.Left) return;
                //gr.DrawLine(pBrush, pf1, new PointF(e.X + 1, e.Y + 1)); // ф-я, рис линиб е.loc.. показ событие движ мышки когда я двигаюсь, вызыв событие, которое генирирует пикчебокс, вызыв соб, показ где у менят точка находится
                this._operation(e);
            this.pictureBox1.Image = this.bitmap;
        }

        private void ____(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.pictureBox1.Refresh();
            this.gr.Clear(this.pictureBox1.BackColor);
            this.pictureBox1.Image = this.bitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ToolBrush();
        }
        public void ToolDraw(MouseEventArgs e)  { _operation(e); }
        public void ToolBrush() { _operation = tBrush; }// объекты присваиваем }
        public void ToolPen() { _operation = tPen; }
        // +++++++++++++++++++++++++++++++++++
        // Функции рисования линии
        private void tBrush(MouseEventArgs e) 
        {
            gr.DrawLine(pBrush, e.X, e.Y, e.X + 1, e.Y + 1); // ф-я, рис линиб е.loc.. показ событие движ мышки когда я двигаюсь, вызыв событие, которое генирирует пикчебокс, вызыв соб, показ где у менят точка находится
        }
        private void tPen(MouseEventArgs e) // Присваивается функция?
        {
            gr.DrawLine(pen, e.X, e.Y, e.X + 1, e.Y + 1); // ф-я, рис линиб е.loc.. показ событие движ мышки когда я двигаюсь, вызыв событие, которое генирирует пикчебокс, вызыв соб, показ где у менят точка находится
        }
        private void tDrawWeb(MouseEventArgs e)
        {
            // Структура, представляет упорядоченную пару координат Х и Y с плавающей запятой
            PointF pointf;
            int step = 4;
            this.gr.Clear(this.pictureBox1.BackColor);
            this.pictureBox1.Image = this.bitmap;
            for (int i = 0; i <step; i++)
            {
                pointf = new PointF(0, (i * (this.pictureBox1.Size.Height)) / step);
                // Height
                this.gr.DrawLine(pen, pointf, e.Location);

                pointf.X = this.pictureBox1.Size.Width;
                this.gr.DrawLine(pen, pointf, e.Location);

                pointf.X = (i * (this.pictureBox1.Size.Width)) / step;
                pointf.Y = 0;
                // Size.Width
                this.gr.DrawLine(pen, pointf, e.Location);
                pointf.Y = this.pictureBox1.Size.Height;
                this.gr.DrawLine(pen, pointf, e.Location);
            }
            this.gr.DrawLine(pen, this.pictureBox1.Size.Width,
                                  this.pictureBox1.Size.Height, e.X, e.Y);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            _operation = tDrawWeb;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ToolPen();
        }
    }//end class
}
