using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab_i_12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Обработчики событий заданы в дизайнере!!!
            //this.button1.MouseDown += new MouseEventHandler(this.button1_MouseDown);// Созд. метод, обраб. MouseMove, MouseDown или MouseUp
            // 1 обработчик для каждой панели
            //splitContainer1.Panel1.DragEnter += new DragEventHandler(this.Panels_DragEntre);
            //splitContainer1.Panel1.DragDrop += new DragEventHandler(this.Panels_DragDrop);
            //splitContainer1.Panel2.DragEnter += new DragEventHandler(this.Panels_DragEntre);
            //splitContainer1.Panel2.DragDrop += new DragEventHandler(this.Panels_DragDrop);
        }
        //Зачем? Работает и так
        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            this.button1.MouseDown += new MouseEventHandler(this.button1_MouseMove);//запуск события MouseDown-тащим
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            // при нажатии на кнопку вызывается перетаскивание указанного объекта
            this.button1.DoDragDrop(button1, DragDropEffects.Move); // метод, начинает операцию перетаскивания. 
        }
        // Данный обработчик представляет собой более сложную версию, общую для всех панелей и кнопок 
        /*private void Panels_DragDrop(object sender, DragEventArgs e)
         {

             Object item = (object)e.Data.GetData(typeof(Button)); // Проверяем тип перетаскиваемого объекта
             if (item!=null) { 
             // в поле data - ссылка на объект от которого выполняется вызов DoDradDrop
             // приведем тип к Button и установим поле parent (Родительский элемент кнопки)
             // ссфлка на который находится в sender (т.к. это обработчик события DragEntre)

             ((Button)e.Data.GetData(typeof(Button))).Parent = (Panel)sender;//проверка на кнопку

             // Метод проведения экранных координат к клиенским
             // в зависимрсти от сендера меняем координаты кнопки
             // (e.X, e.Y) - относительно экрана (события), PointToClient - относительно панели (клиенская область панели)
             if (sender == this.splitContainer1.Panel1)
                 ((Button)e.Data.GetData(typeof(Button))).Location = splitContainer1.Panel1.PointToClient(new Point (e.X, e.Y)); // на панеле находим место, где находится точка (e.X, e.Y)
             if (sender == this.splitContainer1.Panel2)
                 ((Button)e.Data.GetData(typeof(Button))).Location = splitContainer1.Panel2.PointToClient(new Point (e.X, e.Y));
             }
         }*/

        private void Panels_DragEntre(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Pane1_DragDrop(object sender, DragEventArgs e)
        {
            this.button1.Parent = (Panel)sender;
            this.button1.Location = splitContainer1.Panel1.PointToClient(new Point (e.X, e.Y));

        }
        private void Pane2_DragDrop(object sender, DragEventArgs e)
        {
            this.button1.Parent = (Panel)sender;
            this.button1.Location = splitContainer1.Panel2.PointToClient(new Point(e.X, e.Y));

        }

        private void splitContainer1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move; 
        }

        private void splitContainer1_Panel1_DragOver(object sender, DragEventArgs e)
        {
            //MessageBox.Show("DragOver");
        }

        private void checkBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.checkBox1.DoDragDrop(checkBox1, DragDropEffects.Move);
        }

    }
}
