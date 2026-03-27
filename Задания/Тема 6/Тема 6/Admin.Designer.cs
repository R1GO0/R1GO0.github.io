namespace SPB_Quiz
{
    partial class Admin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpTopic = new System.Windows.Forms.GroupBox();
            this.numLevel = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTopics = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpQuestion = new System.Windows.Forms.GroupBox();
            this.txtQuestionText = new System.Windows.Forms.TextBox();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowseImg = new System.Windows.Forms.Button();
            this.txtHint = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grpAnswers = new System.Windows.Forms.GroupBox();
            this.dgvAnswers = new System.Windows.Forms.DataGridView();
            this.Текст = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Правильный = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSaveQuestion = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ofdImage = new System.Windows.Forms.OpenFileDialog();
            this.grpTopic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).BeginInit();
            this.grpQuestion.SuspendLayout();
            this.grpAnswers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnswers)).BeginInit();
            this.SuspendLayout();
            // 
            // grpTopic
            // 
            this.grpTopic.Controls.Add(this.label2);
            this.grpTopic.Controls.Add(this.numLevel);
            this.grpTopic.Controls.Add(this.label1);
            this.grpTopic.Controls.Add(this.cmbTopics);
            this.grpTopic.Location = new System.Drawing.Point(12, 12);
            this.grpTopic.Name = "grpTopic";
            this.grpTopic.Size = new System.Drawing.Size(219, 95);
            this.grpTopic.TabIndex = 0;
            this.grpTopic.TabStop = false;
            this.grpTopic.Text = "Тема и уровень:";
            // 
            // numLevel
            // 
            this.numLevel.Location = new System.Drawing.Point(93, 55);
            this.numLevel.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLevel.Name = "numLevel";
            this.numLevel.Size = new System.Drawing.Size(40, 20);
            this.numLevel.TabIndex = 2;
            this.numLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выбор темы:";
            // 
            // cmbTopics
            // 
            this.cmbTopics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTopics.FormattingEnabled = true;
            this.cmbTopics.Location = new System.Drawing.Point(85, 19);
            this.cmbTopics.Name = "cmbTopics";
            this.cmbTopics.Size = new System.Drawing.Size(121, 21);
            this.cmbTopics.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Выбор уровня:";
            // 
            // grpQuestion
            // 
            this.grpQuestion.Controls.Add(this.label4);
            this.grpQuestion.Controls.Add(this.txtHint);
            this.grpQuestion.Controls.Add(this.btnBrowseImg);
            this.grpQuestion.Controls.Add(this.label3);
            this.grpQuestion.Controls.Add(this.txtImagePath);
            this.grpQuestion.Controls.Add(this.txtQuestionText);
            this.grpQuestion.Location = new System.Drawing.Point(12, 113);
            this.grpQuestion.Name = "grpQuestion";
            this.grpQuestion.Size = new System.Drawing.Size(285, 184);
            this.grpQuestion.TabIndex = 1;
            this.grpQuestion.TabStop = false;
            this.grpQuestion.Text = "Вопрос:";
            // 
            // txtQuestionText
            // 
            this.txtQuestionText.Location = new System.Drawing.Point(9, 20);
            this.txtQuestionText.Multiline = true;
            this.txtQuestionText.Name = "txtQuestionText";
            this.txtQuestionText.Size = new System.Drawing.Size(270, 60);
            this.txtQuestionText.TabIndex = 0;
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(105, 87);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(174, 20);
            this.txtImagePath.TabIndex = 1;
            this.txtImagePath.Text = "images...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Путь к картинке:";
            // 
            // btnBrowseImg
            // 
            this.btnBrowseImg.Location = new System.Drawing.Point(9, 116);
            this.btnBrowseImg.Name = "btnBrowseImg";
            this.btnBrowseImg.Size = new System.Drawing.Size(270, 23);
            this.btnBrowseImg.TabIndex = 3;
            this.btnBrowseImg.Text = "Обзор...";
            this.btnBrowseImg.UseVisualStyleBackColor = true;
            this.btnBrowseImg.Click += new System.EventHandler(this.btnBrowseImg_Click);
            // 
            // txtHint
            // 
            this.txtHint.Location = new System.Drawing.Point(105, 146);
            this.txtHint.Multiline = true;
            this.txtHint.Name = "txtHint";
            this.txtHint.Size = new System.Drawing.Size(174, 20);
            this.txtHint.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Подсказка:";
            // 
            // grpAnswers
            // 
            this.grpAnswers.Controls.Add(this.dgvAnswers);
            this.grpAnswers.Location = new System.Drawing.Point(320, 113);
            this.grpAnswers.Name = "grpAnswers";
            this.grpAnswers.Size = new System.Drawing.Size(324, 184);
            this.grpAnswers.TabIndex = 2;
            this.grpAnswers.TabStop = false;
            this.grpAnswers.Text = "Варианты ответов:";
            // 
            // dgvAnswers
            // 
            this.dgvAnswers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnswers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Текст,
            this.Правильный});
            this.dgvAnswers.Location = new System.Drawing.Point(6, 20);
            this.dgvAnswers.Name = "dgvAnswers";
            this.dgvAnswers.Size = new System.Drawing.Size(312, 158);
            this.dgvAnswers.TabIndex = 0;
            // 
            // Текст
            // 
            this.Текст.HeaderText = "Текст";
            this.Текст.Name = "Текст";
            // 
            // Правильный
            // 
            this.Правильный.HeaderText = "Правильный?";
            this.Правильный.Name = "Правильный";
            // 
            // btnSaveQuestion
            // 
            this.btnSaveQuestion.Location = new System.Drawing.Point(226, 327);
            this.btnSaveQuestion.Name = "btnSaveQuestion";
            this.btnSaveQuestion.Size = new System.Drawing.Size(160, 39);
            this.btnSaveQuestion.TabIndex = 3;
            this.btnSaveQuestion.Text = "Сохранить вопрос";
            this.btnSaveQuestion.UseVisualStyleBackColor = true;
            this.btnSaveQuestion.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(713, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ofdImage
            // 
            this.ofdImage.FileName = "openFileDialog1";
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSaveQuestion);
            this.Controls.Add(this.grpAnswers);
            this.Controls.Add(this.grpQuestion);
            this.Controls.Add(this.grpTopic);
            this.Name = "Admin";
            this.Text = "Admin";
            this.grpTopic.ResumeLayout(false);
            this.grpTopic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).EndInit();
            this.grpQuestion.ResumeLayout(false);
            this.grpQuestion.PerformLayout();
            this.grpAnswers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnswers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTopic;
        private System.Windows.Forms.ComboBox cmbTopics;
        private System.Windows.Forms.NumericUpDown numLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpQuestion;
        private System.Windows.Forms.TextBox txtQuestionText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHint;
        private System.Windows.Forms.Button btnBrowseImg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.GroupBox grpAnswers;
        private System.Windows.Forms.DataGridView dgvAnswers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Текст;
        private System.Windows.Forms.DataGridViewTextBoxColumn Правильный;
        private System.Windows.Forms.Button btnSaveQuestion;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.OpenFileDialog ofdImage;
    }
}