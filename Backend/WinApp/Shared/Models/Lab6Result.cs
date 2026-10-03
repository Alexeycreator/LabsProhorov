namespace Shared.Models;

public sealed class Lab6Result
{
    // --- Массивы ---
    public double[] T { get; set; }
    public double[] I_input { get; set; }
    public double[] I_output_num { get; set; }

    public double[] z_array { get; set; }
    public double[,] I_2D { get; set; }
    public double[,] I_anal_2D { get; set; }

    public double[] I_at_zs { get; set; }
    public double[] I_anal_at_zs { get; set; }

    // --- Скаляры ---
    public double z_s { get; set; }
    public double Error { get; set; }
}