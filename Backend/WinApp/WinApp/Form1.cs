using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp
{
    public partial class Form1 : Form
    {
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
                    throw new Exception("Введите длину волны в диапазоне от 1,4 до 1,6");
                if (n1 < n2)
                    throw new Exception("Параметр n1 должен быть больше n2");

                lambda /= 1000000;
                a /= 1000000;
                double k0 = 2 * Math.PI / lambda;
                double V = k0 * a * Math.Sqrt(n1 - n2);

                label_answer.Text = $"Ответ: V = {V:F5}.";
                if (V < 2.4)
                    label_answer.Text += "Такое волокно называют одномодовым";
                else if (V > 2.4)
                    label_answer.Text += "Такое волокно называют многомодовым";
                else
                    label_answer.Text += "Волокно на границе одномодового/многомодового режима.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
