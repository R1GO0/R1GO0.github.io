using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Тема_8
{
    public partial class SettingsForm : Form
    {
        private Color[] tempColors;

        public SettingsForm()
        {
            InitializeComponent();

            tempColors = new Color[GameSettings.CockroachColors.Length];
            Array.Copy(GameSettings.CockroachColors, tempColors, GameSettings.CockroachColors.Length);

            trbSpeed.Value = GameSettings.GameSpeedMultiplier;
            lblSpeedValue.Text = $"Текущая скорость: x{trbSpeed.Value}";
        }

        
        private void trbSpeed_Scroll(object sender, EventArgs e)
        {
            lblSpeedValue.Text = $"Текущая скорость: x{trbSpeed.Value}";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Array.Copy(tempColors, GameSettings.CockroachColors, tempColors.Length);
            GameSettings.GameSpeedMultiplier = trbSpeed.Value;

            MessageBox.Show("Настройки сохранены!\nОни применятся к следующему забегу.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}