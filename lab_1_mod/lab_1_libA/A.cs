using System;

public class A
{
    private B b = null;
    private C c = null;
    private J j = null;
    public A(B b, C c, J j)
    {
        this.b = b;
        this.c = c;
        this.j = j;

        c.cq = 7;
        Console.WriteLine("Сработал конструктор A");
    }
    ~A() { }
    public void OpA()
    {
        Console.WriteLine("Сработала операция A");
    }
    public bool FunA()
    {
        Console.WriteLine("Сработала функция A");
        return true;
    }
    public B Ab
    {
        set { Console.WriteLine("set j"); b = value; }
        get { Console.Write("get b -> "); return b; }
    }
    public C Ac
    {
        set { Console.WriteLine("set j"); c = value; }
        get { Console.Write("get c -> "); return c; }
    }
    public J Aj
    {
        set { Console.WriteLine("set j"); j = value; }
        get { Console.Write("get j -> "); return j; }
    }
}
