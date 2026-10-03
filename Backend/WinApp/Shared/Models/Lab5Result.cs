namespace Shared.Models;

public sealed class Lab5Result
{
    // --- Массивы для визуализации (LAB-72) ---
    public double[] T { get; set; } // пс
    public double[] I_input { get; set; } // Вт
    public double[] I_output_num { get; set; } // Вт

    public double[] w { get; set; } // рад/пс
    public double[] S_input { get; set; } // отн. ед.
    public double[] S_output { get; set; } // отн. ед.

    // --- Скалярные характеристики ---
    public double LD { get; set; } // км
    public double LNL { get; set; } // км
    public double N_soliton { get; set; } // безразмерный
    public string Mode { get; set; } // «Дисперсионный» / «Нелинейный» / ...
}