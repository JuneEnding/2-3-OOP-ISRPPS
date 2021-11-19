using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace lab_i_12_2
{
    //delegate double GetDistance(Point pnt1, Point pnt2);
    delegate double GetDistance(int x1, int y1, int x2, int y2);
    public partial class Form1 : Form
    {
        private Rectangle clipRect = new Rectangle(0,0,80,80); // Задаем прямоугольник сами
        public Form1() {
            InitializeComponent();
        }

        // Первоначальный алгоритм
        //private double GetDistance(Point pnt1, Point pnt2) // Используем тиарему пифагора и вычисляем расстояние между 2 точками
        //{
        //    int a = pnt2.X - pnt1.X;
        //    int b = pnt2.Y - pnt1.Y;
        //    return Math.Sqrt(a * a + b * b);
        //    //return ();
        //}

        private void button2_Click(object sender, EventArgs e){
            OpenFileDialog dl = new OpenFileDialog();
            if (dl.ShowDialog() == DialogResult.OK){
                try{
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();
                    pictureBox1.Image = Image.FromFile(dl.FileName); // Загрузили картинки
                } catch (Exception) { MessageBox.Show("Invalid Image File"); } 
            }
        }

        private void button3_Click(object sender, EventArgs e){
            DialogResult dr = this.saveFileDialog1.ShowDialog();
            switch (dr){
                case DialogResult.OK:
                    try{
                        Bitmap savedBit = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                        pictureBox2.DrawToBitmap(savedBit, pictureBox2.ClientRectangle); // куда  мы рисуем  и какую область
                        savedBit.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg); // с каким именем и форматом
                    } catch (IOException exc) { // Попадаем сюда при ошибках файловой системы (места нет\прав нет...)
                        MessageBox.Show(exc.Message, "Error");
                        return;
                    }
                    break;
            }// end switch
        }

        private void button1_Click(object sender, EventArgs e){
            if (pictureBox1.Image != null)
                pictureBox2.Image = ClipImage(pictureBox1.Image, clipRect);
        }

            // Реализация метода Dispose используется для освобождения неуправляемых ресурсов.
            // Для его вызова можно использовать следующую конструкцию try..catch, но есть синонимичная с using
            // Конструкция using оформляет блок кода и создает объект некоторого класса, который реализует интерфейс IDisposable, 
            // в частности, его метод Dispose. При завершении блока кода у объекта вызывается метод Dispose.

        private Image ClipImage(Image origIm, Rectangle clipRect) {
            Bitmap clipBmp = new Bitmap(clipRect.Width, clipRect.Height); // Cозд. битмап размера clipRect
            using (Graphics g = Graphics.FromImage(clipBmp)) // берем графику из clipBmp
            {
                // DrawImage рисует заданную часть указанного объекта System.Drawing.Image в заданном месте, используя заданный размер.
                g.DrawImage(origIm,                                             // Объект рисования
                            new Rectangle(0,0,clipRect.Width, clipRect.Height), // Задает расположение и размер формируемогоизображения. 
                            clipRect,                                           // Задает часть объекта image для рисования
                            GraphicsUnit.Pixel);                                // Единица измерения для данных
            }
            return (Image)clipBmp; // Возвращаем выделенную clipRect область Image clipBmp
        }

        // Первоначальный алгоритм преобразования clipRect
        //private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
        //    // Отрисовка clipRect
        //    if (e.Button == MouseButtons.Left) {

        //        // Точки углов clipRect
        //        Point topLeft = new Point(clipRect.Left, clipRect.Top);
        //        Point topRight = new Point(clipRect.Right, clipRect.Top);
        //        Point bottomLeft = new Point(clipRect.Left, clipRect.Bottom);
        //        Point bottomRight = new Point(clipRect.Right, clipRect.Bottom);

        //        // сработает на ближней точке к курсору
        //        if (getDistance(e.Location.X, e.Location.Y, clipRect.Left, clipRect.Top) <= threshold) { 
        //            topLeft = e.Location;
        //            bottomLeft.X = topLeft.X;
        //            topRight.Y = topLeft.Y;
        //        }
        //        else if (getDistance(e.Location.X, e.Location.Y, clipRect.Right, clipRect.Top) <= threshold) {
        //            topRight = e.Location;
        //            bottomRight.X = topRight.X;
        //            topLeft.Y = topRight.Y;
        //        }
        //        else if (getDistance(e.Location.X, e.Location.Y, clipRect.Left, clipRect.Bottom) <= threshold) {
        //            bottomLeft = e.Location;
        //            topLeft.X = bottomLeft.X;
        //            bottomRight.Y = bottomLeft.Y;
        //        }
        //        else if (getDistance(e.Location.X, e.Location.Y, clipRect.Right, clipRect.Bottom) <= threshold) {
        //            bottomRight = e.Location;
        //            topRight.X = bottomRight.X;
        //            bottomLeft.Y = bottomRight.Y;
        //        }
        //        else if (clipRect.Contains(e.Location)) {
        //            topLeft.X = e.Location.X - (clipRect.Width / 2);
        //            topLeft.Y = e.Location.Y - (clipRect.Height / 2);
        //            topRight.X = e.Location.X + (clipRect.Width / 2);
        //            topRight.Y = topLeft.Y;
        //            bottomLeft.X = topLeft.X;
        //            bottomLeft.Y = e.Location.Y + (clipRect.Height / 2);
        //            bottomRight.X = topLeft.X;
        //            bottomRight.Y = bottomLeft.Y;
        //        }

        //        // Обновление прямоугольника clip
        //        int width = topRight.X - topLeft.X;
        //        int height = bottomLeft.Y - topLeft.Y;
        //        clipRect = new Rectangle(topLeft.X, topLeft.Y, width, height);
        //        this.Refresh();
        //    }
        //}

        // Альтернативный алгоритм 
        // Чем он лучше:
        // 1. Не создается новых переменных, используются e.Location и clipRect
        // 2. Идет преобразование объекта clipRect
        // 3. Прямоугольник не исчезает при отрицательных значениях перемещения("выворачивании")

        GetDistance getDistance = (x1, y1, x2, y2) => Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        // порог
        int threshold = 30;

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            // Отрисовка clipRect
            if (e.Button == MouseButtons.Left) {
                if (getDistance(e.Location.X, e.Location.Y, clipRect.Left, clipRect.Top) <= threshold)  {
                    clipRect.Width = Math.Abs(clipRect.Width + clipRect.X - e.Location.X);
                    clipRect.Height = Math.Abs(clipRect.Height + clipRect.Y - e.Location.Y);
                    clipRect.X = e.Location.X;
                    clipRect.Y = e.Location.Y;
                }
                else if (getDistance(e.Location.X, e.Location.Y, clipRect.Right, clipRect.Top) <= threshold) {
                    clipRect.Width = Math.Abs(e.Location.X - clipRect.X);
                    clipRect.Height = Math.Abs(clipRect.Height + clipRect.Y - e.Location.Y);
                    clipRect.Y = e.Location.Y;
                }
                else if (getDistance(e.Location.X, e.Location.Y, clipRect.Left, clipRect.Bottom) <= threshold) {
                    clipRect.Width = Math.Abs(clipRect.Width + clipRect.X - e.Location.X);
                    clipRect.Height = Math.Abs(e.Location.Y - clipRect.Y);
                    clipRect.X = e.Location.X;
                }
                else if (getDistance(e.Location.X, e.Location.Y, clipRect.Right, clipRect.Bottom) <= threshold) {
                    clipRect.Height = Math.Abs(e.Location.Y - clipRect.Y);
                    clipRect.Width = Math.Abs(e.Location.X - clipRect.X);
                }
                else if (clipRect.Contains(e.Location)) {
                    clipRect.X = e.Location.X - (clipRect.Width / 2);
                    clipRect.Y = e.Location.Y - (clipRect.Height / 2);
                }
                this.Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            // Прямоугольник - clip
            e.Graphics.DrawRectangle(Pens.Lime, clipRect);
        }
    }
}
