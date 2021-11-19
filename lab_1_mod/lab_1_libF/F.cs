using System;
public class F
{
    public F() { Console.WriteLine("Сработал конструктор F"); }
    ~F() { }
    public void OpF()
    {
        Console.WriteLine("Сработал метод F");
    }
    public bool FunF()
    {
        Console.WriteLine("Сработала функция F");
        return true;
    }

}