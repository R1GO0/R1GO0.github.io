using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Тема_8
{
    public partial class LoginForm : Form
    {
        private Dictionary<string, Player> allPlayers;

        public LoginForm()
        {
            InitializeComponent();
            allPlayers = DataManager.LoadPlayers();

            txtLogin.Focus();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Пожалуйста, введите имя игрока!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Player currentPlayer;

            if (allPlayers.ContainsKey(login))
            {
                currentPlayer = allPlayers[login];
                MessageBox.Show($"С возвращением, {currentPlayer.Login}! Ваш баланс: {currentPlayer.Balance}$", "Успех");
            }
            else
            {
                currentPlayer = new Player(login);
                allPlayers.Add(login, currentPlayer);
                MessageBox.Show($"Добро пожаловать, {currentPlayer.Login}! Вам начислено 1000$ стартового капитала.", "Новый игрок");

                DataManager.SavePlayers(allPlayers);
            }

            Form1 gameForm = new Form1(currentPlayer, allPlayers);

            gameForm.FormClosed += (s, args) =>
            {
                DataManager.SavePlayers(allPlayers);
                this.Close(); 
            };

            gameForm.Show();
            this.Hide(); 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {
            btnEnter.Enabled = !string.IsNullOrWhiteSpace(txtLogin.Text);
        }
    }
}