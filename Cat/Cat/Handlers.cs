using System.Drawing;
using System.Windows.Forms;

// ***************************************************************************
// В данной реализации есть возможность:
// 1. Использовать готовые методы - обработчики
// 2. Самому конструировать цепочки обработчиков, используя открытые классы обработчиков
// ***************************************************************************

namespace Cat
{
    public class Handlers{
        // Чем хорошо иметь объекты-обработчики?
        //      + Можно формировать цепочки запросов, работая не с классами, а с объектами
        //      + Можно вызывать цепочку обработчиков с любого звена
        //      - больше свободы для людей, использующих этод код => вероятность неправильного использования
        //        кода => ошибки 
        Handler h1 = new H1();
        Handler h2 = new H2();
        Handler h3 = new H3();
        Handler h4 = new H4();
        Handler h5 = new H5();
        Handler h6 = new H6();
        Handler h7 = new H7();
        Handler h8 = new H8();
        Handler h9 = new H9();
        Handler h10 = new H10();
        // С использованием методов эта инициализация бесполезна!!!
        public Handlers() {
            h1.nextHandler = h2;
            h2.nextHandler = h3;
            h3.nextHandler = h4;
            h4.nextHandler = h5;
            h5.nextHandler = h6;
            h6.nextHandler = h7;
            h7.nextHandler = h8;
            h8.nextHandler = h9;
            h9.nextHandler = h10;
            h10.nextHandler = h1;
        }

        public void Hand_all(int action, PictureBox pictureBox)
        {
            h1.HandleRequest(action, pictureBox);
        }

        public abstract class Handler{
            public int timepause { set; get; } = 63;
            public Handler nextHandler { set; get; } = null; // Самый последний объект цепочки
            public abstract void HandleRequest(int action, PictureBox pictureBox); // Обработчик запроса (параметр - номер вып. сцены) 
        }
        public class H1 : Handler {
            
