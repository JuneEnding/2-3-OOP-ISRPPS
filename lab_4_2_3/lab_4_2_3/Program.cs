using System;
using System.IO;

namespace lab_4_2
{
    class B
    {
        public B() { Console.WriteLine("Контструктор В сработал"); }
        public int b { set; get; }
        public virtual void fb()
        {
            Console.WriteLine("Функция В сработал");
        }

    }
    abstract class B1
    {
        public B1() { Console.WriteLine("Контструктор В1 сработал"); }
        public abstract int fb1();
        public int b1 { set; get; }

    }

    /* abstract class G 
     {
         abstract int fg();
     }*/

    interface K
    {
        int fk();
        // public int c { set; get; }
    }
    interface C : K
    {
        int fc();
        // public int c { set; get; }
    }
    public interface D
    {
        public int fd();
    }
    class A : B, C, D
    {
        public A()
        {
            Console.WriteLine("Контструктор A сработал");
        }
        public override void fb()
        {
            base.fb();
            Console.WriteLine("Функция В сработала из А");
        }
        public int fc() { return 55; } // Д б алг, раб с интерф
        public int fd() { return 99; } // Д б алг, раб с интерф
        public int fk() { return 99; } // Д б алг, раб с интерф
        public int a { set; get; }
    }
    class A1 : B1, C, D // наслеб абстр класс и исп неск интерфейсов
    {
        public A1()
        {
            Console.WriteLine("Контструктор A сработал");
        }
        public override int fb1()
        {

            Console.WriteLine("Функция В сработала из А");
            return 111;
        }
        public int fc() { return 55; } // Д б алг, раб с интерф
        public int fd() { return 99; } // Д б алг, раб с интерф
        public int fk() { return 99; } // Д б алг, раб с интерф
        public int a { set; get; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            B b = new B();
            b.b = 11;
            b = new A();
            ((A)b).fb();

            Console.WriteLine("Шаг 2");

            C c = null;  // Объект создать нельзя => только (объявляем?) ссылку
            c = new A();
            ((A)C).fb();


            //C = new A():  !!! Нельзя преобразовать
            // Console.WriteLine($"c.fc = {c.fc}");
            // c.fc - cсыл на ноль - нужно преобр

            A a = new A();
            a.fb();

            D d = null;
            d = new A();
            Console.WriteLine($"d.fd = {d.fd}");
            Console.ReadKey();
            Console.WriteLine($"d.fd = {c.fk}");
            //WriteLine(

            Console.ReadKey();
        }
    }
}
