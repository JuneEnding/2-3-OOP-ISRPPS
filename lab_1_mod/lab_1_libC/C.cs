using System;
public class C
{
    E e = null;
    F f = null;
    public C(E e, F f)
    {
        this.e = e;
        this.f = f;

        Console.WriteLine("Сработал конструктор C");
    }
    ~C() {  }
    public void OpC()
    {
        Console.WriteLine("Сработал метод C");
    }
    public bool FunC()
    {
        Console.WriteLine("Сработала функция C");
        return true;
    }
    public E Ce
    {
        set { Console.WriteLine("set e"); e = value; }
        get { Console.Write("get e -> "); return e; }
    }
    public F Cf
    {
        set { Console.WriteLine("set f"); f = value; }
        get { Console.Write("get f -> "); return f; }
    }
    // Атрибут доступа _из примера_
    // Примечание: делать атрибуты с модификатором доступа public чревато
    //             ошибками! Если нужно сделать атрибуты с возможностью
    //             доступа к ним, лучше делать это через свойства get и set!
    public int cq { get; set; }
}