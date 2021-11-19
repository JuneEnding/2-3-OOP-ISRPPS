using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_i_13
{
    public partial class Form1 : Form
    {
        Graphics grfx;
        int cx;
        int cy;
        public Form1()
        {
            InitializeComponent();
            //this.Handle;
        }

        Pen penRed5 = new Pen(Color.Red, 5);
        Pen penPink2 = new Pen(Color.Pink, 2);
        private void button1_Click(object sender, EventArgs e)
        {
            // берем дискриптор окна
            grfx = Graphics.FromHwnd(this.pictureBox1.Handle);
            cx = pictureBox1.Size.Width;
            cy = pictureBox1.Size.Height - 60;
            grfx.DrawLine(penRed5, 0,0,cx,cy);
            grfx.DrawLine(penRed5, cx,0,0,cy);
            grfx.DrawLine(penRed5, 0, cy, cx, pictureBox1.Size.Height);
            grfx.DrawLine(penRed5, cx, cy, 0, pictureBox1.Size.Height);
            grfx.DrawLine(penPink2, 0, 0, cx, cy);
            grfx.DrawLine(penPink2, cx, 0, 0, cy);
            grfx.DrawLine(penPink2, 0, cy, cx, pictureBox1.Size.Height);
            grfx.DrawLine(penPink2, cx, cy, 0, pictureBox1.Size.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            grfx = Graphics.FromHwnd(this.pictureBox2.Handle);
            cx = pictureBox2.Size.Width;
            cy = pictureBox2.Size.Height;
            PointF[] aptf = new PointF[cx];
            //grfx.Clear(Color.SkyBlue);
            for (int i = 0; i < cx; i++) {
                aptf[i].X = i;
                aptf[i].Y = cy/2*(1-(float)Math.Sin(i*2*Math.PI/(cx-1))); // синусоида разбита на 2 части, i - шаг
            }
            grfx.DrawLines(new Pen(Color.Orange,5), aptf);
            grfx.DrawLines(new Pen(Color.LightYellow,2), aptf);
        }
        SolidBrush solidBrushY = new SolidBrush(Color.Yellow);
        private void button3_Click(object sender, EventArgs e)
        {
            grfx = Graphics.FromHwnd(this.pictureBox3.Handle);
            cx = pictureBox3.Size.Width;
            cy = pictureBox3.Size.Height;
            //grfx.Clear(Color.Silver);
            grfx.FillEllipse(solidBrushY, 0, 0, cx, cy);
            grfx.FillEllipse(new SolidBrush(Color.White), 0+5, 0+5, cx-10, cy-10);
            grfx.FillEllipse(solidBrushY, 0+10, 0+10, cx-20, cy-20);
            grfx.FillEllipse(new SolidBrush(Color.Black), 0+20, 0+20, cx-40, cy-40);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            grfx = Graphics.FromHwnd(this.pictureBox4.Handle);
            cx = pictureBox4.Size.Width;
            cy = pictureBox4.Size.Height;
            //grfx.Clear(Color.Silver);
            grfx.DrawEllipse(new Pen(Color.Green,7), 0, 0, cx, cy);
            grfx.DrawEllipse(new Pen(Color.LightSeaGreen,4), 0, 0, cx, cy);
            grfx.DrawEllipse(new Pen(Color.White,1), 0, 0, cx, cy);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            grfx = Graphics.FromHwnd(this.pictureBox5.Handle);
            cx = pictureBox5.Size.Width;
            cy = pictureBox5.Size.Height;
            //grfx.Clear(Color.Silver);

            grfx.DrawBezier(new Pen(Color.SkyBlue,8), 
                            new PointF(0, 0),
                            new PointF(cx+40, cy + 40),
                            new PointF(cx+30 , 0 - 100),
                            new PointF(0, cy));
            grfx.DrawBezier(new Pen(Color.White, 2),
                            new PointF(0, 0),
                            new PointF(cx + 40, cy + 40),
                            new PointF(cx + 30, 0 - 100),
                            new PointF(0, cy));
            //grfx.DrawBezier(new Pen(Color.White, 2),
            //    new PointF(cx / 2 + 2, cy / 2 + 2),
            //    new PointF(cx / 2, cy / 4),
            //    new PointF(cx / 4, cy / 2),
            //    new PointF(cx / 2 + 2, cy / 2 + 2));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            grfx = Graphics.FromHwnd(this.pictureBox6.Handle);
            cx = pictureBox6.Size.Width;
            cy = pictureBox6.Size.Height;
            //grfx.Clear(Color.Silver);
            PointF[] points = new PointF[4] {new PointF(0+2,0+2),
                                             new PointF(cx/2-20,cy-20),
                                             new PointF(cx/2+20,cy-20),
                                             new PointF(cx,0)};

            grfx.DrawCurve(new Pen(Color.Violet, 6), points);
            grfx.DrawCurve(new Pen(Color.Pink, 2), points);
        }
    }
}
