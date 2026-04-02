using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Тема_8
{
    public partial class HistoryForm : Form
    {
        public HistoryForm(Player player)
        {
            InitializeComponent();

            lblTitle.Text = $"История игр: {player.Login}";
            lblTotal.Text = $"Всего игр: {player.History.Count}";

            dgvHistory.AutoGenerateColumns = false;
            dgvHistory.Columns.Clear();

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Date", HeaderText = "Дата", Width = 120 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Bet", HeaderText = "Ставка ($)", Width = 80 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Выбор", Width = 100 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Победитель", Width = 100 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Profit",  
                DataPropertyName = "Profit",
                HeaderText = "Результат ($)",
                Width = 100
            });
            var reportData = player.History.Select(h => new
            {
                Date = h.Date.ToString("dd.MM.yyyy HH:mm"),
                h.Bet,
                Choice = $"Таракан #{h.SelectedCockroach + 1}",
                Winner = $"Таракан #{h.Winner + 1}",
                Profit = h.Profit
            }).ToList();

            dgvHistory.DataSource = reportData;

            foreach (DataGridViewRow row in dgvHistory.Rows)
            {
                if (row.Cells["Profit"].Value != null)
                {
                    int profit = Convert.ToInt32(row.Cells["Profit"].Value);
                    if (profit > 0)
                        row.Cells["Profit"].Style.ForeColor = Color.Green;
                    else if (profit < 0)
                        row.Cells["Profit"].Style.ForeColor = Color.Red;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}