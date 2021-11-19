using System.Collections.Generic;
using System.Drawing;

namespace GrafObj
{
    public class ControllerEditor
    {

        public class Diagramm
        {
            public Model model;

            public Point clikpoint { get; set; }    // нажатая кнопка
            public int scale { get; set; }  = 4;    // масштабирование (теоретически можно скалировать объект формы)
            const int wobj = 9;                     // ширина объекта
            const int hobj = 9;                     // высота объекта
            public int cobj { get; set; } = 3;      // количество ссылок
            public int woffset { get; set; }        // горизонтальное смещение (для отрисовки оси)
            public int hoffset { get; set; }        // вертикальное смещение (для отрисовки оси)
            public Font font;
            private bool checkSelectRel = true;

            // Конструктор
            //----------------------------------------------------------------------------
            public Diagramm(Model model)
            {
                this.model = model;
            }

            // Операции
            //----------------------------------------------------------------------------
            // отрисовка с нижней стороны
            // отрисовка стрелки вниз
            private void DrawArrow(Graphics gr, Pen pen, float X, float Y)
            {
                gr.DrawLine(pen, X - pen.Width * 2, Y - pen.Width * 4, X, Y);
                gr.DrawLine(pen, X + pen.Width * 2, Y - pen.Width * 4, X, Y);
            }
            // отрисовка в gr
            public void Paint(Graphics gr)
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

                //model.SetSelectedObject( -1);
                //цикл по объектам
                for (int count = 0; count < model.countel; count++)
                {
                    Model.DataElem el = model.adjmatrix[count * 11];

                    curX1 = el.location.X;
                    curY1 = el.location.Y;

                    Pen elpen1 = new Pen(Color.FromName(el.color), bold);
                    Pen elpen3 = new Pen(Color.FromName(el.color), 3 * bold);
                    SolidBrush elBrush = new SolidBrush(Color.FromName(el.color));



                    //__________________________________________________________________
                    // объект
                    //gr.DrawRectangle( 
                    gr.FillRectangle(elBrush, curX1, curY1, curw, curh);
                    if (!model.ObjectSelected())
                    {
                        if (model.PointInObject(clikpoint, curw, curh) == el)
                        {
                            gr.DrawRectangle(pen3, curX1, curY1, curw, curh);
                            model.SetSelectedObject(count * 11);
                        }

                    }



                    // имя объекта
                    gr.DrawString(el.name, font, new SolidBrush(Color.Black),   curX1 + 0.2f * curw, curY1 + 0.2f * curh);

                    //__________________________________________________________________
                    // связи
                    for (int numrel = 0; numrel < model.countel; numrel++)
                    {
                        int ind = count * 10 + numrel;
                        Model.DataElem rel = model.adjmatrix[ind];
                        if (rel != null)
                        if (rel.direction > 0)
                        {
                            Model.DataElem elto = model.adjmatrix[numrel * 11];
                            Pen curpen = new Pen(Color.FromName(el.color), bold);
                            
                            if (checkSelectRel)
                            if (!model.ObjectSelected())
                            if (model.PointInRelated(clikpoint, curw, curh) == rel)
                            {
                                gr.DrawLine(pen3,   el.location.X + curw, el.location.Y + curh,
                                                    elto.location.X, elto.location.Y);
                                model.SetSelectedObject(ind);
                            }

                                // ссылка
                                //gr.DrawRectangle(curpen, curX1 + curw * (numrel + 1), curY1, curw, curh);
                                //DrawLinkDown(this, gr, curpen, rel, numrel, curX1 + curw * (numrel + 1), curY1, curw, curh, shift);
                                gr.DrawLine(curpen, el.location.X + curw, el.location.Y + curh, 
                                                    elto.location.X, elto.location.Y);

                        }
                    }

                }
            }
        }
    }
}
