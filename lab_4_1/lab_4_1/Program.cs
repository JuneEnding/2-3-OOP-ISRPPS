using System;
//Наследование: расширение, спецификация, специализация, конструирование, (комбинирование во втором проекте)
//расширение - расширение функцией fb
//расширение самой функции (замещаемой) - изменение её кода.
//общая спецификация - перечень операций одинаковый для обьектов?
//отношения между классами определяется произвольно(на наш выбор)
namespace lab_4
{
    //определение класса
    class A //супер-класс, определим для него расширение
    {
        public A()
        {
            this.a = 1;// !!! 1- null
            Console.WriteLine("Конструктор A() сработал");
        }

        ~A()
        {
            Console.WriteLine("Деструктор ~A()");
            //Thread.Sleep(2000);
        }
        //virtual - модификатор для замещаемой функции
        public virtual int fa()
        {
            Console.WriteLine("Сработала функция fa() из класса А");
            return a + 1;
        }
        public int Aa // Модификатор доступа к атрибуту а класса А
        {
            set { Console.WriteLine(" set А "); a = value; }
            get { Console.WriteLine(" get А "); return a; }
        }
        protected int a = 1;// 

    }
    // Расширение В -> A
    // В  - специализированный класс по отношению к А
    class B : A
    {
        public B()
        {
            this.a = 20;
            this.b = 10;
            this.b1 = -1;
            Console.WriteLine("Конструктор B() сработал. Атрибут this.a = 20");
        }
        ~B() { Console.WriteLine("distructor ~Bush()"); }

        // Замещение + выполнение предыдущего кода в суперклассе
        // Расширение функции
        // Ключевое слово base используется для вызов метода супер-класса
        public override int fa()
        {
            Console.WriteLine("fa() сработал из класса В");
            base.fa();
            return a + 10;
        }
        // Расширение класса
        public int fb()
        {
            Console.WriteLine("class B fb()");
            return a + b + 10;
        }

        protected   int b { set; get; } // Атрибут виден наследникам B
        public      int b1 { set; get; }
    }

    //*************************************
    // Ключ. сл. abstract позволяет создавать неполные супер-классы, которые должны быть реализованы в классе-наследнике
    // Абстрактные классы могут определять абстрактные методы.
    // Абстрактные методы не имеют РЕАЛИЗАЦИИ, поэтому определение такого метода заканчивается 
    // точкой с запятой вместо обычного блока метода (они не имеют функционала). 
    // Классы-наследники должны реализовывать ВСЕ абстрактные методы. 
    abstract class C // Абстрактный класс, нельзя использовать для создания его объекта
    {
        //abstract public int fc();//нет тела
        abstract public int fc_1();
        abstract public int fc_2();
        public void print() { Console.WriteLine("class C print"); }//будет просто наследоваться
        public int Cc
        {
            set { c = value; }
            get { Console.WriteLine(" get С "); return c; }
        }

        protected int c = 1;
    }
    class E : C //!!! ВСЕ абстрактные члены абстр. суперкласса д.б. реализованы!!!
    {
        public E() { this.c = 22; }
        public override int fc_1() // Реализуем эту функцию из класса С
        {
            Console.WriteLine("class E fc1()");
            return c * 5;
        }
        public override int fc_2() // Реализуем эту функцию из класса С
        {
            Console.WriteLine("class E fc2()");
            return c / 2;
        }

    }

    class F : C
    {
        public F() { this.c = 1022; }
        public override int fc_1() // Реализуем эту функцию из класса С
        {
            Console.WriteLine("class F fc1()");
            return c * 15;

        }
        public override int fc_2() // Реализуем эту функцию из класса С
        {
            Console.WriteLine("class F fc2()");
            return c / 2 + 1000;

        }
    }
    interface J     // можно писать только функции, по опр все , атрибуты нельзы определять
    {               // Интерфейс наследуется только от интерфейса!!!!!!!!!
        int fj_1(); // Нельзя абстр!!! Виртуальные по определению
        int fj_2();
        //int fj() { return 0; } // оверрайт не пишем, такая сигнатура Error
        // public? недопустим?!!!!!?
        //public int a        { get; set; } // Error
    }

    class K : J
    {
        public K() { }
        public int fj_1() { return 10; }
        public int fj_2() { return 20; }

        //public int a { get; set; }

    }
    class D : B
    {
        public D() { }
        public override int fa()
        {
            Console.WriteLine("class B fa()");
            return a + 4;
        }
    }
    class V : B // Конструирование (ставим заглушки на любые функции) Принцип подстановки полностью соблюдается
    {
        public V() { base.fa(); }
        public override int fa() // чтобы убрать функционал, при переопределении убираем текст наслед. замещ. ф-и
        {
            //Console.WriteLine("Не используется");
            return 0;
        }
    }

    //************************************************************************************
    // Принцип подстановки сохраняется частично
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            //создаём обьект
            Console.WriteLine("a.fa() = {0}", a.fa());
            Console.ReadKey();
            Console.WriteLine();

            a = new B();
            Console.WriteLine("a.fa() = {0}", a.fa()); //Замещённая функция (вместо супер-класса становится функцией подкласса)
            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine("(B)a.fb() = {0}", ((B)a).fb()); //расширение по функциям
            //Console.WriteLine("(B)a.fb() = {0}", ((B)a).b1()); //расширение по атрибутам




            Console.ReadKey();
            Console.WriteLine();

            C c = null; //создаём ссылку на обьект  и подставляем под него обьект который мы пронаследовали 
            c = new E();
            c.Cc = 455;//поработали с обьектом суперкласса
            c.print();//пронаследовалась
            c.fc_1(); //замещение fc() класса С функцией fc() класса Е
            c.fc_2();


            Console.ReadKey();
            c = new F();//подставляем обьект F
            c.print();//пронаследовалась
            c.fc_1(); //замещение fc() класса С функцией fc() класса Е
            c.fc_2();
            //*************************))))))))))))))))

            Console.WriteLine("Шаг 2: интерфейс");
            Console.ReadKey();
            J j = null;
            j = new K(); // Подстановка объекта К
            Console.WriteLine($" j.fj_1() {j.fj_1()}");
            Console.WriteLine($" j.fj_2() {j.fj_2()}");

            Console.WriteLine(" Шаг 3: А интерфейс");/*
            c = new E();
            c.fa();
            Console.WriteLine(" a.fa() ", c.fa());
            c.fa();
            ((C)c).fc_1();

            Console.WriteLine(" Шаг 4");
            Console.ReadKey();
            c = new D();
            c.fa();
            c = new V();
            c.fa();
            ((C)c).fc_1();
            */



            Console.ReadKey();
        }
    }
}