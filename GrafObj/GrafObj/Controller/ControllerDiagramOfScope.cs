using System.Collections.Generic;
using System.Drawing;

namespace GrafObj
{
    public class ControllerDiagramOfScope
    {

        // список объектов
        public class Obj
        {
            public string name;
            public Color color;
            public List<Relation> relationsto = new List<Relation>();
            public List<Relation> relationsfrom = new List<Relation>();

            public Obj(string name, Color color)
            {
                this.name = name;
                this.color = color;
            }
        }

        // список отношений
        public class Relation
        {
            // Доработка: создать класс отношений "to/from"
            public int to;    // номер объекта, из которого выходим
            public Color color;
            public int direction;

            public Relation(int to, Color color, int direction)
            {
                this.to = to;
                this.color = color;
                this.direction = direction;
            }
        }
        // ДОРАБОТАТЬ : вынести дублированные данные в отдельный класс
        public class Diagramm {
            List<Obj> objects = new List<Obj>();

            public  Point   clikpoint { get; set; }      // нажатая кнопка
            public  int     scale { get; set; }  = 3;    // масштабирование (теоретически можно скалировать объект формы)
            const   int     wobj = 9;                    // ширина объекта
            const   int     hobj = 9;                    // высота объекта
            public  int     cobj { get; set; } = 4;      // количество ссылок на агрегированные объекты
            public  int     woffset { get; set; }        // горизонтальное смещение (для отрисовки оси)
            public  int     hoffset { get; set; }        // вертикальное смещение (для отрисовки оси)
            public  Font    font = new Font("Times New Roman", 10, FontStyle.Regular);

            // Конструктор
            //----------------------------------------------------------------------------
            public Diagramm(Model model)
            {
                for (int i = model.dataelem.Length; i > 0; i--)
                {
                    Model.DataElem el = model.dataelem[i-1];
                    AddObj(el.name, el.color);
                }
                int maxi = model.adjmatrix.GetLength(0);
                for (int j = 0; j < maxi; j++)
                    for (int i = 0 ; i < maxi; i++)
                    {
                        int jj = maxi - j - 1;
                        if ( model.adjmatrix[i, jj] > 0)
                        {
                            AddRelFrom(model.dataelem[jj].name, model.dataelem[i].name, model.dataelem[i].color, 0);
                            AddRelTo(model.dataelem[i].name, model.dataelem[jj].name, model.dataelem[i].color, 1);
                        }
                    }
            }

            // Операции
            //----------------------------------------------------------------------------
            // получить смещение для отношения
            public float GetShift(int i, int j)
            {
                Obj elfrom = objects[i];
                Relation relfrom = elfrom.relationsto[j];
                Obj elto = objects[relfrom.to];
                int relto = elto.relationsfrom.FindIndex(x => x.to == i);
                return (float)(elto.relationsfrom.Count - relto) / (elto.relationsfrom.Count + 1);
            }
            public void AddObj(string name)
            {
                objects.Add(new Obj(name, Color.Black));
            }
            public void AddObj(string name, Color color)
            {
                objects.Add(new Obj(name, color));
            }
            public Obj GetObj(string name)
            {
                Obj tmp = objects.Find(x => x.name == name);
                return tmp;
            }
            public int GetInd(string name)
            {
                int tmp = objects.FindIndex(x => x.name == name);
                return tmp;
            }
            public string AddRelTo(string from, string to, Color color, int direction)
            {
                return AddRelTo(this.GetInd(from), this.GetObj(to), color, direction);
            }
            public string AddRelTo(int from, Obj to, Color color, int direction)
            {
                to.relationsto.Add(new Relation(from, color, direction));
                return to.name + " <- " + from + " : " + color.Name;
            }
            public string AddRelFrom(string from, string to, Color color, int direction)
            {
                return AddRelFrom(this.GetInd(from), this.GetObj(to), color, direction);
            }
            public string AddRelFrom(int from, Obj to, Color color, int direction)
            {
                to.relationsfrom.Add(new Relation(from, color, direction));
                return to.name + " <- " + from + " : " + color.Name;
            }

            // отрисовка с нижней стороны
            private void DrawLinkDown(Diagramm diagramm, Graphics gr, Pen pen, Relation rel, int numrel, float curX1, float curY1, float curw, float curh, float shift) {
                // кусочек стрелочки внутри объекта
                gr.DrawLine(pen,curX1 + curw / 2,
                                curY1 + curh / 2,
                                curX1 + curw / 2,
                                hoffset + rel.to * curh * 3 + curh * shift - curh * 0.7f);
                // Рис. маленьких кружочков
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
                //font = new Font("Times New Roman", 4 * scale, FontStyle.Regular);
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

                    curX1 = woffset + curw / 2 + count * curw * cobj;
                    curY1 = hoffset + curh * 1 + count * curh * 3;

                    Pen elpen1 = new Pen(el.color, bold);
                    Pen elpen3 = new Pen(el.color, 3 * bold);
                    SolidBrush elBrush = new SolidBrush(el.color);

                    //__________________________________________________________________
                    // горизонтальная линия координатной сетки
                    gr.DrawLine(elpen1, curX1 - curw / 2,   hoffset, 
                                        // 3 с выпиранием вперед для продолжения оси(1.5 точная длина)
                                        curX1 + curw * (cobj - 0.5f),   hoffset);
                    // засечка
                    gr.DrawLine(elpen3, curX1 + (curw * cobj) - curw / 2, 0,
                                        curX1 + (curw * cobj) - curw / 2, hoffset + bold * 2);
                    // имя объекта
                    gr.DrawString(el.name, font, elBrush,   curX1 + 0.2f * curw, 0);

                    //__________________________________________________________________
                    // вертикальная линия координатной сетки
                    gr.DrawLine(elpen1, woffset,            curY1 - 0.5f * curh,
                                        woffset,            curY1 + 2.5f * curh);
                    // засечка
                    gr.DrawLine(elpen3, 0,                  curY1 - 0.5f * curh,
                                        woffset + 5,        curY1 - 0.5f * curh);
                    // номер события (здесь он не нужен)
                    //gr.DrawString(count.ToString(), font, elBrush, 0, curY1 - curh - font.SizeInPoints);

                    //__________________________________________________________________
                    // объект
                    //gr.DrawRectangle( 
                    gr.FillRectangle(elBrush, curX1, curY1, curw, curh);
                    if ((curX1 < clikpoint.X && clikpoint.X < curX1 + curw) &&
                        (curY1 < clikpoint.Y && clikpoint.Y < curY1 + curh))
                        gr.DrawRectangle(pen3, curX1, curY1, curw, curh);

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
                        float shift = GetShift(count, numrel);
                        //Relation relto = el.relationsfrom[rel.to];
                        Pen curpen = new Pen(rel.color, bold);
                        // контур объекта
                        gr.DrawRectangle(curpen, curX1 + curw * (numrel + 1), curY1, curw, curh);
                        // ссылка, та же, только x + curw * (numrel + 1)
                        DrawLinkDown(this, gr, curpen, rel, numrel, curX1 + curw * (numrel + 1), curY1, curw, curh, shift);
                    }
                }
            }
        }
    }
}
