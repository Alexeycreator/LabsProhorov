using System;

namespace Shared.Calculate
{
    public sealed class Calculation
    {
        private const long Denominator = 1000000;
        private const double B1 = 0.696163;
        private const double B2 = 0.8974794;
        private const double B3 = 0.4079426;
        private const double Lambda1 = 0.0684043;
        private const double Lambda2 = 0.1162414;
        private const double Lambda3 = 9.896161;

        #region CalculateLab_1_2

        public double GetValueV(double a, double lambda, double n1, double n2)
        {
            var correctLambda = GetValueLambda(lambda);
            var correctA = GetValueA(a);
            var k0 = GetValueK0(correctLambda);
            return k0 * correctA * Math.Sqrt(n1 - n2);
        }

        public double GetValueP_t(double p0, double l, double lambda, double c)
        {
            return GetValueP0(p0) * Math.Pow(Math.E, -(GetValueA(lambda, c) * l));
        }

        public double GetValueLambda(double lambda)
        {
            return lambda / Denominator;
        }

        private double GetValueA(double a)
        {
            return a / Denominator;
        }

        private double GetValueP0(double p0)
        {
            return p0 / 1000;
        }

        private double GetValueA(double lambda, double C)
        {
            return C / (Math.Pow(lambda, 4) * 4343);
        }

        private double GetValueK0(double lambda)
        {
            return 2 * Math.PI / lambda;
        }

        #endregion

        #region CalculateLab3

        /// <summary>
        /// Возвращает показатель преломления
        /// </summary>
        /// <param name="lambda">лямбда, вводимая пользователем</param>
        /// <returns>показатель преломления</returns>
        public double GetRefractiveIndex(double lambda)
        {
            return CalculateRefractiveIndex(lambda);
        }

        public double GetGroupSpeed(double n, double lambda, double nu, double d)
        {
            return CalculateGroupSpeed(n, lambda, nu, d);
        }

        public double GetDgs()
        {
            return 0;
        }

        public double GetTaylorSeries(double betta0, double betta1, double betta2, double nu, double nu0)
        {
            var taylorSeries = CalculateTaylorSeries(betta0, betta1, betta2, nu, nu0);
            return taylorSeries;
        }

        public bool IsTaylorSeriesEqualsDgs(double taylorSeries, double dgs)
        {
            return false;
        }

        private double CalculateRefractiveIndex(double lambda)
        {
            var lambdaPow = Math.Pow(lambda, 2);
            return 1 + B1 * lambdaPow / (lambdaPow - Math.Pow(Lambda1, 2)) +
                   B2 * lambdaPow / (lambdaPow - Math.Pow(Lambda2, 2)) +
                   B3 * lambdaPow / (lambdaPow - Math.Pow(Lambda3, 2));
        }

        private double CalculateFirstDerivative(double n, double lambda, double nu, double d)
        {
            return (d * n) / (d * lambda) * (CalculateSecondDerivative(lambda));
        }

        private double CalculateC()
        {
            return 3 * Math.Pow(10, -8);
        }

        private double CalculateSecondDerivative(double lambda)
        {
            return -(Math.Pow(lambda, 2) / (2 * Math.PI * CalculateC()));
        }

        private double CalculateGroupSpeed(double n, double lambda, double nu, double d)
        {
            var c = CalculateC();
            return c / (n + nu * CalculateFirstDerivative(n, lambda, nu, d));
        }


        private double CalculateTaylorSeries(double betta0, double betta1, double betta2, double nu, double nu0)
        {
            return betta0 + betta1 * (nu - nu0) + 0.5 * betta2 * Math.Pow(nu - nu0, 2);
        }

        #endregion
    }
}