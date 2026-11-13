using System;

public class Point
{
    private static int createdCount;
    private double x;
    private double y;

    public static int CreatedCount => createdCount;

    public double X
    {
        get => x;
        set => x = value;
    }

    public double Y
    {
        get => y;
        set => y = value;
    }

    public Point()
        : this(0, 0)
    {
    }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
        createdCount++;
    }

    public static double GetDistanceToOrigin(Point point)
    {
        return GetDistance(point.X, point.Y, 0, 0);
    }

    public double GetDistanceToOrigin()
    {
        return GetDistance(X, Y, 0, 0);
    }

    public override string ToString()
    {
        return $"({X:F2}; {Y:F2})";
    }

    public static Point operator --(Point point)
    {
        return new Point(point.X - 1, point.Y - 1);
    }

    public static Point operator -(Point point)
    {
        return new Point(point.Y, point.X);
    }

    public static implicit operator int(Point point)
    {
        return (int)point.X;
    }

    public static explicit operator double(Point point)
    {
        return point.Y;
    }

    public static Point operator -(Point point, int number)
    {
        return new Point(point.X - number, point.Y);
    }

    public static Point operator -(int number, Point point)
    {
        return new Point(point.X, point.Y - number);
    }

    public static double operator -(Point left, Point right)
    {
        return GetDistance(left.X, left.Y, right.X, right.Y);
    }

    private static double GetDistance(double firstX, double firstY, double secondX, double secondY)
    {
        var deltaX = firstX - secondX;
        var deltaY = firstY - secondY;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }
}