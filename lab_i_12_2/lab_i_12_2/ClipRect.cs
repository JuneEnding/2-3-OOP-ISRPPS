using System;
using System.Drawing;

namespace lab_i_12_2 {

    //delegate double GetDistance(Point pnt1, Point pnt2);
    public delegate double GetDistance(int x1, int y1, int x2, int y2);

    public class ClipRect {

        private Rectangle clipRect;

        public ClipRect(){
            this.clipRect = new Rectangle(0, 0, 80, 80);
        }
        public ClipRect(int x, int y, int width, int height) {
            this.clipRect = new Rectangle(x, y, width, height);
        }

        // По переданной картинке возвращается ее часть, выделенная clipRect
        public Image ClipImage(Image origIm)
        {
            Bitmap clipBmp = new Bitmap(this.clipRect.Width, this.clipRect.Height); // Cозд. битмап размера clipRect
            using (Graphics g = Graphics.FromImage(clipBmp)) // берем графику из clipBmp
            {
                // DrawImage рисует заданную часть указанного объекта System.Drawing.Image в заданном месте, используя заданный размер.
                g.DrawImage(origIm,                                                         // Объект рисования
                            new Rectangle(0, 0, this.clipRect.Width, this.clipRect.Height), // Задает расположение и размер формируемогоизображения. 
                            this.clipRect,                                                  // Задает часть объекта image для рисования
                            GraphicsUnit.Pixel);                                            // Единица измерения для данных
            }
            return (Image)clipBmp; // Возвращаем выделенную clipRect область Image clipBmp
        }

        GetDistance getDistance = (x1, y1, x2, y2) => Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        // порог
        public int threshold = 30;
        public void PointTransform(Point Location) {
            if (getDistance(Location.X, Location.Y, clipRect.Left, clipRect.Top) <= threshold)
            {
                clipRect.Width = Math.Abs(clipRect.Width + clipRect.X - Location.X);
                clipRect.Height = Math.Abs(clipRect.Height + clipRect.Y - Location.Y);
                clipRect.X = Location.X;
                clipRect.Y = Location.Y;
            }
            else if (getDistance(Location.X, Location.Y, clipRect.Right, clipRect.Top) <= threshold)
            {
                clipRect.Width = Math.Abs(Location.X - clipRect.X);
                clipRect.Height = Math.Abs(clipRect.Height + clipRect.Y - Location.Y);
                clipRect.Y = Location.Y;
            }
            else if (getDistance(Location.X, Location.Y, clipRect.Left, clipRect.Bottom) <= threshold)
            {
                clipRect.Width = Math.Abs(clipRect.Width + clipRect.X - Location.X);
                clipRect.Height = Math.Abs(Location.Y - clipRect.Y);
                clipRect.X = Location.X;
            }
            else if (getDistance(Location.X, Location.Y, clipRect.Right, clipRect.Bottom) <= threshold)
            {
                clipRect.Height = Math.Abs(Location.Y - clipRect.Y);
                clipRect.Width = Math.Abs(Location.X - clipRect.X);
            }
            else if (clipRect.Contains(Location))
            {
                clipRect.X = Location.X - (clipRect.Width / 2);
                clipRect.Y = Location.Y - (clipRect.Height / 2);
            }
        }

        public void DrawClipRectangle(Pen pen, Graphics graphics) {
            graphics.DrawRectangle(pen, this.clipRect);
        }
    }
}
