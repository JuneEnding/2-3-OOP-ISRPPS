using System.Collections.Generic;
using System.Drawing;

namespace GrafObj
{
    public class ControllerCreateObject: Controller.ControllerDiagram
    {
        public ControllerCreateObject(Model model) : base(model) { }
        // отрисовка в gr
        public override void Paint(Graphics gr)
        {
            //Graphics gr = picture.CreateGraphics();
            font = new Font("Times New Roman", 4 * scale, FontStyle.Regular);
            woffset = 5 * scale;
            hoffset = 8 * scale;
            int bold = scale / 4 + 1;
            Pen pen1 = new Pen(Color.Black, 1 * bold);
            Pen pen3 = new Pen(Color.Black, 3 * bold);

            // текущие координаты для объекта
            int curX1 = 0;
            int curY1 = 0;
            int curw = scale * wobj;
            int curh = scale * hobj;

            // угол координатной сетки
            // X + 1/2 ширины объекта
            gr.DrawLine(new Pen(Color.Black, bold), woffset, hoffset, woffset + curw / 2, hoffset);
            // Y + высота объекта
            gr.DrawLine(new Pen(Color.Black, bold), woffset, hoffset, woffset, hoffset + curh / 2);

            //цикл по объектам
            for (int count = 0; count < objects.Count; count++)
            {
                Obj el = objects[count];

                curX1 = woffset + curw / 2 + count * curw * 2;
                curY1 = hoffset + curh * 1 + count * curh * 3;

                Pen elpen1 = new Pen(el.color, bold);
                Pen elpen3 = new Pen(el.color, 3 * bold);
                SolidBrush elBrush = new SolidBrush(el.color);

                //__________________________________________________________________
                // горизонтальная линия координатной сетки
                gr.DrawLine(pen1, curX1 - curw / 2, hoffset,
                                    // 3 с выпиранием вперед для продолжения оси(1.5 точная длина)
                                    curX1 + 1.5f * curw, hoffset);
                // засечка
                gr.DrawLine(pen3, curX1 + 1.5f * curw, 0,
                                    curX1 + 1.5f * curw, hoffset + bold * 2);
                // имя объекта
                gr.DrawString(el.name, font, elBrush, curX1 + 0.5f * curw, 0);

                //__________________________________________________________________
                // вертикальная линия координатной сетки
                gr.DrawLine(pen1, woffset, curY1 - 0.5f * curh,
                                    woffset, curY1 + 2.5f * curh);
                // засечка
                gr.DrawLine(elpen3, 0, curY1 - 0.5f * curh,
                                    woffset + 5, curY1 - 0.5f * curh);
                // номер события
                gr.DrawString(count.ToString(), font, elBrush, 0, curY1 - curh - font.SizeInPoints);

                //__________________________________________________________________
                // объект
                //gr.DrawRectangle( 
                gr.FillRectangle(elBrush, curX1, curY1, curw, curh);
                if ((curX1 <= clikpoint.X && clikpoint.X <= curX1 + curw) &&
                    (curY1 <= clikpoint.Y && clikpoint.Y <= curY1 + curh)
                    )
                {
                    if (!model.ObjectSelected())
                    {
                        model.SetSelectedObject(el.ind);
                    }
                    gr.DrawRectangle(pen3, curX1, curY1, curw, curh);
                }

                //__________________________________________________________________
                // конструктор (pen3.Width/2 небольшой сдвиг вниз из за толщины)
                gr.DrawLine(pen3, curX1, curY1 - 0.5f * curh + pen3.Width / 2,
                                    curX1 + curw, curY1 - 0.5f * curh + pen3.Width / 2);
                // выход конструктора
                gr.DrawLine(pen1, curX1 + curw / 2, curY1 - 0.5f * curh,
                                    curX1 + curw / 2, curY1);
                DrawArrow(gr, pen1, curX1 + curw / 2, curY1);

                //__________________________________________________________________
                // связи
                for (int numrel = 0; numrel < el.relationsto.Count; numrel++)
                {
                    Relation rel = el.relationsto[numrel];
                    float shift = GetShift(count, numrel);
                    //Relation relto = el.relationsfrom[rel.to];
                    Pen curpen = new Pen(rel.color, bold);
                    DrawLinkDown(this, gr, curpen, rel, numrel, curX1, curY1, curw, curh, shift);
                }
            }

            curX1 = woffset + curw / 2 + objects.Count * curw * 2;
            curY1 = hoffset + curh * 1 + objects.Count * curh * 3;
            //__________________________________________________________________
            // горизонтальная линия координатной сетки
            gr.DrawLine(pen1, curX1 - curw / 2, hoffset, curX1 + 2.5f * curw, hoffset);
            gr.DrawString("objects", font, new SolidBrush(Color.Black), curX1 + 0.5f * curw, 0);
            // стрелка вправо
            gr.DrawLine(pen1, curX1 + 2.5f * curw - pen1.Width * 4, hoffset - pen1.Width * 2, curX1 + 2.5f * curw, hoffset);
            gr.DrawLine(pen1, curX1 + 2.5f * curw - pen1.Width * 4, hoffset + pen1.Width * 2, curX1 + 2.5f * curw, hoffset);

            //__________________________________________________________________
            // вертикальная линия координатной сетки
            gr.DrawLine(pen1, woffset, curY1 - 0.5f * curh, woffset, curY1);
            gr.DrawString("event", font, new SolidBrush(Color.Black), 0, curY1);
            // стрелка вниз
            gr.DrawLine(pen1, woffset - pen1.Width * 2, curY1 - pen1.Width * 4, woffset, curY1);
            gr.DrawLine(pen1, woffset + pen1.Width * 2, curY1 - pen1.Width * 4, woffset, curY1);
        }
    }
}
