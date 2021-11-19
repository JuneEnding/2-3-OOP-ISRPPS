using System;
using System.Windows.Forms;

namespace lab_i_1_Facade
{
    public static class Facade
    {
        static SubsystemA a = new SubsystemA();
        static SubsystemB b = new SubsystemB();
        static SubsystemD d = new SubsystemD();
        public static string Operation1()
        {
            //MessageBox.Show(
            return "Operation 1\n" +
            a.A1() +
            a.A2() +
            b.B1();
        }
        public static string Operation2()
        {
            //MessageBox.Show(
            return "Operation 2\n" +
            b.B1() +
            d.D1();
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //Program.Operation1();
            //Program.Operation2();
            // Wait for user
            Console.Read();

        }
    }
}
