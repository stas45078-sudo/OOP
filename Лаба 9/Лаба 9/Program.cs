using System;
using System.Globalization;
using System.Text;

public class Program
{
    private static PointArray pointArray;

    public static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        Console.OutputEncoding = Encoding.UTF8;

        string choice;
        do
        {
            choice = ConsoleUi.ReadChoice(@"Лабораторная работа №9 (Вариант 9)
1. Часть 1. Класс Point
2. Часть 2. Перегруженные операции
3. Часть 3. Класс-коллекция PointArray
4. Выход");
            switch (choice)
            {
                case "1":
                    RunPart1();
                    break;
                case "2":
                    RunPart2();
                    break;
                case "3":
                    RunPart3Menu();
                    break;
                case "4":
                    ConsoleUi.PrintMessage("Выход из программы");
                    break;
                default:
                    ConsoleUi.PrintMessage("Неверный ввод");
                    break;
            }

            Console.WriteLine();
        } while (choice != "4");
    }

    public static Point FindClosestToOrigin(PointArray points)
    {
        if (points.Length == 0)
            throw new ArgumentException("Массив пуст");

        var closestPoint = points[0];
        var minDistance = closestPoint.GetDistanceToOrigin();
        for (var i = 1; i < points.Length; i++)
        {
            var distance = points[i].GetDistanceToOrigin();
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = points[i];
            }
        }

        return closestPoint;
    }

    private static void RunPart1()
    {
        var defaultPoint = new Point();
        var parametricPoint = new Point(3, 4);
        var userPoint = ConsoleUi.ReadPoint("Введите координаты точки:");
        var demoPoints = new[] { defaultPoint, parametricPoint, userPoint };

        Console.WriteLine();
        for (var i = 0; i < demoPoints.Length; i++)
        {
            var point = demoPoints[i];
            ConsoleUi.PrintMessage($"Точка {i + 1}:");
            ConsoleUi.PrintPointAttributes(point);
            ConsoleUi.PrintNumber("  Расстояние до начала координат (статическая функция)", Point.GetDistanceToOrigin(point));
            ConsoleUi.PrintNumber("  Расстояние до начала координат (метод класса)", point.GetDistanceToOrigin());
        }

        ConsoleUi.PrintInteger("Создано объектов Point", Point.CreatedCount);
    }

    private static void RunPart2()
    {
        var point = ConsoleUi.ReadPoint("Введите точку p:");
        var otherPoint = ConsoleUi.ReadPoint("Введите точку Q:");
        var number = ConsoleUi.ReadInt("Введите целое число N: ");
        Console.WriteLine();

        ConsoleUi.PrintPoint("p", point);
        ConsoleUi.PrintPoint("Q", otherPoint);
        ConsoleUi.PrintInteger("N", number);
        Console.WriteLine();

        var decreasedPoint = point;
        decreasedPoint--;
        ConsoleUi.PrintPoint("p-- (x и y уменьшены на 1)", decreasedPoint);

        var swappedPoint = -point;
        ConsoleUi.PrintPoint("-p (x и y поменяны местами)", swappedPoint);

        int integerX = point;
        ConsoleUi.PrintInteger("(int)p неявно (целая часть x)", integerX);

        var coordinateY = (double)point;
        ConsoleUi.PrintNumber("(double)p явно (координата y)", coordinateY);

        ConsoleUi.PrintPoint("p - N (x уменьшен на N)", point - number);
        ConsoleUi.PrintPoint("N - p (y уменьшен на N)", number - point);
        ConsoleUi.PrintNumber("p - Q (расстояние между точками)", point - otherPoint);
        ConsoleUi.PrintInteger("Создано объектов Point", Point.CreatedCount);
    }

    private static void RunPart3Menu()
    {
        string choice;
        do
        {
            choice = ConsoleUi.ReadChoice(@"Класс-коллекция PointArray
1. Создать пустой массив
2. Создать массив случайных точек
3. Создать массив из точек, введённых с клавиатуры
4. Напечатать элементы массива
5. Найти точку, ближайшую к началу координат
6. Получить элемент по индексу
7. Показать количество созданных объектов
8. Назад");
            switch (choice)
            {
                case "1":
                    CreateEmptyArray();
                    break;
                case "2":
                    CreateRandomArray();
                    break;
                case "3":
                    CreateArrayFromInput();
                    break;
                case "4":
                    PrintArray();
                    break;
                case "5":
                    PrintClosestToOrigin();
                    break;
                case "6":
                    PrintElementByIndex();
                    break;
                case "7":
                    PrintCreatedCounts();
                    break;
                case "8":
                    break;
                default:
                    ConsoleUi.PrintMessage("Неверный ввод");
                    break;
            }

            Console.WriteLine();
        } while (choice != "8");
    }

    private static void CreateEmptyArray()
    {
        pointArray = new PointArray();
        ConsoleUi.PrintMessage("Создан пустой массив (конструктор без параметров):");
        pointArray.Print();
    }

    private static void CreateRandomArray()
    {
        var length = ConsoleUi.ReadInt("Введите количество точек: ");
        var minCoordinate = ConsoleUi.ReadDouble("Минимальное значение координат: ");
        var maxCoordinate = ConsoleUi.ReadDouble("Максимальное значение координат: ");

        try
        {
            pointArray = new PointArray(length, minCoordinate, maxCoordinate);
        }
        catch (ArgumentException ex)
        {
            ConsoleUi.PrintMessage($"Ошибка: {ex.Message}");
            return;
        }

        ConsoleUi.PrintMessage("Создан массив случайных точек:");
        pointArray.Print();
    }

    private static void CreateArrayFromInput()
    {
        var length = ConsoleUi.ReadInt("Введите количество точек: ");
        if (length < 0)
        {
            ConsoleUi.PrintMessage("Ошибка: Количество точек не может быть отрицательным");
            return;
        }

        pointArray = new PointArray(ConsoleUi.ReadPoints(length));
        ConsoleUi.PrintMessage("Создан массив из введённых точек:");
        pointArray.Print();
    }

    private static void PrintArray()
    {
        if (!IsPointArrayCreated())
            return;

        pointArray.Print();
    }

    private static void PrintClosestToOrigin()
    {
        if (!IsPointArrayCreated())
            return;

        if (pointArray.Length == 0)
        {
            ConsoleUi.PrintMessage("Массив пуст, искать нечего");
            return;
        }

        var closestPoint = FindClosestToOrigin(pointArray);
        ConsoleUi.PrintPoint("Ближайшая к началу координат точка", closestPoint);
        ConsoleUi.PrintNumber("Расстояние до начала координат", closestPoint.GetDistanceToOrigin());
    }

    private static void PrintElementByIndex()
    {
        if (!IsPointArrayCreated())
            return;

        var index = ConsoleUi.ReadInt("Введите индекс элемента: ");
        try
        {
            ConsoleUi.PrintPoint($"Элемент [{index}]", pointArray[index]);
        }
        catch (ArgumentException ex)
        {
            ConsoleUi.PrintMessage($"Ошибка: {ex.Message}");
        }
    }

    private static void PrintCreatedCounts()
    {
        ConsoleUi.PrintInteger("Создано объектов Point", Point.CreatedCount);
        ConsoleUi.PrintInteger("Создано объектов PointArray", PointArray.CreatedCount);
    }

    private static bool IsPointArrayCreated()
    {
        if (pointArray != null)
            return true;

        ConsoleUi.PrintMessage("Сначала создайте массив");
        return false;
    }
}

