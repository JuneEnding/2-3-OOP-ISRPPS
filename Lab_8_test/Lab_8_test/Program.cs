using System;
//конкретизация

namespace Lab_8_test
{
    class A // По умолчанию и так есть конструктор
    {
        public A() { } // Прописали конструктор по умолчанию, а могли и не писать в данном случае
    }
    class B { } // Конструктор не пишем => будет исп. констр. "по умолчанию"
    public interface IA // D -> IA
    {
        void F();
    }
    class D : IA
    {
        public void F() { Console.WriteLine("Сработала функция F из D"); } // Реализация ф-и интерфейса IA
    }

    // 1. Класс нуждается в конкретизации T <- X (обобщенный класс или шаблон) 
    // 2. Универсальный параметр Т ограничен классом
    // where - ограничение универсального типа
    class U <T> where T : class
    {
        // 1.Конкретизация конструктора
        // 2.Конкретизация с ограничениями ( оператор is)
        // Оператор is проверяет выражение на соответствие шаблону, 
        // он проверяет, можно ли преобразовать выражение в указанный тип и, 
        // если можно, приводит его к переменной этого типа
        // (или проверяет совместимость результата выражения с заданным типом) 
        public U(T t)
        {
            if (t is IA)
                Console.WriteLine("yes IA"); // Иначе сообщ не вывод.
            this.t = t;
            Console.WriteLine("Сработал конструктор U");
        }
        public void F1() { Console.WriteLine("Сработала функция F1 из U"); }
        // Атрибут "универсального типа", который может принимать разные типы 
        public T t { set; get; }
        public void F()
        {
            Console.WriteLine(t is IA); // Печать результата функции
            Console.WriteLine(t); // Пишет пространство имен существования
            if (t is IA)
            {   // Создаем локальную переменную типа IA с помощью присваивания ей 
                // значения __переопределенной (IA)__ переменной t, которая в свою очередь
                // Является универсальным параметром, который определили типом
                // класса из-за ограничения where 
                IA ia = (IA)t; 
                ia.F(); // Просто вызыв функции от ia
            }
            Console.WriteLine("Сработала функция F из U");
        }
    } //ed class U


    // 1. Множественная конкретизация T1 <- X, T2 <- Y
    // 2. Множественные ограничения (предикаты)
    class L<T1, T2> where T1 : class
                    where T2 : class
    {
        // Конкретизация параметров метода
        public void f1(T1 t1, T2 t2) 
        {
            Console.WriteLine(" Hi T1, T2 ");
        }
    }
    // Пример не полиморфного swap
    class Swap //<T> 
    {
        public Swap() { }
        // Меняем адреса в глобальной памяти местами
        public void FSwap(ref A x, ref A y) //берётся конкретно ссылка вне зоны действия операции
        //public void FSwap(A x, A y) Берется ссылка по объекту, создаются лишние переменные - плохо
        {
            A t = x; //А т - локальная переменная
            x = y;   // !!! The t is a local variable, deleted after use
            y = t;
        }
    }

    // 1. Конкретизация T <- A класса
    // 2. Конкретизация с ограничениями
    //    В параметр Т. можно подставить класс А, В, С...
    class SwapT <T> where T : class 
        //предикат where. Вместо Т могу контекретизировать только классом
    {
        public SwapT() { }
        // Конкретизация по параметру T <- X
        public void FSwap(ref T x, ref T y)
        {
            T t = x;
            x = y;
            y = t;
        }
    }

    // Конкретизация метода T <-
    // Пример через "отнош. использ.-утилита" (статический класс)
    /*
    static class SwapTST 
    {
        public static void FSwapTST <T>(ref T x, ref T y)
        {
            T t = x;
            x = y;
            y = t;
        }
    }*/

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Step 1");
            Swap swap = new Swap();
            A a1, a2 = null;
            a1 = new A(); a2 = new A();
            Console.WriteLine(" a1 addr {0}", a1.GetHashCode().ToString());
            Console.WriteLine(" a2 addr {0}", a2.GetHashCode().ToString());

            swap.FSwap(ref a1, ref a2); // подставляем объекты

            Console.WriteLine();
            Console.WriteLine("Step 2");
            Console.WriteLine(" a1 addr {0}", a1.GetHashCode().ToString());
            Console.WriteLine(" a2 addr {0}", a2.GetHashCode().ToString());

            Console.WriteLine();
            Console.WriteLine("Step 3");
            // Создаем объект swapt типа класса SwapT<T>, в который вместо параметра Т
            // Подставили тип класса А - "настроили на класс А"
            // Вызвали конкретизированный конструктор шаблона SwapT (настроенный на А)
            SwapT<A> swapt = new SwapT<A>();
            swapt.FSwap(ref a1, ref a2); //берём адрес этих объектов
            Console.WriteLine(" a1 addr {0}", a1.GetHashCode().ToString());
            Console.WriteLine(" a2 addr {0}", a2.GetHashCode().ToString());

            Console.WriteLine("\nStep 3.2");
            // Конкретизация метода
            Swap<A>(ref a1, ref a2);
            Console.WriteLine(" a1 addr {0}", a1.GetHashCode().ToString());
            Console.WriteLine(" a2 addr {0}", a2.GetHashCode().ToString());
            Swap<A>(ref a1, ref a2);

            //SwapT<int> swaptI = new SwapT<int>(); // С ограничениями не получится!
            //int a_1 = 5; int a_2 = 6;
            //swapt.FSwap(a_1, a_2); не можем подставить целочисленные значения

            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine("\nStep 4");
            U<A> ua = new U<A>(a1); // а1 не входит в иерархию
            ua.F();
            Console.ReadKey();
            Console.WriteLine("Step 4.1");// D - наследник IA => его можно привести к типу IA и он пройдет проверку is
            U<D> ud = new U<D>(new D());
            ud.F();
            // Разница в месте создания D
            Console.ReadKey();
            D d1 = new D(); 
            Console.WriteLine(" d1.GetHashCode() = {0}", d1.GetHashCode());
            U<D> ud_1 = new U<D>(d1); 
            ud_1.F();// Проверка срабатывает и в ф-и внутри U вызывается ф-я, реализованная внутри В

            Console.WriteLine();
            Console.ReadKey();
            // Множественная конкретизация
            Console.WriteLine("Step 5");
            L<D, A> l = new L<D, A>();
            l.f1(d1, a1); // Как видим, метод срабатывает 

            Console.ReadKey();
        }
        // Конкретизация метода (и его параметров) Т <- X
        public static void Swap<T>(ref T x, ref T y)
        {
            T t = x;
            x = y;
            y = t;
        }
    }
}