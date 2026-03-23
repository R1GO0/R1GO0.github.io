using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using Тема_4;

namespace Тема_4
{
    public partial class Form1 : Form
    {
        private HerbCollection herbData;

        public Form1()
        {
            InitializeComponent();
            herbData = new HerbCollection();
            SetupGrid();
        }

        private void SetupGrid()
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "Название травы";
            dataGridView1.Columns[1].HeaderText = "% вхождения";
            dataGridView1.Columns[2].HeaderText = "Вес травы (г)";
            dataGridView1.Columns[3].HeaderText = "Статус";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название травы!");
                return;
            }

            if (!double.TryParse(txtPercent.Text, out double percent) || percent < 0)
            {
                MessageBox.Show("Некорректный процент!");
                return;
            }

            if (!double.TryParse(txtTotalWeight.Text, out double totalWeight) || totalWeight <= 0)
            {
                MessageBox.Show("Введите корректный общий вес сбора!");
                return;
            }

            if (herbData.TotalWeight != totalWeight)
            {
                herbData.TotalWeight = totalWeight;
                herbData.CalculateWeights();
                UpdateGrid();
                DrawChart();
            }

            herbData.AddItem(txtName.Text, percent);

            double sum = herbData.GetSumPercent();
            string comment = sum > 100 ? "ПЕРЕБОР!" : (sum < 100 ? "НЕДОБОР" : "НОРМА");

            UpdateGrid();
            DrawChart();

            txtName.Clear();
            txtPercent.Clear();
            txtName.Focus();

            if (sum > 100.1)
                MessageBox.Show($"Внимание! Сумма процентов превысила 100% ({sum:F2}%). Проверьте данные.");
        }

        private void UpdateGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in herbData.Items)
            {
                double sum = herbData.GetSumPercent();
                string comment = sum > 100 ? "ПЕРЕБОР" : (sum < 100 ? "НЕДОБОР" : "НОРМА");
                dataGridView1.Rows.Add(item.Name, item.Percent, item.Weight.ToString("F2"), comment);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    herbData.SaveToFile(saveFileDialog1.FileName);
                    MessageBox.Show("Данные успешно сохранены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения: {ex.Message}");
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    herbData.LoadFromFile(openFileDialog1.FileName);
                    txtTotalWeight.Text = herbData.TotalWeight.ToString();
                    UpdateGrid();
                    DrawChart();
                    MessageBox.Show("Данные успешно загружены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки: {ex.Message}");
                }
            }
        }

        private void DrawChart()
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();

            if (herbData.Items.Count == 0) return;

            var series = new Series("SeriesHerbs")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                LabelFormat = "{0}%"
            };

            chart1.BackColor = Color.LightGray;
            chart1.BorderlineColor = Color.Black;
            chart1.BorderlineDashStyle = ChartDashStyle.Solid;

            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            chart1.ChartAreas[0].Area3DStyle.Inclination = 20;

            chart1.Titles.Add("Доля трав в сборе");
            chart1.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);

            foreach (var item in herbData.Items)
            {
                series.Points.AddXY(item.Name, item.Percent);
            }

            chart1.Series.Add(series);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                DrawChart();
            }
        }
    }
}