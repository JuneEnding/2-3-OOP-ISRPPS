using System;
//using System.;
using System.Windows.Forms;

namespace Lab_I_2
{
    static class Controller // �ontroller - ����� ���� ����������
    {
        public static bool fb = false; // ������ �� �� false
        public static Model model = new Model(); // � ������� ������� ����� �������������� ����� � ������������
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application ������������� ������ � �������� static ��� ���������� �����������
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form1 = new Form1();
            //Application.Run(new Form1());
            Application.Run(form1);
            /// �������
            if (fb)
            {
                int r = model.mdl();
                //form1.
                model.AccessCheck = r; 
                MessageBox.Show("Bye, dear user. . . Your code:" + model.AccessCheck); 
                
                //form1.label1.Text = "fb = true";
                //form2.ShowDialog(); ���������� ����� ��� ��������� ���������� ����
                //Application.Run(new Form2());
                // int r = Model.op(); // �� ������ ����� ������
                // form2.DungeK = model.op();
                //form2.ShowDialog(); // ��� - � ����� ������ - � ���, ������ �� ���������� � --������ ���
                //Application.Run(new Form2());
            }
        }
    }
}
