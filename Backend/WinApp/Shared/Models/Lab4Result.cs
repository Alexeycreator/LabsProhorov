namespace Shared.Models
{
    public sealed class Lab4Result
    {
        public double[] T { get; set; } = System.Array.Empty<double>();
        public double[] I_input { get; set; } = System.Array.Empty<double>();
        public double[] I_output_num { get; set; } = System.Array.Empty<double>();
        public double[] I_output_anal { get; set; } = System.Array.Empty<double>();

        public double LD { get; set; }
        public double Error { get; set; }
        public double FWHM_input { get; set; }
        public double FWHM_output { get; set; }
    }
}