using System;

namespace lab_9_1
{
    // Объявляется множество сигнатур с помощ опер делегата
    // Объекту данного делегата можно присвоить объект с такой же сигнатурой (искл. имя)
    delegate int A(int x, int y); // м-во ф-й с 1 сигнатурой
    delegate void B(); // м-во ф-й с 1 сигнитурой
    // м-во сигнатур такого класса
    // если в программ - это будет влежоно...~~~
    class Program
    {
        // delegate
        static void Main(string[] args)
        {
            // Делегаты можно создавать с помощью анонимных методов:
            B b = delegate () // анонимная макрооперация б - не нужно имени
            // Анонимная операция 
            {
            Console.WriteLine("Step");
            Console.ReadKey();
            };
            Console.ReadKey();
            
            //Console.WriteLine($"{Program.Add(2, 5)}"); // Проверка статической функции

            // !!!Полиморфная ссылка на мво сигнатур данного класса!!!
            b();
            A a = null; // Объект неопределен поэтому ссылка 
            Console.WriteLine("Создадим делегат:");
            Console.WriteLine("A a = null;");
            // Нужно определить метод, соответствующий этой СИГНАТУРЕ
            a = Add; // По ссылке а присвоили ф-ю эдд
            Console.WriteLine("a = Add;");
            int result = a(5, 6); // Фактически Add(4,5)
            Console.WriteLine("Use method a(5, 6) = {0}", result);

            b();
            a = Sub;
            Console.WriteLine("a = Sub;");
            Console.WriteLine("Use method a(3, 2) = {0}", a(3,2));

            b();
            a = delegate (int x, int y) { return x / y; }; //в сигнатуре ;  
            Console.WriteLine("a = delegate (int x, int y) { return x / y; };");
            Console.WriteLine("Use method a(5, 2) = {0}", a(5, 2)); // Возвращаемый тип int => округление.
            
            b();
            // Не созд имени ф-и созд ---ф-ю?
            // Можно подставлять любые ф-и, подходящие данной сигнатуре делегейт
            a = delegate (int a, int b) { return a * b;};
            Console.WriteLine("a = delegate (int a, int b) { return a * b;};");
            Console.WriteLine("Use method a(5, 2) = {0}", a(5, 2));

            b();
            a = delegate (int a, int b) { return a % b;}; // Остаток от деления левого операнда на правый
            Console.WriteLine("a = delegate (int a, int b) { return a % b;};");
            Console.WriteLine("Use method a(5, 2) = {0}", a(5, 2));
            // pointer - указатель
            Console.WriteLine("\nStep 2 delegate as pointer");
            A apointer = null;
            // Добавляем в список вызовов делегата указатели на новые функции
            Console.WriteLine("Step 2.1"); 
            apointer = Add;
            Console.WriteLine("apointer = Add;");
            // Инкремент
            apointer += Sub;
            Console.WriteLine("apointer += Sub;");
            apointer += Dev;
            Console.WriteLine("apointer += Dev;");
            // Invoke - выполняет делегат,необходим для безопасности
            // Если в список вызова делегата не добавить методы, а оставить его = null
            // То при вызове списка получится исключение
            // Эта конструкция исключает эту проблему:
            // apointer.Invoke(55, 5);
            // Если в списке вызова несколько методов, возвращается значение последнего метода
            Console.WriteLine("apointer.Invoke(55, 5)) = {0}", apointer.Invoke(55, 5)); 
            b();
            // Декремент
            apointer -= Dev;
            Console.WriteLine("Step 2.2");
            Console.WriteLine("apointer -= Dev;");
            Console.WriteLine("apointer.Invoke(55, 5) = {0}", apointer.Invoke(55, 5));
            b();
            apointer += Dev;
            apointer -= Sub;
            Console.WriteLine("Step 2.3");
            Console.WriteLine("apointer += Dev;");
            Console.WriteLine("apoiner  -= Sub");
            Console.WriteLine("apointer.Invoke(55, 5) = {0}", apointer.Invoke(55, 5));
            b();
            apointer += delegate (int a1, int b1) { return a1 * b1; };
            Console.WriteLine("Step 2.4"); 
            Console.WriteLine("apointer += delegate (int a1, int b1) { return a1 * b1; };"); 
            Console.WriteLine("apointer.Invoke(55, 5) = {0}", apointer.Invoke(55, 5)); 
            b();
            // Делегирование в качестве параметра
            Console.WriteLine("\nStep 3 Multicast (delegate as parametr)");
            Console.WriteLine("Multicast(55, 5, apointer) = {0}", Multicast(55, 5, apointer)); 

            Console.ReadKey();
        }
        // A-b static =>
        // 1. Можно к ним обращаться по имени класса 
        // 2. Они "статические"
        private static int Add(int x1, int y1)
        {
            return x1 + y1;
        }
        private static int Sub(int x1, int y1)
        {
            return x1 - y1;
        }
        private static int Dev(int x1, int y1)
        {
            return x1 / y1;
        }
        // Функция с делегатом в качестве параметра
        // ФУНКЦИЯ, чтобы показать, что конструкция с делегатом в виде параметра работает
        private static int Multicast(int x, int y, A apoiner) { return apoiner.Invoke(x, y); }
    }
}
