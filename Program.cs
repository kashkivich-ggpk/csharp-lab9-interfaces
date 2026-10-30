using System;

interface Ix
{
    void IxF0(int x);
    void IxF1();
}

interface Iy
{
    void F0(int x);
    void F1();
}

interface Iz
{
    void F0(int x);
    void F1();
}

class TestClass : Ix, Iy, Iz
{
    private int w;

    public TestClass(int w)
    {
        this.w = w;
    }

    // Интерфейс Ix: x^3 - 10
    public void IxF0(int x)
    {
        Console.WriteLine($"Ix.IxF0({x}): x^3 - 10 = {x * x * x - 10}");
    }

    public void IxF1()
    {
        Console.WriteLine($"Ix.IxF1(), w = {w}: w^3 - 10 = {w * w * w - 10}");
    }

    // Неявная реализация: методы F0 и F1 одновременно подходят для Iy и Iz
    // (неоднозначность), для Iy они и используются: x^3 - x^2
    public void F0(int x)
    {
        Console.WriteLine($"F0({x}) (неявная, Iy): x^3 - x^2 = {x * x * x - x * x}");
    }

    public void F1()
    {
        Console.WriteLine($"F1(), w = {w} (неявная, Iy): w^3 - w^2 = {w * w * w - w * w}");
    }

    // Явная реализация интерфейса Iz: 2x - 3
    void Iz.F0(int x)
    {
        Console.WriteLine($"Iz.F0({x}) (явная): 2x - 3 = {2 * x - 3}");
    }

    void Iz.F1()
    {
        Console.WriteLine($"Iz.F1(), w = {w} (явная): 2w - 3 = {2 * w - 3}");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        TestClass t = new TestClass(4);

        Console.WriteLine("1. Вызов методов через объект класса TestClass:");
        t.IxF0(3);
        t.IxF1();
        t.F0(3);   // неявная реализация (Iy и Iz)
        t.F1();

        Console.WriteLine();
        Console.WriteLine("2. Вызов с явным приведением к типу интерфейса:");
        ((Ix)t).IxF0(3);
        ((Ix)t).IxF1();
        ((Iy)t).F0(3);
        ((Iy)t).F1();
        ((Iz)t).F0(3);
        ((Iz)t).F1();

        Console.WriteLine();
        Console.WriteLine("3. Вызов через интерфейсные ссылки:");
        Ix ix = t;
        Iy iy = t;
        Iz iz = t;
        ix.IxF0(5);
        ix.IxF1();
        iy.F0(5);
        iy.F1();
        iz.F0(5);
        iz.F1();
    }
}