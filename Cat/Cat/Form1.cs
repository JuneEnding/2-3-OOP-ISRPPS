using System;
using System.Windows.Forms;
using System.Drawing;

namespace Cat
{
    public partial class ChainResronsability : Form
    {
        Input input = new Input();
        Handlers handlers = new Handlers();
        public ChainResronsability()
        {
            InitializeComponent();
        }


        private void buttonRun_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None; // d стиля формы
            TransparencyKey = Color.White;          // белый цвет на форме становится невидимым
            buttonRun.Visible = false;
            foreach (int action in input.sequence)
            {
                handlers.Hand_all(action, this.pictureBox1);
                Left = Left - 16;                   // смещение формы влево
                Update();
            }
            buttonRun.Visible = true;
            FormBorderStyle = FormBorderStyle.Sizable;
            TransparencyKey = DefaultBackColor; 
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            buttonRun.Visible = true;
        }
    }
}
