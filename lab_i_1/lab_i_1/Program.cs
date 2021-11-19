using System;

namespace lab_i_1
{
    public class Data //вложенный класс
    {
        int day;
        int month;
        int year; //защищённый атрибут
        int NowYear; 
        public Data(){
            day     = 0;
            month   = 0;
            year    = 0;
            NowYear = 2020;
        }
        public int DayA { //атрибут доступа
            set {
                if (value < 1 || value > 31) {
                    throw new ArgumentOutOfRangeException("День должен быть в диапазоне от 1 до 32");
                } else {
                    day = value; //value - значение, которое стоит в операторе присваивания
                }
            }
            get { return day; }
        }
        public int MonthA
        { //атрибут доступа
            set {
                if (value < 1 || value > 12) {
                    throw new ArgumentOutOfRangeException("Месяц должен быть в диапазоне от 1 до 12");
                } else {
                    month = value; //value - значение, которое стоит в операторе присваивания
                }
            }
            get { return month; }
        }
        public int YearA { //атрибут доступа
            set {
                // throw cообщает о возникновении исключения во время выполнения программы
                // ArgumentOutOfRangeException - класс, Исключение, которое выдается, если значение аргумента 
                // не соответствует допустимому диапазону значений, установленному вызванным методом.
                if (value < 1 || value > NowYear) {
                    throw new ArgumentOutOfRangeException($"Год должен быть в диапазоне от 1 до {NowYear}");
                } else {
                    year = value; //value - значение, которое стоит в операторе присваивания
                }
            }
            get { return year; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data();
            string inform = "";
            // Console - класс, предоставляет стандартные потоки для консольных приложений: входной, выходной и поток сообщений об ошибках
            Console.WriteLine("введите значение типа float"); // Считывает следующую строку символов из стандартного входного потока
            inform = Console.ReadLine();
            // Replace - возвращает новую строку, в которой все вхождения заданного знака Юникода в текущем экземпляре заменены другим заданным знаком Юникода.
            // Single.Parse - преобразует строковое представление числа в эквивалентное ему число одиночной точности с плавающей запятой, корректно парсит числа, разделенные символом ","
            // var - неявно типизированные локальные переменные является строго типизированными, как если бы вы объявили тип самостоятельно, однако его определяет компилятор
            var p = Single.Parse(inform.Replace('.', ',')); //!! (char oldChar, char newChar)
            Console.WriteLine("Значение равно: {0:F6}", p); //(F) Fixed point:
            Console.WriteLine("");
            Console.WriteLine("Byte.MaxValue - поле, равное {0}", byte.MaxValue); //MaxValue - константа для удобства программирования, byte - целочистленный тип без знака с макс. знач. 255
            float[] ms = { 1, 2, 3, 4, 67.78F, 89};
            Console.WriteLine("\nМассив:");
            foreach (var f in ms)
                Console.WriteLine("{0} ", f);

            Console.WriteLine("\nКласс Data");
            for (; ;)
            {
                // Оператор try-catch состоит из блока try, за которым следует одно или 
                // несколько предложений catch, задающих обработчики для различных исключений.
                // При возникновении исключения общеязыковая среда выполнения (CLR) ищет оператор catch, 
                // который обрабатывает это исключение.
                // Блок try содержит защищенный код, который может вызвать исключение.
                try
                {
                    Console.WriteLine("Введите день");
                    inform = Console.ReadLine();
                    data.DayA = Int32.Parse(inform); //Int32 - 32-разрядное целое число
                    Console.WriteLine("Введите месяц");
                    inform = Console.ReadLine();
                    data.MonthA = Int32.Parse(inform); 
                    Console.WriteLine("Введите год");
                    inform = Console.ReadLine();
                    data.YearA = Int32.Parse(inform);
                    break;
                }
                catch (ArgumentOutOfRangeException exc)
                {
                    Console.WriteLine(exc.Message);
                }
                catch (IndexOutOfRangeException exc)
                {
                    Console.WriteLine(exc.Message);
                }
                catch (FormatException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }

            Console.WriteLine("День:    " + data.DayA);
            Console.WriteLine("Месяц:   " + data.MonthA);
            Console.WriteLine("Год:     " + data.YearA);

            Console.ReadKey();
        }
    }
}
