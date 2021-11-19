using lab_2_1_final;
using System;

namespace lab_2_1_final
{
    class A
    {
        private B b = new B();
        private C c = new C();
        private J j = new J();
        public A()
        {
            c.c1 = 7;
            Console.WriteLine("Сработал конструктор A");
        }
        ~A() { }
        public void MetA()
        {
            Console.WriteLine("Сработал метод A");
        }
        public B Ab
        {
            get { Console.Write("get b -> "); return b; }
        }
        public C Ac
        {
            get { Console.Write("get c -> "); return c; }
        }
        public J Aj
        {
            get { Console.Write("get j -> "); return j; }
        }
    }

    class B
    {
        private D d = new D();
        private E e = new E();
        public B() { Console.WriteLine("Сработал конструктор B"); }
        ~B() { }
        public void MetB()
        {
            Console.WriteLine("Сработал метод B");
        }
        public D Bd
        {
            get { Console.Write("get d -> "); return d; }
        }
        public E Be
        {
            get { Console.Write("get e -> "); return e; }
        }
    }

    class C
    {
        E e = new E();
        F f = new F();
        public C()
        {
            c1 = 40;
            Console.WriteLine("Сработал конструктор C");
        } 
        ~C() { }
        public void MetC()
        {
            Console.WriteLine("Сработал метод C");
        }
        public E Ce
        {
            get { Console.Write("get e -> "); return e; }
        }
        public F Cf
        {
            get { Console.Write("get f -> "); return f; }
        }
        // Атрибут доступа _из примера_
        // Примечание: делать атрибуты с модификатором доступа public чревато
        //             ошибками! Если нужно сделать атрибуты с возможностью
        //             доступа к ним, лучше делать это через свойства get и set!
        public int c1 { get; set; }
    }

    class D
    {
        public D() { Console.WriteLine("Сработал конструктор D"); }
        ~D() { }
        public void MetD()
        {
            Console.WriteLine("Сработал метод D");
        }
    }

    class E
    {
        public E() { Console.WriteLine("Сработал конструктор E"); }
        ~E() { }
        public void MetE()
        {
            Console.WriteLine("Сработал метод E");
        }
    }

    class F
    {
        public F() { Console.WriteLine("Сработал конструктор F"); }
        ~F() { }
        public void MetF()
        {
            Console.WriteLine("Сработал метод F");
        }
    }

    class J
    {
        public J() { Console.WriteLine("Сработал конструктор J");  }
        ~J() { }
        public void MetJ()
        {
            Console.WriteLine("Сработал метод J");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            Console.WriteLine($"\nПЕЧАТЬ АТРИБУТА ДОСТУПА: {a.Ac.c1}\n");
            Console.WriteLine("ДОСТУП К ОБЪЕКТАМ ПО ЗНАЧЕНИЯМ:");
            Console.WriteLine("ВЫЗОВ МЕТОДОВ:\n");
            a.MetA();
            a.Ab.MetB();
            a.Ac.MetC();
            a.Aj.MetJ();
            a.Ab.Bd.MetD();
            a.Ab.Be.MetE();
            a.Ac.Ce.MetE();
            a.Ac.Cf.MetF();
            Console.ReadKey();
        }
    }
}

