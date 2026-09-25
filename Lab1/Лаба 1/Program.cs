using System;
using System.Globalization;

public class Program
{
    private const double A = 1000.0;
    private const double B = 0.0001;

    public static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        Console.WriteLine("Task 1");
        SolveTask1();

        Console.WriteLine("\nTask 2");
        SolveTask2();

        Console.WriteLine("\nTask 3");
        SolveTask3();
    }

    private static void SolveTask1()
    {
        var initialM = ReadInt("m?");
        var initialN = ReadInt("n?");

        var m = initialM;
        var n = initialN;
        var quotient = 0;
        if (n == 0)
            Console.WriteLine("Нельзя вычислить");
        else
        {
            quotient = m++ / n--;
            Console.WriteLine("m={0} n={1} m++/n--={2}", m, n, quotient);
        }

        m = initialM;
        n = initialN;
        var isLessThanDecremented = ++m < n--;
        Console.WriteLine("m={0} n={1} ++m<n--={2}", m, n, isLessThanDecremented);

        m = initialM;
        n = initialN;
        var isNDecrementedGreater = n-- > m;
        Console.WriteLine("m={0} n={1} n-->m={2}", m, n, isNDecrementedGreater);

        var x = ReadDouble("x?");
        var underRoot = x * x + x * x * x;
        var fifthRoot = Math.Sign(underRoot) * Math.Pow(Math.Abs(underRoot), 1.0 / 5.0);
        var formulaValue = Math.Sin(Math.Pow(x, 3)) + Math.Pow(x, 4) + fifthRoot;
        Console.WriteLine("x={0} f(x)={1}", x, formulaValue);
    }

    /// <summary>
    /// Задача 2: точка (x, y) должна лежать в треугольнике с вершинами (-2, 0), (2, 0), (0, 2).
    /// </summary>
    private static void SolveTask2()
    {
        var pointX = ReadDouble("x?");
        var pointY = ReadDouble("y?");
        var isInside = Math.Abs(pointX) <= 2 && pointY >= 0 && pointY <= 2 - Math.Abs(pointX);
        PrintTask2Result(pointX, pointY, isInside);
    }

    private static void PrintTask2Result(double x, double y, bool isInside)
    {
        Console.WriteLine("Point ({0}, {1}) is in area: {2}", x, y, isInside);
    }

    /// <summary>
    /// Задача 3: сравнение точности float и double.
    /// </summary>
    private static void SolveTask3()
    {
        Console.WriteLine("Result for float: {0}", ComputeFormulaAsFloat());
        Console.WriteLine("Result for double: {0}", ComputeFormulaAsDouble());
    }

    /// <summary>
    /// ((a+b)^3 - (a^3 + 3ab^2)) / (3a^2b + b^3) в типе float.
    /// Теоретически результат равен 1, но float даёт погрешность.
    /// </summary>
    private static float ComputeFormulaAsFloat()
    {
        var a = (float)A;
        var b = (float)B;
        var numerator = (float)Math.Pow(a + b, 3) - ((float)Math.Pow(a, 3) + 3.0f * a * (float)Math.Pow(b, 2));
        var denominator = 3.0f * (float)Math.Pow(a, 2) * b + (float)Math.Pow(b, 3);
        return numerator / denominator;
    }

    /// <summary> Та же формула в типе double — более точный результат.</summary>
    private static double ComputeFormulaAsDouble()
    {
        var numerator = Math.Pow(A + B, 3) - (Math.Pow(A, 3) + 3.0 * A * Math.Pow(B, 2));
        var denominator = 3.0 * Math.Pow(A, 2) * B + Math.Pow(B, 3);
        return numerator / denominator;
    }

    /// <summary> Считывает целое число с подсказкой.</summary>
    private static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        return int.Parse(Console.ReadLine()!);
    }

    /// <summary> Считывает вещественное число с подсказкой.</summary>
    private static double ReadDouble(string prompt)
    {
        Console.Write(prompt);
        return double.Parse(Console.ReadLine()!);
    }
}