using System.Collections.Generic;
using System.Drawing;

namespace GrafObj {
    public class ControllerCreateObject {
        // список объектов
        public class Obj {
            public string name;
            public Color color;
            public List<Relation> relationsto = new List<Relation>();   // К кому ->  (исп для стрелок)
            public List<Relation> relationsfrom = new List<Relation>(); // от кого к нему -> (исп для смещений входящих стрелок)

            public Obj(string name, Color color)
            {
                this.name = name;
                this.color = color;
            }
        } // end class Obj

        // список отношений
        public class Relation {
            public int to;        // номер объекта, в который входим
            public Color color;
            public int direction; // направление (стрелка справа или слева)

            public Relation(int to, Color color, int direction){
                this.to = to;
                this.color = color;
                this.direction = direction;
            }
        } // end class relation

        // Графика
        public class Diagramm {
            List<Obj> objects = new List<Obj>();
        
            public  Point   clikpoint { get; set; } = new Point(0, 0);   // нажатие мыши
            public  int     scale { get; set; }  = 3;               // масштабирование (теоретически можно скалировать объект формы)
            const   int     wobj = 9;                               // ширина объекта
            const   int     hobj = 9;                               // высота объекта
            public  int     woffset { get; set; }                   // горизонтальное смещение (для отрисовки оси)
            public  int     hoffset { get; set; }                   // вертикальное смещение (для отрисовки оси)
            public  Font    font = new Font("Times New Roman", 10, FontStyle.Regular);

            // Конструктор - получение данных из модели
            //----------------------------------------------------------------------------
            public Diagramm(Model model) {
                foreach (var el in model.dataelem)
                    AddObj(el.name, el.color);              // создаем объекты элементов

                int maxi = model.adjmatrix.GetLength(0);    
                for (int i = 0 ; i < maxi; i++)             // i - строка
                    for (int j = 0; j < maxi; j++) {        // j - колонка
                        int jj = maxi - j - 1;              // перебор в обратном порядке (иначе пересеч. стрелок!)
                        if ( model.adjmatrix[i, jj] > 0) {
                            AddRelTo(model.dataelem[jj].name, model.dataelem[i].name, model.dataelem[i].color, model.adjmatrix[i, jj]);
                            AddRelFrom(model.dataelem[i].name, model.dataelem[jj].name);
                        }
                    }
            }

            // Операции
            //----------------------------------------------------------------------------
            // получить смещение для отношения
            public float GetShift(int i, int numRelation) {         // i -> j; -> - передача в КОНСТРУТОР
                Obj elfrom = objects[i];                            // получаем объект из списка, из которого рисуем ->
                Relation relfrom = elfrom.relationsto[numRelation]; // получаем отношение к объекту, к которому рисуем
                Obj elto = objects[relfrom.to];                     // получаем объект, к которому рисуем
                // в какую точку конструктора рисуем стрелку?
                int relto = elto.relationsfrom.FindIndex(x => x.to == i); // получаем номер стрелки, вход. в объект to
                            // [(количество ссылок в to)-(номер ссылки, нач. с 0)] / ((количество ссылок в to)+1)
                return (float)(elto.relationsfrom.Count - relto) / (elto.relationsfrom.Count + 1);
            }
            // доб. объект в лист 
            public void AddObj(string name) {
                objects.Add(new Obj(name, Color.Black));
            }
            public void AddObj(string name, Color color) {
                objects.Add(new Obj(name, color));
            }
            // Получить объект по имени
            public Obj GetObj(string name) {
                Obj tmp = objects.Find(x => x.name == name);
                return tmp;
            }
            public int GetInd(string name) {
                int tmp = objects.FindIndex(x => x.name == name); // ищем по имени, возвращаем индекс

                return tmp;
            }

            public string AddRelTo(string from, string to, Color color, int direction){
                //              № от кого  | получить объект к кому | цвет | направление
                return AddRelTo(this.GetInd(from), this.GetObj(to), color, direction); 
            }
            // В список ссылок to добавляем ссылку на объект
            public string AddRelTo(int from, Obj to, Color color, int direction){
                //
                to.relationsto.Add(new Relation(from, color, direction));
                return to.name + " <- " + from + " : " + color.Name;
            }
            // В список ссылок from добавляем ссылку на объект
            public string AddRelFrom(string from, string to, Color color, int direction){
                return AddRelFrom(this.GetInd(from), this.GetObj(to), color, direction);
            }
            public string AddRelFrom(string from, string to){
                return AddRelFrom(this.GetInd(from), this.GetObj(to), Color.Black, 0);
            }
            public string AddRelFrom(int from, Obj to, Color color, int direction) {
                to.relationsfrom.Add(new Relation(from, color, direction));
                return to.name + " <- " + from + " : " + color.Name;
            }

