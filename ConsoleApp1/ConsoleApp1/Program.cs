using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_15_dll2;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Registry mem1 = new Registry("Ann", 1, 27, 169);
            Console.ReadKey();
            Registry mem2 = new Registry("Nina");
            Console.ReadKey();
            //Registry mem3 = new Registry();
            //Console.WriteLine("\nДемонстрация работы операции print:\n");
            mem1.print();
            mem2.print();
            //mem3.print();
            //mem3.print(1);
            Console.WriteLine("Демонстрация работы функции SuitHeight:");
            bool tem = mem1.SuitHeight();
            Console.WriteLine($"Функция вернула значение: {tem}");
            tem = mem1.SuitHeight(165);
            Console.WriteLine($"Функция вернула значение: {tem}");
            Console.WriteLine("\nДемонстрация работы оператора -:");
            Registry mem4 = new Registry("Nik", 4, 30, 180);
            Console.WriteLine("Проведем операцию разности по росту между объектами:");
            mem1.print();
            mem4.print();
            int dif = mem1 - mem4;
            Console.WriteLine($"Разность объектов класса Регистр вернула значение: {dif}");
            Console.WriteLine($"Отнимаем 30 от роста первого объекта операцией -");
            dif = mem1 - 30;
            Console.WriteLine($"Разность объекта класса Регистр и целого числа вернула: {dif}");
        }
    }
}
