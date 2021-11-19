using System;

namespace lab_6_3
{
    //1 : 1
    //В D ссылка на E и наоборот
    class D
    {
        public D(E e)  { this.e = e; Console.WriteLine("Сработал конструктор D с пар. e");}
        ~D() { Console.WriteLine("Сработал ~D"); }
        public int fd() { return 111; }
        public E e = null; // Созд ссылку на объект класса Е!
    }
    class E
    {
        public E() { Console.WriteLine("Сработал конструктор по умолчанию E"); }
        public E(D d) { this.d = d; Console.WriteLine("Сработал конструктор E с пар. d"); }
        ~E() { Console.WriteLine("Сработал ~E"); }
        public int fe() { return 222; }
        public D d { set; get; } // Созд ссылку на объект класса D
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ассоциакия один к одному:");
            E e = new E();
            D d = new D(e); // Констр по умолч нет....
            e.d = d;
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //D d_1 = new D(new E()); // Записи эквивалентны с 3-мя выше?
            // Но разве в таком случае не остается непроинициализированным атрибут d в объекте e

            // Нельзя обращаться к функциям, пока не проведена ассоциация
            // e.d - возвращается сылка на объект d
            // d.fd() - привычным уже образом вызывается функция объекта d

            Console.WriteLine($"e.d.fd() = {e.d.fd()}"); 
            Console.WriteLine($"e.d.fd() = {d.e.fe()}");

            Console.ReadKey();

        }
    }
}
