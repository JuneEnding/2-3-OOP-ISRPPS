using System;

namespace Lab_4_2
{
    class B { // A -> B специализация 
        public B() { Console.WriteLine("constr B"); }
        public int b { set; get; }
        public virtual void fb() {
            Console.WriteLine("fb B");
        }
    }
    /*
    abstract class G {
        abstract int fg();
    }
    */
    interface E  {
       int fe();          
    }
    interface C: E {
       int fc();         
    }
    interface J  {
       int fj();              
    }
    class A : B, C, J {
        public A() { Console.WriteLine("constr A"); }
        // Замещение операции суперкласса В 
        public override void fb() {
            base.fb(); 
            Console.WriteLine("fb override A");
        } 
        // Реализация функционала интерфейсов
        public int fc() { Console.WriteLine("fc is implementationed by A"); return 55; }
        public int fj() { Console.WriteLine("fj is implementationed by A"); return 99; }
        public int fe() { Console.WriteLine("fe is implementationed by A"); return 22; }
        public int a { set; get; }
    }
    /* Та же иерархия, только здесь вместо специализации от В, спецификация от В1
    class D1 { }
     
    abstract class B1 {
          public abstract int fb1();
          public int fb { set; get; } 
    }
    class A1 : B1, C, J  // D1
    {
        public A1() { Console.WriteLine("constr A1"); }
        // Замещение операции суперкласса В1 функцией
        public override int fb1() 
        {
            Console.WriteLine("fb override B1");
            return 111;
        }
        // Реализация функционала интерфейсов
        public int fc() { Console.WriteLine("fc is implementationed by A1"); return 55; }
        public int fj() { Console.WriteLine("fj is implementationed by A1"); return 99; }
        public int fe() { Console.WriteLine("fe is implementationed by A1"); return 22; }
        public int a { set; get; }

    }*/
    //==================================================================================
    class Program
    {
        static void Main(string[] args) {

            B b = new B();
            b.b = 11;

            b = new A(); // Подстановка А в В
            //((A)b).fb(); Зачем пр-е? Ф-я В и так замещена в А, а из А и так выполняется замещ. ф-я В!
            b.fb(); // Замещение
            Console.ReadKey();

            Console.WriteLine("step 2");

            C c = null;  // Ссылка, ибо создать объект С невозможно
            c = new A(); // Подстановка А в С
            Console.WriteLine(" c.fc() {0} ", c.fc());
            Console.ReadKey();
            ((A)c).fb(); // Преобразование С к типу А, из а можно вызвать ф-ю fb()
            A a = new A();
            a.fb();
            J j = null;
            j = new A();
            Console.WriteLine(" j.fj() {0}", j.fj());
            Console.WriteLine(" j.fe() {0}", ((A)j).fe());
            Console.ReadKey();
            Console.WriteLine(" c.fe() {0}", c.fe()); // В с находится ссылка А

            Console.ReadKey();

            E e = null;
            e = new A();
            e.fe();
            ((A)e).fc();
            ((C)e).fc();

            Console.ReadKey();
        }
    }
}