            // отрисовка с нижней стороны
                                      // Параметры отрисовки СТРЕЛОК и КРУЖОЧКОВ на Viev
            private void DrawLinkDown(Graphics gr, Pen pen, Relation rel, int numrel, float curX1, float curY1, float curw, float curh, float shift) {
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

                    gr.DrawLine(pen,woffset + curw / 2 + rel.to * curw * 2 + curw * shift,
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

                    gr.DrawLine(pen,woffset + curw + rel.to * curw * 2 - curw * shift + curw / 2,
                                    hoffset + rel.to * curh * 3 + curh * shift - curh * 0.7f,
                                    woffset + curw + rel.to * curw * 2 - curw * shift + curw / 2,
                                    hoffset + rel.to * curh * 3 + curh / 2);

                    DrawArrow(gr, pen, woffset + curw + rel.to * curw * 2 - curw * shift + curw / 2,
                                    hoffset + rel.to * curh * 3 + curh / 2);
                }
            }
            // отрисовка стрелки вниз
            private void DrawArrow(Graphics gr, Pen pen, float X, float Y) {
                gr.DrawLine(pen, X - pen.Width * 2, Y - pen.Width * 4, X, Y);
                gr.DrawLine(pen, X + pen.Width * 2, Y - pen.Width * 4, X, Y);
            }

            // отрисовка в объект gr
            public void Paint(Graphics gr) {
                // затирали графику, а надо было передавать графику события paint, возник.. при инвалидации
                // Graphics gr = picture.CreateGraphics(); 
                //font = new Font("Times New Roman", 4 * scale, FontStyle.Regular);
                // Смещение СК'
                woffset = 5 * scale; 
                hoffset = 8 * scale;
                int bold = scale / 4 + 1;
                Pen pen1 = new Pen(Color.Black, 1 * bold);
                Pen pen3 = new Pen(Color.Black, 3 * bold);

                // текущие координаты для объекта
                int curX1 = 0;
                int curY1 = 0;
                int curw = scale * wobj; // масштаб * стандартная ширина
                int curh = scale * hobj;

                // отрисовка угла координатной сетки
                // X + 1/2 ширины объекта
                gr.DrawLine(pen1, woffset, hoffset, woffset + curw / 2, hoffset);
                // Y + высота объекта
                gr.DrawLine(pen1, woffset, hoffset, woffset, hoffset + curh / 2);

                //цикл по объектам
                for (int count = 0; count < objects.Count; count++) {
                    Obj el = objects[count];

                    // коорд. верхнего угла объекта
                    curX1 = woffset + curw / 2 + count * curw * 2;
                    curY1 = hoffset + curh * 1 + count * curh * 3;

                    Pen elpen1 = new Pen(el.color, bold);
                    Pen elpen3 = new Pen(el.color, 3 * bold);
                    SolidBrush elBrush = new SolidBrush(el.color); // (fill) заливаем выбранную область

                    //__________________________________________________________________
                    // горизонтальная линия координатной сетки
                    gr.DrawLine(elpen1, curX1 - curw / 2,   hoffset, 
                                        // 3 с выпиранием вперед для продолжения оси(1.5 точная длина)
                                        curX1 + 1.5f * curw,   hoffset);
                    // засечка
                    gr.DrawLine(elpen3, curX1 + 1.5f * curw,0,
                                        curX1 + 1.5f * curw,hoffset + bold * 2);
                    // имя объекта
                    gr.DrawString(el.name, font, elBrush,   curX1 + 0.5f * curw, 0);

                    //__________________________________________________________________
                    // вертикальная линия координатной сетки
                    gr.DrawLine(elpen1, woffset,            curY1 - 0.5f * curh,
                                        woffset,            curY1 + 2.5f * curh);
                    // засечка
                    gr.DrawLine(elpen3, 0,                  curY1 - 0.5f * curh,
                                        woffset + 5,        curY1 - 0.5f * curh);
                    // номер события
                    gr.DrawString(count.ToString(), font, elBrush, 0, curY1 - curh - font.SizeInPoints);

                    //__________________________________________________________________
                    // объект
                    gr.FillRectangle(elBrush, curX1, curY1, curw, curh); // коорд. верх. лев. угла, заливка ВНУТРИ обл.
                    // Рамочка при нажатии
                    if ((curX1 < clikpoint.X && clikpoint.X < curX1 + curw) &&
                        (curY1 < clikpoint.Y && clikpoint.Y < curY1 + curh))
                        gr.DrawRectangle(pen1, curX1, curY1, curw, curh);

                    //__________________________________________________________________
                    // конструктор с небольшим сдвигом вниз на pen3.Width/2, ибо стрелочек не видо
                    gr.DrawLine(pen3,   curX1,              curY1 - 0.5f * curh + pen3.Width/2,
                                        curX1 + curw,       curY1 - 0.5f * curh + pen3.Width/2);
                    // стрелочка из конструктора
                    gr.DrawLine(pen1,   curX1 + curw / 2,   curY1 - 0.5f * curh,
                                        curX1 + curw / 2,   curY1);
                    DrawArrow(gr, pen1, curX1 + curw / 2,   curY1);

                    //__________________________________________________________________
                    // связи
                    for (int numrel = 0; numrel < el.relationsto.Count; numrel++) {
                        Relation rel = el.relationsto[numrel];
                        float shift = GetShift(count, numrel); // номер объекта, номер ссылки
                        Pen curpen = new Pen(rel.color, bold);
                        DrawLinkDown( gr, curpen, rel, numrel, curX1, curY1, curw, curh, shift);
                    }
                }
            } // end method Paint
        }  // end class Diagramm
    }
}
