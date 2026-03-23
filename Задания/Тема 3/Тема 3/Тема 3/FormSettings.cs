using System;
using System.Drawing;
using System.Windows.Forms;

namespace Theme3_Var23
{
    public partial class FormSettings : Form
    {
        public Color SelectedColor { get; set; }
        public float SpeedValue { get; set; }

        public FormSettings(Color initialColor, float initialSpeed)
        {
            InitializeComponent();

            SelectedColor = initialColor;
            SpeedValue = initialSpeed;

            labelColor.BackColor = initialColor;
            labelColor.BorderStyle = BorderStyle.FixedSingle;

            trackBarSpeed.Value = (int)(initialSpeed * 1000);
            UpdateSpeedLabel();
        }

        private void UpdateSpeedLabel()
        {
            SpeedValue = trackBarSpeed.Value / 1000f;
            labelSpeed.Text = $"Скорость: {SpeedValue:F3}";
        }

        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = SelectedColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                SelectedColor = colorDialog1.Color;
                labelColor.BackColor = SelectedColor;
            }
        }

        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            UpdateSpeedLabel();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}