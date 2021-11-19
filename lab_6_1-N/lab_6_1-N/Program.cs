using System;

namespace lab_6
{
    class D// 1
    {
        public D() { Console.WriteLine("Сработал конструктор D "); }
        ~D() { Console.WriteLine("Сработал ~D"); }

        public int fd() { return 111; }
        public void setE(E e)
        {
            if (size < N)
            {
                this.e[size] = e;
                size++;
            }

        }
        private E[] e = null; // Ссылка на массив
        public E eA {
            set {
                if (size < N) {
                    this.e[size] = value;
                    size++; 
                }
                else
                    Console.WriteLine("size >= N ");
            }

            get { return this.e[--size]; }// Префиксная записсь!!!
        }
        private int size = 0; // Инициализация первичного числа заполненных ячеек массива
        private int N = 7;    // Инициализация максимального размера массива
        this.e = new E[N];    // Инициализация массива!!!
    }


    class E // N
    {
        public E() { Console.WriteLine("Сработал конструктор по умолчанию E"); }
        public E(D d) { this.d = d; Console.WriteLine("Сработал конструктор E с пар. d"); }
        ~E() { Console.WriteLine("Сработал ~E"); }
        public int fe() { return 222; }
        public D d { set; get; } // \\Созд ссылку на объект класса D\\ было точнее
    }           // Cсылка на объ D (8+++)
    class Program
    {
    static void Main(string[] args)
    {
        Console.WriteLine("Ассоциакия один ко многим:");
        //E e = new E();
        D d = new D(e); // Констр по умолч нет....
        E e1 = new E();
        E e2 = new E();
        E e3 = new E();
        E e4 = new E();
        E e5 = new E();

        d.eA = e1; d.eA = e2; d.eA = e3; d.eA = e4; d.eA = e5;
            d.eA.fe();
        e1.d = d; e2.d = d; e3.d = d; e4.d = d; e5.d = d;


            //e.d = d;
            //++++++++++++++++++++++++++++++++++++++++
            //D d_1 = new D(new E()); // Записи эквивалентны с 3-мя выше
            //e.d.fd();
            // Console.WriteLine($"e.d.fd() = {e.d.fd()}");
            // Console.WriteLine($"e.d.fd() = {d.e.fe()}");

            Console.ReadKey();

    }
    }
}

