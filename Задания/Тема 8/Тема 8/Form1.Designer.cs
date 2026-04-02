namespace Тема_8
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.historyItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rulesItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gamePanel = new DoubleBufferedPanel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.gbBet = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.numBet = new System.Windows.Forms.NumericUpDown();
            this.lblBetAmount = new System.Windows.Forms.Label();
            this.cmbCockroach = new System.Windows.Forms.ComboBox();
            this.lblSelect = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.gamePanel.SuspendLayout();
            this.gbBet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBet)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.settingsItem,
            this.profileMenu,
            this.helpMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(808, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(48, 20);
            this.fileMenu.Text = "Файл";
            // 
            // exitItem
            // 
            this.exitItem.Name = "exitItem";
            this.exitItem.Size = new System.Drawing.Size(108, 22);
            this.exitItem.Text = "Выход";
            this.exitItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsItem
            // 
            this.settingsItem.Name = "settingsItem";
            this.settingsItem.Size = new System.Drawing.Size(79, 20);
            this.settingsItem.Text = "Настройки";
            this.settingsItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // profileMenu
            // 
            this.profileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.historyItem});
            this.profileMenu.Name = "profileMenu";
            this.profileMenu.Size = new System.Drawing.Size(71, 20);
            this.profileMenu.Text = "Профиль";
            // 
            // historyItem
            // 
            this.historyItem.Name = "historyItem";
            this.historyItem.Size = new System.Drawing.Size(143, 22);
            this.historyItem.Text = "История игр";
            this.historyItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rulesItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(65, 20);
            this.helpMenu.Text = "Справка";
            // 
            // rulesItem
            // 
            this.rulesItem.Name = "rulesItem";
            this.rulesItem.Size = new System.Drawing.Size(122, 22);
            this.rulesItem.Text = "Правила";
            this.rulesItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // gamePanel
            // 
            this.gamePanel.BackColor = System.Drawing.Color.LightGreen;
            this.gamePanel.Controls.Add(this.lblStatus);
            this.gamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePanel.Location = new System.Drawing.Point(0, 24);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(808, 582);
            this.gamePanel.TabIndex = 1;
            this.gamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gamePanel_Paint);
            this.gamePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gamePanel_MouseClick);
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Location = new System.Drawing.Point(6, 12);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(128, 18);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Ожидание старта...";
            // 
            // gbBet
            // 
            this.gbBet.Controls.Add(this.btnStart);
            this.gbBet.Controls.Add(this.numBet);
            this.gbBet.Controls.Add(this.lblBetAmount);
            this.gbBet.Controls.Add(this.cmbCockroach);
            this.gbBet.Controls.Add(this.lblSelect);
            this.gbBet.Controls.Add(this.lblBalance);
            this.gbBet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbBet.Location = new System.Drawing.Point(0, 606);
            this.gbBet.Name = "gbBet";
            this.gbBet.Size = new System.Drawing.Size(808, 100);
            this.gbBet.TabIndex = 0;
            this.gbBet.TabStop = false;
            this.gbBet.Text = "Сделать ставку:";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStart.ForeColor = System.Drawing.Color.Red;
            this.btnStart.Location = new System.Drawing.Point(643, 26);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(151, 45);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "СТАРТ";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // numBet
            // 
            this.numBet.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numBet.Location = new System.Drawing.Point(122, 56);
            this.numBet.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBet.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numBet.Name = "numBet";
            this.numBet.Size = new System.Drawing.Size(68, 20);
            this.numBet.TabIndex = 4;
            this.numBet.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblBetAmount
            // 
            this.lblBetAmount.AutoSize = true;
            this.lblBetAmount.Location = new System.Drawing.Point(6, 58);
            this.lblBetAmount.Name = "lblBetAmount";
            this.lblBetAmount.Size = new System.Drawing.Size(82, 13);
            this.lblBetAmount.TabIndex = 3;
            this.lblBetAmount.Text = "Сумма ставки:";
            // 
            // cmbCockroach
            // 
            this.cmbCockroach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCockroach.FormattingEnabled = true;
            this.cmbCockroach.Items.AddRange(new object[] {
            "Таракан 1",
            "Таракан 2",
            "Таракан 3",
            "Таракан 4"});
            this.cmbCockroach.Location = new System.Drawing.Point(122, 23);
            this.cmbCockroach.Name = "cmbCockroach";
            this.cmbCockroach.Size = new System.Drawing.Size(143, 21);
            this.cmbCockroach.TabIndex = 2;
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Location = new System.Drawing.Point(6, 26);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(110, 13);
            this.lblSelect.TabIndex = 1;
            this.lblSelect.Text = "Выберите таракана:";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBalance.Location = new System.Drawing.Point(342, 3);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(93, 13);
            this.lblBalance.TabIndex = 0;
            this.lblBalance.Text = "Баланс: 1000$";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 706);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.gbBet);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Тараканьи бега";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gamePanel.ResumeLayout(false);
            this.gbBet.ResumeLayout(false);
            this.gbBet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsItem;
        private System.Windows.Forms.ToolStripMenuItem profileMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem exitItem;
        private System.Windows.Forms.ToolStripMenuItem historyItem;
        private System.Windows.Forms.ToolStripMenuItem rulesItem; 
        private DoubleBufferedPanel gamePanel;
        private System.Windows.Forms.GroupBox gbBet;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.ComboBox cmbCockroach;
        private System.Windows.Forms.Label lblBetAmount;
        private System.Windows.Forms.NumericUpDown numBet;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblStatus;
    }
}

