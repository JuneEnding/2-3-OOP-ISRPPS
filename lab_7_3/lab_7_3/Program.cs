using System;

namespace lab_7_3
{// D -> E use
    // Клиент
    class D
    {
        public D(){ Console.WriteLine("Сработал конструктор D");}
        ~D() { Console.WriteLine("Сработал деструктор D"); }
        public void fd(E e) { 
            Console.WriteLine("Алгоритм");
            // В классе D используется атрибут е и функция, принадлежащие классу Е
            this.d = e.e + e.fe();
            // Использование ресурсов утилиты Es
            Console.WriteLine($"Es.fe() = {Es.fe()}"); 
            Console.WriteLine($"Es.fentry() = {Es.fentry()}"); 
        } // меж е и дэ орг отноше через параметр
        private int d { set; get; }
    }
    // Сервер
    class E
    {
        public E() { Console.WriteLine("Сработал конструктор E"); }
        ~E() { Console.WriteLine("Сработал деструктор E"); }
        public int e { set; get; }// атр доступа
        public int fe() { return 888; }
        public static int fs() { return 333; }
    }
    // Утилита
    static class Es // Клиент - УТИЛИТА в одном кл набор функций методов. операций, без атрибутов, сама фя статич
    {
        public static int fe() { return 888; }
        public static int fentry() { return 1111; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            D d = new D();
            E e = new E();// Сервер
            d.fd(e);
            Console.ReadKey();

            Console.WriteLine("\nStatic f:");
            Console.WriteLine($"Es.fe() = {E.fs()}");
            Console.WriteLine($"Es.fe() = {Es.fe()}");
            Console.WriteLine($"Es.fentry() = {Es.fentry()}");

            Console.ReadKey();
        }
    }
}
