using SlovarLib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;

namespace Theme5_Var3
{
    public partial class Form1 : Form
    {
        private Slovar currentSlovar;
        private string currentFilePath = "";

        public Form1()
        {
            InitializeComponent();
            lstResults.SelectionMode = SelectionMode.MultiExtended;
        }

        private void btnSelectDict_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currentFilePath = ofd.FileName;
                    currentSlovar = new Slovar(currentFilePath);
                    lstDictionary.DataSource = null;
                    lstDictionary.DataSource = currentSlovar.Words;
                    lblStatus.Text = $"Загружено слов: {currentSlovar.Count}";
                    MessageBox.Show("Словарь успешно загружен!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            if (currentSlovar == null) return;
            var filtered = currentSlovar.FindByPrefix(txtFilter.Text);
            lstDictionary.DataSource = null;
            lstDictionary.DataSource = filtered;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (currentSlovar == null || string.IsNullOrWhiteSpace(txtNewWord.Text)) return;

            if (currentSlovar.AddWord(txtNewWord.Text))
            {
                lstDictionary.DataSource = null;
                lstDictionary.DataSource = currentSlovar.Words;
                txtNewWord.Clear();
                MessageBox.Show("Слово добавлено.", "Успех");
            }
            else
            {
                MessageBox.Show("Такое слово уже есть или введено некорректно.", "Внимание");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentSlovar == null || lstDictionary.SelectedItem == null) return;

            string wordToDelete = lstDictionary.SelectedItem.ToString();
            if (MessageBox.Show($"Удалить слово '{wordToDelete}'?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (currentSlovar.RemoveWord(wordToDelete))
                {
                    lstDictionary.DataSource = null;
                    lstDictionary.DataSource = currentSlovar.Words;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (currentSlovar == null || string.IsNullOrWhiteSpace(txtSearchWord.Text))
            {
                MessageBox.Show("Выберите словарь и введите слово для поиска.");
                return;
            }

            List<string> results = new List<string>();

            if (rbAnagrams.Checked)
            {
                // ВАРИАНТ 3: Поиск анаграмм
                results = currentSlovar.FindAnagrams(txtSearchWord.Text);
                if (results.Count == 0)
                    MessageBox.Show("Анаграммы не найдены.", "Результат");
            }
            else if (rbFuzzy.Checked)
            {
                int maxDist = (int)numMaxDist.Value;
                results = currentSlovar.FuzzySearch(txtSearchWord.Text, maxDist);
                if (results.Count == 0)
                    MessageBox.Show($"Слова с расстоянием Левенштейна <= {maxDist} не найдены.", "Результат");
            }

            lstResults.DataSource = null;
            lstResults.DataSource = results;
        }

        private void btnSaveResults_Click(object sender, EventArgs e)
        {
            if (lstResults.Items.Count == 0)
            {
                MessageBox.Show("Нет результатов для сохранения.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files|*.txt";
            sfd.FileName = "search_results.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<string> lines = new List<string>();
                    lines.Add($"Поиск для слова: {txtSearchWord.Text}");
                    lines.Add(rbAnagrams.Checked ? "Тип: Анаграммы" : "Тип: Нечеткий поиск");
                    lines.Add("-------------------");

                    foreach (var item in lstResults.Items)
                    {
                        lines.Add(item.ToString());
                    }

                    File.WriteAllLines(sfd.FileName, lines, System.Text.Encoding.UTF8);
                    MessageBox.Show("Результаты сохранены в файл: " + sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения: " + ex.Message);
                }
            }
        }
    }
}