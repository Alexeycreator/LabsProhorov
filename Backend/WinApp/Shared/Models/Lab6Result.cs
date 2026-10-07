namespace Shared.Models
{
    public sealed class Lab6Result
    {
        public double[] T { get; set; } = System.Array.Empty<double>();
        public double[] I_input { get; set; } = System.Array.Empty<double>();
        public double[] I_output_num { get; set; } = System.Array.Empty<double>();

        public double[] z_array { get; set; } = System.Array.Empty<double>();
        public double[,] I_2D { get; set; } = new double[0, 0];
        public double[,] I_anal_2D { get; set; } = new double[0, 0];

        public double[] I_at_zs { get; set; } = System.Array.Empty<double>();
        public double[] I_anal_at_zs { get; set; } = System.Array.Empty<double>();

        public double z_s { get; set; }
        public double Error { get; set; }
    }
}