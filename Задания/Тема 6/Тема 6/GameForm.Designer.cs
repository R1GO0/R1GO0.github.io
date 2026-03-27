namespace SPB_Quiz
{
    partial class GameForm
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
            this.lblQuestion = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lblHintCaption = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblAnswer = new System.Windows.Forms.Label();
            this.cmbAnswers = new System.Windows.Forms.ComboBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnShowHint = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQuestion.Location = new System.Drawing.Point(277, 9);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Padding = new System.Windows.Forms.Padding(5);
            this.lblQuestion.Size = new System.Drawing.Size(278, 43);
            this.lblQuestion.TabIndex = 0;
            this.lblQuestion.Text = "Вопрос:";
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(262, 55);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(310, 164);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // lblHintCaption
            // 
            this.lblHintCaption.AutoSize = true;
            this.lblHintCaption.Location = new System.Drawing.Point(274, 222);
            this.lblHintCaption.Name = "lblHintCaption";
            this.lblHintCaption.Size = new System.Drawing.Size(69, 13);
            this.lblHintCaption.TabIndex = 2;
            this.lblHintCaption.Text = "Подсказка: ";
            this.lblHintCaption.Visible = false;
            // 
            // lblHint
            // 
            this.lblHint.ForeColor = System.Drawing.Color.Blue;
            this.lblHint.Location = new System.Drawing.Point(340, 222);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(215, 56);
            this.lblHint.TabIndex = 3;
            this.lblHint.Text = "...";
            this.lblHint.Visible = false;
            // 
            // lblAnswer
            // 
            this.lblAnswer.AutoSize = true;
            this.lblAnswer.Location = new System.Drawing.Point(246, 284);
            this.lblAnswer.Name = "lblAnswer";
            this.lblAnswer.Size = new System.Drawing.Size(91, 13);
            this.lblAnswer.TabIndex = 4;
            this.lblAnswer.Text = "Выберите ответ:";
            // 
            // cmbAnswers
            // 
            this.cmbAnswers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnswers.FormattingEnabled = true;
            this.cmbAnswers.Location = new System.Drawing.Point(343, 281);
            this.cmbAnswers.Name = "cmbAnswers";
            this.cmbAnswers.Size = new System.Drawing.Size(249, 21);
            this.cmbAnswers.TabIndex = 5;
            this.cmbAnswers.SelectedIndexChanged += new System.EventHandler(this.cmbAnswers_SelectedIndexChanged);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(277, 313);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(278, 23);
            this.btnCheck.TabIndex = 6;
            this.btnCheck.Text = "Проверить";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnShowHint
            // 
            this.btnShowHint.Location = new System.Drawing.Point(578, 216);
            this.btnShowHint.Name = "btnShowHint";
            this.btnShowHint.Size = new System.Drawing.Size(133, 23);
            this.btnShowHint.TabIndex = 7;
            this.btnShowHint.Text = "Показать подсказку";
            this.btnShowHint.UseVisualStyleBackColor = true;
            this.btnShowHint.Click += new System.EventHandler(this.btnShowHint_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(381, 384);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "Далее";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(419, 355);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnShowHint);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.cmbAnswers);
            this.Controls.Add(this.lblAnswer);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.lblHintCaption);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.lblQuestion);
            this.Name = "GameForm";
            this.Text = "GameForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label lblHintCaption;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label lblAnswer;
        private System.Windows.Forms.ComboBox cmbAnswers;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnShowHint;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblStatus;
    }
}