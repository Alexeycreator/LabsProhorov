using System;

namespace Shared.Calculate
{
    public sealed class Calculation
    {
        private const long Denominator = 1000000;

        public double GetValueV(double a, double lambda, double n1, double n2)
        {
            var correctLambda = GetValueLambda(lambda);
            var correctA = GetValueA(a);
            var k0 = GetValueK0(correctLambda);
            return k0 * correctA * Math.Sqrt(n1 - n2);
        }

        public double GetValueP_t(double p0, double alpha, double l)
        {
            return p0 * Math.Pow(Math.E, -(alpha * l));
        }

        public double GetValueLambda(double lambda)
        {
            return lambda / Denominator;
        }

        private double GetValueA(double a)
        {
            return a / Denominator;
        }

        private double GetValueK0(double lambda)
        {
            return 2 * Math.PI / lambda;
        }
    }
}