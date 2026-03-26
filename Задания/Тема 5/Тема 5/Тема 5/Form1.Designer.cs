namespace Theme5_Var3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstDictionary = new System.Windows.Forms.ListBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.txtNewWord = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSelectDict = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.btnSaveResults = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.numMaxDist = new System.Windows.Forms.NumericUpDown();
            this.txtSearchWord = new System.Windows.Forms.TextBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.rbFuzzy = new System.Windows.Forms.RadioButton();
            this.rbAnagrams = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDist)).BeginInit();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstDictionary
            // 
            this.lstDictionary.FormattingEnabled = true;
            this.lstDictionary.Location = new System.Drawing.Point(26, 27);
            this.lstDictionary.Name = "lstDictionary";
            this.lstDictionary.Size = new System.Drawing.Size(177, 121);
            this.lstDictionary.TabIndex = 0;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(26, 231);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(177, 20);
            this.txtFilter.TabIndex = 1;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Location = new System.Drawing.Point(26, 257);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(177, 23);
            this.btnApplyFilter.TabIndex = 2;
            this.btnApplyFilter.Text = "Применить";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            // 
            // txtNewWord
            // 
            this.txtNewWord.Location = new System.Drawing.Point(325, 28);
            this.txtNewWord.Name = "txtNewWord";
            this.txtNewWord.Size = new System.Drawing.Size(99, 20);
            this.txtNewWord.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(247, 54);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(177, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(26, 167);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(177, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Удалить выделенное слово";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSelectDict
            // 
            this.btnSelectDict.Location = new System.Drawing.Point(26, 326);
            this.btnSelectDict.Name = "btnSelectDict";
            this.btnSelectDict.Size = new System.Drawing.Size(177, 23);
            this.btnSelectDict.TabIndex = 6;
            this.btnSelectDict.Text = "Выбор файла";
            this.btnSelectDict.UseVisualStyleBackColor = true;
            this.btnSelectDict.Click += new System.EventHandler(this.btnSelectDict_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(23, 151);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(74, 13);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Кол-во слов: ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(801, 452);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.lstDictionary);
            this.tabPage1.Controls.Add(this.lblStatus);
            this.tabPage1.Controls.Add(this.txtFilter);
            this.tabPage1.Controls.Add(this.btnSelectDict);
            this.tabPage1.Controls.Add(this.btnApplyFilter);
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.txtNewWord);
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(793, 426);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Работа со словарем";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Фильтр по первой букве/слогу:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Новое слово:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.lstResults);
            this.tabPage2.Controls.Add(this.btnSaveResults);
            this.tabPage2.Controls.Add(this.btnSearch);
            this.tabPage2.Controls.Add(this.numMaxDist);
            this.tabPage2.Controls.Add(this.txtSearchWord);
            this.tabPage2.Controls.Add(this.groupBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(793, 426);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Поиск";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Результаты поиска:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Макс. расстояние";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Искомое слово:";
            // 
            // lstResults
            // 
            this.lstResults.FormattingEnabled = true;
            this.lstResults.Location = new System.Drawing.Point(413, 43);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(229, 173);
            this.lstResults.TabIndex = 5;
            // 
            // btnSaveResults
            // 
            this.btnSaveResults.Location = new System.Drawing.Point(567, 234);
            this.btnSaveResults.Name = "btnSaveResults";
            this.btnSaveResults.Size = new System.Drawing.Size(75, 23);
            this.btnSaveResults.TabIndex = 4;
            this.btnSaveResults.Text = "Сохранить в файл";
            this.btnSaveResults.UseVisualStyleBackColor = true;
            this.btnSaveResults.Click += new System.EventHandler(this.btnSaveResults_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(36, 215);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(227, 31);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Поиск";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // numMaxDist
            // 
            this.numMaxDist.Location = new System.Drawing.Point(138, 179);
            this.numMaxDist.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numMaxDist.Name = "numMaxDist";
            this.numMaxDist.Size = new System.Drawing.Size(37, 20);
            this.numMaxDist.TabIndex = 2;
            this.numMaxDist.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // txtSearchWord
            // 
            this.txtSearchWord.Location = new System.Drawing.Point(128, 143);
            this.txtSearchWord.Name = "txtSearchWord";
            this.txtSearchWord.Size = new System.Drawing.Size(135, 20);
            this.txtSearchWord.TabIndex = 1;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.rbFuzzy);
            this.groupBox.Controls.Add(this.rbAnagrams);
            this.groupBox.Location = new System.Drawing.Point(36, 43);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(227, 84);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Тип поиска";
            // 
            // rbFuzzy
            // 
            this.rbFuzzy.AutoSize = true;
            this.rbFuzzy.Location = new System.Drawing.Point(23, 54);
            this.rbFuzzy.Name = "rbFuzzy";
            this.rbFuzzy.Size = new System.Drawing.Size(106, 17);
            this.rbFuzzy.TabIndex = 1;
            this.rbFuzzy.TabStop = true;
            this.rbFuzzy.Text = "Нечеткий поиск";
            this.rbFuzzy.UseVisualStyleBackColor = true;
            // 
            // rbAnagrams
            // 
            this.rbAnagrams.AutoSize = true;
            this.rbAnagrams.Location = new System.Drawing.Point(23, 30);
            this.rbAnagrams.Name = "rbAnagrams";
            this.rbAnagrams.Size = new System.Drawing.Size(111, 17);
            this.rbAnagrams.TabIndex = 0;
            this.rbAnagrams.TabStop = true;
            this.rbAnagrams.Text = "Поиск анаграмм";
            this.rbAnagrams.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDist)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstDictionary;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.TextBox txtNewWord;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSelectDict;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSaveResults;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.NumericUpDown numMaxDist;
        private System.Windows.Forms.TextBox txtSearchWord;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton rbFuzzy;
        private System.Windows.Forms.RadioButton rbAnagrams;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}

