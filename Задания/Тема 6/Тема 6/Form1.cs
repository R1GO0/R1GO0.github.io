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
            int currentLevel = (int)numLevel.Value;
            if (cmbTopic.SelectedItem == null) return;
            currentTopic = cmbTopic.SelectedItem.ToString();
            StartLevel(currentLevel);
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