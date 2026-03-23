using System;
using System.Windows.Forms;
using NumberUtils; // ЗАМЕНИТЕ на имя вашего основного проекта (namespace)

namespace Тема2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Вариант 3: Множители, НОД, НОК";
            this.Size = new System.Drawing.Size(500, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Элементы
            var lblNum1 = new Label { Text = "Первое число:", Left = 20, Top = 20, Width = 120 };
            var txtNum1 = new TextBox { Name = "txtNum1", Left = 150, Top = 17, Width = 100 };
            txtNum1.KeyPress += TxtNum_KeyPress;
            txtNum1.TextChanged += Txt_TextChanged;

            var lblNum2 = new Label { Text = "Второе число:", Left = 20, Top = 55, Width = 120 };
            var txtNum2 = new TextBox { Name = "txtNum2", Left = 150, Top = 52, Width = 100, Enabled = false };
            txtNum2.KeyPress += TxtNum_KeyPress;
            txtNum2.TextChanged += Txt_TextChanged;

            var lblOp = new Label { Text = "Операция:", Left = 20, Top = 90, Width = 120 };
            var cmbOp = new ComboBox
            {
                Name = "cmbOp",
                Left = 150,
                Top = 87,
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbOp.Items.AddRange(new[] {
                "Найти множители одного числа",
                "Найти НОД двух чисел",
                "Найти НОК двух чисел"
            });
            cmbOp.SelectedIndex = 0;
            cmbOp.SelectedIndexChanged += (s, e) => txtNum2.Enabled = (cmbOp.SelectedIndex > 0);
            cmbOp.SelectedIndexChanged += (s, e) => ValidateInput();

            var btnCalc = new Button
            {
                Name = "btnCalc",
                Text = "Вычислить",
                Left = 150,
                Top = 125,
                Width = 120,
                Enabled = false
            };
            btnCalc.Click += BtnCalc_Click;

            var lblRes = new Label { Text = "Результат:", Left = 20, Top = 165, Width = 100 };
            var txtRes = new TextBox
            {
                Name = "txtRes",
                Left = 20,
                Top = 190,
                Width = 440,
                Height = 180,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = System.Drawing.Color.White
            };

            this.Controls.AddRange(new Control[] { lblNum1, txtNum1, lblNum2, txtNum2, lblOp, cmbOp, btnCalc, lblRes, txtRes });
        }

        private void Txt_TextChanged(object sender, EventArgs e) => ValidateInput();

        private void ValidateInput()
        {
            var txt1 = this.Controls["txtNum1"] as TextBox;
            var txt2 = this.Controls["txtNum2"] as TextBox;
            var cmb = this.Controls["cmbOp"] as ComboBox;
            var btn = this.Controls["btnCalc"] as Button;

            bool hasNum1 = !string.IsNullOrWhiteSpace(txt1.Text);
            bool hasNum2 = !string.IsNullOrWhiteSpace(txt2.Text);

            // Кнопка активна, если первое число есть, и (если нужно второе) оно тоже есть
            if (cmb.SelectedIndex == 0)
                btn.Enabled = hasNum1;
            else
                btn.Enabled = hasNum1 && hasNum2;
        }

        private void TxtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnCalc_Click(object sender, EventArgs e)
        {
            var txt1 = this.Controls["txtNum1"] as TextBox;
            var txt2 = this.Controls["txtNum2"] as TextBox;
            var cmb = this.Controls["cmbOp"] as ComboBox;
            var txtRes = this.Controls["txtRes"] as TextBox;

            try
            {
                long num1 = long.Parse(txt1.Text);
                if (num1 < 1) throw new ArgumentException("Число должно быть >= 1");

                switch (cmb.SelectedIndex)
                {
                    case 0: // Множители
                        var pairs = FactorCalculator.GetFactorPairs(num1);
                        txtRes.Text = $"Множители числа {num1}:\n{FactorCalculator.FormatFactorPairs(pairs)}";
                        break;

                    case 1: // НОД
                        long num2_gcd = long.Parse(txt2.Text);
                        if (num2_gcd < 1) throw new ArgumentException("Число должно быть >= 1");
                        long gcd = FactorCalculator.CalculateGCD(num1, num2_gcd);
                        txtRes.Text = $"НОД({num1}, {num2_gcd}) = {gcd}";
                        break;

                    case 2: // НОК
                        long num2_lcm = long.Parse(txt2.Text);
                        if (num2_lcm < 1) throw new ArgumentException("Число должно быть >= 1");
                        long lcm = FactorCalculator.CalculateLCM(num1, num2_lcm);
                        txtRes.Text = $"НОК({num1}, {num2_lcm}) = {lcm}";
                        break;
                }
            }
            catch (Exception ex)
            {
                txtRes.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}