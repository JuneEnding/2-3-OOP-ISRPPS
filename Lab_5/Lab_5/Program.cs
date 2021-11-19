using System;
// Конфликт имен в С#?
namespace Lab_5
{
    interface A  {
        void fa(); 
    }
    interface B : A
    {
        void fb();
    }
    class J : A {
        public J() { Console.WriteLine("Constr J"); } 
        public J(int j) { this.j = j; Console.WriteLine("Constr J with parameters: j"); } 
        public J(int j, int j1) { this.j = j; Console.WriteLine("Constr  J with 2 parameters: j, j1"); } 
        public void fa(){ Console.WriteLine("fa() ran from the J"); }
        public int j { set; get; }
        //public void fb() { Console.WriteLine("fb() ran from the C"); }
        protected int j1 { set; get; }
    }

    class C : J, B {
        public C() { Console.WriteLine("Constr C");  }// Вызыв сист., конструктор по умолчанию
        // Ключевое слово base используется для доступа к членам базового из производного класса
        public C(int c) : base(c)
        {
            this.c = c;
            j1 = c + 22; // И так base
            Console.WriteLine("Constr C with parameters: c"); // Исп. констр по умолч, можно забыть проинициализировать атрибуты!
        }
        public C(int c, int j1 ) : base(c, j1)
        {
            this.c = c;
            Console.WriteLine("Constr C with 2 parameters: c, j1");
        }
        //public void fa() { Console.WriteLine("fa() сработала из класса C"); } // Реализация операции в С
        public void fb() { Console.WriteLine("fb() ran from the C"); }
        public int c { set; get; }
    }
    //==========================================================
    class Program
    {
        static void Main(string[] args)
        {
            A a = new C();
            a.fa(); // Реализована в J =>  вызовется из J
            Console.ReadKey();
            ((B)a).fb();
            ((C)a).fb();

            Console.ReadKey();
            ((C)a).fa();
            Console.ReadKey();

            C c = new C();

            Console.WriteLine("\nШаг 2");
            // Атр. j не инициализирован и вызван констр. по умолч., 
            // если не импользовать base- перенаправление в конструктор суперлкасса
            C c_1 = new C(55);
            Console.WriteLine($"с_1 = {c_1.c}");
            Console.WriteLine($"j   = {c_1.j}");
            //Console.WriteLine($"j1   = {c_1.j1}"); Просмотр при отладке
            Console.ReadKey();
            C c_2 = new C(100, 6);
            Console.WriteLine($"с_1 = {c_2.c}");
            Console.WriteLine($"j   = {c_2.j}");
            //Console.WriteLine($"j1   = {c_2.j1}"); Просмотр при отладке

            Console.ReadKey();
        }
    }
}
