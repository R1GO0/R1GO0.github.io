namespace SPB_Quiz
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTopic = new System.Windows.Forms.Label();
            this.cmbTopic = new System.Windows.Forms.ComboBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.numLevel = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnAdmin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblTitle.Location = new System.Drawing.Point(261, 138);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(303, 17);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Викторина: памятники, дворцы и мосты СПб";
            // 
            // lblTopic
            // 
            this.lblTopic.AutoSize = true;
            this.lblTopic.Location = new System.Drawing.Point(290, 182);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new System.Drawing.Size(87, 13);
            this.lblTopic.TabIndex = 1;
            this.lblTopic.Text = "Выберите тему:";
            // 
            // cmbTopic
            // 
            this.cmbTopic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTopic.FormattingEnabled = true;
            this.cmbTopic.Location = new System.Drawing.Point(383, 179);
            this.cmbTopic.Name = "cmbTopic";
            this.cmbTopic.Size = new System.Drawing.Size(142, 21);
            this.cmbTopic.TabIndex = 2;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(290, 222);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(112, 13);
            this.lblLevel.TabIndex = 3;
            this.lblLevel.Text = "Уровень сложности:";
            // 
            // numLevel
            // 
            this.numLevel.Location = new System.Drawing.Point(408, 220);
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
            this.numLevel.Size = new System.Drawing.Size(35, 20);
            this.numLevel.TabIndex = 4;
            this.numLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(293, 262);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(232, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Начать игру";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnAdmin
            // 
            this.btnAdmin.Location = new System.Drawing.Point(647, 415);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(141, 23);
            this.btnAdmin.TabIndex = 6;
            this.btnAdmin.Text = "Администрирование";
            this.btnAdmin.UseVisualStyleBackColor = true;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.numLevel);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.cmbTopic);
            this.Controls.Add(this.lblTopic);
            this.Controls.Add(this.lblTitle);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTopic;
        private System.Windows.Forms.ComboBox cmbTopic;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.NumericUpDown numLevel;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnAdmin;
    }
}

