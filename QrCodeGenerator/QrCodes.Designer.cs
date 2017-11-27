namespace QrCodeGenerator
{
    partial class QrCodes
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
            this.QrCountNumber = new System.Windows.Forms.NumericUpDown();
            this.GenerateNewButton = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.NewQrCodesList = new System.Windows.Forms.ListBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CleanButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllInFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.QrCountNumber)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // QrCountNumber
            // 
            this.QrCountNumber.Location = new System.Drawing.Point(12, 71);
            this.QrCountNumber.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.QrCountNumber.Name = "QrCountNumber";
            this.QrCountNumber.Size = new System.Drawing.Size(136, 20);
            this.QrCountNumber.TabIndex = 1;
            this.QrCountNumber.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // GenerateNewButton
            // 
            this.GenerateNewButton.Location = new System.Drawing.Point(154, 68);
            this.GenerateNewButton.Name = "GenerateNewButton";
            this.GenerateNewButton.Size = new System.Drawing.Size(88, 23);
            this.GenerateNewButton.TabIndex = 2;
            this.GenerateNewButton.Text = "Generate New";
            this.GenerateNewButton.UseVisualStyleBackColor = true;
            this.GenerateNewButton.Click += new System.EventHandler(this.GenerateNewButton_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(32, 26);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(189, 25);
            this.Title.TabIndex = 3;
            this.Title.Text = "Qr Code Generators";
            // 
            // NewQrCodesList
            // 
            this.NewQrCodesList.FormattingEnabled = true;
            this.NewQrCodesList.Location = new System.Drawing.Point(12, 98);
            this.NewQrCodesList.Name = "NewQrCodesList";
            this.NewQrCodesList.Size = new System.Drawing.Size(230, 238);
            this.NewQrCodesList.TabIndex = 4;
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(271, 98);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(273, 23);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save In File";
            this.SaveButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveNewButton_Click);
            // 
            // CleanButton
            // 
            this.CleanButton.Enabled = false;
            this.CleanButton.Location = new System.Drawing.Point(271, 156);
            this.CleanButton.Name = "CleanButton";
            this.CleanButton.Size = new System.Drawing.Size(273, 23);
            this.CleanButton.TabIndex = 8;
            this.CleanButton.Text = "Clean";
            this.CleanButton.UseVisualStyleBackColor = true;
            this.CleanButton.Click += new System.EventHandler(this.CleanButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Enabled = false;
            this.CopyButton.Location = new System.Drawing.Point(271, 127);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(273, 23);
            this.CopyButton.TabIndex = 9;
            this.CopyButton.Text = "Copy New Data To Clipboard";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataBaseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(556, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.cleanToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveNewButton_Click);
            // 
            // cleanToolStripMenuItem
            // 
            this.cleanToolStripMenuItem.Enabled = false;
            this.cleanToolStripMenuItem.Name = "cleanToolStripMenuItem";
            this.cleanToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cleanToolStripMenuItem.Text = "Clean";
            this.cleanToolStripMenuItem.Click += new System.EventHandler(this.CleanButton_Click);
            // 
            // dataBaseToolStripMenuItem
            // 
            this.dataBaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportAllInFileToolStripMenuItem,
            this.statisticToolStripMenuItem});
            this.dataBaseToolStripMenuItem.Name = "dataBaseToolStripMenuItem";
            this.dataBaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.dataBaseToolStripMenuItem.Text = "DataBase";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // exportAllInFileToolStripMenuItem
            // 
            this.exportAllInFileToolStripMenuItem.Name = "exportAllInFileToolStripMenuItem";
            this.exportAllInFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportAllInFileToolStripMenuItem.Text = "Export";
            this.exportAllInFileToolStripMenuItem.Click += new System.EventHandler(this.SaveAllButton_Click);
            // 
            // statisticToolStripMenuItem
            // 
            this.statisticToolStripMenuItem.Name = "statisticToolStripMenuItem";
            this.statisticToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.statisticToolStripMenuItem.Text = "Statistic";
            this.statisticToolStripMenuItem.Click += new System.EventHandler(this.StatisticButton_Click);
            // 
            // QrCodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 354);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.CleanButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.NewQrCodesList);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.GenerateNewButton);
            this.Controls.Add(this.QrCountNumber);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "QrCodes";
            this.Text = "Qr Codes";
            ((System.ComponentModel.ISupportInitialize)(this.QrCountNumber)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown QrCountNumber;
        private System.Windows.Forms.Button GenerateNewButton;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.ListBox NewQrCodesList;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CleanButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataBaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAllInFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanToolStripMenuItem;
    }
}

