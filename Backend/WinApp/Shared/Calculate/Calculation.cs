using System;

namespace Shared.Calculate
{
    public sealed class Calculation
    {
        private const long Denominator = 1000000;
        private const double B0 = 0.1162414;
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

        #region Lab3_1

        /// <summary>
        /// Расчет показателя преломления
        /// </summary>
        /// <param name="lambda">лямбда, вводимая пользователем</param>
        /// <returns>показатель преломления</returns>
        public double GetRefractiveIndex(double lambda)
        {
            return Math.Sqrt(CalculateRefractiveIndex(lambda));
        }

        /// <summary>
        /// Расчет показателя преломления по формуле Селлмейера
        /// </summary>
        /// <param name="lambda">Значение, введенное пользователем</param>
        /// <returns>Возвращает значение по формуле Селлмейера</returns>
        private double CalculateRefractiveIndex(double lambda)
        {
            var lambdaPow = Math.Pow(lambda, 2);
            return 1 + B1 * lambdaPow / (lambdaPow - Math.Pow(Lambda1, 2)) +
                   B2 * lambdaPow / (lambdaPow - Math.Pow(Lambda2, 2)) +
                   B3 * lambdaPow / (lambdaPow - Math.Pow(Lambda3, 2));
        }

        #endregion

        #region Lab3_2

        /// <summary>
        /// Расчет групповой скорости
        /// </summary>
        /// <param name="n">вводит пользователь</param>
        /// <param name="lambda">вводит пользователь</param>
        /// <param name="d">вводит пользователь</param>
        /// <returns>Возвращает групповую скорость</returns>
        public double GetGroupSpeed(double n, double lambda, double d)
        {
            return CalculateGroupSpeed(n, lambda, d);
        }

        /// <summary>
        /// Расчет групповой скорости по формуле
        /// </summary>
        /// <param name="n">Показатель преломления</param>
        /// <param name="lambda">Длина волны</param>
        /// <param name="d">Производная</param>
        /// <returns>Групповая скорость</returns>
        private double CalculateGroupSpeed(double n, double lambda, double d)
        {
            var c = CalculateC();
            var omega = CalculateOmega(lambda);
            var dNdOmega = CalculateFirstDerivative(n, lambda, d);
            return c / (n + omega * dNdOmega);
        }

        /// <summary>
        /// Расчет скорости света в вакууме
        /// </summary>
        /// <returns>Скорость света в вакууме</returns>
        private double CalculateC()
        {
            return 3 * Math.Pow(10, 8);
        }

        /// <summary>
        /// Расчет круговой частоты (омега)
        /// </summary>
        /// <returns>Круговая частота</returns>
        private double CalculateOmega(double lambda)
        {
            var c = CalculateC();
            return 2 * Math.PI * c / lambda;
        }

        /// <summary>
        /// Расчет производной показателя преломления по длине волны
        /// </summary>
        /// <param name="n">Показатель преломления</param>
        /// <param name="lambda">Длина волны</param>
        /// <param name="d">Производная</param>
        /// <returns>Показатель преломления</returns>
        private double CalculateFirstDerivative(double n, double lambda, double d)
        {
            var wavelengthByFrequency = CalculateSecondDerivative(lambda);
            return (d * n) / (d * lambda) * wavelengthByFrequency;
        }

        /// <summary>
        /// Расчет производной длины волны по частоте
        /// </summary>
        /// <param name="lambda">Длина волны</param>
        /// <returns>Длина волны</returns>
        private double CalculateSecondDerivative(double lambda)
        {
            var c = CalculateC();
            return -(Math.Pow(lambda, 2) / (2 * Math.PI * c));
        }

        #endregion

        #region Lab3_3

        /// <summary>
        /// Расчет ДГС-2
        /// </summary>
        /// <returns>Возвращает ДГС-2</returns>
        public double GetDgs(double lambda0, double lambda, double nLambda, double dNdOmega)
        {
            var c = CalculateC();
            var b2 = CalculateB2(c, dNdOmega, nLambda, CalculateOmega(lambda));
            return b2 * Math.Pow(10, 5);
        }

        private double CalculateSecondDerivative(double d, double n, double omega)
        {
            return Math.Pow(d, 2) * n / (d * Math.Pow(omega, 2));
        }

        private double CalculateB2(double c, double d, double n, double omega)
        {
            var d2NdOmega2 = CalculateSecondDerivative(d, n, omega);
            return 1 / c * (2 * (d * n / (d * omega)) + omega * d2NdOmega2);
        }

        #endregion

        #region Lab3_4

        /// <summary>
        /// Разложение в ряд Тейлора
        /// </summary>
        /// <param name="omega0">Угловая частота</param>
        /// <returns>Ряд Тейлора</returns>
        public double GetTaylorSeries(double omega0)
        {
            var omega = CalculateOmega(Lambda1);
            var c = CalculateC();
            var b0 = CalculateB0(GetRefractiveIndex(Lambda1), Lambda1, omega0);
            var b1 = CalculateB1();
            var b2 = CalculateB2(c, CalculateFirstDerivative(GetRefractiveIndex(Lambda1), Lambda1, 0),
                GetRefractiveIndex(Lambda1), omega);
            var taylorSeries = CalculateTaylorSeries(b0, b1, b2, omega, omega0);
            return taylorSeries;
        }

        private double CalculateB0(double n, double lambda0, double omega0)
        {
            var c = CalculateC();
            return n * lambda0 * omega0 / c;
        }

        private double CalculateB1()
        {
            var groupSpeed = GetGroupSpeed(GetRefractiveIndex(Lambda1), Lambda1,
                CalculateFirstDerivative(GetRefractiveIndex(Lambda1), Lambda1, 0));
            return 1 / groupSpeed;
        }

        /// <summary>
        /// Разложение в ряд Тейлора
        /// </summary>
        /// <param name="betta0">Коэффициент</param>
        /// <param name="betta1">Коэффициент</param>
        /// <param name="betta2">Коэффициент</param>
        /// <param name="omega">Угловая частота</param>
        /// <param name="omega0">Угловая частота</param>
        /// <returns>Значение ряда Тейлора</returns>
        private double CalculateTaylorSeries(double betta0, double betta1, double betta2, double omega, double omega0)
        {
            return betta0 + betta1 * (omega - omega0) + 0.5 * betta2 * Math.Pow(omega - omega0, 2);
        }

        /// <summary>
        /// Сравнение разложения в ряд Тейлора и ДГС-2
        /// </summary>
        /// <returns>True, если ряд Тейлора равен ДГС-2, иначе False</returns>
        public bool IsTaylorSeriesEqualsDgs()
        {
            var taylorSeries = GetTaylorSeries(CalculateOmega(Lambda1));
            var dgs2 = GetDgs(Lambda1, Lambda1, GetRefractiveIndex(Lambda1),
                CalculateFirstDerivative(GetRefractiveIndex(Lambda1), Lambda1, 0));
            if (Math.Abs(taylorSeries - dgs2) < 0.0001)
                return true;
            return false;
        }

        #endregion

        #endregion
    }
}