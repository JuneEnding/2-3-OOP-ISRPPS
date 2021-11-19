using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GrafObj
{
    delegate void DelegateView(Graphics g);

    public partial class Form1 : Form
    {
        private Model model = new Model();
        private DelegateView _view = null;
        private ControllerCreateObject.Diagramm graf1;
        private ControllerDiagramOfScope.Diagramm graf2;
        private Point clikpoint;
        public Form1() {
            InitializeComponent();
            graf1 = new ControllerCreateObject.Diagramm(model);
            graf2 = new ControllerDiagramOfScope.Diagramm(model);
            this.ScaleNnum.BringToFront();
            this.label1.BringToFront();
        }

        public void ObjectCreationDiagram() { _view = tObjectCreationDiagram; }
        public void DiagramOfScope()        { _view = tDiagramOfScope; }

        private void tObjectCreationDiagram(Graphics g) {
            graf1.clikpoint = clikpoint;
            graf1.scale = Convert.ToInt32(ScaleNnum.Value);
            graf1.Paint(g);                         // передаем графику в паинт (куда мы рисуем)
        }
        private void tDiagramOfScope(Graphics g) {
            graf2.clikpoint = clikpoint;
            graf2.scale = Convert.ToInt32(ScaleNnum.Value);
            graf2.Paint(g);
        }

                         // кто передал событие | какое событие 
        private void pictureObj_Paint(object sender, PaintEventArgs e) {
            _view?.Invoke(e.Graphics);
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            this.toolStripTextBox1.Text = ScaleNnum.Value.ToString();
            pictureObj.Invalidate(); // инвалидация при d размера
        }
        private void pictureObj_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(PointToScreen(e.Location));
            }
            else {
                pictureObj.Invalidate();
                clikpoint = e.Location; // получ. координат клика 
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void воВесьЭкранToolStripMenuItem_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Maximized;
        }

        private void диаграммаСозданияОбъектовToolStripMenuItem_Click(object sender, EventArgs e){
            toolStripStatusLabel1.Text = "Object creation diagram";
            ScaleNnum.Value = graf1.scale;
            ObjectCreationDiagram(); // делегат отрисовки
            pictureObj.Invalidate();
        }

        private void диаграммаОбластиСидимостиОбъектовToolStripMenuItem_Click(object sender, EventArgs e) {
            toolStripStatusLabel1.Text = "Diagram of scope";
            ScaleNnum.Value = graf2.scale;
            DiagramOfScope();
            pictureObj.Invalidate();
        }

        private void objectCreationDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Object creation diagram";
            ScaleNnum.Value = graf1.scale;
            ObjectCreationDiagram(); // делегат отрисовки
            pictureObj.Invalidate();
        }

        private void diagramOfScopeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Diagram of scope";
            ScaleNnum.Value = graf2.scale;
            DiagramOfScope();
            pictureObj.Invalidate();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (( 1<= Convert.ToInt32(this.toolStripTextBox1.Text))&&(Convert.ToInt32(this.toolStripTextBox1.Text) <= 10 ))
            {
                    this.toolStripTextBox1.ForeColor = Color.Black;
                    graf1.scale = Convert.ToInt32(this.toolStripTextBox1.Text);
                    this.ScaleNnum.Value = Convert.ToInt32(this.toolStripTextBox1.Text);
                    //ObjectCreationDiagram(); // делегат отрисовки
                    pictureObj.Invalidate();
            } else {
                    this.toolStripTextBox1.ForeColor = Color.Red; }
        }
 
        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e) {
            if (this.toolStripProgressBar1.Value<100)
                this.toolStripProgressBar1.Value += 10;
        }
        
        private void pictureObj_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Object creation diagram";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.D) {
                this.toolStripStatusLabel3.Text = Convert.ToString(Convert.ToInt32(this.toolStripStatusLabel3.Text) + 1);
            }
        }



        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip2.Show(PointToScreen(e.Location));
            }
        }

        private void скрытьМасштабToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Visible = !panel3.Visible;
            //ScaleNnum.Visible = !ScaleNnum.Visible;
            //label1.Visible = !label1.Visible;
        }


        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.fontDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    richTextBox1.Text = "Font: " + this.fontDialog1.Font.Name + ", "+ this.fontDialog1.Font.Size;
                    richTextBox1.Font = this.fontDialog1.Font;
                    graf1.font = this.fontDialog1.Font;
                    graf2.font = this.fontDialog1.Font;
                    pictureObj.Invalidate();
                    break;
            }// end switch
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e){
            DialogResult dr = this.saveFileDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    //MessageBox.Show(this.openFileDialog1.FileName);
                    try {
                        Bitmap savedBit = new Bitmap(pictureObj.Width, pictureObj.Height); 
                        pictureObj.DrawToBitmap(savedBit, pictureObj.ClientRectangle); // куда  мы рисуем  и какую область
                        savedBit.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg); // с каким именем и форматом
                    }
                    catch (IOException exc) { // улетаем сюда при ошибках файловой системы (места нет\прав нет...)
                        MessageBox.Show(exc.Message, "Error");
                        return;
                    }
                    break;
                default:
                    break;
            }// end switch
            //this.saveFileDialog1.Filter = ".exe";
        }

        private void загрузитьКартинкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    //MessageBox.Show(this.openFileDialog1.FileName);
                    try
                    {
                        pictureObj.ImageLocation = openFileDialog1.FileName; // полное имя файла?
                        pictureObj.Load();
                    }
                    catch (IOException exc)
                    { // улетаем сюда при ошибках файловой системы (места нет\прав нет...)
                        MessageBox.Show(exc.Message, "Error");
                        return;
                    }
                    break;
                default:
                    break;
            }// end switch
            //this.saveFileDialog1.Filter = ".exe";
        }

        private void залитьБелымToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureObj.ImageLocation = null;
            this.pictureObj.BackColor = Color.White;
        }

        private void задатьЦветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.colorDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    pictureObj.ImageLocation = null;
                    this.pictureObj.BackColor = this.colorDialog1.Color;
                    break;
            }// end switch
        }

        private void путьСохраненияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.folderBrowserDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    saveFileDialog1.InitialDirectory = folderBrowserDialog1.SelectedPath;
                    break;
            }// end switch
        }
        

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            graf1.font = richTextBox1.Font;
            graf2.font = richTextBox1.Font;
            pictureObj.Invalidate();
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStRiprichTextBox.Show(PointToScreen(e.Location));
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = Clipboard.GetText();
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStRiprichTextBox.Show(PointToScreen(e.Location));
            }
        }
    }
}
