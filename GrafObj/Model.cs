using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GrafObj
{
    public class Model
    {

        DataElem[] dataelem =
        {
            new DataElem("a", Color.Red),
            new DataElem("b", Color.Orange),
            new DataElem("c", Color.Green),
            new DataElem("d", Color.Blue),
            new DataElem("e", Color.Violet),
            new DataElem("f", Color.Magenta),
            new DataElem("j", Color.DarkBlue)
        };

        int[,] adjmatrix = {
           //a b c d e f j
            {0,1,1,0,0,0,1}, //a
            {0,0,0,1,1,0,0}, //b
            {0,0,0,0,1,1,0}, //c
            {0,0,0,0,0,0,0}, //d
            {0,0,0,0,0,0,0}, //e
            {0,0,0,0,0,0,0}, //f
            {0,0,0,0,0,0,0}, //j
        };

        public enum TypeValueAdj
        {
            Null,
            AggregationByLink,
            AggregationByValue,
            AggregationByAttachment
            //... and so on
        }

        class DataElem
        {
            string name;
            Color color;
            public DataElem (string name, Color color)
            {
                this.name = name;
                this.color = color;
            }
        }

        public class Diagramm
        {
            //----------------------------------------------------------------------------
            List<Obj> objects = new List<Obj>();
            //List<GrafObj> grafObjs = new List<GrafObj>();
            //Point point
            const int scale = 4;
            const int wobj = 10;
            const int hobj = 10;
            const int woffset = 20;
            const int hoffset = 20;

            // Конструктор
            //----------------------------------------------------------------------------
            public Diagramm()
            {

            }

            // Операции
            //----------------------------------------------------------------------------
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
            public string AddRel(string from, string to, Color color)
            {
                return AddRel(this.GetInd(from), this.GetObj(to), color);
            }
            public string AddRel(int from, Obj to, Color color)
            {
                to.relations.Add(new Relation(from, color));
                return to.name + " <- " + from + " : " + color.Name;
            }

            public void Paint(PictureBox picture)
            {
                Graphics gr = picture.CreateGraphics();

                int curX1 = 0;
                int curY1 = 0;
                int curw = scale * wobj;
                int curh = scale * hobj;
                gr.DrawLine(new Pen(Color.Black, 1), hoffset, woffset, hoffset + 1.5f * curw, woffset);
                gr.DrawLine(new Pen(Color.Black, 1), hoffset, woffset, hoffset, woffset + 2.0f * curh);

                for (int count = 0; count < objects.Count;)
                {
                    Obj el = objects[count];

                    curX1 = woffset + curw + count * curw * 2;
                    curY1 = hoffset * 2 + curh + count * curh * 3;
                    // горизонтальная линия
                    gr.DrawLine(new Pen(el.color, 1), curX1 - 0.5f * curw, woffset,
                                                      curX1 + 1.5f * curw, woffset);
                    gr.DrawLine(new Pen(el.color, 1), curX1 + 1.5f * curw, woffset + 5,
                                                      curX1 + 1.5f * curw, woffset - 5);
                    // вертикальная линия
                    gr.DrawLine(new Pen(el.color, 1), hoffset, curY1 - 1.0f * curh,
                                                      hoffset, curY1 + 2.0f * curh);
                    gr.DrawLine(new Pen(el.color, 1), hoffset - 5, curY1 - 1.0f * curh,
                                                      hoffset + 5, curY1 - 1.0f * curh);
                    //gr.DrawRectangle( 
                    gr.FillRectangle(
                        new SolidBrush(el.color),
                        new Rectangle(curX1, curY1, curw, curh));

                    for (int numrel = 0; numrel < el.relations.Count;)
                    {
                        Relation rel = el.relations[numrel];
                        gr.DrawLine(new Pen(rel.color), curX1 + curw,
                                                        curY1 + numrel * 5,
                                                        woffset + curw + rel.to * curw * 2,
                                                        curY1 + numrel * 5);
                        //hoffset * 2 + curh + rel.to * curh * 3);

                        numrel++;
                    }
                    count++;
                }



            }

        }

    }// End class Model
}
