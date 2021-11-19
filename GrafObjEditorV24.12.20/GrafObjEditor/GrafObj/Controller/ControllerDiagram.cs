using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace GrafObj.Controller
{
    public abstract class ControllerDiagram
    {

        public Model model;

        // список объектов
        public class Obj
        {
            public string name;
            public Color color;
            public bool fading;     // признак затухания цвета
            public int ind;         // индекс объекта в модели
            public List<Relation> relationsto = new List<Relation>();
            public List<Relation> relationsfrom = new List<Relation>();

            public Obj(string name, Color color, int ind)
            {
                this.name = name;
                this.color = color;
                this.ind = ind;
                this.fading = false;
            }
            public Obj(string name, Color color, int ind, bool fading)
            {
                this.name = name;
                this.color = color;
                this.ind = ind;
                this.fading = fading;
            }
        }

        // список отношений
        public class Relation
        {
            public int to;
            public Color color;
            public int direction;

            public Relation(int to, Color color, int direction)
            {
                this.to = to;
                this.color = color;
                this.direction = direction;
            }
        }

        public List<Obj> objects = new List<Obj>();

        public Point clikpoint { get; set; } = new Point(0, 0);   // нажатие мыши
        public int scale { get; set; } = 4;             // масштабирование (теоретически можно скалировать объект формы)
        public const int wobj = 9;                      // ширина объекта
        public const int hobj = 9;                      // высота объекта
        public int woffset { get; set; }                // горизонтальное смещение (для отрисовки оси)
        public int hoffset { get; set; }                // вертикальное смещение (для отрисовки оси)
        public Font font;
        public virtual Color ColorFromName(string color)
        {
            Color col = Color.FromName(color);
            if (col.ToArgb() == 0)
                col = Color.FromArgb(int.Parse(color, NumberStyles.HexNumber));
            return col;
        }

        // Конструктор
        //----------------------------------------------------------------------------
        public ControllerDiagram(){ }
        public ControllerDiagram(Model model)
        {
            this.model = model;
            for (int i = 0; i < model.countel; i++)
            {
                Model.DataElem el = model.adjmatrix[i * 11];
                AddObj(el.name, el.color, i * 11);
            }

            int maxi = model.countel;
            for (int i = 0; i < maxi; i++)
                for (int j = 0; j < maxi; j++)
                {
                    int jj = maxi - j - 1;
                    if (model.adjmatrix[i * 10 + jj] != null)
                        if (model.adjmatrix[i * 10 + jj].direction > 0)
                        {
                            Model.DataElem eli = model.adjmatrix[i * 11];
                            Model.DataElem elj = model.adjmatrix[jj * 11];

                            AddRelTo(elj.name, eli.name, ColorFromName(eli.color), model.adjmatrix[i * 10 + jj].direction);
                            AddRelFrom(eli.name, elj.name, ColorFromName(eli.color), 0);
                        }
                }
        }

        // Операции
        //----------------------------------------------------------------------------
        // получить смещение для отношения
        public virtual float GetShift(int i, int j)
        {
            Obj elfrom = objects[i];
            Relation relfrom = elfrom.relationsto[j];
            Obj elto = objects[relfrom.to];
            int relto = elto.relationsfrom.FindIndex(x => x.to == i);
            return (float)(elto.relationsfrom.Count - relto) / (elto.relationsfrom.Count + 1);
        }

        public virtual void AddObj(string name, string color, int ind)
        {
            objects.Add(new Obj(name, ColorFromName(color), ind));
        }
        public virtual void AddObj(string name, string color, int ind, bool fading)
        {
            objects.Add(new Obj(name, ColorFromName(color), ind, fading));
        }
        public virtual Obj GetObj(string name)
        {
            Obj tmp = objects.Find(x => x.name == name);
            return tmp;
        }
        public virtual int GetInd(string name)
        {
            int tmp = objects.FindIndex(x => x.name == name);
            return tmp;
        }
        public virtual string AddRelTo(string from, string to, Color color, int direction)
        {
            return AddRelTo(this.GetInd(from), this.GetObj(to), color, direction);
        }
        public virtual string AddRelTo(int from, Obj to, Color color, int direction)
        {
            to.relationsto.Add(new Relation(from, color, direction));
            return to.name + " <- " + from + " : " + color.Name;
        }
        public virtual string AddRelFrom(string from, string to, Color color, int direction)
        {
            return AddRelFrom(this.GetInd(from), this.GetObj(to), color, direction);
        }
        public virtual string AddRelFrom(int from, Obj to, Color color, int direction)
        {
            to.relationsfrom.Add(new Relation(from, color, direction));
            return to.name + " <- " + from + " : " + color.Name;
        }

        // отрисовка с нижней стороны
        public virtual void DrawLinkDown(ControllerDiagram diagramm, Graphics gr, Pen pen, Relation rel, int numrel, float curX1, float curY1, float curw, float curh, float shift)
        {
            gr.FillEllipse(new SolidBrush(Color.Black), curX1 + curw / 4 * 1,
                                curY1 + curw / 4 * 1,
                                curw / 2,
                                curw / 2);

            if (rel.direction == 1)
            {
                gr.DrawLine(pen, curX1 + curw / 2,
                                curY1 + curh,
                                curX1 + curw / 2,
                                curY1 + curh + curh / 2);

                gr.DrawLine(pen, curX1 + curw / 2,
                                curY1 + curh + curh / 2,
                                woffset + curw / 2 + rel.to * curw * 2 + curw * shift,
                                curY1 + curh + curh / 2);

                gr.DrawLine(pen, woffset + curw / 2 + rel.to * curw * 2 + curw * shift,
                                curY1 + curh + curh / 2,
                                woffset + curw / 2 + rel.to * curw * 2 + curw * shift,
                                hoffset + rel.to * curh * 3 + curh / 2);

                DrawArrow(gr, pen, woffset + curw / 2 + rel.to * curw * 2 + curw * shift,
                                hoffset + rel.to * curh * 3 + curh / 2);
            }
            else
            {
                gr.DrawLine(pen, curX1 + curw / 2,
                                curY1 + curh,
                                curX1 + curw / 2,
                                hoffset + rel.to * curh * 3 + curh * shift - curh * 0.7f);

                gr.DrawLine(pen, curX1 + curw / 2,
                                hoffset + rel.to * curh * 3 + curh * shift - curh * 0.7f,
                                woffset + curw + rel.to * curw * 2 - curw * shift + curw / 2,
                                hoffset + rel.to * curh * 3 + curh * shift - curh * 0.7f);

                gr.DrawLine(pen, woffset + curw + rel.to * curw * 2 - curw * shift + curw / 2,
                                hoffset + rel.to * curh * 3 + curh * shift - curh * 0.7f,
                                woffset + curw + rel.to * curw * 2 - curw * shift + curw / 2,
                                hoffset + rel.to * curh * 3 + curh / 2);

                DrawArrow(gr, pen, woffset + curw + rel.to * curw * 2 - curw * shift + curw / 2,
                                hoffset + rel.to * curh * 3 + curh / 2);
            }
        }
        // отрисовка стрелки вниз
        public virtual void DrawArrow(Graphics gr, Pen pen, float X, float Y)
        {
            gr.DrawLine(pen, X - pen.Width * 2, Y - pen.Width * 4, X, Y);
            gr.DrawLine(pen, X + pen.Width * 2, Y - pen.Width * 4, X, Y);
        }
        // отрисовка в gr
        public abstract void Paint(Graphics gr);

    }
}
