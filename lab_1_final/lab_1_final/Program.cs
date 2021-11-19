using System;

namespace lab_1_final
{
    class A
    {
        private B b = null;
        private C c = null;
        private J j = null;
        public A(B b, C c, J j)
        {
            this.b = b;
            this.c = c;
            this.j = j;

            //c.cq = 7;
        }
        ~A() { }
        public void OpA()
        {
            Console.WriteLine("Сработал оператор A");
        }
        public bool FunA()
        {
            Console.WriteLine("Сработала функция A");
            return true;
        }
        public B Ab
        {
            set { Console.WriteLine("set j"); b = value; }
            get { Console.Write("get b -> "); return b; }
        }
        public C Ac
        {
            set { Console.WriteLine("set j"); c = value; }
            get { Console.Write("get c -> "); return c; }
        }
        public J Aj
        {
            set { Console.WriteLine("set j"); j = value; }
            get { Console.Write("get j -> "); return j; }
        }
    }

    class B
    {
        private D d = null;
        private E e = null;
        public B(D d, E e)
        {
            this.d = d;
            this.e = e;
        }
        ~B() { }
        public void OpB()
        {
            Console.WriteLine("Сработал метод B");
        }
        public bool FunB()
        {
            Console.WriteLine("Сработала функция B");
            return true;
        }
        public D Bd
        {
            set { Console.WriteLine("set d"); d = value; }
            get { Console.Write("get d -> "); return d; }
        }
        public E Be
        {
            set { Console.WriteLine("set e"); e = value; }
            get { Console.Write("get e -> "); return e; }
        }
    }
    class C
    {
        E e = null;
        F f = null;
        public C(E e, F f)
        {
            this.e = e;
            this.f = f;
        }
        ~C() { }
        public void OpC()
        {
            Console.WriteLine("Сработал метод C");
        }
        public bool FunC()
        {
            Console.WriteLine("Сработала функция C");
            return true;
        }
        public E Ce
        {
            set { Console.WriteLine("set e"); e = value; }
            get { Console.Write("get e -> "); return e; }
        }
        public F Cf
        {
            set { Console.WriteLine("set f"); f = value; }
            get { Console.Write("get f -> "); return f; }
        }
        // Атрибут доступа _из примера_
        // Примечание: делать атрибуты с модификатором доступа public чревато
        //             ошибками! Если нужно сделать атрибуты с возможностью
        //             доступа к ним, лучше делать это через свойства get и set!
        //public int cq { get; set; }
    }
    class D
    {
        public D() { }
        ~D() { }
        public void OpD()
        {
            Console.WriteLine("Сработал метод D");
        }
        public bool FunD()
        {
            Console.WriteLine("Сработала функция D");
            return true;
        }

    }
    class E
    {
        public E() { }
        ~E() { }
        public void OpE()
        {
            Console.WriteLine("Сработал метод E");
        }
        public bool FunE()
        {
            Console.WriteLine("Сработала функция E");
            return true;
        }

    }
    class F
    {
        public F() { }
        ~F() { }
        public void OpF()
        {
            Console.WriteLine("Сработал метод F");
        }
        public bool FunF()
        {
            Console.WriteLine("Сработала функция F");
            return true;
        }

    }
    class J
    {
        public J() { }
        ~J() { }
        public void OpJ()
        {
            Console.WriteLine("Сработал метод J");
        }
        public bool FunJ()
        {
            Console.WriteLine("Сработала функция J");
            return true;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            D d = new D();
            E e = new E();
            F f = new F();
            J j = new J();
            C c = new C(e, f);
            B b = new B(d, e);
            A a = new A(b, c, j);
            //Console.WriteLine($"ПЕЧАТЬ АТРИБУТА ДОСТУПА: {c.cq}\n");
            Console.WriteLine("ДОСТУП К МЕТОДАМ АГРЕГИРОВАННЫХ ОБЪЕКТОВ ПО ССЫЛКЕ:\n");
            Console.WriteLine("ВЫЗОВ ОПЕРАТОРОВ:\n");
            a.OpA();
            a.Ab.OpB();
            a.Ac.OpC();
            a.Aj.OpJ();
            a.Ab.Bd.OpD();
            a.Ab.Be.OpE();
            a.Ac.Ce.OpE();
            a.Ac.Cf.OpF();
            Console.WriteLine("\n\nВЫЗОВ ФУНКЦИЙ:\n");
            bool tem = a.FunA();
            Console.WriteLine($"Возвращено значение: {tem}\n");
            tem = a.Ab.FunB();
            Console.WriteLine($"Возвращено значение: {tem}\n");
            tem = a.Ac.FunC();
            Console.WriteLine($"Возвращено значение: {tem}\n");
            tem = a.Aj.FunJ();
            Console.WriteLine($"Возвращено значение: {tem}\n");
            tem = a.Ab.Bd.FunD();
            Console.WriteLine($"Возвращено значение: {tem}\n");
            tem = a.Ab.Be.FunE();
            Console.WriteLine($"Возвращено значение: {tem}\n");
            tem = a.Ac.Ce.FunE();
            Console.WriteLine($"Возвращено значение: {tem}\n");
            tem = a.Ac.Cf.FunF();
            Console.WriteLine($"Возвращено значение: {tem}\n");
            Console.ReadKey();
        }
    }
}
