namespace KFlearning.Views
{
    partial class AdminForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkWriteProtect = new System.Windows.Forms.CheckBox();
            this.chkRegistry = new System.Windows.Forms.CheckBox();
            this.chkTaskManager = new System.Windows.Forms.CheckBox();
            this.chkControlPanel = new System.Windows.Forms.CheckBox();
            this.chkDesktop = new System.Windows.Forms.CheckBox();
            this.chkWallpaper = new System.Windows.Forms.CheckBox();
            this.rdWDefault = new System.Windows.Forms.RadioButton();
            this.rdWCustom = new System.Windows.Forms.RadioButton();
            this.cmdBrowseWallpaper = new System.Windows.Forms.LinkLabel();
            this.lblFileName = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCredential = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(431, 49);
            this.panel1.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Webdings", 25F);
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 34);
            this.label2.TabIndex = 2;
            this.label2.Text = "@";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(67, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Administrator";
            // 
            // chkWriteProtect
            // 
            this.chkWriteProtect.AutoSize = true;
            this.chkWriteProtect.Location = new System.Drawing.Point(18, 65);
            this.chkWriteProtect.Name = "chkWriteProtect";
            this.chkWriteProtect.Size = new System.Drawing.Size(124, 19);
            this.chkWriteProtect.TabIndex = 16;
            this.chkWriteProtect.Text = "Kunci copy ke USB";
            this.chkWriteProtect.UseVisualStyleBackColor = true;
            // 
            // chkRegistry
            // 
            this.chkRegistry.AutoSize = true;
            this.chkRegistry.Location = new System.Drawing.Point(18, 90);
            this.chkRegistry.Name = "chkRegistry";
            this.chkRegistry.Size = new System.Drawing.Size(135, 19);
            this.chkRegistry.TabIndex = 17;
            this.chkRegistry.Text = "Kunci Registry Editor";
            this.chkRegistry.UseVisualStyleBackColor = true;
            // 
            // chkTaskManager
            // 
            this.chkTaskManager.AutoSize = true;
            this.chkTaskManager.Location = new System.Drawing.Point(18, 115);
            this.chkTaskManager.Name = "chkTaskManager";
            this.chkTaskManager.Size = new System.Drawing.Size(131, 19);
            this.chkTaskManager.TabIndex = 18;
            this.chkTaskManager.Text = "Kunci Task Manager";
            this.chkTaskManager.UseVisualStyleBackColor = true;
            // 
            // chkControlPanel
            // 
            this.chkControlPanel.AutoSize = true;
            this.chkControlPanel.Location = new System.Drawing.Point(18, 139);
            this.chkControlPanel.Name = "chkControlPanel";
            this.chkControlPanel.Size = new System.Drawing.Size(131, 19);
            this.chkControlPanel.TabIndex = 19;
            this.chkControlPanel.Text = "Kunci Control Panel";
            this.chkControlPanel.UseVisualStyleBackColor = true;
            // 
            // chkDesktop
            // 
            this.chkDesktop.AutoSize = true;
            this.chkDesktop.Location = new System.Drawing.Point(212, 69);
            this.chkDesktop.Name = "chkDesktop";
            this.chkDesktop.Size = new System.Drawing.Size(101, 19);
            this.chkDesktop.TabIndex = 20;
            this.chkDesktop.Text = "Kunci desktop";
            this.chkDesktop.UseVisualStyleBackColor = true;
            // 
            // chkWallpaper
            // 
            this.chkWallpaper.AutoSize = true;
            this.chkWallpaper.Location = new System.Drawing.Point(212, 94);
            this.chkWallpaper.Name = "chkWallpaper";
            this.chkWallpaper.Size = new System.Drawing.Size(110, 19);
            this.chkWallpaper.TabIndex = 21;
            this.chkWallpaper.Text = "Kunci wallpaper";
            this.chkWallpaper.UseVisualStyleBackColor = true;
            this.chkWallpaper.CheckedChanged += new System.EventHandler(this.chkWallpaper_CheckedChanged);
            // 
            // rdWDefault
            // 
            this.rdWDefault.AutoSize = true;
            this.rdWDefault.Checked = true;
            this.rdWDefault.Location = new System.Drawing.Point(0, 0);
            this.rdWDefault.Name = "rdWDefault";
            this.rdWDefault.Size = new System.Drawing.Size(166, 19);
            this.rdWDefault.TabIndex = 23;
            this.rdWDefault.TabStop = true;
            this.rdWDefault.Text = "Gunakan wallpaper default";
            this.rdWDefault.UseVisualStyleBackColor = true;
            // 
            // rdWCustom
            // 
            this.rdWCustom.AutoSize = true;
            this.rdWCustom.Location = new System.Drawing.Point(0, 25);
            this.rdWCustom.Name = "rdWCustom";
            this.rdWCustom.Size = new System.Drawing.Size(145, 19);
            this.rdWCustom.TabIndex = 24;
            this.rdWCustom.Text = "Gunakan wallpaper ini:";
            this.rdWCustom.UseVisualStyleBackColor = true;
            // 
            // cmdBrowseWallpaper
            // 
            this.cmdBrowseWallpaper.AutoSize = true;
            this.cmdBrowseWallpaper.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(160)))), ((int)(((byte)(230)))));
            this.cmdBrowseWallpaper.Location = new System.Drawing.Point(147, 27);
            this.cmdBrowseWallpaper.Name = "cmdBrowseWallpaper";
            this.cmdBrowseWallpaper.Size = new System.Drawing.Size(30, 15);
            this.cmdBrowseWallpaper.TabIndex = 26;
            this.cmdBrowseWallpaper.TabStop = true;
            this.cmdBrowseWallpaper.Text = "Pilih";
            this.cmdBrowseWallpaper.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdBrowseWallpaper_LinkClicked);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(17, 45);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(16, 15);
            this.lblFileName.TabIndex = 27;
            this.lblFileName.Text = "...";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.cmdSave.FlatAppearance.BorderSize = 0;
            this.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSave.Location = new System.Drawing.Point(321, 214);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(98, 29);
            this.cmdSave.TabIndex = 1;
            this.cmdSave.Text = "Simpan";
            this.cmdSave.UseVisualStyleBackColor = false;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCredential
            // 
            this.cmdCredential.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCredential.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.cmdCredential.FlatAppearance.BorderSize = 0;
            this.cmdCredential.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCredential.Location = new System.Drawing.Point(217, 214);
            this.cmdCredential.Name = "cmdCredential";
            this.cmdCredential.Size = new System.Drawing.Size(98, 29);
            this.cmdCredential.TabIndex = 29;
            this.cmdCredential.Text = "Kredensial";
            this.cmdCredential.UseVisualStyleBackColor = false;
            this.cmdCredential.Click += new System.EventHandler(this.cmdCredential_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdWDefault);
            this.panel2.Controls.Add(this.lblFileName);
            this.panel2.Controls.Add(this.rdWCustom);
            this.panel2.Controls.Add(this.cmdBrowseWallpaper);
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(231, 118);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(188, 70);
            this.panel2.TabIndex = 30;
            // 
            // ofd
            // 
            this.ofd.DefaultExt = "jpg";
            this.ofd.Filter = "Gambar JPG|*.jpg";
            this.ofd.Title = "Pilih file sebagai wallpaper.";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(431, 257);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cmdCredential);
            this.Controls.Add(this.chkWallpaper);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.chkDesktop);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkWriteProtect);
            this.Controls.Add(this.chkControlPanel);
            this.Controls.Add(this.chkRegistry);
            this.Controls.Add(this.chkTaskManager);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrasi";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkWriteProtect;
        private System.Windows.Forms.CheckBox chkRegistry;
        private System.Windows.Forms.CheckBox chkTaskManager;
        private System.Windows.Forms.CheckBox chkControlPanel;
        private System.Windows.Forms.CheckBox chkDesktop;
        private System.Windows.Forms.CheckBox chkWallpaper;
        private System.Windows.Forms.LinkLabel cmdBrowseWallpaper;
        private System.Windows.Forms.RadioButton rdWCustom;
        private System.Windows.Forms.RadioButton rdWDefault;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCredential;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.OpenFileDialog ofd;
    }
}