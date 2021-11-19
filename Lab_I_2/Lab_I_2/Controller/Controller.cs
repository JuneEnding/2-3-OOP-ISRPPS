using System;
//using System.;
using System.Windows.Forms;

namespace Lab_I_2
{
    static class Controller // сontroller - через него управление
    {
        public static bool fb = false; // сделай ка ты false
        public static Model model = new Model(); // С помощью объекта будет осуществляться связь с контроллером
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application предоставляет методы и свойства static для управления приложением
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form1 = new Form1();
            //Application.Run(new Form1());
            Application.Run(form1);
            /// Условия
            if (fb)
            {
                int r = model.mdl();
                //form1.
                model.AccessCheck = r; 
                MessageBox.Show("Bye, dear user. . . Your code:" + model.AccessCheck); 
                
                //form1.label1.Text = "fb = true";
                //form2.ShowDialog(); Отображает форму как модальное диалоговое окно
                //Application.Run(new Form2());
                // int r = Model.op(); // из модели получ данные
                // form2.DungeK = model.op();
                //form2.ShowDialog(); // ран - в новом потоке - а так, данные не передаются и --просто зап
                //Application.Run(new Form2());
            }
        }
    }
}
