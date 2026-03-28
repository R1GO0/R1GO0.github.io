using System;
using System.Windows.Forms;

namespace SPB_Quiz
{
    public partial class Form1 : Form
    {
        private XmlDataManager dataManager;
        private string currentTopic;

        public Form1()
        {
            InitializeComponent();
            dataManager = new XmlDataManager("spb_quiz.xml");
            LoadTopics();
        }

        private void LoadTopics()
        {
            cmbTopic.Items.Clear();
            cmbTopic.Items.AddRange(dataManager.GetTopics().ToArray());
            if (cmbTopic.Items.Count > 0) cmbTopic.SelectedIndex = 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cmbTopic.SelectedItem == null)
            {
                MessageBox.Show("Выберите тему!");
                return;
            }

            string topic = cmbTopic.SelectedItem.ToString();
            int level = (int)numLevel.Value;
            while (true)
            {
                using (var gameForm = new GameForm(dataManager, topic, level))
                {
                    DialogResult result = gameForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        int nextLevel = level + 1;

                        if (dataManager.HasNextLevel(topic, level))
                        {
                            level = nextLevel;
                            continue;
                        }
                        else
                        {
                            break; 
                        }
                    }
                    else if (result == DialogResult.Retry)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void StartLevel(int level)
        {
            int currentLevel = (int)numLevel.Value;
            using (var gameForm = new GameForm(dataManager, currentTopic, level))
            {
                DialogResult result = gameForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    currentLevel++;
                    StartLevel(currentLevel);
                }
                else if (result == DialogResult.Retry)
                {
                    StartLevel(level);
                }
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            var adminForm = new Admin(dataManager);
            adminForm.ShowDialog();
            LoadTopics();
        }
    }
}