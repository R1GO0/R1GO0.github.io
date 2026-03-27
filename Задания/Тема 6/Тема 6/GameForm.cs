using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SPB_Quiz
{
    public partial class GameForm : Form
    {
        private XmlDataManager dataManager;
        private string currentTopic;
        private int currentLevel;
        private List<QuestionModel> questions;
        private int currentQuestionIndex;
        private int score;
        private const int QuestionsPerSession = 2;
        public GameForm(XmlDataManager dm, string topic, int level)
        {
            InitializeComponent();
            dataManager = dm;
            currentTopic = topic;
            currentLevel = level;
            score = 0;

            LoadQuestions();
            ShowQuestion();
        }

        private void LoadQuestions()
        {
            var allQuestions = dataManager.GetQuestions(currentTopic, currentLevel);

            if (allQuestions.Count == 0)
            {
                MessageBox.Show("Вопросы для этого уровня не найдены!");
                this.Close();
                return;
            }

            var random = new Random();
            questions = allQuestions.OrderBy(x => random.Next()).Take(QuestionsPerSession).ToList();
            currentQuestionIndex = 0;
        }

        private void ShowQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                FinishGame();
                return;
            }

            var q = questions[currentQuestionIndex];
            lblQuestion.Text = q.Text;
            lblHint.Visible = false;
            lblHint.Text = "";
            lblStatus.Text = "";
            cmbAnswers.SelectedIndex = -1;
            btnCheck.Enabled = false;
            btnNext.Enabled = false;

            pictureBox.Image = null;
            if (!string.IsNullOrEmpty(q.ImagePath))
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, q.ImagePath);
                if (File.Exists(fullPath))
                {
                    try { pictureBox.Image = Image.FromFile(fullPath); }
                    catch { lblStatus.Text = "Ошибка загрузки картинки"; }
                }
            }

            cmbAnswers.Items.Clear();

            var shuffledAnswers = new List<AnswerOption>(q.Answers);

            Random rand = new Random();
            shuffledAnswers = shuffledAnswers.OrderBy(x => rand.Next()).ToList();

            foreach (var ans in shuffledAnswers)
            {
                cmbAnswers.Items.Add(ans);
            }
        }

        private void cmbAnswers_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCheck.Enabled = true;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (cmbAnswers.SelectedItem == null) return;

            var selectedOption = cmbAnswers.SelectedItem as AnswerOption;
            if (selectedOption == null) return;

            var currentQ = questions[currentQuestionIndex];

            bool isCorrect = selectedOption.IsCorrect;

            if (isCorrect)
            {
                score += 100 / QuestionsPerSession;
                lblStatus.Text = "Верно!";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.Text = "Неверно! Правильный ответ: " +
                                 currentQ.Answers.First(a => a.IsCorrect).Text;
                lblStatus.ForeColor = Color.Red;
            }

            btnCheck.Enabled = false;
            btnNext.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentQuestionIndex++;
            ShowQuestion();
        }

        private void btnShowHint_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex < questions.Count)
            {
                lblHint.Text = "Подсказка: " + questions[currentQuestionIndex].Hint;
                lblHint.Visible = true;
                score -= 10;
            }
        }

        private void FinishGame()
        {
            string message = $"Игра окончена!\nВаш счет: {score} из 100";
            DialogResult resultDialog;

            if (score >= 80)
            {
                bool hasMoreLevels = dataManager.HasNextLevel(currentTopic, currentLevel);

                if (hasMoreLevels)
                {
                    message += "\n\nПоздравляем! Вы набрали более 80 баллов.\nПерейти на следующий уровень сложности?";
                    resultDialog = MessageBox.Show(message, "Успех!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultDialog == DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.OK; 
                        this.Close();
                        return;
                    }
                }
                else
                {
                    message += "\n\nПоздравляем! Вы прошли все доступные уровни сложности в этой теме!\nВы настоящий знаток Санкт-Петербурга!";
                    MessageBox.Show(message, "Победа!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                message += "\n\nДля перехода на следующий уровень нужно набрать минимум 80 баллов.\nПопробовать снова?";
                resultDialog = MessageBox.Show(message, "Результат", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultDialog == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Retry; 
                    this.Close();
                    return;
                }
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}