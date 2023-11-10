using System;

namespace OOP1LW
{
    public class CalculationClass
    {
        private static double F1(double x)
        {
            return 3 * Math.Pow(x, 5) - 1.0/Math.Tan(Math.PI * Math.Pow(x, 3));
        }
        private static double F2(double x)
        {
            return Math.Pow(x + 1, 0.3) + Math.Sin(2 * Math.Pow(x, 3));
        }
        private static double F3(double x)
        {
            return 5 * x / Math.Pow(x, 2);
        }
        public static double[,] Calculate(params double[] prms)
        {
            int rows = 100;
            rows = (int)(Math.Ceiling((prms[1] - prms[0]) / prms[2]) + 1);
            double[,] result = new double[rows, 2];
            int i = 0;
            for (double x = prms[0]; x <= prms[1]; x += prms[2])
            {
                result[i, 0] = x;
                if (x <= 0)
                {
                    result[i, 1] = F1(x);
                }
                else if (x > 0 && x <= prms[3])
                {
                    result[i, 1] = F2(x);
                }
                else
                {
                    result[i, 1] = F3(x);
                }
                if (x + prms[2] > prms[1] && x != prms[1])
                {
                    x = prms[1] - prms[2];
                }

                i++;
            }
            return result;
        }
    }
}