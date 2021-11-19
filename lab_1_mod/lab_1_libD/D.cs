using System;
public class D
{
    public D() { Console.WriteLine("Сработал конструктор D"); }
    ~D() { }
    public void OpD()
    {
        Console.WriteLine("Сработал метод D");
    }
    public bool FunD()
    {
        Console.WriteLine("Сработала функция D");
        return true;
    }

}