using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_i_14._4
{
    public partial class Form1 : Form
    {
        public float Angle;
        private System.Drawing.Drawing2D.Matrix rotatematrix = new System.Drawing.Drawing2D.Matrix();
        public static readonly float ToRadin = (float)(Math.PI/180);
        public Form1()
        {
            InitializeComponent();
            this.pictureBox1.Bounds = new Rectangle(0,0, this.Width, this.Height+100);// Bounds - границы
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.Angle = trackBar1.Value;
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            if (((PictureBox)sender).Image == null) return;
            // Выбор высокого качества интерполяции
            // Интерполяция - метод увеличения размера пикселей внутри изображения.
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            // Линейное пр-е сдвиг начала координат на вектр(х/2, y/2)
            e.Graphics.TranslateTransform(((PictureBox)sender).Width / 2+50, ((PictureBox)sender).Height / 2-70);
            e.Graphics.RotateTransform(Angle);
            //Рисование изображ. в центре picBox
            if (((PictureBox)sender).Image.Width < ((PictureBox)sender).Image.Width / 2F && ((PictureBox)sender).Image.Height < ((PictureBox)sender).Image.Height / 2F)
            {
                e.Graphics.DrawImage(((PictureBox)sender).Image, -((PictureBox)sender).Image.Width / 2F,
                                        -((PictureBox)sender).Image.Height / 2F);
            }
            else
            {
                e.Graphics.DrawImage(((PictureBox)sender).Image, -2 * ((PictureBox)sender).Width / 6F, -2 * ((PictureBox)sender).Height / 9F,
                                        ((PictureBox)sender).Width / 2F, ((PictureBox)sender).Height / 3F);
            }
        }
    }
}
