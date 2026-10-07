using System;
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
        private const double C = 2.99792458e8; // м/с

        #region LAB-1/2

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

        public double GetValueLambda(double lambda) => lambda / Denominator;

        private double GetValueA(double a) => a / Denominator;

        private double GetValueP0(double p0) => p0 / 1000;

        private double GetValueA(double lambda, double C) =>
            C / (Math.Pow(lambda, 4) * 4343);

        private double GetValueK0(double lambda) => 2 * Pi / lambda;

        #endregion

        #region LAB-3

        public double GetRefractiveIndex(double lambda)
        {
            double l2 = lambda * lambda;
            double n2 = 1
                        + B1 * l2 / (l2 - Lambda1 * Lambda1)
                        + B2 * l2 / (l2 - Lambda2 * Lambda2)
                        + B3 * l2 / (l2 - Lambda3 * Lambda3);
            return Math.Sqrt(n2);
        }

        public double GetDnDlambda(double lambda)
        {
            double l2 = lambda * lambda;
            double dN2 =
                -2 * B1 * Lambda1 * Lambda1 * lambda / Math.Pow(l2 - Lambda1 * Lambda1, 2)
                - 2 * B2 * Lambda2 * Lambda2 * lambda / Math.Pow(l2 - Lambda2 * Lambda2, 2)
                - 2 * B3 * Lambda3 * Lambda3 * lambda / Math.Pow(l2 - Lambda3 * Lambda3, 2);
            double n = GetRefractiveIndex(lambda);
            return dN2 / (2 * n);
        }

        public double GetD2nDlambda2(double lambda)
        {
            const double h = 1e-5;
            return (GetDnDlambda(lambda + h) - GetDnDlambda(lambda - h)) / (2 * h);
        }

        public double GetGroupSpeed(double lambda)
        {
            double n = GetRefractiveIndex(lambda);
            double dn = GetDnDlambda(lambda);
            double nGroup = n - lambda * dn;
            return C / nGroup;
        }

        public double GetBeta2(double lambda)
        {
            double lambdaM = lambda * 1e-6;
            double d2n = GetD2nDlambda2(lambda);
            double d2nM = d2n * 1e12;
            return -Math.Pow(lambdaM, 2) / (2 * Math.PI * C) * d2nM;
        }

        public (double b0, double b1, double b2) GetPropagationConstants(double lambda)
        {
            double lambdaM = lambda * 1e-6;
            double omega = 2 * Math.PI * C / lambdaM;
            double n = GetRefractiveIndex(lambda);
            double vg = GetGroupSpeed(lambda);
            double d2n = GetD2nDlambda2(lambda) * 1e12;

            double b0 = n * omega / C;
            double b1 = 1.0 / vg;
            double b2 = -Math.Pow(lambdaM, 2) / (2 * Math.PI * C) * d2n;

            return (b0, b1, b2);
        }

        public double GetTaylorSeries(double lambda, double omega)
        {
            var (b0, b1, b2) = GetPropagationConstants(lambda);
            double omega0 = 2 * Math.PI * C / (lambda * 1e-6);
            return b0 + b1 * (omega - omega0) + 0.5 * b2 * Math.Pow(omega - omega0, 2);
        }

        public double GetDgs(double lambda) => GetPropagationConstants(lambda).b2;

        public bool IsTaylorSeriesEqualsDgs(double lambda)
        {
            var (_, _, b2) = GetPropagationConstants(lambda);
            double b2FromDgs = GetDgs(lambda);
            return Math.Abs(b2 - b2FromDgs) < 1e-3;
        }

        #endregion

        #region LAB-4

        public bool IsPowerOfTwo(int n) => n > 0 && (n & (n - 1)) == 0;

        // --- 4_1 ---
        public (double ld, double[] t, double[] w, double dT, double dw)
            CalculatePart1(double beta2, double t0, int n, int m)
        {
            double ld = DispersionLength(t0, beta2);
            BuildGrids(t0, n, out double[] t, out double[] w, out double dT, out double dw);
            return (ld, t, w, dT, dw);
        }

        private double DispersionLength(double t0, double beta2) =>
            Math.Pow(t0, 2) / Math.Abs(beta2);

        private void BuildGrids(double t0, int n,
            out double[] t, out double[] w,
            out double dT, out double dw)
        {
            double tWindow = WindowFactor * t0;
            dT = tWindow / n;
            dw = 2.0 * Pi / (n * dT);

            t = new double[n];
            w = new double[n];

            for (int j = 0; j < n; j++)
            {
                t[j] = (j - n / 2.0) * dT;
                w[j] = (j - n / 2.0) * dw;
            }
        }

        // --- 4_2 ---
        public void CalculatePart2(double t0, double[] t, int n,
            out double[] uRe, out double[] uIm, out double[] iInput)
        {
            uRe = new double[n];
            uIm = new double[n];
            iInput = new double[n];

            for (int j = 0; j < n; j++)
            {
                double u0 = U0(t[j], t0);
                uRe[j] = u0;
                uIm[j] = 0.0;
                iInput[j] = uRe[j] * uRe[j] + uIm[j] * uIm[j];
            }
        }

        private double U0(double t, double t0) =>
            Math.Exp(-t * t / (2.0 * t0 * t0));

        // --- 4_3 ---
        /// <summary>
        /// SSFM для чисто дисперсионного режима.
        /// M — число шагов по z (берётся из формы).
        /// </summary>
        public void CalculatePart3(double[] uRe, double[] uIm, double[] w,
            double beta2, double ld, double L, int n, int M,
            out double[] iOutputNum)
        {
            if (M < 1) M = 1;
            double h = L / M;

            double[] re = new double[n];
            double[] im = new double[n];
            Array.Copy(uRe, re, n);
            Array.Copy(uIm, im, n);

            for (int step = 0; step < M; step++)
            {
                // 1. Прямое БПФ
                Fft(re, im, n, inverse: false);

                // 2. Дисперсионный оператор exp(i * 0.5 * beta2 * w^2 * h)
                for (int k = 0; k < n; k++)
                {
                    double phase = 0.5 * beta2 * w[k] * w[k] * h;
                    double cosP = Math.Cos(phase);
                    double sinP = Math.Sin(phase);

                    double a = re[k], b = im[k];
                    re[k] = a * cosP - b * sinP;
                    im[k] = a * sinP + b * cosP;
                }

                // 3. Обратное БПФ
                Fft(re, im, n, inverse: true);
            }

            iOutputNum = new double[n];
            for (int j = 0; j < n; j++)
                iOutputNum[j] = re[j] * re[j] + im[j] * im[j];
        }

        private void Fft(double[] re, double[] im, int n, bool inverse)
        {
            if ((n & (n - 1)) != 0)
                throw new ArgumentException("N должно быть степенью 2");

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

            double sign = inverse ? +1.0 : -1.0;
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

                        double tRe = curRe * re[b] - curIm * im[b];
                        double tIm = curRe * im[b] + curIm * re[b];

                        re[b] = re[a] - tRe;
                        im[b] = im[a] - tIm;

                        re[a] += tRe;
                        im[a] += tIm;

                        double newCurRe = curRe * wRe - curIm * wIm;
                        double newCurIm = curRe * wIm + curIm * wRe;
                        curRe = newCurRe;
                        curIm = newCurIm;
                    }
                }
            }

            if (inverse)
            {
                for (int i = 0; i < n; i++)
                {
                    re[i] /= n;
                    im[i] /= n;
                }
            }
        }

        // --- 4_4 ---
        public void CalculatePart4(double t0, double beta2, double ld,
            double[] t, int n,
            out double[] iOutputAnal)
        {
            double z = ld;
            double t0Sq = t0 * t0;

            double denomRe = t0Sq;
            double denomIm = -beta2 * z;
            double denomAbsSq = denomRe * denomRe + denomIm * denomIm;

            double prefRe = t0Sq * denomRe / denomAbsSq;
            double prefIm = -t0Sq * denomIm / denomAbsSq;

            iOutputAnal = new double[n];

            for (int j = 0; j < n; j++)
            {
                double T = t[j];
                double tSq = T * T;

                double inv2DenomRe = denomRe / (2.0 * denomAbsSq);
                double inv2DenomIm = -denomIm / (2.0 * denomAbsSq);

                double argRe = -tSq * inv2DenomRe;
                double argIm = -tSq * inv2DenomIm;

                double expRe = Math.Exp(argRe) * Math.Cos(argIm);
                double expIm = Math.Exp(argRe) * Math.Sin(argIm);

                double uRe = prefRe * expRe - prefIm * expIm;
                double uIm = prefRe * expIm + prefIm * expRe;

                iOutputAnal[j] = uRe * uRe + uIm * uIm;
            }
        }

        // --- 4_5 ---
        public void CalculatePart5(double[] iInput, double[] iOutputNum, double[] iOutputAnal,
            double[] t, int n,
            out double error, out double fwhmInput, out double fwhmOutput)
        {
            double maxDiff = 0.0;
            double maxAnal = 0.0;

            for (int j = 0; j < n; j++)
            {
                double diff = Math.Abs(iOutputNum[j] - iOutputAnal[j]);
                if (diff > maxDiff) maxDiff = diff;

                if (iOutputAnal[j] > maxAnal) maxAnal = iOutputAnal[j];
            }

            error = (maxAnal > 0.0) ? maxDiff / maxAnal : 0.0;

            fwhmInput = Fwhm(iInput, t, n);
            fwhmOutput = Fwhm(iOutputNum, t, n);
        }

        private double Fwhm(double[] i, double[] t, int n)
        {
            if (i == null || t == null || n < 2) return 0.0;

            // --- 1. Максимум ---
            double max = double.NegativeInfinity;
            int iMax = -1;
            for (int j = 0; j < n; j++)
            {
                if (double.IsNaN(i[j])) continue;             // игнорируем NaN
                if (i[j] > max) { max = i[j]; iMax = j; }
            }
            if (iMax < 0 || max <= 0) return 0.0;              // всё нули/NaN — ширина не определена

            double half = 0.5 * max;

            // --- 2. Левая граница: идём влево от максимума до первого j, где i[j] < half ---
            int left = iMax;
            for (int j = iMax; j >= 1; j--)
            {
                if (i[j] >= half && i[j - 1] < half) { left = j; break; }
            }

            // --- 3. Правая граница: идём вправо от максимума до первого j, где i[j+1] < half ---
            int right = iMax;
            for (int j = iMax; j < n - 1; j++)
            {
                if (i[j] >= half && i[j + 1] < half) { right = j; break; }
            }

            // --- 4. Линейная интерполяция через half ---
            double tLeft, tRight;

            if (left == iMax)
            {
                // не нашли точку перехода слева — считаем, что импульс шире окна,
                // берём край сетки
                tLeft = t[0];
            }
            else
            {
                // t at i[left-1] < half < i[left]
                double i0 = i[left - 1], i1 = i[left];
                double frac = (half - i0) / (i1 - i0);   // 0..1
                tLeft = t[left - 1] + frac * (t[left] - t[left - 1]);
            }

            if (right == iMax)
            {
                tRight = t[n - 1];
            }
            else
            {
                double i0 = i[right], i1 = i[right + 1];
                double frac = (half - i0) / (i1 - i0);   // 0..1
                tRight = t[right] + frac * (t[right + 1] - t[right]);
            }

            return tRight - tLeft;
        }

        // --- 4_6 ---
        public Lab4Result CalculatePart6(double beta2, double t0, int n, int m)
        {
            var result = new Lab4Result();

            var (ld, T, w, dT, dw) = CalculatePart1(beta2, t0, n, m);
            _ = dT; _ = dw;
            result.LD = ld;
            result.T = T;

            CalculatePart2(t0, T, n,
                out double[] uRe,
                out double[] uIm,
                out double[] iInput);
            result.I_input = iInput;

            // ВАЖНО: передаём m — теперь SSFM делает реальные M шагов
            CalculatePart3(uRe, uIm, w, beta2, ld, ld, n, m,
                out double[] iOutputNum);
            result.I_output_num = iOutputNum;

            CalculatePart4(t0, beta2, ld, T, n,
                out double[] iOutputAnal);
            result.I_output_anal = iOutputAnal;

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

        #region LAB-5

        public (double[] t, double[] w, double ld, double lnl, double nSoliton, double h)
            CalculatePart1Lab5(double beta2, double gamma, double t0, double p0,
                double l, int n, int m)
        {
            double ld = Math.Pow(t0, 2) / Math.Abs(beta2);
            double lnl = 1.0 / (gamma * p0);
            double nSoliton = Math.Sqrt(ld / lnl);

            BuildGridsLab5(t0, n, out double[] t, out double[] w, out double dT, out double dW);
            _ = dT; _ = dW;

            double h = l / m;
            return (t, w, ld, lnl, nSoliton, h);
        }

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

        public void CalculatePart2Lab5(double p0, double t0, double[] t, int n,
            out double[] uRe, out double[] uIm, out double[] iInput)
        {
            uRe = new double[n];
            uIm = new double[n];
            iInput = new double[n];

            for (int j = 0; j < n; j++)
            {
                double u0 = Math.Sqrt(p0) * Math.Exp(-t[j] * t[j] / (2.0 * t0 * t0));
                uRe[j] = u0;
                uIm[j] = 0.0;
                iInput[j] = u0 * u0;
            }
        }

        public string DetermineMode(double l, double ld, double lnl)
        {
            if (ld <= 0 || lnl <= 0) return "Промежуточный";
            const double SmallFactor = 0.2;

            if (l < SmallFactor * lnl && l < SmallFactor * ld) return "Дисперсионный";
            if (l > lnl && l < SmallFactor * ld) return "Нелинейный";
            if (Math.Abs(l - lnl) / lnl < 0.5 && Math.Abs(l - ld) / ld < 0.5) return "Смешанный";
            return "Промежуточный";
        }

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
                NonlinearStepLab5(re, im, gamma, h / 2.0);

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

        public Lab5Result CalculateLab5(double beta2, double gamma, double t0, double p0,
            double l, int n, int m)
        {
            var result = new Lab5Result();

            var (T, w, ld, lnl, nSoliton, h) =
                CalculatePart1Lab5(beta2, gamma, t0, p0, l, n, m);

            result.T = T;
            result.w = w;
            result.LD = ld;
            result.LNL = lnl;
            result.N_soliton = nSoliton;

            CalculatePart2Lab5(p0, t0, T, n,
                out double[] u0Re,
                out double[] u0Im,
                out double[] iInput);
            result.I_input = iInput;

            result.Mode = DetermineMode(l, ld, lnl);

            CalculatePart4Lab5(u0Re, u0Im, w, beta2, gamma, l, h, n,
                out double[] iOutputNum,
                out double[] uOutRe,
                out double[] uOutIm);
            result.I_output_num = iOutputNum;

            CalculatePart5Lab5(u0Re, u0Im, uOutRe, uOutIm, w, n,
                out double[] sInput,
                out double[] sOutput,
                out double[] wOut);
            result.S_input = sInput;
            result.S_output = sOutput;

            return result;
        }

        #endregion

        #region LAB-6

        public (double[] t, double[] w, double zs, double h, double[] zArray)
            CalculatePart1Lab6(double s, double lnl, double t0, double l, int n, int m)
        {
            double zs = Math.Sqrt(Math.E / 2.0) * lnl / (3.0 * s);

            BuildGridsLab6(t0, n, out double[] t, out double[] w, out double dT, out double dW);
            _ = dW;

            double h = l / m;

            double[] zArray = new double[m + 1];
            for (int i = 0; i <= m; i++)
                zArray[i] = i * h;

            return (t, w, zs, h, zArray);
        }

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
                double u0 = Math.Exp(-t[j] * t[j] / (2.0 * t0 * t0));
                uRe[j] = u0;
                uIm[j] = 0.0;
                iInput[j] = u0 * u0;
                phiInput[j] = 0.0;
            }
        }

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

            for (int j = 0; j < n; j++)
                i2D[0, j] = re[j] * re[j] + im[j] * im[j];

            int idxAtZs = -1;

            for (int step = 0; step < M; step++)
            {
                double[] intensity = new double[n];
                for (int j = 0; j < n; j++)
                    intensity[j] = re[j] * re[j] + im[j] * im[j];

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

                dIdxRe[0] = dIdxRe[1];
                dIdxIm[0] = dIdxIm[1];
                dIdxRe[n - 1] = dIdxRe[n - 2];
                dIdxIm[n - 1] = dIdxIm[n - 2];

                for (int j = 0; j < n; j++)
                {
                    double iuRe = intensity[j] * re[j];
                    double iuIm = intensity[j] * im[j];

                    double nlRe = -iuIm;
                    double nlIm = iuRe;

                    double dispRe = -s * dIdxRe[j];
                    double dispIm = -s * dIdxIm[j];

                    re[j] += h * (nlRe + dispRe);
                    im[j] += h * (nlIm + dispIm);
                }

                for (int j = 0; j < n; j++)
                    i2D[step + 1, j] = re[j] * re[j] + im[j] * im[j];

                double zCurrent = (step + 1) * h;
                if (idxAtZs < 0 && zCurrent >= zs)
                    idxAtZs = step + 1;
            }

            iOutputNum = new double[n];
            for (int j = 0; j < n; j++)
                iOutputNum[j] = re[j] * re[j] + im[j] * im[j];

            iAtZs = new double[n];
            int take = idxAtZs >= 0 ? idxAtZs : M;
            for (int j = 0; j < n; j++)
                iAtZs[j] = i2D[take, j];
        }

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

            iAnalAtZs = new double[n];
            int idxZs = 0;
            double minDiff = Math.Abs(zArray[0] - zs);
            for (int i = 1; i < m; i++)
            {
                double diff = Math.Abs(zArray[i] - zs);
                if (diff < minDiff) { minDiff = diff; idxZs = i; }
            }

            for (int j = 0; j < n; j++)
                iAnalAtZs[j] = iAnal2D[idxZs, j];
        }

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

        public Lab6Result CalculateLab6(double s, double lnl, double t0, double l,
            int n, int m)
        {
            var result = new Lab6Result();

            var (T, w, zs, h, zArray) = CalculatePart1Lab6(s, lnl, t0, l, n, m);

            result.T = T;
            result.z_s = zs;
            result.z_array = zArray;

            CalculatePart2Lab6(t0, T, n,
                out double[] u0Re,
                out double[] u0Im,
                out double[] iInput,
                out double[] phiInput);
            _ = phiInput;
            result.I_input = iInput;

            double tWindow = WindowFactor * t0;
            double dT = tWindow / n;

            CalculatePart3Lab6(u0Re, u0Im, w, h, l, s, dT, zs, n,
                out double[] iOutputNum,
                out double[,] i2D,
                out double[] iAtZs);
            result.I_output_num = iOutputNum;
            result.I_2D = i2D;
            result.I_at_zs = iAtZs;

            CalculatePart4Lab6(T, zArray, s, zs, n,
                out double[,] iAnal2D,
                out double[] iAnalAtZs);
            result.I_anal_2D = iAnal2D;
            result.I_anal_at_zs = iAnalAtZs;

            CalculatePart5Lab6(iAtZs, iAnalAtZs, n, out double error);
            result.Error = error;

            return result;
        }

        #endregion
    }
}