using System;
public class J
{
    public J() { Console.WriteLine("Сработал конструктор J");  }
    ~J() { }
    public void OpJ()
    {
        Console.WriteLine("Сработал метод J");
    }
    public bool FunJ()
    {
        Console.WriteLine("Сработала функция J");
        return true;
    }

}
