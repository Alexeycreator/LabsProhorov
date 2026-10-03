using System;
using System.Numerics;
using Shared.Models;

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
        private const double Pi = Math.PI;
        private const double WindowFactor = 20.0;

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
            return 2 * Pi / lambda;
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
            return 2 * Pi * c / lambda;
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
            return -(Math.Pow(lambda, 2) / (2 * Pi * c));
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

        #region CalculateLab4

        // нужна для проверки числа на степень двойки, так как в лабораторной работе 4 нужно проверять, является ли число степенью двойки
        public bool IsPowerOfTwo(int n)
        {
            return n > 0 && (n & (n - 1)) == 0;
        }

        #region 4_1

        /// <summary>
        /// Часть 1: дисперсионная длина + временная и частотная сетки.
        /// Вызывается напрямую с формы.
        /// </summary>
        /// <param name="beta2">Параметр из формы</param>
        /// <param name="t0">Параметр из формы</param>
        /// <param name="n">Число точек N</param>
        /// <param name="m">Параметр M (зарезервирован)</param>
        /// <returns>
        /// ld — дисперсионная длина,
        /// t  — временная сетка T[],
        /// w  — частотная сетка w[],
        /// dT — шаг по времени,
        /// dw — шаг по частоте
        /// </returns>
        public (double ld, double[] t, double[] w, double dT, double dw)
            CalculatePart1(double beta2, double t0, int n, int m)
        {
            // 1. Дисперсионная длина
            double ld = DispersionLength(t0, beta2);

            // 2. Временная и частотная сетки
            BuildGrids(t0, n, out double[] t, out double[] w, out double dT, out double dw);

            return (ld, t, w, dT, dw);
        }

        /// <summary>
        /// Расчёт дисперсионной длины
        /// </summary>
        private double DispersionLength(double t0, double beta2)
        {
            return Math.Pow(t0, 2) / Math.Abs(beta2);
        }

        /// <summary>
        /// Построение временной и частотной сеток
        /// </summary>
        private void BuildGrids(double t0, int n,
            out double[] t, out double[] w,
            out double dT, out double dw)
        {
            // Временное окно
            double tWindow = WindowFactor * t0;

            // Шаги
            dT = tWindow / n;
            dw = 2.0 * Pi / (n * dT);

            // Выделение памяти
            t = new double[n];
            w = new double[n];

            // Заполнение сеток
            for (int j = 0; j < n; j++)
            {
                t[j] = (j - n / 2.0) * dT;
                w[j] = (j - n / 2.0) * dw;
            }
        }

        #endregion

        #region 4_2

        /// <summary>
        /// Часть 2: формирование начального импульса U0(T) и массива интенсивности I_input.
        /// Комплексное число представлено парой массивов: действительная (URe) и мнимая (UIm) части.
        /// </summary>
        /// <param name="t0">Параметр T0</param>
        /// <param name="t">Временная сетка T[] из части 1</param>
        /// <param name="n">Число точек N</param>
        /// <param name="uRe">Действительная часть U[] (для LAB-51)</param>
        /// <param name="uIm">Мнимая часть U[] (для LAB-51)</param>
        /// <param name="iInput">Интенсивность I_input[] = |U0(T)|^2 (для LAB-54)</param>
        public void CalculatePart2(double t0, double[] t, int n,
            out double[] uRe, out double[] uIm, out double[] iInput)
        {
            // Выделение памяти
            uRe = new double[n];
            uIm = new double[n];
            iInput = new double[n];

            for (int j = 0; j < n; j++)
            {
                // U0(T) = exp(-T^2 / (2*T0^2))
                double u0 = U0(t[j], t0);

                // Начальный импульс: мнимая часть = 0
                uRe[j] = u0;
                uIm[j] = 0.0;

                // Интенсивность |U0|^2 = Re^2 + Im^2
                iInput[j] = uRe[j] * uRe[j] + uIm[j] * uIm[j];
            }
        }

        /// <summary>
        /// Функция начального импульса U0(T) = exp(-T^2 / (2*T0^2))
        /// </summary>
        /// <param name="t">Текущее время</param>
        /// <param name="t0">Параметр T0</param>
        /// <returns>Значение U0(T)</returns>
        private double U0(double t, double t0)
        {
            return Math.Exp(-t * t / (2.0 * t0 * t0));
        }

        #endregion

        #region 4_3

        /// <summary>
        /// Часть 3: SSFM-распространение импульса (только дисперсия).
        /// Собственная реализация БПФ (radix-2 Cooley–Tukey), комплексные числа — парами Re/Im.
        /// </summary>
        /// <param name="uRe">Действительная часть U[] из LAB-50</param>
        /// <param name="uIm">Мнимая часть U[] из LAB-50</param>
        /// <param name="w">Частотная сетка w[] из LAB-49</param>
        /// <param name="beta2">Параметр β₂</param>
        /// <param name="ld">Дисперсионная длина L_D</param>
        /// <param name="L">Длина волокна (z = L_D по условию)</param>
        /// <param name="n">Число точек N (должно быть степенью 2)</param>
        /// <param name="iOutputNum">Выходная интенсивность I_output_num[] = |U(L,T)|²</param>
        public void CalculatePart3(double[] uRe, double[] uIm, double[] w,
            double beta2, double ld, double L, int n,
            out double[] iOutputNum)
        {
            // --- Число шагов SSFM и шаг по z ---
            int M = 1; // по условию z = L_D — достаточно одного шага
            double h = L / M;

            // --- Копии Re/Im для FFT ---
            double[] re = new double[n];
            double[] im = new double[n];
            Array.Copy(uRe, re, n);
            Array.Copy(uIm, im, n);

            // --- SSFM-цикл ---
            for (int step = 0; step < M; step++)
            {
                // 1. Прямое БПФ
                Fft(re, im, n, inverse: false);

                // 2. Дисперсионный оператор: U_hat[k] *= exp(i * 0.5 * beta2 * w[k]^2 * h)
                for (int k = 0; k < n; k++)
                {
                    double phase = 0.5 * beta2 * w[k] * w[k] * h;
                    double cosP = Math.Cos(phase);
                    double sinP = Math.Sin(phase);

                    // (a + i b) * (cosP + i sinP) = (a*cosP - b*sinP) + i (a*sinP + b*cosP)
                    double a = re[k], b = im[k];
                    re[k] = a * cosP - b * sinP;
                    im[k] = a * sinP + b * cosP;
                }

                // 3. Обратное БПФ
                Fft(re, im, n, inverse: true);
            }

            // --- Интенсивность на выходе ---
            iOutputNum = new double[n];
            for (int j = 0; j < n; j++)
                iOutputNum[j] = re[j] * re[j] + im[j] * im[j];
        }

        /// <summary>
        /// Прямое/обратное БПФ radix-2 (Cooley–Tukey), in-place.
        /// N должно быть степенью 2.
        /// </summary>
        /// <param name="re">Действительная часть (вход/выход)</param>
        /// <param name="im">Мнимая часть (вход/выход)</param>
        /// <param name="n">Размер массива (степень 2)</param>
        /// <param name="inverse">true — обратное БПФ с нормировкой 1/N</param>
        private void Fft(double[] re, double[] im, int n, bool inverse)
        {
            // Проверка на степень 2
            if ((n & (n - 1)) != 0)
                throw new ArgumentException("N должно быть степенью 2");

            // --- 1. Перестановка битов (bit-reversal) ---
            for (int i = 1, j = 0; i < n; i++)
            {
                int bit = n >> 1;
                for (; (j & bit) != 0; bit >>= 1)
                    j ^= bit;
                j ^= bit;

                if (i < j)
                {
                    (re[i], re[j]) = (re[j], re[i]);
                    (im[i], im[j]) = (im[j], im[i]);
                }
            }

            // --- 2. Бабочки ---
            double sign = inverse ? +1.0 : -1.0; // для обратного знак меняется
            for (int len = 2; len <= n; len <<= 1)
            {
                double ang = 2.0 * Math.PI / len * sign;
                double wRe = Math.Cos(ang);
                double wIm = Math.Sin(ang);

                for (int i = 0; i < n; i += len)
                {
                    double curRe = 1.0;
                    double curIm = 0.0;

                    for (int j = 0; j < len / 2; j++)
                    {
                        int a = i + j;
                        int b = i + j + len / 2;

                        // t = cur * u[b]
                        double tRe = curRe * re[b] - curIm * im[b];
                        double tIm = curRe * im[b] + curIm * re[b];

                        // u[b] = u[a] - t
                        re[b] = re[a] - tRe;
                        im[b] = im[a] - tIm;

                        // u[a] = u[a] + t
                        re[a] += tRe;
                        im[a] += tIm;

                        // cur *= w
                        double newCurRe = curRe * wRe - curIm * wIm;
                        double newCurIm = curRe * wIm + curIm * wRe;
                        curRe = newCurRe;
                        curIm = newCurIm;
                    }
                }
            }

            // --- 3. Нормировка при обратном БПФ ---
            if (inverse)
            {
                for (int i = 0; i < n; i++)
                {
                    re[i] /= n;
                    im[i] /= n;
                }
            }
        }

        #endregion

        #region 4_4

        /// <summary>
        /// Часть 4: аналитическое решение (формула 6 методички).
        /// U_anal(T) = (T0² / (T0² − i·β₂·z)) · exp( −T² / (2·(T0² − i·β₂·z)) ),  z = L_D.
        /// </summary>
        /// <param name="t0">Параметр T0 из LAB-49</param>
        /// <param name="beta2">Параметр β₂ из LAB-49</param>
        /// <param name="ld">Дисперсионная длина L_D из LAB-49</param>
        /// <param name="t">Временная сетка T[] из LAB-49</param>
        /// <param name="n">Число точек N</param>
        /// <param name="iOutputAnal">Выходная интенсивность I_output_anal[] = |U_anal(T)|²</param>
        public void CalculatePart4(double t0, double beta2, double ld,
            double[] t, int n,
            out double[] iOutputAnal)
        {
            double z = ld; // z = L_D
            double t0Sq = t0 * t0;

            // denom = T0² − i·β₂·z
            double denomRe = t0Sq;
            double denomIm = -beta2 * z;

            // Модуль denom в квадрате: |denom|² = denomRe² + denomIm²
            double denomAbsSq = denomRe * denomRe + denomIm * denomIm;

            // prefactor = T0² / denom = T0² · conj(denom) / |denom|²
            double prefRe = t0Sq * denomRe / denomAbsSq;
            double prefIm = -t0Sq * denomIm / denomAbsSq; // (a/bi -> -bi)

            iOutputAnal = new double[n];

            for (int j = 0; j < n; j++)
            {
                double T = t[j];
                double tSq = T * T;

                // arg = −T² / (2·denom) — комплексное число
                // 1/(2·denom) = conj(denom) / (2·|denom|²)
                double inv2DenomRe = denomRe / (2.0 * denomAbsSq);
                double inv2DenomIm = -denomIm / (2.0 * denomAbsSq);

                // arg = −T² · (inv2DenomRe + i·inv2DenomIm)
                double argRe = -tSq * inv2DenomRe;
                double argIm = -tSq * inv2DenomIm;

                // exp(arg) = e^argRe · (cos(argIm) + i·sin(argIm))
                double expRe = Math.Exp(argRe) * Math.Cos(argIm);
                double expIm = Math.Exp(argRe) * Math.Sin(argIm);

                // U_anal = prefactor · exp(arg)
                double uRe = prefRe * expRe - prefIm * expIm;
                double uIm = prefRe * expIm + prefIm * expRe;

                // I_output_anal = |U_anal|²
                iOutputAnal[j] = uRe * uRe + uIm * uIm;
            }
        }

        #endregion

        #region 4_5

        /// <summary>
        /// Часть 5: относительная ошибка между численным и аналитическим решениями,
        /// а также FWHM входного и выходного импульсов.
        /// </summary>
        /// <param name="iInput">I_input[] из LAB-50</param>
        /// <param name="iOutputNum">I_output_num[] из LAB-51</param>
        /// <param name="iOutputAnal">I_output_anal[] из LAB-52</param>
        /// <param name="t">Временная сетка T[] из LAB-49</param>
        /// <param name="n">Число точек N</param>
        /// <param name="error">Относительная ошибка</param>
        /// <param name="fwhmInput">FWHM входного импульса</param>
        /// <param name="fwhmOutput">FWHM выходного импульса</param>
        public void CalculatePart5(double[] iInput, double[] iOutputNum, double[] iOutputAnal,
            double[] t, int n,
            out double error, out double fwhmInput, out double fwhmOutput)
        {
            // --- 1. Относительная ошибка ---
            double maxDiff = 0.0;
            double maxAnal = 0.0;

            for (int j = 0; j < n; j++)
            {
                double diff = Math.Abs(iOutputNum[j] - iOutputAnal[j]);
                if (diff > maxDiff) maxDiff = diff;

                if (iOutputAnal[j] > maxAnal) maxAnal = iOutputAnal[j];
            }

            error = (maxAnal > 0.0) ? maxDiff / maxAnal : 0.0;

            // --- 2. FWHM входного и выходного импульсов ---
            fwhmInput = Fwhm(iInput, t, n);
            fwhmOutput = Fwhm(iOutputNum, t, n);
        }

        /// <summary>
        /// Расчёт FWHM (ширина по уровню 0.5 от максимума).
        /// </summary>
        /// <param name="i">Массив интенсивности</param>
        /// <param name="t">Временная сетка</param>
        /// <param name="n">Число точек</param>
        /// <returns>FWHM = T[right] - T[left], либо 0, если уровень 0.5 не пересекается</returns>
        private double Fwhm(double[] i, double[] t, int n)
        {
            // 1. Максимум
            double max = i[0];
            int iMax = 0;
            for (int j = 1; j < n; j++)
            {
                if (i[j] > max)
                {
                    max = i[j];
                    iMax = j;
                }
            }

            double half = 0.5 * max;

            // 2. Левая граница — идём влево от максимума, ищем первое пересечение уровня half
            int left = iMax;
            for (int j = iMax; j >= 1; j--)
            {
                if (i[j] >= half && i[j - 1] < half)
                {
                    left = j;
                    break;
                }
            }

            // Если уровень half не пересекается (импульс шире окна) — left = 0
            if (i[0] >= half) left = 0;

            // 3. Правая граница — идём вправо от максимума
            int right = iMax;
            for (int j = iMax; j < n - 1; j++)
            {
                if (i[j] >= half && i[j + 1] < half)
                {
                    right = j;
                    break;
                }
            }

            // Если уровень half не пересекается — right = n-1
            if (i[n - 1] >= half) right = n - 1;

            // 4. FWHM = T[right] - T[left]
            return t[right] - t[left];
        }

        #endregion

        #region 4_6

        /// <summary>
        /// Полный расчёт LAB-49…LAB-53.
        /// Возвращает объект Lab4Result со всеми массивами и скалярными величинами.
        /// </summary>
        public Lab4Result CalculatePart6(double beta2, double t0, int n, int m)
        {
            var result = new Lab4Result();

            // --- LAB-49: сетки T[], w[], дисперсионная длина LD ---
            var (ld, T, w, dT, dw) = CalculatePart1(beta2, t0, n, m);
            result.LD = ld;
            result.T = T;

            // --- LAB-50: начальный импульс U[] (Re/Im) и I_input[] ---
            CalculatePart2(t0, T, n,
                out double[] uRe,
                out double[] uIm,
                out double[] iInput);
            result.I_input = iInput;

            // --- LAB-51: численное SSFM-распространение ---
            CalculatePart3(uRe, uIm, w, beta2, ld, ld, n,
                out double[] iOutputNum);
            result.I_output_num = iOutputNum;

            // --- LAB-52: аналитическое решение ---
            CalculatePart4(t0, beta2, ld, T, n,
                out double[] iOutputAnal);
            result.I_output_anal = iOutputAnal;

            // --- LAB-53: относительная ошибка и FWHM ---
            CalculatePart5(iInput, iOutputNum, iOutputAnal, T, n,
                out double error,
                out double fwhmIn,
                out double fwhmOut);
            result.Error = error;
            result.FWHM_input = fwhmIn;
            result.FWHM_output = fwhmOut;

            return result;
        }

        #endregion

        #endregion

        #region CalculateLab5

        #region 5_1

        /// <summary>
        /// LAB-67: расчёт характерных длин, порядка солитона,
        /// временной/частотной сеток и шага по z.
        /// </summary>
        /// <returns>
        /// t         — временная сетка T[], пс;
        /// w         — частотная сетка w[], рад/пс;
        /// ld        — дисперсионная длина L_D, км;
        /// lnl       — нелинейная длина L_NL, км;
        /// nSoliton  — порядок солитона N_soliton;
        /// h         — шаг по z, км
        /// </returns>
        public (double[] t, double[] w, double ld, double lnl, double nSoliton, double h)
            CalculatePart1Lab5(double beta2, double gamma, double t0, double p0,
                double l, int n, int m)
        {
            // --- 1. Характерные длины ---
            double ld = DispersionLengthLab5(t0, beta2);
            double lnl = NonlinearLengthLab5(gamma, p0);

            // --- 2. Порядок солитона ---
            double nSoliton = SolitonOrderLab5(ld, lnl);

            // --- 3. Временная и частотная сетки ---
            BuildGridsLab5(t0, n, out double[] t, out double[] w, out double dT, out double dW);
            _ = dT;
            _ = dW;

            // --- 4. Шаг по z ---
            double h = StepZLab5(l, m);

            return (t, w, ld, lnl, nSoliton, h);
        }

        /// <summary>
        /// Дисперсионная длина L_D = T0² / |β₂|
        /// </summary>
        private double DispersionLengthLab5(double t0, double beta2)
        {
            return Math.Pow(t0, 2) / Math.Abs(beta2);
        }

        /// <summary>
        /// Нелинейная длина L_NL = 1 / (γ · P0)
        /// </summary>
        private double NonlinearLengthLab5(double gamma, double p0)
        {
            return 1.0 / (gamma * p0);
        }

        /// <summary>
        /// Порядок солитона N_soliton = sqrt(L_D / L_NL)
        /// </summary>
        private double SolitonOrderLab5(double ld, double lnl)
        {
            return Math.Sqrt(ld / lnl);
        }

        /// <summary>
        /// Шаг по z: h = L / M
        /// </summary>
        private double StepZLab5(double l, int m)
        {
            return l / m;
        }

        /// <summary>
        /// Построение временной и частотной сеток для LAB-5.
        /// </summary>
        private void BuildGridsLab5(double t0, int n,
            out double[] t, out double[] w,
            out double dT, out double dW)
        {
            double tWindow = WindowFactor * t0;

            dT = tWindow / n;
            dW = 2.0 * Pi / (n * dT);

            t = new double[n];
            w = new double[n];

            for (int j = 0; j < n; j++)
            {
                t[j] = (j - n / 2.0) * dT;
                w[j] = (j - n / 2.0) * dW;
            }
        }

        #endregion

        #region 5_2

        /// <summary>
        /// LAB-68: формирование начального импульса U0(T) = √P0 · exp(−T²/(2·T0²))
        /// и массива интенсивности I_input = |U0|².
        /// </summary>
        public void CalculatePart2Lab5(double p0, double t0, double[] t, int n,
            out double[] uRe, out double[] uIm, out double[] iInput)
        {
            uRe = new double[n];
            uIm = new double[n];
            iInput = new double[n];

            for (int j = 0; j < n; j++)
            {
                double u0 = U0Lab5(t[j], t0, p0);

                uRe[j] = u0;
                uIm[j] = 0.0;

                iInput[j] = uRe[j] * uRe[j] + uIm[j] * uIm[j];
            }
        }

        /// <summary>
        /// U0(T) = √P0 · exp(−T² / (2·T0²))
        /// </summary>
        private double U0Lab5(double t, double t0, double p0)
        {
            return Math.Sqrt(p0) * Math.Exp(-t * t / (2.0 * t0 * t0));
        }

        #endregion

        #region 5_3

        /// <summary>
        /// LAB-69: определение режима распространения импульса.
        /// </summary>
        public string DetermineMode(double l, double ld, double lnl)
        {
            if (ld <= 0 || lnl <= 0)
                return "Промежуточный";

            const double SmallFactor = 0.2;

            if (l < SmallFactor * lnl && l < SmallFactor * ld)
                return "Дисперсионный";

            if (l > lnl && l < SmallFactor * ld)
                return "Нелинейный";

            if (Math.Abs(l - lnl) / lnl < 0.5 && Math.Abs(l - ld) / ld < 0.5)
                return "Смешанный";

            return "Промежуточный";
        }

        #endregion

        #region 5_4

        /// <summary>
        /// LAB-70: симметричный SSFM (дисперсия + нелинейность).
        /// </summary>
        public void CalculatePart4Lab5(double[] uRe, double[] uIm, double[] w,
            double beta2, double gamma, double l, double h, int n,
            out double[] iOutputNum,
            out double[] uOutRe,
            out double[] uOutIm)
        {
            int M = (int)Math.Ceiling(l / h);
            if (M < 1) M = 1;

            double[] re = new double[n];
            double[] im = new double[n];
            Array.Copy(uRe, re, n);
            Array.Copy(uIm, im, n);

            for (int step = 0; step < M; step++)
            {
                // 1. Полушаг нелинейности
                NonlinearStepLab5(re, im, gamma, h / 2.0);

                // 2. Полный шаг дисперсии
                Fft(re, im, n, inverse: false);
                for (int k = 0; k < n; k++)
                {
                    double phase = 0.5 * beta2 * w[k] * w[k] * h;
                    double cosP = Math.Cos(phase);
                    double sinP = Math.Sin(phase);
                    double a = re[k], b = im[k];
                    re[k] = a * cosP - b * sinP;
                    im[k] = a * sinP + b * cosP;
                }

                Fft(re, im, n, inverse: true);

                // 3. Полушаг нелинейности
                NonlinearStepLab5(re, im, gamma, h / 2.0);
            }

            iOutputNum = new double[n];
            uOutRe = new double[n];
            uOutIm = new double[n];
            for (int j = 0; j < n; j++)
            {
                uOutRe[j] = re[j];
                uOutIm[j] = im[j];
                iOutputNum[j] = re[j] * re[j] + im[j] * im[j];
            }
        }

        private void NonlinearStepLab5(double[] re, double[] im, double gamma, double dz)
        {
            for (int j = 0; j < re.Length; j++)
            {
                double intensity = re[j] * re[j] + im[j] * im[j];
                double phase = gamma * intensity * dz;
                double cosP = Math.Cos(phase);
                double sinP = Math.Sin(phase);
                double a = re[j], b = im[j];
                re[j] = a * cosP - b * sinP;
                im[j] = a * sinP + b * cosP;
            }
        }

        #endregion

        #region 5_5

        /// <summary>
        /// LAB-71: спектры входного и выходного импульсов.
        /// </summary>
        public void CalculatePart5Lab5(double[] u0Re, double[] u0Im,
            double[] uRe, double[] uIm,
            double[] w, int n,
            out double[] sInput, out double[] sOutput, out double[] wOut)
        {
            double[] inRe = new double[n];
            double[] inIm = new double[n];
            double[] outRe = new double[n];
            double[] outIm = new double[n];

            Array.Copy(u0Re, inRe, n);
            Array.Copy(u0Im, inIm, n);
            Array.Copy(uRe, outRe, n);
            Array.Copy(uIm, outIm, n);

            Fft(inRe, inIm, n, inverse: false);
            Fft(outRe, outIm, n, inverse: false);

            sInput = new double[n];
            sOutput = new double[n];
            for (int k = 0; k < n; k++)
            {
                sInput[k] = inRe[k] * inRe[k] + inIm[k] * inIm[k];
                sOutput[k] = outRe[k] * outRe[k] + outIm[k] * outIm[k];
            }

            wOut = w;
        }

        #endregion

        #region 5_6

        /// <summary>
        /// LAB-72: полный расчёт LAB-67…LAB-71.
        /// </summary>
        public Lab5Result CalculateLab5(double beta2, double gamma, double t0, double p0,
            double l, int n, int m)
        {
            var result = new Lab5Result();

            // --- LAB-67 ---
            var (T, w, ld, lnl, nSoliton, h) =
                CalculatePart1Lab5(beta2, gamma, t0, p0, l, n, m);

            result.T = T;
            result.w = w;
            result.LD = ld;
            result.LNL = lnl;
            result.N_soliton = nSoliton;

            // --- LAB-68 ---
            CalculatePart2Lab5(p0, t0, T, n,
                out double[] u0Re,
                out double[] u0Im,
                out double[] iInput);
            result.I_input = iInput;

            // --- LAB-69 ---
            result.Mode = DetermineMode(l, ld, lnl);

            // --- LAB-70 ---
            CalculatePart4Lab5(u0Re, u0Im, w, beta2, gamma, l, h, n,
                out double[] iOutputNum,
                out double[] uOutRe,
                out double[] uOutIm);
            result.I_output_num = iOutputNum;

            // --- LAB-71 ---
            CalculatePart5Lab5(u0Re, u0Im, uOutRe, uOutIm, w, n,
                out double[] sInput,
                out double[] sOutput,
                out double[] wOut);
            result.S_input = sInput;
            result.S_output = sOutput;

            return result;
        }

        #endregion

        #endregion

        #region CalculateLab6

        #region 6_1

        /// <summary>
        /// LAB-84: расчёт длины образования ударной волны, временной/частотной сеток,
        /// шага по z и массива z_array.
        /// </summary>
        /// <param name="s">Параметр нелинейной дисперсии s (безразмерный)</param>
        /// <param name="lnl">Нелинейная длина L_NL, км</param>
        /// <param name="t0">Длительность импульса T0, пс</param>
        /// <param name="l">Длина волокна L, км</param>
        /// <param name="n">Число точек N (степень 2)</param>
        /// <param name="m">Число шагов по z M</param>
        /// <returns>
        /// t        — временная сетка T[], безразмерная;
        /// w        — частотная сетка w[], рад/пс;
        /// zs       — длина образования ударной волны z_s, км;
        /// h        — шаг по z, км;
        /// zArray   — массив z-координат для 3D-визуализации, км
        /// </returns>
        public (double[] t, double[] w, double zs, double h, double[] zArray)
            CalculatePart1Lab6(double s, double lnl, double t0, double l, int n, int m)
        {
            // --- 1. Длина образования ударной волны ---
            double zs = ShockWaveLengthLab6(s, lnl);

            // --- 2. Временная и частотная сетки ---
            BuildGridsLab6(t0, n,
                out double[] t, out double[] w,
                out double dT, out double dW);
            _ = dW;

            // --- 3. Шаг по z ---
            double h = StepZLab6(l, m);

            // --- 4. Массив z-координат ---
            double[] zArray = new double[m + 1];
            for (int i = 0; i <= m; i++)
                zArray[i] = i * h;

            return (t, w, zs, h, zArray);
        }

        /// <summary>
        /// Длина образования ударной волны:
        /// z_s = sqrt(e/2) · L_NL / (3 · s)
        /// </summary>
        private double ShockWaveLengthLab6(double s, double lnl)
        {
            return Math.Sqrt(Math.E / 2.0) * lnl / (3.0 * s);
        }

        /// <summary>
        /// Шаг по z: h = L / M
        /// </summary>
        private double StepZLab6(double l, int m)
        {
            return l / m;
        }

        /// <summary>
        /// Построение временной и частотной сеток для LAB-6.
        /// </summary>
        private void BuildGridsLab6(double t0, int n,
            out double[] t, out double[] w,
            out double dT, out double dW)
        {
            double tWindow = WindowFactor * t0;

            dT = tWindow / n;
            dW = 2.0 * Pi / (n * dT);

            t = new double[n];
            w = new double[n];

            for (int j = 0; j < n; j++)
            {
                t[j] = (j - n / 2.0) * dT;
                w[j] = (j - n / 2.0) * dW;
            }
        }

        #endregion

        #region 6_2

        /// <summary>
        /// LAB-85: формирование начального импульса U0(T) = exp(−T²/(2·T0²)),
        /// массива интенсивности I_input = |U0|² и фазы phi_input = 0.
        /// </summary>
        public void CalculatePart2Lab6(double t0, double[] t, int n,
            out double[] uRe, out double[] uIm,
            out double[] iInput, out double[] phiInput)
        {
            uRe = new double[n];
            uIm = new double[n];
            iInput = new double[n];
            phiInput = new double[n];

            for (int j = 0; j < n; j++)
            {
                double u0 = U0Lab6(t[j], t0);

                uRe[j] = u0;
                uIm[j] = 0.0;

                iInput[j] = uRe[j] * uRe[j] + uIm[j] * uIm[j];
                phiInput[j] = 0.0;
            }
        }

        /// <summary>
        /// U0(T) = exp(−T² / (2·T0²))
        /// </summary>
        private double U0Lab6(double t, double t0)
        {
            return Math.Exp(-t * t / (2.0 * t0 * t0));
        }

        #endregion

        #region 6_3

        /// <summary>
        /// LAB-86: SSFM с учётом нелинейности и нелинейной дисперсии.
        /// β₂ = 0, поэтому дисперсионный оператор отсутствует.
        /// </summary>
        /// <param name="uRe">Действительная часть U[] из LAB-85</param>
        /// <param name="uIm">Мнимая часть U[] из LAB-85</param>
        /// <param name="w">Частотная сетка (не используется, β₂ = 0)</param>
        /// <param name="h">Шаг по z, км</param>
        /// <param name="l">Длина волокна L, км</param>
        /// <param name="s">Параметр нелинейной дисперсии</param>
        /// <param name="dT">Шаг по времени</param>
        /// <param name="zs">Длина ударной волны, км</param>
        /// <param name="n">Число точек N</param>
        /// <param name="iOutputNum">I_output_num[] = |U(L,T)|², отн. ед.</param>
        /// <param name="i2D">I_2D[m, j] = |U(z_m, T_j)|² — 2D-массив по z</param>
        /// <param name="iAtZs">I_at_zs[] — срез на z = z_s</param>
        public void CalculatePart3Lab6(double[] uRe, double[] uIm, double[] w,
            double h, double l, double s, double dT, double zs, int n,
            out double[] iOutputNum,
            out double[,] i2D,
            out double[] iAtZs)
        {
            int M = (int)Math.Ceiling(l / h);
            if (M < 1) M = 1;

            double[] re = new double[n];
            double[] im = new double[n];
            Array.Copy(uRe, re, n);
            Array.Copy(uIm, im, n);

            i2D = new double[M + 1, n];

            // Начальный срез
            for (int j = 0; j < n; j++)
                i2D[0, j] = re[j] * re[j] + im[j] * im[j];

            int idxAtZs = -1;

            for (int step = 0; step < M; step++)
            {
                // --- 1. |U|² ---
                double[] intensity = new double[n];
                for (int j = 0; j < n; j++)
                    intensity[j] = re[j] * re[j] + im[j] * im[j];

                // --- 2. dIdx = ∂/∂τ(|U|²·U) через центральную разность ---
                // dIdx[j] = ( |U[j+1]|²·U[j+1] − |U[j-1]|²·U[j-1] ) / (2·dT)
                double[] dIdxRe = new double[n];
                double[] dIdxIm = new double[n];

                for (int j = 1; j < n - 1; j++)
                {
                    double aRe = intensity[j + 1] * re[j + 1];
                    double aIm = intensity[j + 1] * im[j + 1];
                    double bRe = intensity[j - 1] * re[j - 1];
                    double bIm = intensity[j - 1] * im[j - 1];

                    dIdxRe[j] = (aRe - bRe) / (2.0 * dT);
                    dIdxIm[j] = (aIm - bIm) / (2.0 * dT);
                }

                // Края: односторонняя разность (первого порядка) либо нули
                dIdxRe[0] = dIdxRe[1];
                dIdxIm[0] = dIdxIm[1];
                dIdxRe[n - 1] = dIdxRe[n - 2];
                dIdxIm[n - 1] = dIdxIm[n - 2];

                // --- 3. Шаг по z: U += h · ( i·|U|²·U − s·dIdx ) ---
                for (int j = 0; j < n; j++)
                {
                    // i·|U|²·U = i·(I_re + i·I_im), где I = |U|²·U
                    double iuRe = intensity[j] * re[j];
                    double iuIm = intensity[j] * im[j];

                    // i·(iuRe + i·iuIm) = -iuIm + i·iuRe
                    double nlRe = -iuIm;
                    double nlIm = iuRe;

                    // − s·dIdx
                    double dispRe = -s * dIdxRe[j];
                    double dispIm = -s * dIdxIm[j];

                    // Полное приращение
                    double dURe = nlRe + dispRe;
                    double dUIm = nlIm + dispIm;

                    re[j] += h * dURe;
                    im[j] += h * dUIm;
                }

                // --- 4. Сохранение среза ---
                for (int j = 0; j < n; j++)
                    i2D[step + 1, j] = re[j] * re[j] + im[j] * im[j];

                // Ближайший шаг к z_s
                double zCurrent = (step + 1) * h;
                if (idxAtZs < 0 && zCurrent >= zs)
                    idxAtZs = step + 1;
            }

            // --- 5. Выходная интенсивность ---
            iOutputNum = new double[n];
            for (int j = 0; j < n; j++)
                iOutputNum[j] = re[j] * re[j] + im[j] * im[j];

            // --- 6. Срез на z_s ---
            iAtZs = new double[n];
            if (idxAtZs >= 0)
            {
                for (int j = 0; j < n; j++)
                    iAtZs[j] = i2D[idxAtZs, j];
            }
            else
            {
                // z_s вне диапазона [0, L] — берём последний срез
                for (int j = 0; j < n; j++)
                    iAtZs[j] = i2D[M, j];
            }
        }

        #endregion

        #region 6_4

        /// <summary>
        /// LAB-87: аналитическое решение I_anal(Z, τ) = exp(−(τ − 3·s·Z)²).
        /// </summary>
        /// <param name="t">Временная сетка T[]</param>
        /// <param name="zArray">Массив z-координат</param>
        /// <param name="s">Параметр нелинейной дисперсии</param>
        /// <param name="zs">Длина ударной волны z_s</param>
        /// <param name="n">Число точек N</param>
        /// <param name="iAnal2D">I_anal_2D[m, j]</param>
        /// <param name="iAnalAtZs">I_anal_at_zs[] — срез на Z = z_s</param>
        public void CalculatePart4Lab6(double[] t, double[] zArray,
            double s, double zs, int n,
            out double[,] iAnal2D, out double[] iAnalAtZs)
        {
            int m = zArray.Length;
            iAnal2D = new double[m, n];

            for (int i = 0; i < m; i++)
            {
                double z = zArray[i];
                for (int j = 0; j < n; j++)
                {
                    double tau = t[j] - 3.0 * s * z;
                    iAnal2D[i, j] = Math.Exp(-tau * tau);
                }
            }

            // --- Срез на Z = z_s ---
            iAnalAtZs = new double[n];

            // Ищем ближайший индекс к z_s
            int idxZs = 0;
            double minDiff = Math.Abs(zArray[0] - zs);
            for (int i = 1; i < m; i++)
            {
                double diff = Math.Abs(zArray[i] - zs);
                if (diff < minDiff)
                {
                    minDiff = diff;
                    idxZs = i;
                }
            }

            for (int j = 0; j < n; j++)
                iAnalAtZs[j] = iAnal2D[idxZs, j];
        }

        #endregion

        #region 6_5

        /// <summary>
        /// LAB-88: относительная ошибка между численным и аналитическим решениями
        /// на длине z = z_s, а также оценка z_s vs L.
        /// </summary>
        /// <param name="iAtZs">I_at_zs[] из LAB-86</param>
        /// <param name="iAnalAtZs">I_anal_at_zs[] из LAB-87</param>
        /// <param name="n">Число точек N</param>
        /// <param name="error">Относительная ошибка</param>
        public void CalculatePart5Lab6(double[] iAtZs, double[] iAnalAtZs, int n,
            out double error)
        {
            double maxDiff = 0.0;
            double maxAnal = 0.0;

            for (int j = 0; j < n; j++)
            {
                double diff = Math.Abs(iAtZs[j] - iAnalAtZs[j]);
                if (diff > maxDiff) maxDiff = diff;

                if (iAnalAtZs[j] > maxAnal) maxAnal = iAnalAtZs[j];
            }

            error = (maxAnal > 0.0) ? maxDiff / maxAnal : 0.0;
        }

        #endregion

        #region 6_6

        /// <summary>
        /// LAB-89: полный расчёт LAB-84…LAB-88.
        /// </summary>
        public Lab6Result CalculateLab6(double s, double lnl, double t0, double l,
            int n, int m)
        {
            var result = new Lab6Result();

            // --- LAB-84 ---
            var (T, w, zs, h, zArray) =
                CalculatePart1Lab6(s, lnl, t0, l, n, m);

            result.T = T;
            result.z_s = zs;
            result.z_array = zArray;

            // --- LAB-85 ---
            CalculatePart2Lab6(t0, T, n,
                out double[] u0Re,
                out double[] u0Im,
                out double[] iInput,
                out double[] phiInput);
            result.I_input = iInput;

            // Шаг по времени для dIdx
            double tWindow = WindowFactor * t0;
            double dT = tWindow / n;

            // --- LAB-86 ---
            CalculatePart3Lab6(u0Re, u0Im, w, h, l, s, dT, zs, n,
                out double[] iOutputNum,
                out double[,] i2D,
                out double[] iAtZs);
            result.I_output_num = iOutputNum;
            result.I_2D = i2D;
            result.I_at_zs = iAtZs;

            // --- LAB-87 ---
            CalculatePart4Lab6(T, zArray, s, zs, n,
                out double[,] iAnal2D,
                out double[] iAnalAtZs);
            result.I_anal_2D = iAnal2D;
            result.I_anal_at_zs = iAnalAtZs;

            // --- LAB-88 ---
            CalculatePart5Lab6(iAtZs, iAnalAtZs, n, out double error);
            result.Error = error;

            return result;
        }

        #endregion

        #endregion
    }
}