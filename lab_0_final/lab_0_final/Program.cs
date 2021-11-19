using System;

namespace lab_0_final.cs
{
    class Registry
    {
        // Атрибуты(поля) класса. По умолчанию атрибутам ставится мод.доступа private.
        //----------------------------------------------------------------------------
        string  name { get; set; } // Автоматически задает свойства атрибута
        int     num;
        int     old;
        int     height;

        // Конструкторы
        //----------------------------------------------------------------------------
        public Registry() { Console.WriteLine("Конструктор без параметров сработал"); }

        public Registry(string name, int num, int old, int height) // "Главный" констр.
        {
            this.name = name;
            this.num = num;
            this.old = old;
            this.height = height;
            Console.WriteLine("Конструктор с 4 параметрами сработал");
        }
        public Registry(string name) : this(name, 0, 0, 0) { } // Перенаправ. в гл. констр.

        // Операторы
        //----------------------------------------------------------------------------
        public void print()
        {
            Console.WriteLine($"Имя: {this.name}\tНомер: {this.num}\tВозраст: {this.old}\tРост: {this.height}");
        }
        public void print(int param)
        {
            Console.WriteLine("Демонстрация перегрузки оператора 'print'");
            Console.WriteLine($"В этот оператор передался параметр: {param}");
        }

        // Функции
        //----------------------------------------------------------------------------
        public bool SuitHeight()
        {
            if (this.height >= 170)
            {
                Console.WriteLine($"{this.name} подходит по росту: ");
                Console.WriteLine($"{this.height} см при барьере в 170 см ");
                return true;
            }
            else
            {
                Console.WriteLine($"{this.name} не подходит по росту: ");
                Console.WriteLine($"{this.height} см при барьере в 170 см ");
                return false;
            }
        }
        public bool SuitHeight(int height)
        {
            if (this.height >= height)
            {
                Console.WriteLine($"{this.name} подходит по росту: ");
                Console.WriteLine($"{this.height} см при барьере в {height} см ");
                return true;
            }
            else
            {
                Console.WriteLine($"{this.name} не подходит по росту: ");
                Console.WriteLine($"{this.height} см при барьере в {height} см ");
                return false;
            }
        }
        //Операции
        //----------------------------------------------------------------------------
        public static int operator - (Registry a, Registry b)
        {
            return Math.Abs(a.height - b.height);
        }
        public static int operator - (Registry a, int c)
        {
            return Math.Abs(a.height - c);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Registry mem1 = new Registry("Ann", 1, 27, 169);
            Registry mem2 = new Registry("Nina");
            Registry mem3 = new Registry();
            Console.WriteLine("\nДемонстрация работы оператора print:\n");
            mem1.print();
            mem2.print();
            mem3.print();
            mem3.print(1);
            Console.WriteLine("Демонстрация работы функции SuitHeight:");
            bool tem = mem1.SuitHeight();
            Console.WriteLine($"Функция вернула значение: {tem}");
            tem = mem1.SuitHeight(165);
            Console.WriteLine($"Функция вернула значение: {tem}");
            Console.WriteLine("\nДемонстрация работы операции -:");
            Registry mem4 = new Registry("Nik", 4, 30, 180);
            Console.WriteLine("Проведем операцию разности по росту между объектами:");
            mem1.print();
            mem4.print();
            int dif = mem1 - mem4;
            Console.WriteLine($"Операция разности объектов класса Регистр вернула значение: {dif}");
            Console.WriteLine($"Отнимаем 30 от роста первого объекта операцией -");
            dif     = mem1 - 30;
            Console.WriteLine($"Операция разности объекта класса Регистр и целого числа вернула: {dif}");
            Console.ReadKey();
        }
    }
}
