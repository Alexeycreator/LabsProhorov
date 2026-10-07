namespace Shared.Models
{
    public sealed class Lab5Result
    {
        public double[] T { get; set; } = System.Array.Empty<double>();
        public double[] w { get; set; } = System.Array.Empty<double>();
        public double[] I_input { get; set; } = System.Array.Empty<double>();
        public double[] I_output_num { get; set; } = System.Array.Empty<double>();
        public double[] S_input { get; set; } = System.Array.Empty<double>();
        public double[] S_output { get; set; } = System.Array.Empty<double>();

        public double LD { get; set; }
        public double LNL { get; set; }
        public double N_soliton { get; set; }
        public string Mode { get; set; } = "";
    }
}