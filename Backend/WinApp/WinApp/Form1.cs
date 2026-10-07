using System;
using System.Windows.Forms;
using Shared.Calculate;
using Shared.Helpers;
using Shared.Models;

namespace WinApp
{
    public partial class Form1 : Form
    {
        private readonly Calculation calculation = new Calculation();
        private readonly Generation generation = new Generation();

        public Form1()
        {
            InitializeComponent();
        }

        // ============================================================
        // LAB-1
        // ============================================================
        private void button_calculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(textBox_lambda.Text.Replace(".", ","), out double lambda))
                    throw new Exception("Введите корректное значение у параметра лямбда");
                if (!double.TryParse(textBox_n1.Text.Replace(".", ","), out double n1))
                    throw new Exception("Введите корректное значение у параметра n1");
                if (!double.TryParse(textBox_n2.Text.Replace(".", ","), out double n2))
                    throw new Exception("Введите корректное значение у параметра n2");
                if (!double.TryParse(textBox_a.Text.Replace(".", ","), out double a))
                    throw new Exception("Введите корректное значение у параметра a");

                if (lambda <= 0 || n1 <= 0 || n2 <= 0 || a <= 0)
                    throw new Exception("Введите корректное значение");
                if (lambda < 1.4 || lambda > 1.6)
                    throw new Exception("Введите длину волны в диапазоне 1,4–1,6 мкм");
                if (n1 < n2)
                    throw new Exception("Параметр n1 должен быть больше n2");

                double V = calculation.GetValueV(a, lambda, n1, n2);

                label_answer.Text = V < 2.4
                    ? $"Ответ: {V}. Волновод одномодовый"
                    : $"Ответ: {V}. Волновод многомодовый";