public static class ConsoleUi
{
    public static string ReadChoice(string menu)
    {
        Console.WriteLine(menu);
        Console.Write("Выберите пункт: ");
        var choice = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        return choice;
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out var number))
                return number;

            Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число");
        }
    }

    public static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = (Console.ReadLine() ?? string.Empty).Replace(',', '.');
            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out var number))
                return number;

            Console.WriteLine("Некорректный ввод. Пожалуйста, введите число");
        }
    }

    public static Point ReadPoint(string title)
    {
        Console.WriteLine(title);
        var x = ReadDouble("  x = ");
        var y = ReadDouble("  y = ");
        return new Point(x, y);
    }

    public static Point[] ReadPoints(int count)
    {
        var points = new Point[count];
        for (var i = 0; i < count; i++)
        {
            points[i] = ReadPoint($"Точка {i}:");
        }

        return points;
    }

    public static void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public static void PrintPoint(string title, Point point)
    {
        Console.WriteLine("{0}: {1}", title, point);
    }

    public static void PrintPointAttributes(Point point)
    {
        Console.WriteLine("  X = {0:F2}, Y = {1:F2}", point.X, point.Y);
    }

    public static void PrintNumber(string title, double value)
    {
        Console.WriteLine("{0}: {1:F4}", title, value);
    }

    public static void PrintInteger(string title, int value)
    {
        Console.WriteLine("{0}: {1}", title, value);
    }
}