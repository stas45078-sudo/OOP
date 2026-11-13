using System;

public class PointArray
{
    private static readonly Random RandomGenerator = new Random();
    private static int createdCount;
    private Point[] points;

    public static int CreatedCount => createdCount;

    public int Length => points.Length;

    public PointArray()
    {
        points = new Point[0];
        createdCount++;
    }

    public PointArray(int length, double minCoordinate, double maxCoordinate)
        : this()
    {
        if (length < 0)
            throw new ArgumentException("Длина массива не может быть отрицательной");

        if (minCoordinate > maxCoordinate)
            throw new ArgumentException("Минимальная граница не может быть больше максимальной");

        points = new Point[length];
        for (var i = 0; i < length; i++)
        {
            var x = GetRandomCoordinate(minCoordinate, maxCoordinate);
            var y = GetRandomCoordinate(minCoordinate, maxCoordinate);
            points[i] = new Point(x, y);
        }
    }

    public PointArray(Point[] sourcePoints)
        : this()
    {
        if (sourcePoints == null)
            throw new ArgumentException("Массив точек не задан");

        points = new Point[sourcePoints.Length];
        for (var i = 0; i < sourcePoints.Length; i++)
        {
            if (sourcePoints[i] == null)
                throw new ArgumentException("Точка не может быть пустой");

            points[i] = sourcePoints[i];
        }
    }

    public Point this[int index]
    {
        get
        {
            CheckIndex(index);
            return points[index];
        }
        set
        {
            CheckIndex(index);
            if (value == null)
                throw new ArgumentException("Точка не может быть пустой");

            points[index] = value;
        }
    }

    public void Print()
    {
        if (Length == 0)
        {
            Console.WriteLine("Массив пуст");
            return;
        }

        for (var i = 0; i < Length; i++)
        {
            Console.WriteLine("[{0}] {1}", i, points[i]);
        }
    }

    private static double GetRandomCoordinate(double minCoordinate, double maxCoordinate)
    {
        var coordinate = minCoordinate + RandomGenerator.NextDouble() * (maxCoordinate - minCoordinate);
        return Math.Round(coordinate, 2);
    }

    private void CheckIndex(int index)
    {
        if (index < 0 || index >= points.Length)
            throw new ArgumentException("Индекс выходит за пределы массива");
    }
}