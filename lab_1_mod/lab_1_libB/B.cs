using System;

public class B
{
    private D d = null;
    private E e = null;
    public B(D d, E e)
    {
        this.d = d;
        this.e = e;

        Console.WriteLine("Сработал конструктор B");
    }
    ~B() {  }
    public void OpB()
    {
        Console.WriteLine("Сработал метод B");
    }
    public bool FunB()
    {
        Console.WriteLine("Сработала функция B");
        return true;
    }
    public D Bd
    {
        set { Console.WriteLine("set d"); d = value; }
        get { Console.Write("get d -> "); return d; }
    }
    public E Be
    {
        set { Console.WriteLine("set e"); e = value; }
        get { Console.Write("get e -> "); return e; }
    }
}