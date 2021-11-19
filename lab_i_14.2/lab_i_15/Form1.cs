using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_i_15
{
    public partial class Form1 : Form
    {
        int angle = 30;
        Graphics gr;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            if (((PictureBox)sender).Image == null) return;
            //Меняем режим интерполяции на высокий
            //Интерполяция - метод увеличения размера пикселей внутри изображения.
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            // Первоначальное пр-е координат на вектр (х/2, у/2)
            e.Graphics.TranslateTransform(((PictureBox)sender).Width/2, ((PictureBox)sender).Height / 2);
            if (this.radioButtonZ.Checked)
            {
                e.Graphics.RotateTransform(angle);
                // Отрисовка изображения в центре picturebox
                if (((PictureBox)sender).Image.Width<((PictureBox)sender).Width/2F&& (((PictureBox)sender).Height / 2F) < ((PictureBox)sender).Height / 2F)
                {
                    e.Graphics.DrawImage(((PictureBox)sender).Image, -((PictureBox)sender).Image);
                }
                else
                {

                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            angle = this.trackBar1.Value;

            //Invalidate();
        }
    }
}
