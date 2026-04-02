using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Тема_8;

namespace Тема_8
{
    public partial class Form1 : Form
    {
        private List<Cockroach> cockroaches;
        private Timer gameTimer;
        private Player currentPlayer;
        private Dictionary<string, Player> allPlayers;

        private int finishLineX;
        private bool isRaceRunning = false;
        private int currentBet = 0;
        private int selectedCockroachIndex = -1;

        private readonly string[] cockroachImages = new string[4];

        public Form1(Player player, Dictionary<string, Player> players)
        {
            InitializeComponent();
            currentPlayer = player;
            allPlayers = players;

            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
            cockroachImages[0] = Path.Combine(basePath, "cockroach_red.png");
            cockroachImages[1] = Path.Combine(basePath, "cockroach_green.png");
            cockroachImages[2] = Path.Combine(basePath, "cockroach_blue.png");
            cockroachImages[3] = Path.Combine(basePath, "cockroach_orange.png");

            SetupGame();
            UpdateBalanceLabel();

            gameTimer = new Timer();
            gameTimer.Interval = 30;
            gameTimer.Tick += GameTimer_Tick;
        }

        private void SetupGame()
        {
            cockroaches = new List<Cockroach>();
            int laneHeight = gamePanel.ClientSize.Height / 4;

            Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Orange };

            for (int i = 0; i < 4; i++)
            {
                float y = i * laneHeight + (laneHeight - 40) / 2;

                var cr = new Cockroach(i, colors[i], y);

                if (File.Exists(cockroachImages[i]))
                {
                    cr.ImagePath = cockroachImages[i];
                    cr.LoadImage();
                }
                else
                {
                    cr.ImagePath = "";
                }

                cockroaches.Add(cr);
            }

            finishLineX = gamePanel.ClientSize.Width - 60;
            isRaceRunning = false;
            btnStart.Enabled = true;
            cmbCockroach.Enabled = true;
            numBet.Enabled = true;

            cmbCockroach.Items.Clear();
            for (int i = 0; i < 4; i++) cmbCockroach.Items.Add($"Таракан #{i + 1}");
            cmbCockroach.SelectedIndex = 0;

            gamePanel.Invalidate();
        }

        private void UpdateBalanceLabel()
        {
            lblBalance.Text = $"Игрок: {currentPlayer.Login} | Баланс: {currentPlayer.Balance}$";
        }

        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int laneHeight = gamePanel.ClientSize.Height / 4;

            for (int i = 0; i < 4; i++)
            {
                int y = i * laneHeight;
                Color laneColor = (i % 2 == 0) ? Color.FromArgb(240, 255, 240) : Color.FromArgb(230, 255, 230);
                using (Brush b = new SolidBrush(laneColor))
                {
                    g.FillRectangle(b, 0, y, gamePanel.ClientSize.Width, laneHeight);
                }
                g.DrawLine(Pens.Gray, 0, y, gamePanel.ClientSize.Width, y);
            }
            g.DrawLine(Pens.Gray, 0, gamePanel.ClientSize.Height - 1, gamePanel.ClientSize.Width, gamePanel.ClientSize.Height - 1);

            finishLineX = gamePanel.ClientSize.Width - 60;
            int checkSize = 10;
            for (int y = 0; y < gamePanel.ClientSize.Height; y += checkSize)
            {
                for (int x = 0; x < checkSize; x++)
                {
                    bool isBlack = ((x / checkSize) + (y / checkSize)) % 2 == 0;
                    using (Brush b = new SolidBrush(isBlack ? Color.Black : Color.White))
                    {
                        g.FillRectangle(b, finishLineX + x, y, 1, checkSize);
                    }
                }
            }
            g.DrawLine(Pens.Black, finishLineX, 0, finishLineX, gamePanel.ClientSize.Height);
            g.DrawString("ФИНИШ", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, finishLineX + 5, 5);

            foreach (var cr in cockroaches)
            {
                cr.Draw(g);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cmbCockroach.SelectedIndex == -1) return;

