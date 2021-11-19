using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GrafObj.Controller
{
    public class ControllerDiagram
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
    }
}
