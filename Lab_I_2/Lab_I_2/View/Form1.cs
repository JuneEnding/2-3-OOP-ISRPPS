using System;
using System.Windows.Forms;
// тк Model в пространстве имен лабы, не нужно подключать

namespace Lab_I_2
{
    public partial class Form1 : Form
    {
        Model model = new Model();
        public Form1() // Конструктор
        {
            InitializeComponent(); // Метод, который инициализирует все компоненты, расположенные на форме
            Controller.fb = false;
            MessageBox.Show("Hello, dear user!\nThe form was created successfully!"); // MessageBox - класс, отображает окно сообщения (диалоговое окно) с текстом для пользователя
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You have enabled the polite end of the program!\nEnjoy your use!");
            Controller.fb = true;
            model.op(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 2");
            MessageBox.Show("" + model.mdl());
            // Получение данных из модели и вывод ее на форму
            this.label1.Text = model.mdl().ToString(); 
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the lable 1");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
