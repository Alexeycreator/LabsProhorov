using System;
using System.Globalization;
using System.Windows.Forms;
using Shared.Calculate;
using Shared.Helpers;


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
                    throw new Exception("Введите корректное  значение");
                if (lambda < 1.4 || lambda > 1.6)
                    throw new Exception("Введите длину волны в диапазоне от 1,4 мкм до 1,6 мкм");
                if (n1 < n2)
                    throw new Exception("Параметр n1 должен быть больше n2");

                double V = calculation.GetValueV(a, lambda, n1, n2);
                if (V < 2.4)
                    label_answer.Text = $"Ответ: {V}. Волновод одно модовый";
                else
                    label_answer.Text = $"Ответ: {V}. Волновод многомодовый";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void L2_button_calculate_Click(object sender, EventArgs e)
        {
            try
            {
                double C = 0.8;
                if (!double.TryParse(L2_textBox_lambda.Text.Replace(".", ","), out double L2_lambda))
                    throw new Exception("Введите корректное значение у параметра лямбда");
                if (!double.TryParse(L2_textBox_L.Text.Replace(".", ","), out double L2_L))
                    throw new Exception("Введите корректное значение у параметра L");
                if (!double.TryParse(L2_textBox_P0.Text.Replace(".", ","), out double L2_P0))
                    throw new Exception("Введите корректное значение у параметра P0");

                if (L2_L < 0 || L2_lambda < 0 || L2_P0 < 0)
                    throw new Exception("Введите коректные значения");
                if (L2_L < 100 || L2_L > 1000)
                    throw new Exception("Введите значение длины волновода в диапазоне от 100м до 1000м (1км)");
                if (L2_lambda < 1 || L2_lambda > 2)
                    throw new Exception("Введите значение длины волны в диапазоне от 1 мкм до 2 мкм");

                double Pt = calculation.GetValueP_t(L2_P0, L2_L, L2_lambda, C);
                generation.GenerateData(L2_P0);
                L2_label_answer.Text = $"Ответ: {Pt}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void L3_button_calculate_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(L3_textBox_lambda.Text.Replace(".", ","), out double L3_lambda))
                throw new Exception("Введите корректное значение у параметра лямбда");
            if (L3_lambda < 0)
                throw new Exception("Введите коректные значения");
            if (L3_lambda < 0.005 || L3_lambda > 10)
                throw new Exception("Введите значение длины волны в диапазоне от 0.05 мкм до 10 мкм");
        }
    }
}