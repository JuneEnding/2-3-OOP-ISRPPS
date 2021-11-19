using System;

namespace lab_2_2_final
{
    class A
    {
        public A()
        {
            c.c1 = 7;
            Console.WriteLine("Сработал конструктор A");
        }
        ~A() { }
        public class B
        {
            public B() { Console.WriteLine("Сработал конструктор B"); }
            ~B() { }
            public class D
            {
                public D() { Console.WriteLine("Сработал конструктор D"); }
                ~D() { }
                public void MetD() { Console.WriteLine("Сработал метод D"); }
            }

            public class E
            {
                public E() { Console.WriteLine("Сработал конструктор E"); }
                ~E() { }
                public void MetE() { Console.WriteLine("Сработал метод E"); }
            }
            public void MetB()  { Console.WriteLine("Сработал метод B");  }

            public D Bd { get   { Console.Write("get d -> "); return d; } }
            public E Be { get   { Console.Write("get e -> "); return e; } }

            private D d = new D();
            private E e = new E();
        }
        public class C
        {
            public C()
            {
                c1 = 10;
                Console.WriteLine("Сработал конструктор C");
            }
            ~C() { }
            public class E
            {
                public E() { Console.WriteLine("Сработал конструктор E"); }
                ~E() { }
                public void MetE() { Console.WriteLine("Сработал метод E"); }
            }
            public class F
            {
                public F() { Console.WriteLine("Сработал конструктор F"); }
                ~F() { }
                public void MetF() { Console.WriteLine("Сработал метод F"); }
            }
            public void MetC() { Console.WriteLine("Сработал метод C");  }
            public  E Ce { get { Console.Write("get e -> "); return e; } }
            public  F Cf { get { Console.Write("get f -> "); return f; } }

            private E e = new E();
            private F f = new F();

            // Атрибут доступа _из примера_
            // Примечание: делать атрибуты с модификатором доступа public чревато
            //             ошибками! Если нужно сделать атрибуты с возможностью
            //             доступа к ним, лучше делать это через свойства get и set!
            public int c1 { set; get; }
        }
        public class J
        {
            public J() { Console.WriteLine("Сработал конструктор J"); }
            ~J() { }
            public void MetJ() { Console.WriteLine("Сработал метод J"); }
        }
        public void MetA(){ Console.WriteLine("Сработал метод A"); }
        public B Ab { get { Console.Write("get b ->"); return b; } }
        public C Ac { get { Console.Write("get c ->"); return c; } }
        public J Aj { get { Console.Write("get j ->"); return j; } }

        private B b = new B();
        private C c = new C();
        private J j = new J();
    }
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            Console.WriteLine($"\nАТРИБУТ ДОСТУПА: {a.Ac.c1}");
            Console.WriteLine("ДОСТУП К ОБЪЕКТАМ ПО ВЛОЖЕНИЯМ:");
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
