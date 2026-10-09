using System;
using System.Globalization;

public class Program
{
    private const double Epsilon = 0.0001;
    private const int LastTermIndex = 3;
    private const double XStart = 0.1;
    private const double Xend = 0.8;
    private const int PointsCount = 10;

    public static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        var step = (Xend - XStart) / PointsCount;

        for (var i = 0; i < PointsCount; i++)
        {
            var x = XStart + i * step;
            var sumByTerms = ComputeSumByTermsCount(x);
            var sumByEps = ComputeSumByAccuracy(x);
            var exactValue = ComputeExactValue(x);
            Console.WriteLine("X={0:F2} SN={1:F4} SE={2:F4} Y={3:F4}", x, sumByTerms, sumByEps, exactValue);
        }
    }

    private static double ComputeSumByTermsCount(double x)
    {
        var phi = x;
        var psiConstantPart = x * x * x * x;
        var c = 1.0;
        var sum = phi * c;

        for (var n = 1; n <= LastTermIndex; n++)
        {
            var psi = psiConstantPart * (4.0 * n - 3.0) / (4.0 * n + 1.0);
            c *= psi;
            sum += phi * c;
        }
        return sum;
    }

    private static double ComputeSumByAccuracy(double x)
    {
        var phi = x;
        var psiConstantPart = x * x * x * x;
        var c = 1.0;
        var sum = phi * c;
        var n = 1;

        while (true)
        {
            var psi = psiConstantPart * (4.0 * n - 3.0) / (4.0 * n + 1.0);
            c *= psi;
            var term = phi * c;

            if (Math.Abs(term) < Epsilon)
                break;

            sum += term;
            n++;
        }
        return sum;
    }

    private static double ComputeExactValue(double x)
    {
        return 0.25 * Math.Log((1.0 + x) / (1.0 - x)) + 0.5 * Math.Atan(x);
    }
}