using System.IO;
using Shared.Models;

namespace Shared.Helpers
{
    public sealed class Generation
    {
        private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Labs.json");
        private readonly JsonWriter jsonWriter = new();

        /// <summary>
        /// Генерация JSON со всеми шестью лабораторными работами.
        /// </summary>
        public void GenerateData(
            // LAB-1/2
            double p0,
            // LAB-3
            double lambda, double n1, double n2, double a, double c, double l3,
            // LAB-4
            double beta2, double t0, int n, int m,
            // LAB-5
            double gamma, double p0lab5, double l5,
            // LAB-6
            double s, double lnl, double t06, double l6)
        {
            EnsureFileExists();

            var root = new Root
            {
                Labs_1 = new LabData
                {
                    // LAB-1 — если есть отдельные параметры, добавьте сюда
                },

                Labs_2 = new LabData
                {
                    WaveRange = new[] { 1.0, 2.0 },
                    WaveguideLengthRange = new[] { 100.0, 1000.0 },
                    InputPower = p0
                },

                Labs_3 = new LabData
                {
                    Lambda = lambda,
                    N1 = n1,
                    N2 = n2,
                    A = a,
                    C = c,
                    L = l3
                },

                Labs_4 = new LabData
                {
                    Beta2 = beta2,
                    T0 = t0,
                    N = n,
                    M = m
                },

                Labs_5 = new LabData
                {
                    Beta2 = beta2,
                    Gamma = gamma,
                    T0 = t0,
                    P0 = p0lab5,
                    L = l5,
                    N = n,
                    M = m
                },

                Labs_6 = new LabData
                {
                    S = s,
                    LNL = lnl,
                    T0 = t06,
                    L = l6,
                    N = n,
                    M = m
                }
            };

            jsonWriter.Write(filePath, root);
        }

        /// <summary>
        /// Генерация JSON с уже посчитанными результатами.
        /// </summary>
        public void GenerateDataWithResults(
            Lab4Result lab4,
            Lab5Result lab5,
            Lab6Result lab6,
            double beta2, double t0, int n, int m,
            double gamma, double p0lab5, double l5,
            double s, double lnl, double t06, double l6)
        {
            EnsureFileExists();

            var root = new Root
            {
                Labs_1 = new LabData
                {
                },

                Labs_2 = new LabData
                {
                    WaveRange = new[] { 1.0, 2.0 },
                    WaveguideLengthRange = new[] { 100.0, 1000.0 },
                    InputPower = p0lab5
                },

                Labs_3 = new LabData
                {
                },

                Labs_4 = new LabData
                {
                    Beta2 = beta2,
                    T0 = t0,
                    N = n,
                    M = m,
                    T = lab4.T,
                    I_input = lab4.I_input,
                    I_output_num = lab4.I_output_num,
                    I_output_anal = lab4.I_output_anal,
                    LD = lab4.LD,
                    Error = lab4.Error,
                    FWHM_input = lab4.FWHM_input,
                    FWHM_output = lab4.FWHM_output
                },

                Labs_5 = new LabData
                {
                    Beta2 = beta2,
                    Gamma = gamma,
                    T0 = t0,
                    P0 = p0lab5,
                    L = l5,
                    N = n,
                    M = m,

                    T = lab5.T,
                    I_input = lab5.I_input,
                    I_output_num = lab5.I_output_num,
                    W = lab5.w,
                    S_input = lab5.S_input,
                    S_output = lab5.S_output,
                    LD = lab5.LD,
                    LNL_calc = lab5.LNL,
                    N_soliton = lab5.N_soliton,
                    Mode = lab5.Mode
                },

                Labs_6 = new LabData
                {
                    S = s,
                    LNL = lnl,
                    T0 = t06,
                    L = l6,
                    N = n,
                    M = m,

                    T = lab6.T,
                    I_input = lab6.I_input,
                    I_output_num = lab6.I_output_num,
                    z_array = lab6.z_array,
                    I_2D = ToJagged(lab6.I_2D),
                    I_anal_2D = ToJagged(lab6.I_anal_2D),
                    I_at_zs = lab6.I_at_zs,
                    I_anal_at_zs = lab6.I_anal_at_zs,
                    Z_s = lab6.z_s,
                    Error = lab6.Error
                }
            };

            jsonWriter.Write(filePath, root);
        }

        /// <summary>
        /// Приведение double[,] к double[][] для JSON-сериализации.
        /// </summary>
        private static double[][]? ToJagged(double[,]? matrix)
        {
            if (matrix == null) return null;

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            var result = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                result[i] = new double[cols];
                for (int j = 0; j < cols; j++)
                    result[i][j] = matrix[i, j];
            }

            return result;
        }

        private void EnsureFileExists()
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(filePath))
                using (File.Create(filePath))
                    ;
        }
    }
}