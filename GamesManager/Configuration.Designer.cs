namespace GamesManager
{
    partial class Configuration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuration));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.selectGamesFolderButton = new System.Windows.Forms.Button();
            this.gamesSearchPaths = new System.Windows.Forms.CheckedListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.addPathButton = new System.Windows.Forms.Button();
            this.removePathButton = new System.Windows.Forms.Button();
            this.ignoreUnityCrashHandlerCheckBox = new System.Windows.Forms.CheckBox();
            this.resetConfigurationButton = new System.Windows.Forms.Button();
            this.ignoreunins000CheckBox = new System.Windows.Forms.CheckBox();
            this.ignoredFoldersPaths = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.removeIgnoredButton = new System.Windows.Forms.Button();
            this.addIgnoredButton = new System.Windows.Forms.Button();
            this.resetGamesListButton = new System.Windows.Forms.Button();
            this.steamIDTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.steamCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Games search paths:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Games folder:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(231, 20);
            this.textBox1.TabIndex = 2;
            // 
            // selectGamesFolderButton
            // 
            this.selectGamesFolderButton.Location = new System.Drawing.Point(249, 25);
            this.selectGamesFolderButton.Name = "selectGamesFolderButton";
            this.selectGamesFolderButton.Size = new System.Drawing.Size(24, 20);
            this.selectGamesFolderButton.TabIndex = 3;
            this.selectGamesFolderButton.Text = "...";
            this.selectGamesFolderButton.UseVisualStyleBackColor = true;
            this.selectGamesFolderButton.Click += new System.EventHandler(this.selectGamesFolderButton_Click);
            // 
            // gamesSearchPaths
            // 
            this.gamesSearchPaths.FormattingEnabled = true;
            this.gamesSearchPaths.Location = new System.Drawing.Point(12, 64);
            this.gamesSearchPaths.Name = "gamesSearchPaths";
            this.gamesSearchPaths.Size = new System.Drawing.Size(261, 199);
            this.gamesSearchPaths.TabIndex = 4;
            // 
            // addPathButton
            // 
            this.addPathButton.Location = new System.Drawing.Point(198, 264);
            this.addPathButton.Name = "addPathButton";
            this.addPathButton.Size = new System.Drawing.Size(75, 23);
            this.addPathButton.TabIndex = 5;
            this.addPathButton.Text = "Add path";
            this.addPathButton.UseVisualStyleBackColor = true;
            this.addPathButton.Click += new System.EventHandler(this.addPathButton_Click);
            // 
            // removePathButton
            // 
            this.removePathButton.Location = new System.Drawing.Point(117, 264);
            this.removePathButton.Name = "removePathButton";
            this.removePathButton.Size = new System.Drawing.Size(75, 23);
            this.removePathButton.TabIndex = 6;
            this.removePathButton.Text = "Remove path";
            this.removePathButton.UseVisualStyleBackColor = true;
            this.removePathButton.Click += new System.EventHandler(this.removePathButton_Click);
            // 
            // ignoreUnityCrashHandlerCheckBox
            // 
            this.ignoreUnityCrashHandlerCheckBox.AutoSize = true;
            this.ignoreUnityCrashHandlerCheckBox.Location = new System.Drawing.Point(12, 293);
            this.ignoreUnityCrashHandlerCheckBox.Name = "ignoreUnityCrashHandlerCheckBox";
            this.ignoreUnityCrashHandlerCheckBox.Size = new System.Drawing.Size(147, 17);
            this.ignoreUnityCrashHandlerCheckBox.TabIndex = 7;
            this.ignoreUnityCrashHandlerCheckBox.Text = "Ignore UnityCrashHandler";
            this.ignoreUnityCrashHandlerCheckBox.UseVisualStyleBackColor = true;
            this.ignoreUnityCrashHandlerCheckBox.CheckedChanged += new System.EventHandler(this.ignoreUnityCrashHandlerCheckBox_CheckedChanged);
            // 
            // resetConfigurationButton
            // 
            this.resetConfigurationButton.AutoSize = true;
            this.resetConfigurationButton.ForeColor = System.Drawing.Color.Red;
            this.resetConfigurationButton.Location = new System.Drawing.Point(12, 348);
            this.resetConfigurationButton.MaximumSize = new System.Drawing.Size(0, 23);
            this.resetConfigurationButton.Name = "resetConfigurationButton";
            this.resetConfigurationButton.Size = new System.Drawing.Size(109, 23);
            this.resetConfigurationButton.TabIndex = 8;
            this.resetConfigurationButton.Text = "Reset configuration";
            this.resetConfigurationButton.UseVisualStyleBackColor = true;
            this.resetConfigurationButton.Click += new System.EventHandler(this.resetConfigurationButton_Click);
            // 
            // ignoreunins000CheckBox
            // 
            this.ignoreunins000CheckBox.AutoSize = true;
            this.ignoreunins000CheckBox.Location = new System.Drawing.Point(12, 316);
            this.ignoreunins000CheckBox.Name = "ignoreunins000CheckBox";
            this.ignoreunins000CheckBox.Size = new System.Drawing.Size(102, 17);
            this.ignoreunins000CheckBox.TabIndex = 9;
            this.ignoreunins000CheckBox.Text = "Ignore unins000";
            this.ignoreunins000CheckBox.UseVisualStyleBackColor = true;
            this.ignoreunins000CheckBox.CheckedChanged += new System.EventHandler(this.ignoreunins000CheckBox_CheckedChanged);
            // 
            // ignoredFoldersPaths
            // 
            this.ignoredFoldersPaths.FormattingEnabled = true;
            this.ignoredFoldersPaths.Location = new System.Drawing.Point(279, 64);
            this.ignoredFoldersPaths.Name = "ignoredFoldersPaths";
            this.ignoredFoldersPaths.Size = new System.Drawing.Size(261, 199);
            this.ignoredFoldersPaths.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(279, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Ignored folders:";
            // 
            // removeIgnoredButton
            // 
            this.removeIgnoredButton.Location = new System.Drawing.Point(384, 264);
            this.removeIgnoredButton.Name = "removeIgnoredButton";
            this.removeIgnoredButton.Size = new System.Drawing.Size(75, 23);
            this.removeIgnoredButton.TabIndex = 13;
            this.removeIgnoredButton.Text = "Remove path";
            this.removeIgnoredButton.UseVisualStyleBackColor = true;
            this.removeIgnoredButton.Click += new System.EventHandler(this.removeIgnoredButton_Click);
            // 
            // addIgnoredButton
            // 
            this.addIgnoredButton.Location = new System.Drawing.Point(465, 264);
            this.addIgnoredButton.Name = "addIgnoredButton";
            this.addIgnoredButton.Size = new System.Drawing.Size(75, 23);
            this.addIgnoredButton.TabIndex = 12;
            this.addIgnoredButton.Text = "Add";
            this.addIgnoredButton.UseVisualStyleBackColor = true;
            this.addIgnoredButton.Click += new System.EventHandler(this.addIgnoredButton_Click);
            // 
            // resetGamesListButton
            // 
            this.resetGamesListButton.AutoSize = true;
            this.resetGamesListButton.ForeColor = System.Drawing.Color.Red;
            this.resetGamesListButton.Location = new System.Drawing.Point(127, 348);
            this.resetGamesListButton.MaximumSize = new System.Drawing.Size(0, 23);
            this.resetGamesListButton.Name = "resetGamesListButton";
            this.resetGamesListButton.Size = new System.Drawing.Size(94, 23);
            this.resetGamesListButton.TabIndex = 14;
            this.resetGamesListButton.Text = "Reset games list";
            this.resetGamesListButton.UseVisualStyleBackColor = true;
            this.resetGamesListButton.Click += new System.EventHandler(this.resetGamesListButton_Click);
            // 
            // steamIDTextBox
            // 
            this.steamIDTextBox.Enabled = false;
            this.steamIDTextBox.Location = new System.Drawing.Point(279, 329);
            this.steamIDTextBox.Name = "steamIDTextBox";
            this.steamIDTextBox.Size = new System.Drawing.Size(231, 20);
            this.steamIDTextBox.TabIndex = 16;
            this.steamIDTextBox.TextChanged += new System.EventHandler(this.steamIDTextBox_TextChanged);
            this.steamIDTextBox.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.steamIDTextBox_HelpRequested);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(279, 313);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Steam ID:";
            this.label4.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.steamIDTextBox_HelpRequested);
            // 
            // steamCheckBox
            // 
            this.steamCheckBox.AutoSize = true;
            this.steamCheckBox.Location = new System.Drawing.Point(279, 293);
            this.steamCheckBox.Name = "steamCheckBox";
            this.steamCheckBox.Size = new System.Drawing.Size(76, 17);
            this.steamCheckBox.TabIndex = 17;
            this.steamCheckBox.Text = "Use steam";
            this.steamCheckBox.UseVisualStyleBackColor = true;
            this.steamCheckBox.CheckedChanged += new System.EventHandler(this.steamCheckBox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(279, 353);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Invalid SteamID";
            this.label5.Visible = false;
            // 
            // Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 383);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.steamCheckBox);
            this.Controls.Add(this.steamIDTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.resetGamesListButton);
            this.Controls.Add(this.removeIgnoredButton);
            this.Controls.Add(this.addIgnoredButton);
            this.Controls.Add(this.ignoredFoldersPaths);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ignoreunins000CheckBox);
            this.Controls.Add(this.resetConfigurationButton);
            this.Controls.Add(this.ignoreUnityCrashHandlerCheckBox);
            this.Controls.Add(this.removePathButton);
            this.Controls.Add(this.addPathButton);
            this.Controls.Add(this.gamesSearchPaths);
            this.Controls.Add(this.selectGamesFolderButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Configuration";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Configuration_FormClosing);
            this.Load += new System.EventHandler(this.Configuration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button selectGamesFolderButton;
        private System.Windows.Forms.CheckedListBox gamesSearchPaths;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button addPathButton;
        private System.Windows.Forms.Button removePathButton;
        private System.Windows.Forms.CheckBox ignoreUnityCrashHandlerCheckBox;
        private System.Windows.Forms.Button resetConfigurationButton;
        private System.Windows.Forms.CheckBox ignoreunins000CheckBox;
        private System.Windows.Forms.CheckedListBox ignoredFoldersPaths;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button removeIgnoredButton;
        private System.Windows.Forms.Button addIgnoredButton;
        private System.Windows.Forms.Button resetGamesListButton;
        private System.Windows.Forms.TextBox steamIDTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox steamCheckBox;
        private System.Windows.Forms.Label label5;
    }
}

