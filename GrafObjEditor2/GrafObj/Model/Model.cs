using System;
using System.Drawing;

namespace GrafObj
{
    public class Model
    {

        public int countel = 0;
        private int curentel = -1;
        public class DataElem
        {
            public string name { get; set; }
            public string color { get; set; }
            public int direction { get; set; }
            public Point location { get; set; }

            public DataElem() { }
            public DataElem (string name, string color)
            {
                this.name = name;
                this.color = color;
            }
        }
        public DataElem NewObject(string color)
        {
            if (countel < 10)
            {
                int ind = countel++;
                DataElem el = new DataElem(Convert.ToString(Convert.ToChar(ind + 97)), color);
                adjmatrix[ind * 11] = el;
                return el;
            }

            return null;
        }
        public DataElem NewRelation(DataElem from, DataElem to)
        {

            int ind = GetIndexObject(from) * 10 / 11 + GetIndexObject(to) / 11;
            if (adjmatrix[ind] == null)
            {
                DataElem el = new DataElem();
                el.direction = 1;
                adjmatrix[ind] = el;
            }

            return (DataElem)adjmatrix[ind];
        }

        public DataElem[] adjmatrix = new DataElem[100];

        //________________________________________________________________________



        public int GetIndexObject(DataElem obj)
        {
            if (obj != null)
                for (int i = 0; i < countel; i++)
                for (int j = 0; j < countel; j++) { 
                    int ind = i * 10 + j;
                    if (adjmatrix[ind] == obj)
                        return ind;
                }
            return -1;
        }

        public DataElem PointInObject(Point clikpoint, int curw, int curh)
        {
            for (int count = 0; count < countel; count++) {
                int ind = count * 11;
                int curX1 = adjmatrix[ind].location.X;
                int curY1 = adjmatrix[ind].location.Y;
                if ((curX1 <= clikpoint.X && clikpoint.X <= curX1 + curw) &&
                    (curY1 <= clikpoint.Y && clikpoint.Y <= curY1 + curh)
                    )
                    return adjmatrix[ind];
            }
            return null;
        }

        // p1 и p2 - прямая, p проверяемая точка
        // во первых - точка внутри прямоугольника
        // во вторых - ширина или высота прямоугольника может быть не большой (деление на 0)
        // и уже после, в третих - на линии
        public float PointInLine(Point p1, Point p2, Point p)
        {
            int e = 10;
            float t;
            if ((Math.Min(p1.X, p2.X)- e <= p.X && p.X <= Math.Max(p1.X, p2.X)+ e) &&
                (Math.Min(p1.Y, p2.Y)- e <= p.Y && p.Y <= Math.Max(p1.Y, p2.Y)+ e)
                ){
                if ((-e <= (p2.X - p1.X) && (p2.X - p1.X) <= e) ||
                    (-e <= (p2.Y - p1.Y) && (p2.Y - p1.Y) <= e)){
                    return 0;
                }
                t = (float)(p.Y - p1.Y) / (float)(p2.Y - p1.Y) - (float)(p.X - p1.X) / (float)(p2.X - p1.X);
                return t;
            }

            return -1;
        }

        public DataElem PointInRelated(Point clikpoint, int curw, int curh)
        {
            for (int i = 0; i < countel; i++)
            for (int j = 0; j < countel; j++)
            {
                int ind = i * 10 + j;
                if (adjmatrix[ind] != null && adjmatrix[ind].direction > 0)
                    {
                    float delta = PointInLine(
                        new Point(adjmatrix[i * 11].location.X + curw, adjmatrix[i * 11].location.Y + curh),
                        adjmatrix[j * 11].location,
                        clikpoint
                        );
                    if (-0.3 < delta && delta < 0.3)
                        return adjmatrix[ind];
                }
            }
            return null;
        }

        public bool ObjectSelected(){
            return curentel >= 0;
        }

        public DataElem GetSelectedObject(){
            if (ObjectSelected())
                return adjmatrix[curentel];
            else
                return null;
        }
        public void SetSelectedObject(int ind){
            curentel = ind;
        }
        public void DeselectObject(){
            curentel = -1;
        }
    }// End class Model
}
