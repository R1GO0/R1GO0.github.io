using System;
using System.IO;
using System.Windows.Forms;

namespace SPB_Quiz
{
    public partial class Admin : Form
    {
        private XmlDataManager dataManager;

        public Admin(XmlDataManager dm)
        {
            InitializeComponent();
            dataManager = dm;
            SetupGrid();
            LoadTopics();
        }

        private void SetupGrid()
        {
            dgvAnswers.AutoGenerateColumns = false;
            dgvAnswers.Columns.Clear();

            DataGridViewTextBoxColumn colText = new DataGridViewTextBoxColumn();
            colText.Name = "colAnswerText";
            colText.HeaderText = "Текст ответа";
            colText.Width = 250;
            dgvAnswers.Columns.Add(colText);

            DataGridViewCheckBoxColumn colCheck = new DataGridViewCheckBoxColumn();
            colCheck.Name = "colIsCorrect";
            colCheck.HeaderText = "Правильный?";
            colCheck.Width = 80;
            dgvAnswers.Columns.Add(colCheck);
        }

        private void LoadTopics()
        {
            cmbTopics.Items.Clear();
            var topics = dataManager.GetTopics();
            cmbTopics.Items.AddRange(topics.ToArray());
            if (topics.Count > 0)
                cmbTopics.SelectedIndex = 0;
        }

        private void btnBrowseImg_Click(object sender, EventArgs e)
        {
            ofdImage.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp";
            ofdImage.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");

            if (!Directory.Exists(ofdImage.InitialDirectory))
                Directory.CreateDirectory(ofdImage.InitialDirectory);

            if (ofdImage.ShowDialog() == DialogResult.OK)
            {
                string fullPath = ofdImage.FileName;
                string relativePath = "images\\" + Path.GetFileName(fullPath);

                string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
                try
                {
                    File.Copy(fullPath, destPath, true);
                    txtImagePath.Text = relativePath;
                    MessageBox.Show("Картинка скопирована в папку проекта.", "Успех");
                }
                catch (Exception ex)
                {
                    txtImagePath.Text = relativePath;
                    MessageBox.Show($"Файл не скопирован автоматически (ошибка: {ex.Message}). Убедитесь, что он лежит в папке images.", "Внимание");
                }
            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            dgvAnswers.Rows.Add("", false);
        }

        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            if (dgvAnswers.CurrentRow != null)
            {
                dgvAnswers.Rows.Remove(dgvAnswers.CurrentRow);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbTopics.Text))
            {
                MessageBox.Show("Введите название темы!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtQuestionText.Text))
            {
                MessageBox.Show("Введите текст вопроса!");
                return;
            }
            if (dgvAnswers.Rows.Count < 2)
            {
                MessageBox.Show("Должно быть минимум 2 варианта ответа!");
                return;
            }

            int correctCount = 0;
            QuestionModel q = new QuestionModel
            {
                Text = txtQuestionText.Text,
                ImagePath = txtImagePath.Text,
                Hint = txtHint.Text,
                Answers = new System.Collections.Generic.List<AnswerOption>()
            };

            foreach (DataGridViewRow row in dgvAnswers.Rows)
            {
                if (row.IsNewRow) continue;

                string ansText = row.Cells["colAnswerText"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(ansText)) continue;

                bool isCorrect = Convert.ToBoolean(row.Cells["colIsCorrect"].Value ?? false);
                if (isCorrect) correctCount++;

                q.Answers.Add(new AnswerOption
                {
                    Text = ansText,
                    IsCorrect = isCorrect
                });
            }

            if (correctCount == 0)
            {
                MessageBox.Show("Необходимо отметить хотя бы один правильный ответ!");
                return;
            }
            if (correctCount > 1)
            {
                if (MessageBox.Show("Вы отметили несколько правильных ответов. В текущей версии игры поддерживается только один правильный ответ. Продолжить?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            try
            {
                dataManager.AddQuestion(cmbTopics.Text, (int)numLevel.Value, q);
                MessageBox.Show("Вопрос успешно добавлен в XML!", "Сохранено");

                txtQuestionText.Clear();
                txtHint.Clear();
                txtImagePath.Clear();
                dgvAnswers.Rows.Clear();
                LoadTopics(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}