                generation.GenerateLab1(new LabData
                {
                    Lambda = lambda,
                    N1 = n1,
                    N2 = n2,
                    A = a
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ============================================================
        // LAB-2
        // ============================================================
        private void L2_button_calculate_Click(object sender, EventArgs e)
        {
            try
            {
                const double C = 0.8;
                if (!double.TryParse(L2_textBox_lambda.Text.Replace(".", ","), out double L2_lambda))
                    throw new Exception("Введите корректное значение у параметра лямбда");
                if (!double.TryParse(L2_textBox_L.Text.Replace(".", ","), out double L2_L))
                    throw new Exception("Введите корректное значение у параметра L");
                if (!double.TryParse(L2_textBox_P0.Text.Replace(".", ","), out double L2_P0))
                    throw new Exception("Введите корректное значение у параметра P0");

                if (L2_L < 0 || L2_lambda < 0 || L2_P0 < 0)
                    throw new Exception("Введите корректные значения");
                if (L2_L < 100 || L2_L > 1000)
                    throw new Exception("Введите длину волновода 100–1000 м");
                if (L2_lambda < 1 || L2_lambda > 2)
                    throw new Exception("Введите длину волны 1–2 мкм");

                double Pt = calculation.GetValueP_t(L2_P0, L2_L, L2_lambda, C);

                generation.GenerateLab2(L2_P0, L2_lambda, L2_L);

                L2_label_answer.Text = $"Ответ: {Pt}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ============================================================
        // LAB-3
        // ============================================================
        private void L3_button_calculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(L3_textBox_lambda.Text.Replace(".", ","), out double L3_lambda))
                    throw new Exception("Введите корректное значение у параметра лямбда");
                if (L3_lambda < 0.005 || L3_lambda > 10)
                    throw new Exception("Введите длину волны 0.05–10 мкм");

                double n = calculation.GetRefractiveIndex(L3_lambda);
                double vg = calculation.GetGroupSpeed(L3_lambda);
                var (b0, b1, b2) = calculation.GetPropagationConstants(L3_lambda);

                double lambdaM = L3_lambda * 1e-6;
                double omega0 = 2 * Math.PI * 2.99792458e8 / lambdaM;
                double taylor = calculation.GetTaylorSeries(L3_lambda, omega0);
                bool equal = calculation.IsTaylorSeriesEqualsDgs(L3_lambda);

                L3_label_answer.Text =
                    $"n = {n:F6}\n" +
                    $"v_g = {vg:E6} м/с\n" +
                    $"β₂ = {b2:E6} с²/м\n" +
                    $"Taylor = {taylor:E6}\n" +
                    $"Taylor == ДГС-2: {equal}";

                generation.GenerateLab3(L3_lambda, n, vg, b0, b1, b2, taylor, equal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ============================================================
        // LAB-4
        // ============================================================
        private void L4_button_calculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(L4_textBox_B2.Text.Replace(".", ","), out double L4_B2))
                    throw new Exception("Введите корректное значение у параметра B2");
                if (!double.TryParse(L4_textBox_L.Text.Replace(".", ","), out double L4_L))
                    throw new Exception("Введите корректное значение у параметра L");
                if (!int.TryParse(L4_textBox_M.Text.Replace(".", ","), out int L4_M))
                    throw new Exception("Введите корректное значение у параметра M");
                if (!int.TryParse(L4_textBox_N.Text.Replace(".", ","), out int L4_N))
                    throw new Exception("Введите корректное значение у параметра N");
                if (!double.TryParse(L4_textBox_T0.Text.Replace(".", ","), out double L4_T0))
                    throw new Exception("Введите корректное значение у параметра T0");

                if (L4_B2 < 0 || L4_L < 0 || L4_M < 0 || L4_N < 0 || L4_T0 < 0)
                    throw new Exception("Введите корректные значения");
                if (!calculation.IsPowerOfTwo(L4_N))
                    throw new Exception("N должно быть степенью 2");
                if (L4_N < 16)
                    throw new Exception("N должно быть не меньше 16 (иначе SSFM не работает)");
                if (L4_M < 1)
                    throw new Exception("M должно быть не меньше 1");

                Lab4Result lab4 = calculation.CalculatePart6(L4_B2, L4_T0, L4_N, L4_M);

                L4_label_answer.Text =
                    $"L_D = {lab4.LD:E6}\n" +
                    $"Ошибка = {lab4.Error:E6}\n" +
                    $"FWHM вх = {lab4.FWHM_input:F6}\n" +
                    $"FWHM вых = {lab4.FWHM_output:F6}";

                generation.GenerateLab4(lab4, L4_B2, L4_T0, L4_N, L4_M);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ============================================================
        // LAB-5
        // ============================================================
        private void L5_button_calculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(L5_textBox_B2.Text.Replace(".", ","), out double L5_B2))
                    throw new Exception("Введите корректное значение у параметра B2");
                if (!double.TryParse(L5_textBox_L.Text.Replace(".", ","), out double L5_L))
                    throw new Exception("Введите корректное значение у параметра L");
                if (!int.TryParse(L5_textBox_M.Text.Replace(".", ","), out int L5_M))
                    throw new Exception("Введите корректное значение у параметра M");
                if (!int.TryParse(L5_textBox_N.Text.Replace(".", ","), out int L5_N))
                    throw new Exception("Введите корректное значение у параметра N");
                if (!double.TryParse(L5_textBox_T0.Text.Replace(".", ","), out double L5_T0))
                    throw new Exception("Введите корректное значение у параметра T0");
                if (!double.TryParse(L5_textBox_gamma.Text.Replace(".", ","), out double L5_gamma))
                    throw new Exception("Введите корректное значение у параметра Гамма");

                if (L5_B2 < 0 || L5_L < 0 || L5_M < 0 || L5_N < 0 || L5_T0 < 0)
                    throw new Exception("Введите корректные значения");
                if (!calculation.IsPowerOfTwo(L5_N))
                    throw new Exception("N должно быть степенью 2");

                double L5_P0 = 1.0; // при необходимости вынести на форму

                Lab5Result lab5 = calculation.CalculateLab5(
                    L5_B2, L5_gamma, L5_T0, L5_P0, L5_L, L5_N, L5_M);

                L5_label_ansver.Text =
                    $"L_D = {lab5.LD:E6}\n" +
                    $"L_NL = {lab5.LNL:E6}\n" +
                    $"N_soliton = {lab5.N_soliton:F6}\n" +
                    $"Режим = {lab5.Mode}";

                generation.GenerateLab5(lab5, L5_B2, L5_gamma, L5_T0, L5_P0, L5_L, L5_N, L5_M);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ============================================================
        // LAB-6
        // ============================================================
        private void L6_button_calculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(L6_textBox_L.Text.Replace(".", ","), out double L6_L))
                    throw new Exception("Введите корректное значение у параметра L");
                if (!int.TryParse(L6_textBox_M.Text.Replace(".", ","), out int L6_M))
                    throw new Exception("Введите корректное значение у параметра M");
                if (!int.TryParse(L6_textBox_N.Text.Replace(".", ","), out int L6_N))
                    throw new Exception("Введите корректное значение у параметра N");
                if (!double.TryParse(L6_textBox_L_NL.Text.Replace(".", ","), out double L6_L_NL))
                    throw new Exception("Введите корректное значение у параметра L_NL");
                if (!double.TryParse(L6_textBox_s.Text.Replace(".", ","), out double L6_s))
                    throw new Exception("Введите корректное значение у параметра s");

                if (L6_L < 0 || L6_M < 0 || L6_N < 0 || L6_L_NL < 0 || L6_s < 0)
                    throw new Exception("Введите корректные значения");
                if (!calculation.IsPowerOfTwo(L6_N))
                    throw new Exception("N должно быть степенью 2");

                double L6_T0 = 1.0; // при необходимости вынести на форму

                Lab6Result lab6 = calculation.CalculateLab6(
                    L6_s, L6_L_NL, L6_T0, L6_L, L6_N, L6_M);

                L6_label_ansver.Text =
                    $"z_s = {lab6.z_s:E6}\n" +
                    $"Ошибка = {lab6.Error:E6}";

                generation.GenerateLab6(lab6, L6_s, L6_L_NL, L6_T0, L6_L, L6_N, L6_M);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}