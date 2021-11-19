using System;

namespace lab_8_3
{
    class A { }
    class B { }
    class SwapT <T> // конкретизация  T <- А подставляем класс  A  в дан случае
    {
        public void FSWap(ref T x, ref T y)// Мен адресов в глобальной памяти
        //public void FSWap()// Ф-я перессылки между
        {
            T t = x; // В данном случае - переменная
            x = y;
            y = t;
        }
    }
    class Swap<T> // конкретизация свапа?
    {
        public void FSWap(ref A x, ref B y)// Мен адресов в глобальной памяти
        //public void FSWap()// Ф-я перессылки между
        {
            A t = x; // В данном случае - переменная
            x = y;
            y = t;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Swap swap = new Swap();
            A a1, a2 = null; 
            a1 = new A(); a2 = new A(); // адреса в памяти опред?
            //Console.WriteLine($"addr.{a1.ToString()}");
            Console.WriteLine($"a1 addr.{a1.GetHashCode().ToString()}");
            Console.WriteLine($"a2 addr.{a2.GetHashCode().ToString()}");
            //Console.WriteLine($"addr.{Convert.ToInt32(a1).ToString()}");

            swap.FSWap(ref a1, ref a2);

            Console.WriteLine($"\na1 addr.{a1.GetHashCode().ToString()}");
            Console.WriteLine($"a2 addr.{a2.GetHashCode().ToString()}");
            Console.WriteLine($"Шаг 2:");

            Console.WriteLine($"\na1 addr.{a1.GetHashCode().ToString()}");
            Console.WriteLine($"a2 addr.{a2.GetHashCode().ToString()}");

            SwapT<A> swapt = new SwapT<A>(); // конкретизация - настраиваю класс... на класс А
            // Настраиваю в объявлении и в конструкторе
            swapt.FSWap(ref a1, ref a2);

            int a_1 = 5;
            int a_2 = 6;
            swapt.FSWap( ref a_1, ref a_2)

            Console.ReadKey();
        }
    }
}
