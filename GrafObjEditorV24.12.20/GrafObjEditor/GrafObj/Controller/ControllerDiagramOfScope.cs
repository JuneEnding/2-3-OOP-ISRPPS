using System;
using System.Drawing;
using System.Globalization;

namespace GrafObj
{

    public class ControllerDiagramOfScope : Controller.ControllerDiagram
    {

        public int cobj { get; set; } = 3;      // количество ссылок


        // Конструктор
        //----------------------------------------------------------------------------
        public ControllerDiagramOfScope(Model model)
        {
            this.model = model;
            bool fading = model.ObjectSelected();
            for (int i = model.countel; i > 0; i--)
            {
                int ind = (i - 1) * 11;
                Model.DataElem el = model.adjmatrix[ind];
                if (el == model.GetSelectedObject())
                {
                    AddObj(el.name, el.color, ind);
                }else
                    AddObj(el.name, el.color, ind, fading);
            }
            int maxi = model.countel;
            for (int j = 0; j < maxi; j++)
            for (int i = 0; i < maxi; i++)
            {
                int jj = maxi - j - 1;
                if (model.adjmatrix[i * 10 + jj] != null)
                    if (model.adjmatrix[i * 10 + jj].direction > 0)
                    {
                        Model.DataElem eli = model.adjmatrix[i * 11];
                        Model.DataElem elj = model.adjmatrix[jj * 11];

                        AddRelFrom(elj.name, eli.name, ColorFromName(eli.color), 0);
                        AddRelTo(eli.name, elj.name, ColorFromName(eli.color), 1);
                    }
            }
        }



        // отрисовка с нижней стороны
        public override void DrawLinkDown(Controller.ControllerDiagram diagramm, Graphics gr, Pen pen, Relation rel, int numrel, float curX1, float curY1, float curw, float curh, float shift)
        {
            gr.DrawLine(pen,curX1 + curw / 2,
                            curY1 + curh / 2,
                            curX1 + curw / 2,
                            hoffset + rel.to * curh * 3 + curh * shift - curh * 0.7f);
            gr.FillEllipse(new SolidBrush(pen.Color), curX1 + curw / 2 - pen.Width * 2,
                                curY1 + curh / 2 - pen.Width * 2,
                                pen.Width * 4,
                                pen.Width * 4);

            gr.DrawLine(pen, curX1 + curw / 2,
                            hoffset + rel.to * curh * 3 + curh * shift - curh * 0.7f,
                            woffset + curw + rel.to * (curw * cobj) - curw * shift + curw / 2,
                            hoffset + rel.to * curh * 3 + curh * shift - curh * 0.7f);

            gr.DrawLine(pen, woffset + curw + rel.to * (curw * cobj) - curw * shift + curw / 2,
                            hoffset + rel.to * curh * 3 + curh * shift - curh * 0.7f,
                            woffset + curw + rel.to * (curw * cobj) - curw * shift + curw / 2,
                            hoffset + rel.to * curh * 3 + curh / 2);

            DrawArrow(gr, pen, woffset + curw + rel.to * (curw * cobj) - curw * shift + curw / 2,
                            hoffset + rel.to * curh * 3 + curh / 2);
        }
            
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
            gr.DrawLine(pen1, woffset, hoffset, woffset + curw / 2, hoffset);
            // Y + высота объекта
            gr.DrawLine(pen1, woffset, hoffset, woffset, hoffset + curh / 2);

            //цикл по объектам
            for (int count = 0; count < objects.Count; count++)
            {
                Obj el = objects[count];

                curX1 = woffset + curw / 2 + count * curw * cobj;
                curY1 = hoffset + curh * 1 + count * curh * 3;


                Color elcolor = el.color;
                if (el.fading){
                    elcolor = Color.FromArgb(Math.Max(model.timer / 4, 0), elcolor);
                }
                Pen elpen1 = new Pen(elcolor, bold);
                Pen elpen3 = new Pen(elcolor, 3 * bold);


                SolidBrush elBrush = new SolidBrush(elcolor);

                //__________________________________________________________________
                // горизонтальная линия координатной сетки
                gr.DrawLine(pen1, curX1 - curw / 2,   hoffset, 
                                    // 3 с выпиранием вперед для продолжения оси(1.5 точная длина)
                                    curX1 + curw * (cobj - 0.5f),   hoffset);
                // засечка
                gr.DrawLine(pen3, curX1 + (curw * cobj) - curw / 2, 0,
                                    curX1 + (curw * cobj) - curw / 2, hoffset + bold * 2);
                // имя объекта
                gr.DrawString(el.name, font, elBrush,   curX1 + 0.2f * curw, 0);

                //__________________________________________________________________
                // вертикальная линия координатной сетки
                gr.DrawLine(pen1, woffset,            curY1 - 0.5f * curh,
                                    woffset,            curY1 + 2.5f * curh);
                // засечка
                gr.DrawLine(pen3, 0,                  curY1 - 0.5f * curh,
                                    woffset + 5,        curY1 - 0.5f * curh);
                // номер события (здесь он не нужен)
                //gr.DrawString(count.ToString(), font, elBrush, 0, curY1 - curh - font.SizeInPoints);

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
                        model.SetSelectedObject( el.ind );
                        model.timer = 1000;
                    }
                    gr.DrawRectangle(pen3, curX1, curY1, curw, curh);
                }

                //__________________________________________________________________
                // конструктор (pen3.Width/2 небольшой сдвиг вниз из за толщины)
                gr.DrawLine(elpen3, curX1,              curY1 - 0.5f * curh + pen3.Width/2,
                                    curX1 + curw,       curY1 - 0.5f * curh + pen3.Width/2);
                // выход конструктора
                gr.DrawLine(elpen1, curX1 + curw / 2,   curY1 - 0.5f * curh,
                                    curX1 + curw / 2,   curY1);
                DrawArrow(gr,elpen1,curX1 + curw / 2,   curY1);

                //__________________________________________________________________
                // связи
                for (int numrel = 0; numrel < el.relationsto.Count; numrel++)
                {
                    Relation rel = el.relationsto[numrel];
                    Obj elto = objects[rel.to];
                    if (!el.fading)
                        objects[rel.to].fading = el.fading;
                    float shift = GetShift(count, numrel);
                    //Relation relto = el.relationsfrom[rel.to];

                    Color relcolor = rel.color;
                    if (el.fading)
                    {
                        relcolor = Color.FromArgb(Math.Max(model.timer / 4, 0), relcolor);
                    }

                    Pen curpen = new Pen(relcolor, bold);
                    // ссылка
                    gr.DrawRectangle(curpen, curX1 + curw * (numrel + 1), curY1, curw, curh);
                    DrawLinkDown(this, gr, curpen, rel, numrel, curX1 + curw * (numrel + 1), curY1, curw, curh, shift);
                }
            }
            curX1 = woffset + curw / 2 + objects.Count * curw * cobj;
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
