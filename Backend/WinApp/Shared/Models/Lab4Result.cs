namespace Shared.Models;

public sealed class Lab4Result
{
    // Массивы для визуализации (LAB-53, LAB-54)
    public double[] T { get; set; }
    public double[] I_input { get; set; }
    public double[] I_output_num { get; set; }
    public double[] I_output_anal { get; set; }

    // Скалярные характеристики
    public double LD { get; set; }
    public double Error { get; set; }
    public double FWHM_input { get; set; }
    public double FWHM_output { get; set; }
}