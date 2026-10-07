namespace Shared.Models
{
    public sealed class LabData
    {
        // LAB-1 / LAB-2
        public double[]? WaveRange { get; set; }
        public double[]? WaveguideLengthRange { get; set; }
        public double InputPower { get; set; }

        // LAB-1 / LAB-3
        public double? Lambda { get; set; }
        public double? N1 { get; set; }
        public double? N2 { get; set; }
        public double? A { get; set; }
        public double? C { get; set; }
        public double? L { get; set; }

        // LAB-3 (расширение)
        public double? GroupSpeed { get; set; }
        public double? B0 { get; set; }
        public double? B1 { get; set; }
        public double? Beta2 { get; set; }
        public double? TaylorSeries { get; set; }
        public bool?   TaylorEqualsDgs { get; set; }

        // LAB-4 / LAB-5 / LAB-6
        public double? T0 { get; set; }
        public int?    N { get; set; }
        public int?    M { get; set; }

        // LAB-5
        public double? Gamma { get; set; }
        public double? P0 { get; set; }

        // LAB-6
        public double? S { get; set; }
        public double? LNL { get; set; }

        // Результаты
        public double[]? T { get; set; }
        public double[]? I_input { get; set; }
        public double[]? I_output_num { get; set; }
        public double[]? I_output_anal { get; set; }

        public double? LD { get; set; }
        public double? LNL_calc { get; set; }
        public double? N_soliton { get; set; }
        public double? Error { get; set; }
        public double? FWHM_input { get; set; }
        public double? FWHM_output { get; set; }
        public string? Mode { get; set; }

        public double[]? W { get; set; }
        public double[]? S_input { get; set; }
        public double[]? S_output { get; set; }

        public double[]? z_array { get; set; }
        public double[][]? I_2D { get; set; }
        public double[][]? I_anal_2D { get; set; }
        public double[]? I_at_zs { get; set; }
        public double[]? I_anal_at_zs { get; set; }
        public double? Z_s { get; set; }
    }
}