            public override void HandleRequest(int action, PictureBox pictureBox) { // Замещаем? Реализуем/специфицируем метод
                if (action == 1) {                           // Проверка на возможность обработки данных
                    pictureBox.Image = Image.FromFile(System.IO.Path.GetFullPath(@"..\..\") + "cat\\1.jpg");
                    System.Threading.Thread.Sleep(timepause);
                }
                else if (nextHandler != null)
                { // Если след. об. есть, передаем ему данные
                    nextHandler.HandleRequest(action, pictureBox);
                }
            }
        } // end class H1
        public class H2 : Handler
        {

            public override void HandleRequest(int action, PictureBox pictureBox)
            { // Замещаем? Реализуем/специфицируем метод
                if (action == 2)
                {                           // Проверка на возможность обработки данных
                    pictureBox.Image = Image.FromFile(System.IO.Path.GetFullPath(@"..\..\") + "cat\\2.jpg");
                    System.Threading.Thread.Sleep(timepause);
                }
                else if (nextHandler != null)
                { // Если след. об. есть, передаем ему данные
                    nextHandler.HandleRequest(action, pictureBox);
                }
            }
        } // end class H2
        public class H3 : Handler
        {

            public override void HandleRequest(int action, PictureBox pictureBox)
            { // Замещаем? Реализуем/специфицируем метод
                if (action == 3)
                {                           // Проверка на возможность обработки данных
                    pictureBox.Image = Image.FromFile(System.IO.Path.GetFullPath(@"..\..\") + "cat\\3.jpg");
                    System.Threading.Thread.Sleep(timepause);
                }
                else if (nextHandler != null)
                { // Если след. об. есть, передаем ему данные
                    nextHandler.HandleRequest(action, pictureBox);
                }
            }
        } // end class H3
        public class H4 : Handler
        {

            public override void HandleRequest(int action, PictureBox pictureBox)
            { // Замещаем? Реализуем/специфицируем метод
                if (action == 4)
                {                           // Проверка на возможность обработки данных
                    pictureBox.Image = Image.FromFile(System.IO.Path.GetFullPath(@"..\..\") + "cat\\4.jpg");
                    System.Threading.Thread.Sleep(timepause);
                }
                else if (nextHandler != null)
                { // Если след. об. есть, передаем ему данные
                    nextHandler.HandleRequest(action, pictureBox);
                }
            }
        } // end class H4
        public class H5 : Handler
        {

            public override void HandleRequest(int action, PictureBox pictureBox)
            { // Замещаем? Реализуем/специфицируем метод
                if (action == 5)
                {                           // Проверка на возможность обработки данных
                    pictureBox.Image = Image.FromFile(System.IO.Path.GetFullPath(@"..\..\") + "cat\\5.jpg");
                    System.Threading.Thread.Sleep(timepause);
                }
                else if (nextHandler != null)
                { // Если след. об. есть, передаем ему данные
                    nextHandler.HandleRequest(action, pictureBox);
                }
            }
        } // end class H5
        public class H6 : Handler
        {

            public override void HandleRequest(int action, PictureBox pictureBox)
            { // Замещаем? Реализуем/специфицируем метод
                if (action == 6)
                {                           // Проверка на возможность обработки данных
                    pictureBox.Image = Image.FromFile(System.IO.Path.GetFullPath(@"..\..\") + "cat\\6.jpg");
                    System.Threading.Thread.Sleep(timepause);

                }
                else if (nextHandler != null)
                { // Если след. об. есть, передаем ему данные
                    nextHandler.HandleRequest(action, pictureBox);
                }
            }
        } // end class H6
        public class H7 : Handler
        {

            public override void HandleRequest(int action, PictureBox pictureBox)
            { // Замещаем? Реализуем/специфицируем метод
                if (action == 7)
                {                           // Проверка на возможность обработки данных
                    pictureBox.Image = Image.FromFile(System.IO.Path.GetFullPath(@"..\..\") + "cat\\7.jpg");
                    System.Threading.Thread.Sleep(timepause);
                }
                else if (nextHandler != null)
                { // Если след. об. есть, передаем ему данные
                    nextHandler.HandleRequest(action, pictureBox);
                }
            }
        } // end class H7
        public class H8 : Handler
        {

            public override void HandleRequest(int action, PictureBox pictureBox)
            { // Замещаем? Реализуем/специфицируем метод
                if (action == 8)
                {                           // Проверка на возможность обработки данных
                    pictureBox.Image = Image.FromFile(System.IO.Path.GetFullPath(@"..\..\") + "cat\\8.jpg");
                    System.Threading.Thread.Sleep(timepause);
                }
                else if (nextHandler != null)
                { // Если след. об. есть, передаем ему данные
                    nextHandler.HandleRequest(action, pictureBox);
                }
            }
        } // end class H8
        public class H9 : Handler
        {

            public override void HandleRequest(int action, PictureBox pictureBox)
            { // Замещаем? Реализуем/специфицируем метод
                if (action == 9)
                {                           // Проверка на возможность обработки данных
                    pictureBox.Image = Image.FromFile(System.IO.Path.GetFullPath(@"..\..\") + "cat\\9.jpg");
                    System.Threading.Thread.Sleep(timepause);
                }
                else if (nextHandler != null)
                { // Если след. об. есть, передаем ему данные
                    nextHandler.HandleRequest(action, pictureBox);
                }
            }
        } // end class H9
        public class H10 : Handler {
            public override void HandleRequest(int action, PictureBox pictureBox) {
                DialogResult result = MessageBox.Show("О нет! Ваша кошка сбежала! \n Сможете ли вы смириться с этим?", "Удручающая новость",
                                                     MessageBoxButtons.YesNoCancel,
                                                     MessageBoxIcon.Exclamation,
                                                     MessageBoxDefaultButton.Button2);
                switch (result)
                {
                    case DialogResult.Yes:
                        MessageBox.Show("Вы разочаровали кошку!", "Разочарование кошки");
                        MessageBox.Show("Она не будет по вам скучать.", "Разочарование кошки");
                        break;
                    case DialogResult.No:
                        MessageBox.Show("Кошка оставила вам письмо на прощание:", "Письмо от кошки");
                        MessageBox.Show($"Если любишь - \n\t\tотпусти, \n\t\t\t\tесли любит - \n\t\t\t\t\t\tвернется...", "Письмо от кошки");
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("Хорошая попытка!", "Философские размышления кошки");
                        MessageBox.Show("Но не пытайся склеить разбитую чашку...", "Философские размышления кошки");
                        MessageBox.Show("И догнать черную кошку.......", "Философские размышления кошки");
                        break;
                    default:
                        break;
                }
                Application.Exit();
            }
        } // end class H9


    }
}
