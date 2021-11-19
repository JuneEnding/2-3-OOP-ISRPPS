using System;
using System.Collections.Generic; //!!! В этой библиотеке есть необходимые нам классы - коллекции - контейнеры, 
                                  // он берет список из объектов, определет как шаблон
                                  // Generic - общий
namespace lab_9_2
{
    delegate int Lambda(int x, int y);
    delegate void Lambda_OP();
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Step 1 Lambda");
            //Lambda lambda = null;
            // Лямбда-оператор => используется для отделения входных параметров  
            // с левой стороны от тела лямбда-выражения с правой стороны
            Lambda lambda = (x, y) => { return x + y; }; // Левая часть - параметры
            // ЗАМЕТИМ: параметры лямбда-выражения должны однозначно соответствовать
            // параметрам делегата ==> д.б. соответствие по количеству и типу (происх. неявн. пр-е)
            // И 

            Console.WriteLine($"lambda(55,5)= {lambda.Invoke(55, 5)}");
            Lambda_OP print = () => Console.WriteLine("Hello, it's Lambda_OP print ");
            print();
            Console.ReadKey();

            Console.WriteLine("Step 2 Using Lambda");
            // Func<T,TResult> - встроенный делегат
            // Его параметры и возвращаемое значение конкретизируются
            Func<int, int> square = s => s * s;
            Console.WriteLine($"square = {square(81)}");
            Console.ReadKey();

            Func<int, double> radical = r => Math.Sqrt(r);
            Console.WriteLine($"radical = {radical(81)}");
            Console.ReadKey();

            //Найти индекс первого нечетного элемента в списке
            Console.WriteLine("Find index of first odd element in list");
            // List<T> - класс представляет строго типизированный список объектов (ШАБЛОН), доступных по индексу, 
            // поддерживает методы для поиска по списку...
            // List - конкретизированный класс, НАСТРОИМ его на тип int
            List<int> elements = new List<int>() { 0, 3, 1, 2, 3, 7, 8 };

            // .FindIndex - метод, выполняет поиск элемента, удовлетворяющего условиям указанного предиката, 
            // и возвращает отсчитываемый от нуля индекс первого найденного вхождения в пределах всего списка List<T>
            // Этот метод возвращает значение -1 если соответствующий условию элемент не найден
            // !!! Лямда выражение используется как предикат 
            // А фактически, является анонимной функцией, который принимает делегат (public delegate bool Predicate<in T>(T obj);)
            int NechetIndex = elements.FindIndex(x => x % 2 != 0);
            Console.WriteLine($"NechetIndex = { NechetIndex}");
            Console.ReadKey();
            // .Find - метод, выполняет поиск элемента, удовлетворяющего условиям указанного предиката, 
            // и возвращает первое найденное ЗНАЧЕНИЕ вхождения в пределах всего списка List<T>
            int NechetChislo = elements.Find(x => x % 2 != 0);
            Console.WriteLine($"NechetChislo = { NechetChislo}");
            Console.ReadKey();
            // Возвращает первое найденное значение (x >= 4) && (x <= 8)
            int Chislo_List = elements.Find(x => (x >= 4) && (x <= 8));
            Console.WriteLine($"Chislo_List [4;8]  = { Chislo_List}");
            Console.ReadKey();

            Console.ReadKey();
        }
    }
}
