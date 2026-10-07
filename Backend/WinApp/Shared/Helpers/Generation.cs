using System.IO;
using Newtonsoft.Json;
using Shared.Models;

namespace Shared.Helpers
{
    public sealed class Generation
    {
        private readonly string filePath =
            Path.Combine(Directory.GetCurrentDirectory(), "Files", "Labs.json");

        private Root _root;

        private static readonly JsonSerializerSettings JsonSettings = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        public Generation()
        {
            EnsureFileExists();
            _root = Load() ?? new Root();
        }

        // ---------------- LAB-1 ----------------
        public void GenerateLab1(LabData data)
        {
            _root.Labs_1 = data;
            Save();
        }

        // ---------------- LAB-2 ----------------
        public void GenerateLab2(double p0, double lambda, double length)
        {
            _root.Labs_2 = new LabData
            {
                WaveRange = new[] { 1.0, 2.0 },
                WaveguideLengthRange = new[] { 100.0, 1000.0 },
                InputPower = p0,
                Lambda = lambda,
                L = length
            };
            Save();
        }

        // ---------------- LAB-3 ----------------
        public void GenerateLab3(double lambda, double n, double vg,
            double b0, double b1, double b2,
            double taylor, bool equalsDgs)
        {
            _root.Labs_3 = new LabData
            {
                Lambda = lambda,
                N1 = n,
                GroupSpeed = vg,
                B0 = b0,
                B1 = b1,
                Beta2 = b2,
                TaylorSeries = taylor,
                TaylorEqualsDgs = equalsDgs
            };
            Save();
        }

        // ---------------- LAB-4 ----------------
        public void GenerateLab4(Lab4Result lab4, double beta2, double t0, int n, int m)
        {
            _root.Labs_4 = new LabData
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
            };
            Save();
        }

        // ---------------- LAB-5 ----------------
        public void GenerateLab5(Lab5Result lab5, double beta2, double gamma,
            double t0, double p0, double l, int n, int m)
        {
            _root.Labs_5 = new LabData
            {
                Beta2 = beta2,
                Gamma = gamma,
                T0 = t0,
                P0 = p0,
                L = l,
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
            };
            Save();
        }

        // ---------------- LAB-6 ----------------
        public void GenerateLab6(Lab6Result lab6, double s, double lnl,
            double t0, double l, int n, int m)
        {
            _root.Labs_6 = new LabData
            {
                S = s,
                LNL = lnl,
                T0 = t0,
                L = l,
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
            };
            Save();
        }

        // ---------------- helpers ----------------
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

        private Root? Load()
        {
            try
            {
                var json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json) || json.Trim() == "{}")
                    return new Root();

                return JsonConvert.DeserializeObject<Root>(json, JsonSettings);
            }
            catch
            {
                return new Root();
            }
        }

        private void Save()
        {
            var json = JsonConvert.SerializeObject(_root, JsonSettings);
            File.WriteAllText(filePath, json);
        }

        private void EnsureFileExists()
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "{}");
        }
    }
}