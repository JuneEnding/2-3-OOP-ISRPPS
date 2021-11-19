using System;

namespace ClassLibrary_ForConsol15
{
    public class Registry
    {
        // Атрибуты(поля) класса. По умолчанию атрибутам ставится мод.доступа private.
        //----------------------------------------------------------------------------
        string name { get; set; } // Автоматически задает атрибуты (свойства) атрибута
        int num;
        int old;
        int height;

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
        public Registry(string name) : this(name, 0, 0, 0) { Console.WriteLine("Конструктор с 1 параметром сработал"); }
        // Перенаправ. в гл. констр.

        // Операции
        //----------------------------------------------------------------------------
        public void print()
        {
            Console.WriteLine($"Имя: {name}\tНомер: {num}\tВозраст: {old}\tРост: {height}");
        }
        public void print(int param)
        {
            Console.WriteLine("Демонстрация перегрузки оперции 'print'");
            Console.WriteLine($"В эту операцию передался параметр: {param}");
        }

        // Функции
        //----------------------------------------------------------------------------
        public bool SuitHeight()
        {
            if (this.height >= 170)
            {
                Console.WriteLine($"{name} подходит по росту: ");
                Console.WriteLine($"{height} см при барьере в 170 см ");
                return true;
            }
            else
            {
                Console.WriteLine($"{name} не подходит по росту: ");
                Console.WriteLine($"{height} см при барьере в 170 см ");
                return false;
            }
        }
        public bool SuitHeight(int height)
        {
            if (this.height >= height)
            {
                Console.WriteLine($"{name} подходит по росту: ");
                Console.WriteLine($"{this.height} см при барьере в {height} см ");
                return true;
            }
            else
            {
                Console.WriteLine($"{name} не подходит по росту: ");
                Console.WriteLine($"{this.height} см при барьере в {height} см ");
                return false;
            }
        }
        //Операторы
        //----------------------------------------------------------------------------
        public static int operator -(Registry a, Registry b)
        {
            return Math.Abs(a.height - b.height);
        }
        public static int operator -(Registry a, int c)
        {
            return Math.Abs(a.height - c);
        }
    }
}
