namespace KFlearning.Views
{
    partial class StartupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupForm));
            this.cmdNewProject = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdOpenProject = new System.Windows.Forms.Button();
            this.cmdAdmin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdAbout = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lstHistory = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdClear = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.pnRaf = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnRaf.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdNewProject
            // 
            this.cmdNewProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.cmdNewProject.FlatAppearance.BorderSize = 0;
            this.cmdNewProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdNewProject.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmdNewProject.Location = new System.Drawing.Point(445, 76);
            this.cmdNewProject.Name = "cmdNewProject";
            this.cmdNewProject.Size = new System.Drawing.Size(159, 37);
            this.cmdNewProject.TabIndex = 0;
            this.cmdNewProject.Text = "Proyek Baru";
            this.cmdNewProject.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.cmdNewProject.UseVisualStyleBackColor = false;
            this.cmdNewProject.Click += new System.EventHandler(this.cmdNewProject_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(10, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "KFlearning";
            // 
            // cmdOpenProject
            // 
            this.cmdOpenProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.cmdOpenProject.FlatAppearance.BorderSize = 0;
            this.cmdOpenProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOpenProject.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmdOpenProject.Location = new System.Drawing.Point(445, 125);
            this.cmdOpenProject.Name = "cmdOpenProject";
            this.cmdOpenProject.Size = new System.Drawing.Size(159, 37);
            this.cmdOpenProject.TabIndex = 3;
            this.cmdOpenProject.Text = "Buka Proyek";
            this.cmdOpenProject.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.cmdOpenProject.UseVisualStyleBackColor = false;
            this.cmdOpenProject.Click += new System.EventHandler(this.cmdOpenProject_Click);
            // 
            // cmdAdmin
            // 
            this.cmdAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.cmdAdmin.FlatAppearance.BorderSize = 0;
            this.cmdAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAdmin.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmdAdmin.Location = new System.Drawing.Point(445, 174);
            this.cmdAdmin.Name = "cmdAdmin";
            this.cmdAdmin.Size = new System.Drawing.Size(159, 40);
            this.cmdAdmin.TabIndex = 5;
            this.cmdAdmin.Text = "Administrator";
            this.cmdAdmin.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.cmdAdmin.UseVisualStyleBackColor = false;
            this.cmdAdmin.Click += new System.EventHandler(this.cmdAdmin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.label3.Location = new System.Drawing.Point(126, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 37);
            this.label3.TabIndex = 8;
            this.label3.Text = "Selamat datang!";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(110, 293);
            this.panel1.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KFlearning.Properties.Resources.KFlearning_logo48;
            this.pictureBox1.Location = new System.Drawing.Point(32, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "v1.1 rev 247";
            // 
            // cmdAbout
            // 
            this.cmdAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.cmdAbout.FlatAppearance.BorderSize = 0;
            this.cmdAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAbout.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmdAbout.Location = new System.Drawing.Point(445, 226);
            this.cmdAbout.Name = "cmdAbout";
            this.cmdAbout.Size = new System.Drawing.Size(159, 37);
            this.cmdAbout.TabIndex = 14;
            this.cmdAbout.Text = "Tentang";
            this.cmdAbout.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.cmdAbout.UseVisualStyleBackColor = false;
            this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label4.Font = new System.Drawing.Font("Webdings", 20F);
            this.label4.Location = new System.Drawing.Point(454, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 30);
            this.label4.TabIndex = 15;
            this.label4.Text = "ñ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label5.Font = new System.Drawing.Font("Wingdings", 20F);
            this.label5.Location = new System.Drawing.Point(450, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 30);
            this.label5.TabIndex = 16;
            this.label5.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label6.Font = new System.Drawing.Font("Wingdings", 20F);
            this.label6.Location = new System.Drawing.Point(453, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 30);
            this.label6.TabIndex = 17;
            this.label6.Text = "Ü";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label7.Font = new System.Drawing.Font("Webdings", 20F);
            this.label7.Location = new System.Drawing.Point(451, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 30);
            this.label7.TabIndex = 18;
            this.label7.Text = "i";
            // 
            // lstHistory
            // 
            this.lstHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(53)))), ((int)(((byte)(55)))));
            this.lstHistory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstHistory.ForeColor = System.Drawing.Color.White;
            this.lstHistory.FormattingEnabled = true;
            this.lstHistory.ItemHeight = 20;
            this.lstHistory.Location = new System.Drawing.Point(121, 76);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(295, 204);
            this.lstHistory.TabIndex = 19;
            this.lstHistory.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstHistory_DrawItem);
            this.lstHistory.DoubleClick += new System.EventHandler(this.lstHistory_DoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(442, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(145, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "Mulai sesuatu yang baru...";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(118, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(142, 15);
            this.label9.TabIndex = 21;
            this.label9.Text = "Buka proyek sebelumnya:";
            // 
            // cmdClear
            // 
            this.cmdClear.AutoSize = true;
            this.cmdClear.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(160)))), ((int)(((byte)(230)))));
            this.cmdClear.Location = new System.Drawing.Point(358, 58);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(58, 15);
            this.cmdClear.TabIndex = 22;
            this.cmdClear.TabStop = true;
            this.cmdClear.Text = "Bersihkan";
            this.cmdClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdClear_LinkClicked);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(4, -1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(130, 21);
            this.label10.TabIndex = 23;
            this.label10.Text = "Run-and-Forget";
            // 
            // pnRaf
            // 
            this.pnRaf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.pnRaf.Controls.Add(this.label10);
            this.pnRaf.Location = new System.Drawing.Point(454, 16);
            this.pnRaf.Name = "pnRaf";
            this.pnRaf.Size = new System.Drawing.Size(140, 23);
            this.pnRaf.TabIndex = 24;
            this.pnRaf.Visible = false;
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(627, 293);
            this.Controls.Add(this.pnRaf);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lstHistory);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdAbout);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdAdmin);
            this.Controls.Add(this.cmdOpenProject);
            this.Controls.Add(this.cmdNewProject);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KFlearning Launcher";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnRaf.ResumeLayout(false);
            this.pnRaf.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdNewProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdOpenProject;
        private System.Windows.Forms.Button cmdAdmin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdAbout;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lstHistory;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel cmdClear;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnRaf;
    }
}

