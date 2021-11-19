using System;
using System.Net.Http.Headers;

namespace lab_5
{

    interface A
    {
        void fa();

    }
    public interface B : A
    {
        void fb();

    }
    class D : A
    {
        public D() { Console.WriteLine("Сработал конструктор "); }
        public void fa() { }

    }
    class C : D, B
    {
        public C() { }
        public void fa() { }// Конфликт имен
        public void fb() { }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            A a = new C();
            a.fa();
            ((B)a).fb();
            ((C)a).fa();
            ((C)a).fb();

            C c = new

        }
    }
}
