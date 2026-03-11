using System;
using System.Windows.Forms;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Задание_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double x = double.Parse(textBox2.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                double epsilon = double.Parse(textBox1.Text.Replace(',', '.'), CultureInfo.InvariantCulture);

                if (Math.Abs(x) >= 1)
                {
                    throw new ArgumentException("Модуль x должен быть меньше 1 (|x| < 1)");
                }

                if (epsilon <= 0)
                {
                    throw new ArgumentException("Точность должна быть больше нуля");
                }

                double sum = 0;
                double term = 2 * x;
                int n = 0;

                sum = term;

                while (Math.Abs(term) >= epsilon)
                {
                    n++;
                    term = term * x * x * (2 * n - 1) / (2 * n + 1);
                    sum += term;
                }

                double mathValue = Math.Log((1 + x) / (1 - x));
                double error = Math.Abs(mathValue - sum);

                label3.Text = $"Результат: {mathValue.ToString("F10")}";
                label4.Text = $"Сумма: {sum.ToString("F10")}";
                label5.Text = $"Количество членов ряда: {(n + 1).ToString()}";
                label6.Text = $"Погрешность: {error.ToString("E4")}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label3.Text = "";
                label4.Text = "";
                label5.Text = "";
                label6.Text = "";
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != '-' && ch != '.' && ch != 8)
            {
                e.Handled = true;
            }
            if (ch == '.' && ((System.Windows.Forms.TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
            if (ch == '-' && ((System.Windows.Forms.TextBox)sender).SelectionStart != 0)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != '.' && ch != 8)
            {
                e.Handled = true;
            }
            if (ch == '.' && ((System.Windows.Forms.TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CheckInputs();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CheckInputs();
        }

        private void CheckInputs()
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}