            currentBet = (int)numBet.Value;
            if (currentBet > currentPlayer.Balance)
            {
                MessageBox.Show("Недостаточно средств!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool imagesMissing = false;
            foreach (var cr in cockroaches)
            {
                if (string.IsNullOrEmpty(cr.ImagePath) || !File.Exists(cr.ImagePath))
                    imagesMissing = true;
            }
            if (imagesMissing)
            {
                DialogResult res = MessageBox.Show(
                    "Файлы изображений тараканов не найдены в папке 'images'.\n" +
                    "Игра будет использовать графические примитивы (овалы).\n" +
                    "Хотите продолжить?",
                    "Внимание",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (res == DialogResult.No) return;
            }

            selectedCockroachIndex = cmbCockroach.SelectedIndex;

            currentPlayer.Balance -= currentBet;
            UpdateBalanceLabel();

            foreach (var cr in cockroaches)
            {
                cr.X = 10;
                cr.HasFinished = false;
                cr.IsMoving = true;
                cr.Speed = 0;
                if (!string.IsNullOrEmpty(cr.ImagePath) && File.Exists(cr.ImagePath))
                    cr.LoadImage();
            }

            isRaceRunning = true;
            btnStart.Enabled = false;
            cmbCockroach.Enabled = false;
            numBet.Enabled = false;
            lblStatus.Text = "Гонка началась!";

            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!isRaceRunning) return;

            int winnerIndex = -1;

            foreach (var cr in cockroaches)
            {
                if (!cr.HasFinished)
                {
                    cr.Move(finishLineX);
                    if (cr.HasFinished && winnerIndex == -1)
                    {
                        winnerIndex = cr.Lane;
                        foreach (var other in cockroaches)
                        {
                            other.IsMoving = false; 
                            other.Speed = 0;
                        }
                        break;
                    }
                }
            }

            gamePanel.Invalidate(); 

            if (winnerIndex != -1)
            {
                gameTimer.Stop();
                FinishRace(winnerIndex);
            }
        }

        private void FinishRace(int winnerIndex)
        {
            isRaceRunning = false;
            string message = "";
            int profit = 0;

            if (winnerIndex == selectedCockroachIndex)
            {
                int winAmount = currentBet * 3;
                currentPlayer.Balance += winAmount;
                profit = winAmount - currentBet;
                message = $"Победа! Выиграл Таракан #{winnerIndex + 1}.\nВаш выигрыш: {winAmount}$";
            }
            else
            {
                profit = -currentBet;
                message = $"Проигрыш! Выиграл Таракан #{winnerIndex + 1}.\nВы потеряли {currentBet}$";
            }

            var result = new GameResult
            {
                Date = DateTime.Now,
                Bet = currentBet,
                SelectedCockroach = selectedCockroachIndex,
                Winner = winnerIndex,
                Profit = profit
            };
            currentPlayer.History.Add(result);

            DataManager.SavePlayers(allPlayers);

            UpdateBalanceLabel();
            MessageBox.Show(message, "Результат забега");

            btnStart.Enabled = true;
            cmbCockroach.Enabled = true;
            numBet.Enabled = true;
            lblStatus.Text = "";
        }

        private void gamePanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (!isRaceRunning) return;

            Point clickPoint = e.Location;
            bool pushed = false;

            foreach (var cr in cockroaches)
            {
                if (cr.HitTest(clickPoint) && !cr.HasFinished)
                {
                    cr.Push();
                    pushed = true;
                }
            }

            if (pushed) gamePanel.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm settings = new SettingsForm())
            {
                settings.ShowDialog();
            }
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryForm historyForm = new HistoryForm(currentPlayer);
            historyForm.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Правила:\n1. Выберите таракана и сделайте ставку.\n2. Нажмите Старт.\n3. Если таракан застрял, кликните по нему мышкой!\n4. Победа приносит x3 от ставки.");
        }
    }
}