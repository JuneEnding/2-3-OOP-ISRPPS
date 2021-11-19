using System;
using System.Threading;

namespace lab_3_0
{   // Определение класса
    class A // Суперкласс
    {
        public A()
        {
            // Тело конструктора
            this.a = 1;
            Console.WriteLine("Конструктор A сработал");
        }

        ~A() { Console.WriteLine("Деструктор A сработал");
            Thread.Sleep(2000);
        }

        // virtual - модификатор, позволяющий методу суперкласса
        // быть замещаемым
        public virtual int fa()
        {
            Console.WriteLine("Функция fa() сработала из класса A");
            return a + 1;
        }

        protected int a { get; set; } // Атрибут доступа по умолчанию 
                                      // - все наследники наследуют его
    }

    class B : A // В подкласс А
    {
        public B()
        {
            this.a = 20;
            Console.WriteLine("Конструктор B сработал");
        }
        ~B() { Console.WriteLine("Деструктор B сработал");
            Thread.Sleep(2000);
        }
        // Ф-я суперкласса А замещается функцией В
        public override int fa()
        {
            Console.WriteLine("Функция fa() сработала из класса B");
            return a + 10;
        }

        //protected int b { get; set; }
    }
    class D : B 
    {
        public D()
        {
            this.a = 300;
            Console.WriteLine   ("Конструктор D сработал");
        }
        ~D() { Console.WriteLine("Деструктор D сработал");
            Thread.Sleep(2000);
        }
        // Ф-я суперкласса А замещается функцией D
        public override int fa()
        {
            Console.WriteLine("Функция fa() сработала из класса D");
            return a + 100;
        }

        //protected int d { get; set; }
    }
    class Present // Нет полей, зато можно использовать ф-и класса
    {
        public Present()
        { Console.WriteLine("Конструктор Present сработал"); }
        ~Present() { Console.WriteLine("Деструктор Present сработал"); }
        public void present(A a) // Подстановка в параметре
        {

            if (a.GetType() == typeof(B))
            {
                Console.WriteLine("good luck");
            }
            else
                Console.WriteLine("a (A a) {0}", a.fa() + 55); // Замещение 
        }
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        class Program
    {
        static void Main(string[] args)
        {
            //Тест использования замещения на не полиморфной переменной
            //D d = new D(); 
            //Console.WriteLine   ($"d.fa() = { d.fa()}");
            //Console.ReadKey     ();

            Console.WriteLine   ("Создание объекта класса А и присваивание ссылки на него переменной а:");
            Console.WriteLine   ($"A a = new A();"); // a - полиморфная переменная
            A a     = new A();
            Console.WriteLine   ("Вызыв метода fa от обьекта класса А: ");
            Console.WriteLine   ($"a.fa() = {a.fa()}");
            Console.ReadKey     ();

            Console.WriteLine   ("\nПОДСТАНОВКА в а вместо ссылки на объект суперкласса А ссылку на объект подкласса В");
            Console.WriteLine   ("- а становится ссылкой на объект наследуемого типа");
            Console.WriteLine   ($"a = new B();");
            a = new B();        // При вызове конструктора подкласса без параметров, произойдет 
                                // вызыв конструкторов по умолчанию и по иерархии
            Console.WriteLine   ("Проверка типа а:");
            if (a.GetType() == typeof(B))
                Console.WriteLine($"a.GetType() == typeof (B)");
            Console.WriteLine   ("ЗАМЕЩЕНИЕ функции суперкласса А функцией подкласса В: ");
            Console.WriteLine   ($"a.fa() = {a.fa()} ");
            Console.ReadKey     ();

            Console.WriteLine("\nПОДСТАНОВКА в а вместо ссылки на объект суперкласса B ссылку на объект подкласса D");
            Console.WriteLine   ("a = new D();");
            a = new D();
            Console.WriteLine   ("Проверка типа а:");
            if (a.GetType() == typeof (D))
                Console.WriteLine($"a.GetType() == typeof (D)"); 
            else
                Console.WriteLine($"a.GetType() != typeof (D)");
            Console.WriteLine   ($"ЗАМЕЩЕНИЕ функции суперкласса А функцией подкласса D: ");
            Console.WriteLine   ($"a.fa() = {a.fa()} ");
            Console.ReadKey     ();

            {
               D d = new D(); 
            }

            Console.ReadKey();

            Console.WriteLine("\n\nstep2: подстановка с замещением");
            Present present = new Present();
            present.present(a);
            Console.ReadKey();


            Console.WriteLine("\nstep2.1: подстановка замещением");
            present.present(new A());// Полиморфизм в качестве параметров
            Console.ReadKey();


            // Разница : обьявляем обьект и можем с ним совершать операции
            Console.WriteLine("\nstep2.2: подстановка замещением");
            // Создаём обьект в главной программе
            present.present(new B());// Полиморфизм в качестве параметров
            B b = new B();
            present.present(b);
            Console.WriteLine("b.fa() {0}", b.fa() + 55);

            Console.ReadKey();

            //Console.ReadKey();
            Thread.Sleep(6000);
        }
    }
}
