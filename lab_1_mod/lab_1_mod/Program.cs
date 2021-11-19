using System;

namespace lab_1_mod
{
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
            Console.WriteLine($"\nПЕЧАТЬ АТРИБУТА ДОСТУПА: {c.cq}\n");
            Console.WriteLine("ДОСТУП К МЕТОДАМ АГРЕГИРОВАННЫХ ОБЪЕКТОВ ПО ССЫЛКЕ:\n");
            Console.WriteLine("ВЫЗОВ ОПЕРАЦИЙ:\n");
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
