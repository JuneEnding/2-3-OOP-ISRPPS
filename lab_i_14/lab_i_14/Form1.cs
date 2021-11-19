using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab_i_14
{
    public partial class Form1 : Form
    {
        float angle1 = 30;
        float angle2 = 30;

        public Form1() {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            angle1 += 0.5f;                 // Увеличивается угол
            this.pictureBox1.Invalidate(); // Делает недействительной всю поверхность элемента управления и вызывает его перерисовку.
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show("Вы прекрасны!", "Напоминание");
            angle2 -= -0.1f;                 // Увеличивается угол
            this.pictureBox1.Invalidate(); // Делает недействительной всю поверхность элемента управления и вызывает его перерисовку.
        }

        Graphics g;
        Point[] pts = { new Point(-75, 0), new Point(0, -75), new Point(75, 0), new Point(0, 75) };
        Point[] pts2 = { new Point(-65, 0), new Point(0, -65), new Point(65, 0), new Point(0, 65) };
        Pen penAzure1 = new Pen(Color.Azure, 1);
        Pen penAzure3 = new Pen(Color.Azure, 3);
        Pen penBlack3 = new Pen(Color.Black, 3);
        SolidBrush solidBrushAzure = new SolidBrush(Color.Azure);
        SolidBrush solidBrushBlue = new SolidBrush(Color.Blue);
        // Rectangle для демогстрации перегрузок методов Graphics, полезны при множественной отрисовке объектов с 1 координатами и размерами
        Rectangle rectangle = new Rectangle(-50, -50, 100, 100);
        Rectangle rectangleForEarth = new Rectangle(160, 160, 60, 60);

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {   // 
            g = Graphics.FromHwnd(this.pictureBox1.Handle);
            e.Graphics.TranslateTransform(this.pictureBox1.Width / 2, this.pictureBox1.Height / 2); // Сдвиг матрицы, чтобы угол был в центре pic.box
            e.Graphics.RotateTransform(angle1);// "Вращение" - матрица умножается на угол
            g.TranslateTransform(this.pictureBox1.Width / 2, this.pictureBox1.Height / 2);
            g.RotateTransform(angle2);
            g.FillRectangle(solidBrushAzure, rectangle);
            g.FillPolygon(solidBrushAzure, pts2);
            g.DrawEllipse(penAzure1, -80, -80, 160, 160);
            g.DrawEllipse(penAzure3, -125, -125, 250, 250);
            g.DrawEllipse(penAzure3, -200, -200, 400, 400);
            //g.DrawLine(penAzure1, 50, 50, 250, 250);

            e.Graphics.FillPolygon(solidBrushAzure, pts);
            e.Graphics.FillRectangle(solidBrushAzure, rectangle);
            e.Graphics.DrawEllipse(penBlack3, -270, -270, 540, 540);
            e.Graphics.FillEllipse(solidBrushBlue, rectangleForEarth);

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //e.Graphics.DrawRectangle(new Pen(Color.Azure, 35), 30, 30, 60, 60);
            //e.Graphics.DrawRectangle(new Pen(Color.Azure, 10), 30, 90, 60, 60);
            //e.Graphics.DrawRectangle(new Pen(Color.Azure, 10), 90, 30, 60, 60);
            //e.Graphics.DrawRectangle(new Pen(Color.Azure, 10), 60, 60, 90, 90);
            //e.Graphics.DrawLine(new Pen(Color.Azure, 5), 0, 0, 200, 200);
            //e.Graphics.DrawLine(new Pen(Color.Azure, 5), 0, 0, 30, 200);
            //e.Graphics.DrawLine(new Pen(Color.Azure, 5), 0, 0, 200, 30);

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // e.Graphics.Clear(Color.Transparent);
            //gr2.Clear(Color.Transparent);
            //gr3.Clear(Color.Transparent);
            //e.Graphics.FillRectangle(new SolidBrush(Color.Azure), -50, -50, 100, 100);

            //gr3.TranslateTransform(this.pictureBox1.Width / 2, this.pictureBox1.Height / 2); // Сдвиг матрицы, чтобы угол был в центре pic.box
            //gr3.RotateTransform(angle3);
            //gr3.FillRectangle(new SolidBrush(Color.Azure), -50, -50, 100, 100);

            //gr2.Clear(Color.Transparent);
            //gr2.TranslateTransform(this.pictureBox1.Width / 2, this.pictureBox1.Height / 2); // Сдвиг матрицы, чтобы угол был в центре pic.box
            //gr2.FillEllipse(new SolidBrush(Color.Blue), 125, 125, 60, 60);
            //gr2.FillRectangle(new SolidBrush(Color.Azure), -50, -50, 100, 100);
            //gr2.RotateTransform(angle2*(-1));
        }
        private void toolStripButton1_Click_1(object sender, EventArgs e) {
            this.timer1.Stop();
        }

        private void toolStripButtonStart_Click(object sender, EventArgs e) {
            this.timer1.Start();
        }
    }
}
