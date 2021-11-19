using System.Drawing;

namespace GrafObj
{
    public class Model
    {

        public DataElem[] dataelem = {
            new DataElem("d", Color.Red),
            new DataElem("e", Color.Orange),
            new DataElem("f", Color.Green),
            new DataElem("j", Color.Cyan),
            new DataElem("c", Color.DarkBlue),
            new DataElem("b", Color.DarkViolet),
            new DataElem("a", Color.Red)
            //,new DataElem("объекты", Color.Black)
        };
        // d <- b 
        // d передается в конструктор b в качестве параметра

        public int[,] adjmatrix = {
           //d e f j c b a
            {0,0,0,0,0,1,0}, //d
            {0,0,0,0,1,1,0}, //e
            {0,0,0,0,1,0,0}, //f
            {0,0,0,0,0,0,2}, //j
            {0,0,0,0,0,0,2}, //c
            {0,0,0,0,0,0,2}, //b
            {0,0,0,0,0,0,0}, //a
        };

        public class DataElem
        {
            public string name;
            public Color color;
            public DataElem (string name, Color color)
            {
                this.name = name;
                this.color = color;
            }
        }


    }// End class Model
}
