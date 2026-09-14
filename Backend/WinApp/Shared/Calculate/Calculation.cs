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
        /// <param name="nu">вводит пользователь</param>
        /// <param name="d">вводит пользователь</param>
        /// <returns>Возвращает групповую скорость</returns>
        public double GetGroupSpeed(double n, double lambda, double nu, double d)
        {
            return CalculateGroupSpeed(n, lambda, nu, d);
        }

        /// <summary>
        /// Расчет групповой скорости по формуле
        /// </summary>
        /// <param name="n">Показатель преломления</param>
        /// <param name="lambda">Длина волны</param>
        /// <param name="nu">Частота</param>
        /// <param name="d">Производная</param>
        /// <returns>Групповая скорость</returns>
        private double CalculateGroupSpeed(double n, double lambda, double nu, double d)
        {
            var c = CalculateC();
            return c / (n + nu * CalculateFirstDerivative(n, lambda, nu, d));
        }

        /// <summary>
        /// Расчет скорости света в вакууме
        /// </summary>
        /// <returns>Скорость света в вакууме</returns>
        private double CalculateC()
        {
            return 3 * Math.Pow(10, -8);
        }

        /// <summary>
        /// Расчет первой производной показателя преломления по формуле
        /// </summary>
        /// <param name="n">Показатель преломления</param>
        /// <param name="lambda">Длина волны</param>
        /// <param name="nu">Частота</param>
        /// <param name="d">Производная</param>
        /// <returns>Первая производная</returns>
        private double CalculateFirstDerivative(double n, double lambda, double nu, double d)
        {
            return (d * n) / (d * lambda) * (CalculateSecondDerivative(lambda));
        }

        /// <summary>
        /// Расчет второй производной показателя преломления по формуле
        /// </summary>
        /// <param name="lambda">Длина волны</param>
        /// <returns>Вторая производная</returns>
        private double CalculateSecondDerivative(double lambda)
        {
            return -(Math.Pow(lambda, 2) / (2 * Math.PI * CalculateC()));
        }

        #endregion

        #region Lab3_3

        /// <summary>
        /// Расчет ДГС-2
        /// </summary>
        /// <returns>Возвращает ДГС-2</returns>
        public double GetDgs()
        {
            return 0;
        }

        #endregion

        #region Lab3_4

        /// <summary>
        /// Разложение в ряд Тейлора
        /// </summary>
        /// <param name="nu">вводит пользователь</param>
        /// <param name="nu0">вводит пользователь</param>
        /// <returns>Ряд Тейлора</returns>
        public double GetTaylorSeries(double nu, double nu0)
        {
            var taylorSeries = CalculateTaylorSeries(B0, B1, B2, nu, nu0);
            return taylorSeries;
        }

        /// <summary>
        /// Разложение в ряд Тейлора
        /// </summary>
        /// <param name="betta0">Коэффициент</param>
        /// <param name="betta1">Коэффициент</param>
        /// <param name="betta2">Коэффициент</param>
        /// <param name="nu">Частота</param>
        /// <param name="nu0">Частота</param>
        /// <returns>Значение ряда Тейлора</returns>
        private double CalculateTaylorSeries(double betta0, double betta1, double betta2, double nu, double nu0)
        {
            return betta0 + betta1 * (nu - nu0) + 0.5 * betta2 * Math.Pow(nu - nu0, 2);
        }

        /// <summary>
        /// Сравнение разложения в ряд Тейлора и ДГС-2
        /// </summary>
        /// <param name="taylorSeries">Ряд Тейлора</param>
        /// <param name="dgs">ДГС-2</param>
        /// <returns>True, если ряд Тейлора равен ДГС-2, иначе False</returns>
        public bool IsTaylorSeriesEqualsDgs(double taylorSeries, double dgs)
        {
            return false;
        }

        #endregion

        #endregion
    }
}