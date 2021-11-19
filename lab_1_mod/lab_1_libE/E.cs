using System;
public class E
{
    public E() { Console.WriteLine("Сработал конструктор E"); }
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