using GrafObj.View;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace GrafObj
{
    delegate void DelegateView(Graphics g);

    public partial class Form1 : Form
    {
        private Model model = new Model();
        private DelegateView _view = null;
        private Controller.ControllerDiagram graf1;
        private Controller.ControllerDiagram graf2;
        private ControllerEditor graf3;
        private Point clikpoint;
        private Boolean mousedown = false;
        private TypeAction typeaction = TypeAction.None;
        private enum TypeAction
        {
            None,
            ObjectMove,
            RelationMove
        }

        public Form1()
        {
            InitializeComponent();

            Editor();
            //model = LoadModel( System.IO.Path.GetFullPath(@"..\..\") + "default.fml"); // Дефолтная загрузка по умолчанию
            pictureObj.Invalidate();
        }

        public void ObjectCreationDiagram() { _view = tObjectCreationDiagram; }
        public void DiagramOfScope() { _view = tDiagramOfScope; }
        public void Editor() { _view = tEditor; }
        private void tObjectCreationDiagram(Graphics g)
        {
            graf1 = new ControllerCreateObject(model);
            graf1.clikpoint = clikpoint;
            graf1.scale = Convert.ToInt32(ScaleNnum.Value);
            graf1.Paint(g);
        }

        private void tDiagramOfScope(Graphics g)
        {
            graf2 = new ControllerDiagramOfScope(model);
            graf2.clikpoint = clikpoint;
            graf2.scale = Convert.ToInt32(ScaleNnum.Value);
            graf2.Paint(g);
            if (model.timer == 1000)
                timer100.Start();
        }

        private void tEditor(Graphics g)
        {
            graf3 = new ControllerEditor(model);
            graf3.clikpoint = clikpoint;
            graf3.scale = Convert.ToInt32(ScaleNnum.Value);
            graf3.Paint(g);
        }

        private void buttonPaint_Click(object sender, EventArgs e){
            //ScaleNnum.Value = graf1.scale;
            ObjectCreationDiagram();
            pictureObj.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ScaleNnum.Value = graf2.scale;
            DiagramOfScope();
            pictureObj.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ScaleNnum.Value = graf3.scale;
            Editor();
            pictureObj.Invalidate();
        }

        private void pictureObj_Paint(object sender, PaintEventArgs e)
        {
            _view?.Invoke(e.Graphics);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pictureObj.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.Xml.Serialization.XmlSerializer writerRw = new System.Xml.Serialization.XmlSerializer(typeof(Model));
                System.IO.StreamWriter fileRw = new System.IO.StreamWriter(saveFileDialog1.FileName);
                writerRw.Serialize(fileRw, model);
                fileRw.Close();
            }
        }

        private Model LoadModel(string FileName)
        {
            Model res = null;
            System.Xml.Serialization.XmlSerializer readerRr = new System.Xml.Serialization.XmlSerializer(typeof(Model)); //Получ. по модели формата загружаемого файла
            System.IO.StreamReader fileRr = new System.IO.StreamReader(FileName); // По этому формату грузим из файлнэйм
            res = (Model)readerRr.Deserialize(fileRr);// Десериализуем результат 
            fileRr.Close();
            return res;// И возвращаем его 
        }
        
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                model = LoadModel(openFileDialog1.FileName);
                pictureObj.Invalidate();
            }
        }

        private void newObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model.DataElem el = model.NewObject("Red");
            if (el != null)
            {
                el.location = clikpoint;
                pictureObj.Invalidate();
            }
            else
            {

            }
        }

        private void newRelationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //newrelation = true;
        }

        public Color ColorFromName(string color)
        {
            if (color == null)
                return Color.Black;
            Color col = Color.FromName(color);
            if (col.ToArgb() == 0)
                col = Color.FromArgb(int.Parse(color, NumberStyles.HexNumber));
            return col;
        }

        // Редактор объекта
        public void EditObject(Model.DataElem obj)
        {
            EditObject editObject = new EditObject();
            editObject.textBox.Text = obj.name;
            editObject.bColor.BackColor = ColorFromName(obj.color);
            if (editObject.ShowDialog(this) == DialogResult.OK)
            {
                obj.name = editObject.textBox.Text;
                obj.color = editObject.bColor.BackColor.Name;
            }
            editObject.Dispose();
        }

        private void pictureObj_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                model.DeselectObject();
                clikpoint = e.Location;
                pictureObj.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (typeaction != TypeAction.RelationMove)
                {
                    contextMenuStrip1.Show((Control)sender, e.Location);
                }
            }
        }


        private void pictureObj_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //newrelation = true;
            //if (typeaction == TypeAction.None)
            if (model.ObjectSelected())
            {
                // Запуск редактирования объекта
                EditObject(model.GetSelectedObject());
                pictureObj.Invalidate();
            }
        }

        private void pictureObj_MouseDown(object sender, MouseEventArgs e){
            if (e.Button == MouseButtons.Left)
            {
                mousedown = true;
                if ( model.ObjectSelected() )
                if ( model.PointInObject(e.Location, 40, 40) == model.GetSelectedObject() )
                {
                    typeaction = TypeAction.ObjectMove;
                    // передвигаем курсор в угол выбранного объекта
                    Cursor.Position = new Point(
                        Cursor.Position.X + model.GetSelectedObject().location.X - e.Location.X,
                        Cursor.Position.Y + model.GetSelectedObject().location.Y - e.Location.Y
                    );
                }
                else { if (model.PointInObject(e.Location, 40, 40) == null)
                    typeaction = TypeAction.RelationMove;
                }
            }
            Debug();
        }

        private void pictureObj_MouseUp(object sender, MouseEventArgs e){
            mousedown = false;
            typeaction = TypeAction.None;
            if (model.ObjectSelected())
            {
                //model.GetSelectedObject().location = e.Location;
                pictureObj.Invalidate();
            }
            Debug();
        }

        private void pictureObj_MouseMove(object sender, MouseEventArgs e){
            if (model.ObjectSelected())
            {
                if (typeaction == TypeAction.ObjectMove)
                {
                    clikpoint = e.Location;
                    model.GetSelectedObject().location = e.Location;
                    pictureObj.Invalidate();
                }
                if (typeaction == TypeAction.RelationMove)
                {
                    Model.DataElem elfrom = model.GetSelectedObject();
                    Model.DataElem elto = model.PointInObject(e.Location, 40, 40);
                    if (elto != null)
                        model.NewRelation(elfrom, elto);

                        //clikpoint = e.Location;
                        //model.GetSelectedObject()
                        pictureObj.Invalidate();
                }
            }
            Debug();
        }

        public void Debug(){
            lInfo.Text = "mousedown: " + mousedown
                        + "\ntypeaction: " + typeaction
                        + "\nObjectSelected: " + model.ObjectSelected() + " " + model.GetSelectedObject()?.name
                        + "\nXY: " + clikpoint.ToString()
                        + "\ntimer: " + model.timer.ToString();
            ;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            model.timer -= 100;
            Debug();
            pictureObj.Invalidate();
            if (model.timer < 0)
                timer100.Stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            model.selectrel = !model.selectrel;
        }
    }
}
